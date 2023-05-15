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
                self.unitCfgId = unitCfgId;
            }
        }
        
        [ObjectSystem]
        public class UnitAwakeSystem2: AwakeSystem<Unit>
        {
            protected override void Awake(Unit self)
            {
            }
        }

        public static void Destroy(this Unit self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.UnitOnRemoved() { unit = self });
            self.Dispose();
        }

        public static bool CanBeKilledByDamageInfo(this Unit self, DamageInfo damageInfo)
        {
            return false;
        }
    }
}