using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (GlobalConditionObj))]
    public static class GlobalConditionObjSystem
    {
        [ObjectSystem]
        public class GlobalConditionObjAwakeSystem: AwakeSystem<GlobalConditionObj>
        {
            protected override void Awake(GlobalConditionObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalConditionObjDestroySystem: DestroySystem<GlobalConditionObj>
        {
            protected override void Destroy(GlobalConditionObj self)
            {
            }
        }

        [ObjectSystem]
        public class GlobalConditionObjFixedUpdateSystem: FixedUpdateSystem<GlobalConditionObj>
        {
            protected override void FixedUpdate(GlobalConditionObj self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static async ETTask Init(this GlobalConditionObj self, ChkTriggerBase chkTriggerBase)
        {
            self.globalCondition = chkTriggerBase;
            await ETTask.CompletedTask;
        }

        public static bool ChkConditionPass(this GlobalConditionObj self)
        {
            return self.status;
        }

        public static void EventHandler(this GlobalConditionObj self, ET.AbilityConfig.GlobalBuffTriggerEvent abilityGameMonitorTriggerEvent, ref ActionGameContext actionGameContext)
        {
            if (self.status)
            {
                return;
            }
            if (self.globalCondition is TriggerImmediatelyBase triggerImmediatelyBase)
            {
                bool bPass = GlobalConditionHelper.ChkConditionOne_Immediately(self.DomainScene(), triggerImmediatelyBase, abilityGameMonitorTriggerEvent, ref actionGameContext);
                if (bPass)
                {
                    self.status = true;
                }
            }
            else if (self.globalCondition is TriggerChkConditionBase triggerChkConditionBase)
            {
                bool bPass = GlobalConditionHelper.ChkConditionOne_Condition(self.DomainScene(), triggerChkConditionBase, abilityGameMonitorTriggerEvent, ref actionGameContext);
                if (bPass)
                {
                    self.curCount++;
                    if (self.curCount >= triggerChkConditionBase.NeedCount)
                    {
                        self.status = true;
                    }
                }
            }

        }

        public static void FixedUpdate(this GlobalConditionObj self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

        }

    }
}