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
        
        public static void FixedUpdate(this Unit self, float fixedDeltaTime)
        {
            // self.GetComponent<BuffComponent>().FixedUpdate(fixedDeltaTime);
            // self.GetComponent<SkillComponent>().FixedUpdate(fixedDeltaTime);
            // self.GetComponent<BulletObj>()?.FixedUpdate(fixedDeltaTime);
            // self.GetComponent<AoeObj>()?.FixedUpdate(fixedDeltaTime);
        }
        
        public static bool CanBeKilledByDamageInfo(this Unit self, DamageInfo damageInfo)
        {
            return false;
        }
    }
}