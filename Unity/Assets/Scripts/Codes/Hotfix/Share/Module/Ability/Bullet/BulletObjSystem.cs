using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (BulletObj))]
    public static class BulletObjSystem
    {
        [ObjectSystem]
        public class BulletObjAwakeSystem: AwakeSystem<BulletObj>
        {
            protected override void Awake(BulletObj self)
            {
            }
        }

        [ObjectSystem]
        public class BulletObjDestroySystem: DestroySystem<BulletObj>
        {
            protected override void Destroy(BulletObj self)
            {
            }
        }

        public static void Init(this BulletObj self)
        {
        }

        public static string GetActionId(this BulletObj self, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            if (self.model.monitorTriggers.TryGetValue(abilityBulletMonitorTriggerEvent, out string actionId))
            {
                return actionId;
            }

            return "";
        }

        public static Unit GetUnit(this BulletObj self)
        {
            return self.GetParent<Unit>();
        }
        
        public static void EventHandler(this BulletObj self, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            string actionId = self.GetActionId(abilityBulletMonitorTriggerEvent);
            if (string.IsNullOrWhiteSpace(actionId) == false)
            {
                ActionHandlerHelper.CreateAction(self.GetUnit(), actionId, null);
            }
        }

        public static void FixedUpdate(this BulletObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            self.timeElapsed += timePassed;
            if (self.duration <= 0)
            {
                self.GetUnit().Destroy();
            }
        }
    }
}