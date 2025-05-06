using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalBuffUnitComponent))]
    public static class GlobalBuffUnitComponentSystem
    {
        [ObjectSystem]
        public class GlobalBuffUnitComponentAwakeSystem: AwakeSystem<GlobalBuffUnitComponent>
        {
            protected override void Awake(GlobalBuffUnitComponent self)
            {
                self.removeList = new();

                self.monitorTriggerList = new();

                self.buffTagGroupTypeList = new();
            }
        }

        [ObjectSystem]
        public class GlobalBuffUnitComponentDestroySystem: DestroySystem<GlobalBuffUnitComponent>
        {
            protected override void Destroy(GlobalBuffUnitComponent self)
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
        public class GlobalBuffUnitComponentFixedUpdateSystem: FixedUpdateSystem<GlobalBuffUnitComponent>
        {
            protected override void FixedUpdate(GlobalBuffUnitComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask<GlobalBuffUnitObj> AddGlobalBuff(this GlobalBuffUnitComponent self, long casterPlayerId, string unitGlobalBuffCfgId, long unitId)
        {
            (bool isExist, GlobalBuffUnitObj globalBuffUnitObj) = self.ChkExist(unitId, unitGlobalBuffCfgId);
            if (isExist)
            {
                if (globalBuffUnitObj.casterPlayerId == (long)ET.PlayerId.PlayerNone)
                {
                    globalBuffUnitObj.casterPlayerId = casterPlayerId;
                }
            }
            else
            {
                bool IsEnabled = self.ChkIsEnabledByTagGroup(unitId, unitGlobalBuffCfgId);

                globalBuffUnitObj = self.AddChild<GlobalBuffUnitObj>();
                globalBuffUnitObj.isEnabled = IsEnabled;
                await globalBuffUnitObj.Init(casterPlayerId, unitId, unitGlobalBuffCfgId);
                self.DealWhenAddBuff(globalBuffUnitObj);

            }

            return globalBuffUnitObj;
        }

        public static (bool, GlobalBuffUnitObj) ChkExist(this GlobalBuffUnitComponent self, long unitId, string unitGlobalBuffCfgId)
        {
            if (self.Children.Count <= 0)
            {
                return (false, null);
            }
            foreach (var obj in self.Children.Values)
            {
                GlobalBuffUnitObj globalBuffUnitObj = obj as GlobalBuffUnitObj;
                if (globalBuffUnitObj.CfgId != unitGlobalBuffCfgId)
                {
                    continue;
                }
                if (globalBuffUnitObj.unitId != unitId)
                {
                    continue;
                }

                return (true, globalBuffUnitObj);
            }

            return (false, null);
        }

        public static void DealWhenAddBuff(this GlobalBuffUnitComponent self, GlobalBuffUnitObj buffObj)
        {
            self.AddMonitorTriggerList(buffObj);
            self.AddBuffTagTypeList(buffObj);
        }

        public static void DealWhenRemoveBuff(this GlobalBuffUnitComponent self, GlobalBuffUnitObj buffObj)
        {
            self.RemoveMonitorTriggerList(buffObj);
            self.RemoveBuffTagTypeList(buffObj);

        }

        public static void AddMonitorTriggerList(this GlobalBuffUnitComponent self, GlobalBuffUnitObj globalBuffUnitObj)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = globalBuffUnitObj.GetComponent<GlobalConditionManagerComponent>();
            foreach (var monitorTrigger in globalConditionManagerComponent.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger, globalBuffUnitObj);
            }
        }

        public static void RemoveMonitorTriggerList(this GlobalBuffUnitComponent self, GlobalBuffUnitObj globalBuffUnitObj)
        {
            GlobalConditionManagerComponent globalConditionManagerComponent = globalBuffUnitObj.GetComponent<GlobalConditionManagerComponent>();
            foreach (var monitorTrigger in globalConditionManagerComponent.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger, globalBuffUnitObj);
            }
        }

        public static void AddBuffTagTypeList(this GlobalBuffUnitComponent self, GlobalBuffUnitObj buffObj)
        {
            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Add(buffObj.model.TagGroup.Value, buffObj);
            }
        }

        public static void RemoveBuffTagTypeList(this GlobalBuffUnitComponent self, GlobalBuffUnitObj buffObj)
        {
            if (buffObj.model.TagGroup != null)
            {
                self.buffTagGroupTypeList.Remove(buffObj.model.TagGroup.Value, buffObj);
            }
        }

        public static void EventHandler(this GlobalBuffUnitComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (self.monitorTriggerList.ContainsKey(abilityGameMonitorTriggerEvent) == false)
            {
                return;
            }
            List<EntityRef<GlobalBuffUnitObj>> buffObjs = self.monitorTriggerList[abilityGameMonitorTriggerEvent];
            foreach (GlobalBuffUnitObj globalBuffUnitObj in buffObjs)
            {
                if (globalBuffUnitObj == null)
                {
                    continue;
                }
                if (globalBuffUnitObj.isEnabled == false)
                {
                    continue;
                }

                if (actionGameContext.unitId != 0 && globalBuffUnitObj.unitId != actionGameContext.unitId)
                {
                    continue;
                }
                globalBuffUnitObj.TrigEvent(abilityGameMonitorTriggerEvent, ref actionGameContext);
            }
        }

        public static void Remove(this GlobalBuffUnitComponent self, GlobalBuffUnitObj buffObj)
        {
            self.DealWhenRemoveBuff(buffObj);
            self.DoEnabledTagGroup(buffObj.unitId, buffObj);
            buffObj.Dispose();

        }

        public static void FixedUpdate(this GlobalBuffUnitComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();

            foreach (var obj in self.Children.Values)
            {
                GlobalBuffUnitObj buffObj = obj as GlobalBuffUnitObj;
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

        public static bool ChkBuffByBuffCfgId(this GlobalBuffUnitComponent self, string buffCfgId)
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

        public static List<BuffObj> GetBuffListByBuffCfgId(this GlobalBuffUnitComponent self, string buffCfgId)
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

        public static List<BuffObj> GetAllBuffList(this GlobalBuffUnitComponent self)
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
        public static bool ChkIsEnabledByTagGroup(this GlobalBuffUnitComponent self, long unitId, string unitGlobalBuffCfgId)
        {
            if (self.Children.Count <= 0)
            {
                return true;
            }

            UnitGlobalBuffCfg unitGlobalBuffCfg = UnitGlobalBuffCfgCategory.Instance.Get(unitGlobalBuffCfgId);
            if (unitGlobalBuffCfg.TagGroup == null)
            {
                return true;
            }

            BuffTagGroupType tagGroup = unitGlobalBuffCfg.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
                if (self.buffTagGroupTypeList[tagGroup].Count > 0)
                {
                    foreach (GlobalBuffUnitObj globalBuffUnitObj in self.buffTagGroupTypeList[tagGroup])
                    {
                        if (globalBuffUnitObj.unitId != unitId)
                        {
                            continue;
                        }
                        if (globalBuffUnitObj.model.Priority > unitGlobalBuffCfg.Priority)
                        {
                            string msg = $"buffId[{unitGlobalBuffCfg.Id}] tagGroup[{tagGroup}] Priority[{unitGlobalBuffCfg.Priority}] < buffId[{globalBuffUnitObj.CfgId}] Priority[{globalBuffUnitObj.model.Priority}]";
                            return false;
                        }
                        else
                        {
                            globalBuffUnitObj.SetEnabled(false);
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
        /// <param name="curGlobalBuffUnitObj"></param>
        public static void DoEnabledTagGroup(this GlobalBuffUnitComponent self, long unitId, GlobalBuffUnitObj curGlobalBuffUnitObj)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            if (curGlobalBuffUnitObj.model.TagGroup == null)
            {
                return;
            }
            if (curGlobalBuffUnitObj.isEnabled == false)
            {
                return;
            }

            BuffTagGroupType tagGroup = curGlobalBuffUnitObj.model.TagGroup.Value;
            if (self.buffTagGroupTypeList.ContainsKey(tagGroup))
            {
                if (self.buffTagGroupTypeList[tagGroup].Count > 0)
                {
                    GlobalBuffUnitObj buffObjMaxPriority = null;
                    int curPriority = -999;
                    foreach (GlobalBuffUnitObj globalBuffUnitObj in self.buffTagGroupTypeList[tagGroup])
                    {
                        if (globalBuffUnitObj.unitId != unitId)
                        {
                            continue;
                        }
                        if (globalBuffUnitObj == curGlobalBuffUnitObj)
                        {
                            continue;
                        }
                        if (globalBuffUnitObj.duration <= 0)
                        {
                            continue;
                        }
                        if (globalBuffUnitObj.model.Priority > curPriority)
                        {
                            curPriority = globalBuffUnitObj.model.Priority;
                            buffObjMaxPriority = globalBuffUnitObj;
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