using System;
using System.Collections.Generic;
using System.Xml.Schema;

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

        public static void Init(this BulletObj self, long casterUnitId, int bulletCfgId)
        {
            self.casterUnitId = casterUnitId;
        }

        public static string GetActionId(this BulletObj self, AbilityBulletMonitorTriggerEvent abilityBulletMonitorTriggerEvent)
        {
            for (int i = 0; i < self.model.MonitorTriggers.Count; i++)
            {
                if ((int)self.model.MonitorTriggers[i].BulletTrig == (int)abilityBulletMonitorTriggerEvent)
                {
                    return self.model.MonitorTriggers[i].ActionId;
                }
            }
            // if (self.model.MonitorTriggers.TryGetValue(abilityBulletMonitorTriggerEvent, out string actionId))
            // {
            //     return actionId;
            // }

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
            if (self.hitRecords != null)
            {
                for (int i = self.hitRecords.Count - 1; i >= 0; i--)
                {
                    self.hitRecords[i].timeToCanHit -= timePassed;
                    if (self.hitRecords[i].timeToCanHit <= 0)
                    {
                        self.hitRecords.RemoveAt(i);
                    }
                }
            }

            if (self.canHitAfterCreated > 0)
            {
                self.canHitAfterCreated -= timePassed;
            }

            if (self.duration <= 0 || self.hp <= 0)
            {
                self.GetUnit().Destroy();
            }
        }

        public static bool CanHit(this BulletObj self, Unit unit)
        {
            if (self.hp <= 0)
                return false;
            if (self.canHitAfterCreated > 0)
                return false;
            if (self.hitRecords != null)
            {
                for (int i = 0; i < self.hitRecords.Count; i++)
                {
                    if (self.hitRecords[i].targetUnitId == unit.Id)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}