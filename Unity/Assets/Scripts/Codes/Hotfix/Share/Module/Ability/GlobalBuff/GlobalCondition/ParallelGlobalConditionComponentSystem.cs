using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (ParallelGlobalConditionComponent))]
    public static class ParallelGlobalConditionComponentSystem
    {
        [ObjectSystem]
        public class ParallelGlobalConditionComponentAwakeSystem: AwakeSystem<ParallelGlobalConditionComponent>
        {
            protected override void Awake(ParallelGlobalConditionComponent self)
            {
            }
        }

        [ObjectSystem]
        public class ParallelGlobalConditionComponentDestroySystem: DestroySystem<ParallelGlobalConditionComponent>
        {
            protected override void Destroy(ParallelGlobalConditionComponent self)
            {
            }
        }

        [ObjectSystem]
        public class ParallelGlobalConditionComponentFixedUpdateSystem: FixedUpdateSystem<ParallelGlobalConditionComponent>
        {
            protected override void FixedUpdate(ParallelGlobalConditionComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask Init(this ParallelGlobalConditionComponent self, List<SequenceGlobalCondition> conditions)
        {
            foreach (SequenceGlobalCondition sequenceGlobalCondition in conditions)
            {
                SequenceGlobalConditionComponent sequenceGlobalConditionComponent = self.AddChild<SequenceGlobalConditionComponent>();
                await sequenceGlobalConditionComponent.Init(sequenceGlobalCondition);
            }
        }

        public static bool ChkConditionPass(this ParallelGlobalConditionComponent self)
        {
            return self.status;
        }

        public static void EventHandler(this ParallelGlobalConditionComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (self.status)
            {
                return;
            }

            if (self.Children.Count == 0)
            {
                self.status = true;
                return;
            }

            bool status = false;
            foreach (var children in self.Children.Values)
            {
                SequenceGlobalConditionComponent sequenceGlobalConditionComponent = children as SequenceGlobalConditionComponent;
                sequenceGlobalConditionComponent.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
                if (status == false && sequenceGlobalConditionComponent.ChkConditionPass())
                {
                    status = true;
                }
            }
            if (status)
            {
                self.status = true;
            }
        }

        public static void FixedUpdate(this ParallelGlobalConditionComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

        }

    }
}