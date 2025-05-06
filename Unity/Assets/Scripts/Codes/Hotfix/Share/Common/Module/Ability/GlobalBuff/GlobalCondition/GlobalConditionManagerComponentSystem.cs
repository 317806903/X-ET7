using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalConditionManagerComponent))]
    public static class GlobalConditionManagerComponentSystem
    {
        [ObjectSystem]
        public class GlobalConditionManagerComponentAwakeSystem: AwakeSystem<GlobalConditionManagerComponent>
        {
            protected override void Awake(GlobalConditionManagerComponent self)
            {
                self.monitorTriggerList = new();
            }
        }

        [ObjectSystem]
        public class GlobalConditionManagerComponentDestroySystem: DestroySystem<GlobalConditionManagerComponent>
        {
            protected override void Destroy(GlobalConditionManagerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalConditionManagerComponentFixedUpdateSystem: FixedUpdateSystem<GlobalConditionManagerComponent>
        {
            protected override void FixedUpdate(GlobalConditionManagerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask Init(this GlobalConditionManagerComponent self, List<SequenceGlobalCondition> conditions1, List<SequenceGlobalCondition> conditions2)
        {
            if (conditions1 != null && conditions1.Count > 0)
            {
                ParallelGlobalConditionComponent parallelGlobalConditionComponent1 = self.AddChild<ParallelGlobalConditionComponent>();
                await parallelGlobalConditionComponent1.Init(conditions1);

                self.RecordMonitorTrigger(conditions1);
            }
            if (conditions2 != null && conditions2.Count > 0)
            {
                ParallelGlobalConditionComponent parallelGlobalConditionComponent2 = self.AddChild<ParallelGlobalConditionComponent>();
                await parallelGlobalConditionComponent2.Init(conditions2);

                self.RecordMonitorTrigger(conditions2);
            }
        }

        public static void RecordMonitorTrigger(this GlobalConditionManagerComponent self, List<SequenceGlobalCondition> conditions)
        {
            foreach (SequenceGlobalCondition sequenceGlobalCondition in conditions)
            {
                foreach (ChkTriggerBase globalCondition in sequenceGlobalCondition.Conditions)
                {

                    if (globalCondition is TriggerImmediatelyBase triggerImmediatelyBase)
                    {
                        self.monitorTriggerList.Add(triggerImmediatelyBase.TriggerType);
                    }
                    else if (globalCondition is TriggerChkConditionBase triggerChkConditionBase)
                    {
                        if (globalCondition is TriggerChkConditionImmediately TriggerChkConditionImmediately)
                        {
                            self.monitorTriggerList.Add(TriggerChkConditionImmediately.TriggerImmediatelyType.TriggerType);
                        }
                        else if (globalCondition is TriggerChkConditionStatus TriggerChkConditionStatus)
                        {
                            foreach (TriggerImmediatelyBase triggerImmediatelyType in TriggerChkConditionStatus.TriggerImmediatelyTypes)
                            {
                                self.monitorTriggerList.Add(triggerImmediatelyType.TriggerType);
                            }
                        }
                    }

                }
            }
        }

        public static bool ChkConditionPass(this GlobalConditionManagerComponent self)
        {
            return self.status;
        }

        public static void EventHandler(this GlobalConditionManagerComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (self.status)
            {
                return;
            }
            bool status = true;
            foreach (var children in self.Children.Values)
            {
                ParallelGlobalConditionComponent parallelGlobalConditionComponent = children as ParallelGlobalConditionComponent;
                parallelGlobalConditionComponent.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
                if (status && parallelGlobalConditionComponent.ChkConditionPass() == false)
                {
                    status = false;
                }
            }
            if (status)
            {
                self.status = true;
            }
        }

        public static void FixedUpdate(this GlobalConditionManagerComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

        }

    }
}