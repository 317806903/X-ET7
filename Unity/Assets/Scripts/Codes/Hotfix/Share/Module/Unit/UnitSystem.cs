using System.Collections.Generic;
using ET.Ability;

namespace ET
{
    public static class UnitSystem
    {
        [ObjectSystem]
        public class UnitAwakeSystem: AwakeSystem<Unit, string>
        {
            protected override void Awake(Unit self, string unitCfgId)
            {
                self.CfgId = unitCfgId;
            }
        }

        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, EntityRef<AOIEntity>> GetBeSeePlayers(this Unit self)
        {
            AOIEntity aoiEntity = self.GetComponent<AOIEntity>();
            if (aoiEntity == null)
            {
                return null;
            }
            return aoiEntity.GetBeSeePlayers();
        }

        public static bool ChkIsInDeath(this Unit self)
        {
            return ET.Ability.DeathShowHelper.ChkIsInDeath(self);
        }

        public static void DestroyWithDeathShow(this Unit self)
        {
            ET.Ability.DeathShowHelper.DeathShow(self);
        }

        public static void DestroyNotDeathShow(this Unit self)
        {
            self._Destroy();
        }

        public static void _Destroy(this Unit self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.UnitOnRemoved() { unit = self });
            //self.Dispose();
            UnitHelper.AddWaitRemove(self);
        }

    }
}