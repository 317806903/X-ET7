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

        public static BuffObj AddBuff(this BuffComponent self, string buffCfgId)
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
        
        public static void EventHandler(this BuffComponent self, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onHitUnit, Unit beHurtUnit)
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
                List<BuffActionCall> buffActionCalls = buffObj.GetActionIds(abilityBuffMonitorTriggerEvent);
                if (buffActionCalls.Count > 0)
                {
                    for (int i = 0; i < buffActionCalls.Count; i++)
                    {
                        buffObj.EventHandler(buffActionCalls[i], onHitUnit, beHurtUnit);
                    }
                }
            }
        }

        public static void Remove(this BuffComponent self, BuffObj buffObj)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnDestroy() { buff = buffObj });
            self.RemoveMonitorTriggerList(buffObj);
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
                    EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnRemoved() { buff = buffObj });
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