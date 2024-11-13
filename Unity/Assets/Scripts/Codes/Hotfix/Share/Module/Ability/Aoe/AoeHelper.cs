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

            unit.AddOwnCaller(aoeUnit);
            aoeUnit.AddCaster(unit);

            EventSystem.Instance.Publish(unit.DomainScene(), new AbilityTriggerEventType.UnitOnCreate()
            {
                actionContext = actionContext,
                unit = unit,
                createUnit = aoeUnit,
            });

        }


        public static void EventHandler(Unit unit, AbilityConfig.AoeTriggerEvent abilityAoeMonitorTriggerEvent, ref ActionContext actionContext)
        {
            AoeObj aoeObj = unit.GetComponent<AoeObj>();
            if (aoeObj != null)
            {
                aoeObj.SetAoeActionContext(ref actionContext);
                aoeObj.TrigEvent(abilityAoeMonitorTriggerEvent, ref actionContext);
            }
        }
    }
}