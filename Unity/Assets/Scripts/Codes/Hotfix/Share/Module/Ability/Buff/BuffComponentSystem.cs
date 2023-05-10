using System;
using System.Collections.Generic;

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
            }
        }

        [ObjectSystem]
        public class BuffComponentDestroySystem: DestroySystem<BuffComponent>
        {
            protected override void Destroy(BuffComponent self)
            {
                self.removeList.Clear();
                self.monitorTriggerList.Clear();
            }
        }

        public static BuffObj AddBuff(this BuffComponent self, int buffCfgId)
        {
            BuffObj buffObj = self.AddChild<BuffObj>();
            buffObj.Init(buffCfgId);
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnAwake()
            {
                buff = buffObj,
            });
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnStart()
            {
                buff = buffObj,
            });

            self.AddMonitorTriggerList(buffObj);
            return buffObj;
        }

        public static void AddMonitorTriggerList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (AbilityBuffMonitorTriggerEvent type in Enum.GetValues(typeof(AbilityBuffMonitorTriggerEvent)))
            {
                string actionId = buffObj.GetActionId(type);
                if (string.IsNullOrWhiteSpace(actionId) == false)
                {
                    self.monitorTriggerList.Add(type, buffObj);
                }
            }
        }
        
        public static void RemoveMonitorTriggerList(this BuffComponent self, BuffObj buffObj)
        {
            foreach (AbilityBuffMonitorTriggerEvent type in Enum.GetValues(typeof(AbilityBuffMonitorTriggerEvent)))
            {
                string actionId = buffObj.GetActionId(type);
                if (string.IsNullOrWhiteSpace(actionId) == false)
                {
                    self.monitorTriggerList.Remove(type, buffObj);
                }
            }
        }
        
        public static void EventHandler(this BuffComponent self, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            if (self.monitorTriggerList.ContainsKey(abilityBuffMonitorTriggerEvent) == false)
            {
                return;
            }
            HashSet<BuffObj> buffObjs = self.monitorTriggerList[abilityBuffMonitorTriggerEvent];
            foreach (BuffObj buffObj in buffObjs)
            {
                string actionId = buffObj.GetActionId(abilityBuffMonitorTriggerEvent);
                if (string.IsNullOrWhiteSpace(actionId) == false)
                {
                    ActionHandlerHelper.CreateAction(buffObj.GetUnit(), actionId, null);
                }
            }
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
                    EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnRemoved() { buff = buffObj });
                }
                if (buffObj.ChkNeedRemove())
                {
                    EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnDestroy() { buff = buffObj });
                    self.removeList.Add(buffObj);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.RemoveMonitorTriggerList(self.removeList[i]);
                self.removeList[i].Dispose();
            }

            self.removeList.Clear();
        }
    }
}