using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class AoeHelper
    {
        public static void CreateAoe(Unit unit, ActionCfg_CallAoe actionCfg_CallAoe, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            Unit aoeUnit = ET.GamePlayHelper.CreateAoeByUnit(unit.DomainScene(), unit, actionCfg_CallAoe, selectHandle, ref actionContext);

            EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnCreate()
            {
                unit = unit,
                createUnit = aoeUnit,
            });

        }


        public static void EventHandler(Unit unit, AbilityAoeMonitorTriggerEvent abilityAoeMonitorTriggerEvent)
        {
            unit.GetComponent<AoeObj>()?.TrigEvent(abilityAoeMonitorTriggerEvent);
        }
    }
}