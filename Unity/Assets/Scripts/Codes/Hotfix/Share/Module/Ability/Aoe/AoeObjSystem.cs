using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (AoeObj))]
    public static class AoeObjSystem
    {
        [ObjectSystem]
        public class AoeObjAwakeSystem: AwakeSystem<AoeObj>
        {
            protected override void Awake(AoeObj self)
            {
            }
        }

        [ObjectSystem]
        public class AoeObjDestroySystem: DestroySystem<AoeObj>
        {
            protected override void Destroy(AoeObj self)
            {
            }
        }

        public static void Init(this AoeObj self)
        {
        }

        public static string GetActionId(this AoeObj self, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            if (self.model.monitorTriggers.TryGetValue(abilityAoeMonitorTriggerEvent, out string actionId))
            {
                return actionId;
            }

            return "";
        }

        public static Unit GetUnit(this AoeObj self)
        {
            return self.GetParent<Unit>();
        }

        public static void EventHandler(this AoeObj self, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            string actionId = self.GetActionId(abilityAoeMonitorTriggerEvent);
            if (string.IsNullOrWhiteSpace(actionId) == false)
            {
                ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, null);
            }
        }

        public static void FixedUpdate(this AoeObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            self.timeElapsed += timePassed;
            if (self.duration <= 0)
            {
                self.GetParent<Unit>().Destroy();
                return;
            }
            
            if (self.model.tickTime > 0)
            {
                string actionId = self.GetActionId(AbilityAoeMonitorTriggerEvent.AoeOnTick);
                if (string.IsNullOrWhiteSpace(actionId) == false)
                {
                    //float取模不精准，所以用x1000后的整数来
                    if (Math.Round(self.timeElapsed * 1000) % Math.Round(self.model.tickTime * 1000) == 0)
                    {
                        ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, null);
                        self.ticked += 1;
                    }
                }
            }
        }
    }
}