using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffGameComponent))]
    public static class GlobalBuffGameComponentSystem
    {
        [ObjectSystem]
        public class GlobalBuffGameComponentAwakeSystem: AwakeSystem<GlobalBuffGameComponent>
        {
            protected override void Awake(GlobalBuffGameComponent self)
            {
                self.removeList = new();

                self.monitorTriggerList = new();

                self.buffTagGroupTypeList = new();
            }
        }

        [ObjectSystem]
        public class GlobalBuffGameComponentDestroySystem: DestroySystem<GlobalBuffGameComponent>
        {
            protected override void Destroy(GlobalBuffGameComponent self)
            {
                self.removeList.Clear();
                self.removeList = null;
                self.monitorTriggerList.Clear();
                self.monitorTriggerList = null;

                self.buffTagGroupTypeList.Clear();
                self.buffTagGroupTypeList = null;

            }
        }

        [ObjectSystem]
        public class GlobalBuffGameComponentFixedUpdateSystem: FixedUpdateSystem<GlobalBuffGameComponent>
        {
            protected override void FixedUpdate(GlobalBuffGameComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask<GlobalBuffGameObj> AddGlobalBuff(this GlobalBuffGameComponent self, TeamFlagType teamFlagType, long casterPlayerId, string gameGlobalBuffCfgId)
        {
            (bool isExist, GlobalBuffGameObj globalBuffGameObj) = self.ChkExist(teamFlagType, casterPlayerId, gameGlobalBuffCfgId);
            if (isExist)
            {
                if (globalBuffGameObj.casterPlayerId == 0 || globalBuffGameObj.casterPlayerId == -1)
                {
                    globalBuffGameObj.casterPlayerId = casterPlayerId;
                }
            }
            else
            {
                bool IsEnabled = self.ChkIsEnabledByTagGroup(gameGlobalBuffCfgId);

                globalBuffGameObj = self.AddChild<GlobalBuffGameObj>();
                globalBuffGameObj.isEnabled = IsEnabled;
                await globalBuffGameObj.Init(teamFlagType, casterPlayerId, gameGlobalBuffCfgId);
                self.DealWhenAddBuff(globalBuffGameObj);

            }

            return globalBuffGameObj;
        }

        public static (bool, GlobalBuffGameObj) ChkExist(this GlobalBuffGameComponent self, TeamFlagType teamFlagType, long casterPlayerId, string gameGlobalBuffCfgId)
        {
            if (self.Children.Count <= 0)
            {
                return (false, null);
            }
            foreach (var obj in self.Children.Values)
            {
                GlobalBuffGameObj globalBuffGameObj = obj as GlobalBuffGameObj;
                if (globalBuffGameObj.CfgId != gameGlobalBuffCfgId)
                {
                    continue;
                }

                if (globalBuffGameObj.teamFlagType != teamFlagType)
                {
                    continue;
                }
                return (true, globalBuffGameObj);
            }

            return (false, null);
        }

        public static void DealWhenAddBuff(this GlobalBuffGameComponent self, GlobalBuffGameObj buffObj)
        {
            self.AddMonitorTriggerList(buffObj);
            self.AddBuffTagTypeList(buffObj);
        }

        public static void DealWhenRemoveBuff(this GlobalBuffGameComponent self, GlobalBuffGameObj buffObj)
        {
            self.RemoveMonitorTriggerList(buffObj);
            self.RemoveBuffTagTypeList(buffObj);

        }

        public static void AddMonitorTriggerList(this GlobalBuffGameComponent self, GlobalBuffGameObj globalBuffGameObj)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = globalBuffGameObj.GetComponent<GlobalConditionManagerComponent>();
            foreach (var monitorTrigger in globalConditionManagerComponent.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger, globalBuffGameObj);
            }
        }

        public static void RemoveMonitorTriggerList(this GlobalBuffGameComponent self, GlobalBuffGameObj globalBuffGameObj)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = globalBuffGameObj.GetComponent<GlobalConditionManagerComponent>();
            foreach (var monitorTrigger in globalConditionManagerComponent.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger, globalBuffGameObj);
            }
        }

        public static void AddBuffTagTypeList(this GlobalBuffGameComponent self, GlobalBuffGameObj buffObj)
        {
            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Add(buffObj.model.TagGroup.Value, buffObj);
            }
        }

        public static void RemoveBuffTagTypeList(this GlobalBuffGameComponent self, GlobalBuffGameObj buffObj)
        {
            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Remove(buffObj.model.TagGroup.Value, buffObj);
            }
        }

        public static void EventHandler(this GlobalBuffGameComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (self.monitorTriggerList.ContainsKey(abilityGameMonitorTriggerEvent) == false)
            {
                return;
            }
            List<EntityRef<GlobalBuffGameObj>> buffObjs = self.monitorTriggerList[abilityGameMonitorTriggerEvent];
            foreach (GlobalBuffGameObj globalBuffGameObj in buffObjs)
            {
                if (globalBuffGameObj == null)
                {
                    continue;
                }
                if (globalBuffGameObj.isEnabled == false)
                {
                    continue;
                }
                if (actionGameContext.teamFlagType != TeamFlagType.None && globalBuffGameObj.teamFlagType != actionGameContext.teamFlagType)
                {
                    continue;
                }
                globalBuffGameObj.TrigEvent(abilityGameMonitorTriggerEvent, ref actionGameContext);
            }
        }

        public static void Remove(this GlobalBuffGameComponent self, GlobalBuffGameObj buffObj)
        {
            self.DealWhenRemoveBuff(buffObj);
            self.DoEnabledTagGroup(buffObj);
            buffObj.Dispose();

        }

        public static void FixedUpdate(this GlobalBuffGameComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();

            foreach (var obj in self.Children.Values)
            {
                GlobalBuffGameObj buffObj = obj as GlobalBuffGameObj;
                buffObj.FixedUpdate(fixedDeltaTime);

                if (buffObj.ChkNeedRemove())
                {
                    self.removeList.Add(buffObj);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.Remove(self.removeList[i]);
            }

            self.removeList.Clear();
        }

        public static bool ChkBuffByBuffCfgId(this GlobalBuffGameComponent self, string buffCfgId)
        {
            foreach (var obj in self.Children.Values)
            {
                BuffObj buffObj = obj as BuffObj;

                if (buffObj.CfgId == buffCfgId && buffObj.isEnabled)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<BuffObj> GetBuffListByBuffCfgId(this GlobalBuffGameComponent self, string buffCfgId)
        {
            List<BuffObj> buffList = ListComponent<BuffObj>.Create();
            foreach (var buffObjs in self.Children)
            {
                BuffObj buffObj = buffObjs.Value as BuffObj;
                if (buffObj.CfgId == buffCfgId)
                {
                    buffList.Add(buffObj);
                }
            }
            return buffList;
        }

        public static List<BuffObj> GetAllBuffList(this GlobalBuffGameComponent self)
        {
            List<BuffObj> buffList = ListComponent<BuffObj>.Create();
            foreach (var buffObjs in self.Children)
            {
                BuffObj buffObj = buffObjs.Value as BuffObj;

                buffList.Add(buffObj);
            }
            return buffList;
        }


        /// <summary>
        /// 如果有同组TagGroup的，对优先级低的进行SetEnabled(false)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="addBuffInfo"></param>
        /// <returns></returns>
        public static bool ChkIsEnabledByTagGroup(this GlobalBuffGameComponent self, string gameGlobalBuffCfgId)
        {
            if (self.Children.Count <= 0)
            {
                return true;
            }

            GameGlobalBuffCfg gameGlobalBuffCfg = GameGlobalBuffCfgCategory.Instance.Get(gameGlobalBuffCfgId);
            if (gameGlobalBuffCfg.TagGroup == null)
            {
                return true;
            }

            BuffTagGroupType tagGroup = gameGlobalBuffCfg.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
                if (self.buffTagGroupTypeList[tagGroup].Count > 0)
                {
                    foreach (GlobalBuffGameObj globalBuffGameObj in self.buffTagGroupTypeList[tagGroup])
                    {
                        if (globalBuffGameObj.model.Priority > gameGlobalBuffCfg.Priority)
                        {
                            string msg = $"buffId[{gameGlobalBuffCfg.Id}] tagGroup[{tagGroup}] Priority[{gameGlobalBuffCfg.Priority}] < buffId[{globalBuffGameObj.CfgId}] Priority[{globalBuffGameObj.model.Priority}]";
                            return false;
                        }
                        else
                        {
                            globalBuffGameObj.SetEnabled(false);
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 激活同组TagGroup的优先级最大的下一个buffObj,SetEnabled(true)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="curGlobalBuffGameObj"></param>
        public static void DoEnabledTagGroup(this GlobalBuffGameComponent self, GlobalBuffGameObj curGlobalBuffGameObj)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            if (curGlobalBuffGameObj.model.TagGroup == null)
            {
                return;
            }
            if (curGlobalBuffGameObj.isEnabled == false)
            {
                return;
            }

            BuffTagGroupType tagGroup = curGlobalBuffGameObj.model.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
                if (self.buffTagGroupTypeList[tagGroup].Count > 0)
                {
                    GlobalBuffGameObj buffObjMaxPriority = null;
                    int curPriority = -999;
                    foreach (GlobalBuffGameObj globalBuffGameObj in self.buffTagGroupTypeList[tagGroup])
                    {
                        if (globalBuffGameObj == curGlobalBuffGameObj)
                        {
                            continue;
                        }
                        if (globalBuffGameObj.duration <= 0)
                        {
                            continue;
                        }
                        if (globalBuffGameObj.model.Priority > curPriority)
                        {
                            curPriority = globalBuffGameObj.model.Priority;
                            buffObjMaxPriority = globalBuffGameObj;
                        }
                    }

                    if (buffObjMaxPriority != null)
                    {
                        buffObjMaxPriority.SetEnabled(true);
                    }
                }
            }
        }

    }
}