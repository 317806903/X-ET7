using ET.Ability;

namespace ET
{
    public static class UnitSystem
    {
        [ObjectSystem]
        public class UnitAwakeSystem: AwakeSystem<Unit, int>
        {
            protected override void Awake(Unit self, int configId)
            {
                self.ConfigId = configId;
            }
        }
        
        public static void Destroy(this Unit self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.UnitOnRemoved() { unitId = self.Id });
            self.Dispose();
        }
        
        public static bool CanBeKilledByDamageInfo(this Unit self, DamageInfo damageInfo)
        {
            return false;
        }
    }
}