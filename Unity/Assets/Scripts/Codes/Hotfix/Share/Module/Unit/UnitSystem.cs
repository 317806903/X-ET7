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
        
        public static void DestroyWithDeathShow(this Unit self)
        {
            ET.Ability.DeathShowHelper.DeathShow(self);
        }

        public static void Destroy(this Unit self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.UnitOnRemoved() { unit = self });
            //self.Dispose();
            UnitHelper.AddWaitRemove(self);
        }

    }
}