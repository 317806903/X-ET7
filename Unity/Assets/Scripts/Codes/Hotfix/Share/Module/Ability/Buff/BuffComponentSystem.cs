using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

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
                self.buffControlStateList = new();
            }
        }

        [ObjectSystem]
        public class BuffComponentDestroySystem: DestroySystem<BuffComponent>
        {
            protected override void Destroy(BuffComponent self)
            {
                self.removeList.Clear();
                self.monitorTriggerList.Clear();
                self.buffTagTypeList.Clear();
                self.buffImmuneTagTypeList.Clear();
                self.buffTagGroupTypeList.Clear();
                self.buffImmuneTagGroupTypeList.Clear();
                self.buffTypeList.Clear();
                self.buffControlStateList.Clear();
            }
        }
        
        [ObjectSystem]
        public class BuffComponentFixedUpdateSystem: FixedUpdateSystem<BuffComponent>
        {
            protected override void FixedUpdate(BuffComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }
                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }
        
        public static BuffObj AddBuff(this BuffComponent self, Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo, ActionContext actionContext)
        {
            (bool canAdd, string msg) = self.ChkCanAdd(addBuffInfo);
            if (canAdd == false)
            {
                Log.Debug($" AddBuff canAdd==false msg={msg}");
                return null;
            }

            bool IsEnabled = self.ChkIsEnabledByTagGroup(addBuffInfo);

            (bool canStack, BuffObj buffObj) = self.ChkCanStack(casterUnit, unit, addBuffInfo);
            if (canStack)
            {
                buffObj.AddStackCount(addBuffInfo.AddStack);
                Log.Debug($" AddBuff buffId[{buffObj.model.Id}] canStack==true curStackCount={buffObj.stack}");
            }
            else
            {
                buffObj = self.AddChild<BuffObj>();
                buffObj.isEnabled = IsEnabled;
                buffObj.Init(casterUnit, unit, addBuffInfo);
                buffObj.InitActionContext(actionContext);
                self.DealWhenAddBuff(buffObj);
                
                Log.Debug($" AddBuff buffId[{buffObj.model.Id}] canStack==false curStackCount={buffObj.stack}");
                
                buffObj.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnAwake);
                buffObj.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnStart);
            }
            return buffObj;
        }

        public static (bool, BuffObj) ChkCanStack(this BuffComponent self, Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo)
        {
            if (self.Children.Count <= 0)
            {
                return (false, null);
            }
            foreach (var buffObjs in self.Children)
            {
                BuffObj buffObj = buffObjs.Value as BuffObj;
                if (buffObj.CfgId == addBuffInfo.BuffId && buffObj.casterUnitId == casterUnit.Id)
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
            self.AddBuffTypeList(buffObj);
            self.AddBuffControlStateList(buffObj);

            buffObj.AddBuffWhenModifyAttribute();
        }
        
        public static void DealWhenRemoveBuff(this BuffComponent self, BuffObj buffObj)
        {
            self.RemoveMonitorTriggerList(buffObj);
            self.RemoveBuffTagTypeList(buffObj);
            self.RemoveBuffImmuneTagTypeList(buffObj);
            self.RemoveBuffTypeList(buffObj);
            self.RemoveBuffControlStateList(buffObj);
            
            buffObj.RemoveBuffWhenModifyAttribute();
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
        
        public static void EventHandler(this BuffComponent self, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            if (self.monitorTriggerList.ContainsKey(abilityBuffMonitorTriggerEvent) == false)
            {
                return;
            }
            ListComponent<BuffObj> priorityBuffObjs = ListComponent<BuffObj>.Create();
            List<BuffObj> buffObjs = self.monitorTriggerList[abilityBuffMonitorTriggerEvent];
            buffObjs.Sort((a, b) => a.model.Priority.CompareTo(b.model.Priority));
            foreach (BuffObj buffObj in buffObjs)
            {
                buffObj.TrigEvent(abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit);
            }
        }

        public static void Remove(this BuffComponent self, BuffObj buffObj)
        {
            buffObj.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnDestroy);
            
            self.DealWhenRemoveBuff(buffObj);
            self.DoEnabledTagGroup(buffObj);
            buffObj.Dispose();
        }
        
        public static void FixedUpdate(this BuffComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();
            foreach (var buffObjs in self.Children)
            {
                BuffObj buffObj = buffObjs.Value as BuffObj;
                buffObj.FixedUpdate(fixedDeltaTime);

                if (buffObj.ChkNeedRemove())
                {
                    buffObj.TrigEvent(AbilityBuffMonitorTriggerEvent.BuffOnRemoved);
                }
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
        
    }
}