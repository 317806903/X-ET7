using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffPlayerComponent))]
    public static class GlobalBuffPlayerComponentSystem
    {
        [ObjectSystem]
        public class GlobalBuffPlayerComponentAwakeSystem: AwakeSystem<GlobalBuffPlayerComponent>
        {
            protected override void Awake(GlobalBuffPlayerComponent self)
            {
                self.removeList = new();

                self.monitorTriggerList = new();

                self.buffTagGroupTypeList = new();
            }
        }

        [ObjectSystem]
        public class GlobalBuffPlayerComponentDestroySystem: DestroySystem<GlobalBuffPlayerComponent>
        {
            protected override void Destroy(GlobalBuffPlayerComponent self)
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
        public class GlobalBuffPlayerComponentFixedUpdateSystem: FixedUpdateSystem<GlobalBuffPlayerComponent>
        {
            protected override void FixedUpdate(GlobalBuffPlayerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask<GlobalBuffPlayerObj> AddGlobalBuff(this GlobalBuffPlayerComponent self, long playerId, long casterPlayerId, string playerGlobalBuffCfgId)
        {
            (bool isExist, GlobalBuffPlayerObj globalBuffPlayerObj) = self.ChkExist(casterPlayerId, playerGlobalBuffCfgId);
            if (isExist)
            {
                if (globalBuffPlayerObj.casterPlayerId == 0 || globalBuffPlayerObj.casterPlayerId == -1)
                {
                    globalBuffPlayerObj.casterPlayerId = casterPlayerId;
                }
            }
            else
            {
                bool IsEnabled = self.ChkIsEnabledByTagGroup(playerGlobalBuffCfgId);

                globalBuffPlayerObj = self.AddChild<GlobalBuffPlayerObj>();
                globalBuffPlayerObj.isEnabled = IsEnabled;
                await globalBuffPlayerObj.Init(casterPlayerId, playerId, playerGlobalBuffCfgId);
                self.DealWhenAddBuff(globalBuffPlayerObj);

            }

            return globalBuffPlayerObj;
        }

        public static (bool, GlobalBuffPlayerObj) ChkExist(this GlobalBuffPlayerComponent self, long casterPlayerId, string playerGlobalBuffCfgId)
        {
            if (self.Children.Count <= 0)
            {
                return (false, null);
            }
            foreach (var obj in self.Children.Values)
            {
                GlobalBuffPlayerObj globalBuffPlayerObj = obj as GlobalBuffPlayerObj;
                if (globalBuffPlayerObj.CfgId != playerGlobalBuffCfgId)
                {
                    continue;
                }

                return (true, globalBuffPlayerObj);
            }

            return (false, null);
        }

        public static void DealWhenAddBuff(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj buffObj)
        {
            self.AddMonitorTriggerList(buffObj);
            self.AddBuffTagTypeList(buffObj);
        }

        public static void DealWhenRemoveBuff(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj buffObj)
        {
            self.RemoveMonitorTriggerList(buffObj);
            self.RemoveBuffTagTypeList(buffObj);

        }

        public static void AddMonitorTriggerList(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj globalBuffPlayerObj)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = globalBuffPlayerObj.GetComponent<GlobalConditionManagerComponent>();
            foreach (var monitorTrigger in globalConditionManagerComponent.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger, globalBuffPlayerObj);
            }
        }

        public static void RemoveMonitorTriggerList(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj globalBuffPlayerObj)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = globalBuffPlayerObj.GetComponent<GlobalConditionManagerComponent>();
            foreach (var monitorTrigger in globalConditionManagerComponent.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger, globalBuffPlayerObj);
            }
        }

        public static void AddBuffTagTypeList(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj buffObj)
        {
            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Add(buffObj.model.TagGroup.Value, buffObj);
            }
        }

        public static void RemoveBuffTagTypeList(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj buffObj)
        {
            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Remove(buffObj.model.TagGroup.Value, buffObj);
            }
        }

        public static void EventHandler(this GlobalBuffPlayerComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (self.monitorTriggerList.ContainsKey(abilityGameMonitorTriggerEvent) == false)
            {
                return;
            }
            List<EntityRef<GlobalBuffPlayerObj>> buffObjs = self.monitorTriggerList[abilityGameMonitorTriggerEvent];
            foreach (GlobalBuffPlayerObj globalBuffPlayerObj in buffObjs)
            {
                if (globalBuffPlayerObj == null)
                {
                    continue;
                }
                if (globalBuffPlayerObj.isEnabled == false)
                {
                    continue;
                }
                globalBuffPlayerObj.TrigEvent(abilityGameMonitorTriggerEvent, ref actionGameContext);
            }
        }

        public static void Remove(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj buffObj)
        {
            self.DealWhenRemoveBuff(buffObj);
            self.DoEnabledTagGroup(buffObj);
            buffObj.Dispose();

        }

        public static void FixedUpdate(this GlobalBuffPlayerComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();

            foreach (var obj in self.Children.Values)
            {
                GlobalBuffPlayerObj buffObj = obj as GlobalBuffPlayerObj;
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

        public static bool ChkBuffByBuffCfgId(this GlobalBuffPlayerComponent self, string buffCfgId)
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

        public static List<BuffObj> GetBuffListByBuffCfgId(this GlobalBuffPlayerComponent self, string buffCfgId)
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

        public static List<BuffObj> GetAllBuffList(this GlobalBuffPlayerComponent self)
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
        public static bool ChkIsEnabledByTagGroup(this GlobalBuffPlayerComponent self, string playerGlobalBuffCfgId)
        {
            if (self.Children.Count <= 0)
            {
                return true;
            }

            PlayerGlobalBuffCfg playerGlobalBuffCfg = PlayerGlobalBuffCfgCategory.Instance.Get(playerGlobalBuffCfgId);
            if (playerGlobalBuffCfg.TagGroup == null)
            {
                return true;
            }

            BuffTagGroupType tagGroup = playerGlobalBuffCfg.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
                if (self.buffTagGroupTypeList[tagGroup].Count > 0)
                {
                    foreach (GlobalBuffPlayerObj globalBuffPlayerObj in self.buffTagGroupTypeList[tagGroup])
                    {
                        if (globalBuffPlayerObj.model.Priority > playerGlobalBuffCfg.Priority)
                        {
                            string msg = $"buffId[{playerGlobalBuffCfg.Id}] tagGroup[{tagGroup}] Priority[{playerGlobalBuffCfg.Priority}] < buffId[{globalBuffPlayerObj.CfgId}] Priority[{globalBuffPlayerObj.model.Priority}]";
                            return false;
                        }
                        else
                        {
                            globalBuffPlayerObj.SetEnabled(false);
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
        /// <param name="curGlobalBuffPlayerObj"></param>
        public static void DoEnabledTagGroup(this GlobalBuffPlayerComponent self, GlobalBuffPlayerObj curGlobalBuffPlayerObj)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            if (curGlobalBuffPlayerObj.model.TagGroup == null)
            {
                return;
            }
            if (curGlobalBuffPlayerObj.isEnabled == false)
            {
                return;
            }

            BuffTagGroupType tagGroup = curGlobalBuffPlayerObj.model.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
                if (self.buffTagGroupTypeList[tagGroup].Count > 0)
                {
                    GlobalBuffPlayerObj buffObjMaxPriority = null;
                    int curPriority = -999;
                    foreach (GlobalBuffPlayerObj globalBuffPlayerObj in self.buffTagGroupTypeList[tagGroup])
                    {
                        if (globalBuffPlayerObj == curGlobalBuffPlayerObj)
                        {
                            continue;
                        }
                        if (globalBuffPlayerObj.duration <= 0)
                        {
                            continue;
                        }
                        if (globalBuffPlayerObj.model.Priority > curPriority)
                        {
                            curPriority = globalBuffPlayerObj.model.Priority;
                            buffObjMaxPriority = globalBuffPlayerObj;
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