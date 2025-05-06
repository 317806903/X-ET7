using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class FloatingTextHelper
    {
        public static void AddFloatingText(Unit unit, string floatingTextId, int showNum, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (string.IsNullOrEmpty(floatingTextId))
            {
                return;
            }

            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }

            ActionCfg_FloatingText actionCfg_FloatingText = ActionCfg_FloatingTextCategory.Instance.Get(floatingTextId);
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                Ability.UnitHelper.AddSyncData_UnitFloatingText(targetUnit, floatingTextId, showNum, actionCfg_FloatingText.IsOnlySelfShow);
            }
            list.Dispose();
        }

    }
}