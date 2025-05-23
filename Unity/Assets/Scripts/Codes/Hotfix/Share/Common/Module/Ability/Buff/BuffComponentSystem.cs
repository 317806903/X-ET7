﻿using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (BuffComponent))]
    [FriendOf(typeof (BuffObj))]
    public static class BuffComponentSystem
    {
        [ObjectSystem]
        public class BuffComponentAwakeSystem: AwakeSystem<BuffComponent>
        {
            protected override void Awake(BuffComponent self)
            {
                self.removeList = new();

                self.monitorTriggerList = new();
                self.buffTagTypeList = new();
                self.buffImmuneTagTypeList = new();
                self.buffTagGroupTypeList = new();
                self.buffImmuneTagGroupTypeList = new();
                self.buffTypeList = new();
                self.buffMotionList = new();
                self.buffPlayAnimatorList = new();
            }
        }

        [ObjectSystem]
        public class BuffComponentDestroySystem: DestroySystem<BuffComponent>
        {
            protected override void Destroy(BuffComponent self)
            {
                self.removeList.Clear();
                self.removeList = null;
                self.monitorTriggerList.Clear();
                self.monitorTriggerList = null;
                self.buffTagTypeList.Clear();
                self.buffTagTypeList = null;
                self.buffImmuneTagTypeList.Clear();
                self.buffImmuneTagTypeList = null;
                self.buffTagGroupTypeList.Clear();
                self.buffTagGroupTypeList = null;
                self.buffImmuneTagGroupTypeList.Clear();
                self.buffImmuneTagGroupTypeList = null;
                self.buffTypeList.Clear();
                self.buffTypeList = null;
                self.buffMotionList.Clear();
                self.buffMotionList = null;
                self.buffPlayAnimatorList.Clear();
                self.buffPlayAnimatorList = null;
            }
        }

        [ObjectSystem]
        public class BuffComponentFixedUpdateSystem: FixedUpdateSystem<BuffComponent>
        {
            protected override void FixedUpdate(BuffComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask<BuffObj> AddBuff(this BuffComponent self, Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo, ActionContext actionContext)
        {
            (bool canAdd, string msg) = self.ChkCanAdd(addBuffInfo);
            if (canAdd == false)
            {
                Log.Debug($" AddBuff canAdd==false msg={msg}");
                return null;
            }

            (bool willRemoveExist, HashSet<BuffObj> removeExistBuffObjs) = self.ChkWillRemoveExist(addBuffInfo);
            if (willRemoveExist)
            {
                foreach (BuffObj removeExistBuffObj in removeExistBuffObjs)
                {
                    removeExistBuffObj.ChgDuration(0);
                }
            }

            (bool canStack, BuffObj buffObj) = self.ChkCanStack(casterUnit, unit, addBuffInfo);
            if (canStack)
            {
                buffObj.AddStackCount(addBuffInfo.AddStack);
                //Log.Debug($" AddBuff buffId[{buffObj.model.Id}] canStack==true curStackCount={buffObj.stack}");
            }
            else
            {
                bool IsEnabled = self.ChkIsEnabledByTagGroup(addBuffInfo);

                if (self.isForeaching)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (self.IsDisposed)
                    {
                        return null;
                    }
                }
                buffObj = self.AddChild<BuffObj>();
                buffObj.isEnabled = IsEnabled;
                buffObj.Init(casterUnit, unit, addBuffInfo);
                buffObj.InitActionContext(ref actionContext);
                self.DealWhenAddBuff(buffObj);

                //Log.Debug($" AddBuff buffId[{buffObj.model.Id}] canStack==false curStackCount={buffObj.stack}");

                bool isReady = await ET.AOIHelper.ChkAOIReady(self, unit);
                if (isReady == false)
                {
                    return null;
                }
                if (self.IsDisposed || buffObj.IsDisposed)
                {
                    return null;
                }
                buffObj.ChkBuffAwake();
                buffObj.ChkBuffStart();
            }

            self.NoticeUnitBuffStatusChg();

            return buffObj;
        }

        public static (bool, BuffObj) ChkCanStack(this BuffComponent self, Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo)
        {
            if (self.Children.Count <= 0)
            {
                return (false, null);
            }
            foreach (var obj in self.Children.Values)
            {
                BuffObj buffObj = obj as BuffObj;
                if (buffObj.CfgId != addBuffInfo.BuffId)
                {
                    continue;
                }

                if (buffObj.model.IsIgnoreCasterActor)
                {
                    return (true, buffObj);
                }

                Unit casterUnitActor = UnitHelper.GetUnit(self.DomainScene(), casterUnit.Id).GetCasterRootActor();
                if (buffObj.GetCasterActorUnit() == casterUnitActor)
                {
                    return (true, buffObj);
                }
            }

            return (false, null);
        }

        public static void DealWhenAddBuff(this BuffComponent self, BuffObj buffObj)
        {
            self.AddMonitorTriggerList(buffObj);
            self.AddBuffTagTypeList(buffObj);
            self.AddBuffImmuneTagTypeList(buffObj);
            self.AddBuffMotionList(buffObj);
            self.AddBuffPlayAnimatorList(buffObj);
            self.AddBuffTypeList(buffObj);

            buffObj.DealSelfEffectWhenAddBuff().Coroutine();
            buffObj.DealSpecWhenAddBuff(false);
        }

        public static void DealWhenRemoveBuff(this BuffComponent self, BuffObj buffObj)
        {
            self.RemoveMonitorTriggerList(buffObj);
            self.RemoveBuffTagTypeList(buffObj);
            self.RemoveBuffImmuneTagTypeList(buffObj);
            self.RemoveBuffMotionList(buffObj);
            self.RemoveBuffPlayAnimatorList(buffObj);
            self.RemoveBuffTypeList(buffObj);

            buffObj.DealSelfEffectWhenRemoveBuff();
            buffObj.DealSpecWhenRemoveBuff(false);
        }

        public static void AddMonitorTriggerList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (var monitorTrigger in buffObj.monitorTriggerList)
            {
                self.monitorTriggerList.Add(monitorTrigger.Key, buffObj);
            }
        }

        public static void RemoveMonitorTriggerList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (var monitorTrigger in buffObj.monitorTriggerList)
            {
                self.monitorTriggerList.Remove(monitorTrigger.Key, buffObj);
            }
        }

        public static void EventHandler(this BuffComponent self, AbilityConfig.BuffTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            if (self.monitorTriggerList.ContainsKey(abilityBuffMonitorTriggerEvent) == false)
            {
                return;
            }
            //ListComponent<BuffObj> priorityBuffObjs = ListComponent<BuffObj>.Create();
            List<BuffObj> buffObjs = self.monitorTriggerList[abilityBuffMonitorTriggerEvent];
            //buffObjs.Sort((a, b) => a.model.Priority.CompareTo(b.model.Priority));
            foreach (BuffObj buffObj in buffObjs)
            {
                buffObj.SetBuffActionContext(ref actionContext);
                buffObj.TrigEvent(abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit, ref actionContext);
            }
        }

        public static void Remove(this BuffComponent self, BuffObj buffObj)
        {
            buffObj.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnDestroy, null, null, ref buffObj.actionContext);

            self.DealWhenRemoveBuff(buffObj);
            self.DoEnabledTagGroup(buffObj);
            buffObj.Dispose();

            self.NoticeUnitBuffStatusChg();
        }

        public static void NoticeUnitBuffStatusChg(this BuffComponent self)
        {
            EventType.NoticeUnitBuffStatusChg _NoticeUnitBuffStatusChg = new()
            {
                Unit = self.GetUnit(),
            };
            EventSystem.Instance.Publish(self.DomainScene(), _NoticeUnitBuffStatusChg);
        }

        public static Unit GetUnit(this BuffComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this BuffComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();
            self.isForeaching = true;
            foreach (var obj in self.Children.Values)
            {
                BuffObj buffObj = obj as BuffObj;
                buffObj.FixedUpdate(fixedDeltaTime);

                if (buffObj.ChkNeedRemove())
                {
                    buffObj.TrigEvent(AbilityConfig.BuffTriggerEvent.BuffOnRemoved, null, null, ref buffObj.actionContext);
                }
                if (buffObj.ChkNeedRemove())
                {
                    self.removeList.Add(buffObj);
                }
            }
            self.isForeaching = false;

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.Remove(self.removeList[i]);
            }

            self.removeList.Clear();
        }

        public static bool ChkBuffByBuffCfgId(this BuffComponent self, string buffCfgId)
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

        public static List<BuffObj> GetBuffListByBuffCfgId(this BuffComponent self, string buffCfgId)
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

        public static List<BuffObj> GetAllBuffList(this BuffComponent self)
        {
            List<BuffObj> buffList = ListComponent<BuffObj>.Create();
            foreach (var buffObjs in self.Children)
            {
                BuffObj buffObj = buffObjs.Value as BuffObj;

                buffList.Add(buffObj);
            }
            return buffList;
        }

    }
}