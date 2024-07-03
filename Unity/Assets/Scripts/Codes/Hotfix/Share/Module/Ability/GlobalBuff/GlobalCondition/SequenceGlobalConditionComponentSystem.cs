using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (SequenceGlobalConditionComponent))]
    public static class SequenceGlobalConditionComponentSystem
    {
        [ObjectSystem]
        public class SequenceGlobalConditionComponentAwakeSystem: AwakeSystem<SequenceGlobalConditionComponent>
        {
            protected override void Awake(SequenceGlobalConditionComponent self)
            {
            }
        }

        [ObjectSystem]
        public class SequenceGlobalConditionComponentDestroySystem: DestroySystem<SequenceGlobalConditionComponent>
        {
            protected override void Destroy(SequenceGlobalConditionComponent self)
            {
            }
        }

        [ObjectSystem]
        public class SequenceGlobalConditionComponentFixedUpdateSystem: FixedUpdateSystem<SequenceGlobalConditionComponent>
        {
            protected override void FixedUpdate(SequenceGlobalConditionComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask Init(this SequenceGlobalConditionComponent self, SequenceGlobalCondition sequenceGlobalCondition)
        {
            foreach (ChkTriggerBase chkTriggerBase in sequenceGlobalCondition.Conditions)
            {
                GlobalConditionObj globalConditionObj = self.AddChild<GlobalConditionObj>();
                await globalConditionObj.Init(chkTriggerBase);
            }
        }

        public static bool ChkConditionPass(this SequenceGlobalConditionComponent self)
        {
            return self.status;
        }

        public static void EventHandler(this SequenceGlobalConditionComponent self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
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

            bool status = true;
            foreach (var children in self.Children.Values)
            {
                GlobalConditionObj globalConditionObj = children as GlobalConditionObj;
                globalConditionObj.EventHandler(abilityGameMonitorTriggerEvent, ref actionGameContext);
                if (status && globalConditionObj.ChkConditionPass() == false)
                {
                    status = false;
                }
            }
            if (status)
            {
                self.status = true;
            }
        }

        public static void FixedUpdate(this SequenceGlobalConditionComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

        }

    }
}