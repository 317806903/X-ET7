using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (BuffObj))]
    public static class BuffObjSystem
    {
        [ObjectSystem]
        public class BuffObjAwakeSystem: AwakeSystem<BuffObj>
        {
            protected override void Awake(BuffObj self)
            {
            }
        }

        [ObjectSystem]
        public class BuffObjDestroySystem: DestroySystem<BuffObj>
        {
            protected override void Destroy(BuffObj self)
            {
            }
        }

        public static void Init(this BuffObj self, int buffCfgId)
        {
        }

        public static string GetActionId(this BuffObj self, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent)
        {
            if (self.model.monitorTriggers.TryGetValue(abilityBuffMonitorTriggerEvent, out string actionId))
            {
                return actionId;
            }

            return "";
        }

        public static void FixedUpdate(this BuffObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            self.timeElapsed += timePassed;

            if (self.model.tickTime > 0)
            {
                string actionId = self.GetActionId(AbilityBuffMonitorTriggerEvent.BuffOnTick);
                if (string.IsNullOrWhiteSpace(actionId) == false)
                {
                    //float取模不精准，所以用x1000后的整数来
                    if (Math.Round(self.timeElapsed * 1000) % Math.Round(self.model.tickTime * 1000) == 0)
                    {
                        ActionHandlerHelper.CreateAction(actionId, self.casterUnitId, 0);
                        self.ticked += 1;
                    }
                }
            }
        }

        public static bool ChkNeedRemove(this BuffObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0 || self.stack <= 0)
            {
                return true;
            }

            return false;
        }
    }
}