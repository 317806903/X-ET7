using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class ActionHandlerHelper
    {
        public static void CreateAction(Unit unit, Unit resetPosByUnit, string actionId, float delayTime, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (unit == null)
            {
#if UNITY_EDITOR
                Log.Error($"CreateAction unit == null");
#endif
                return;
            }
            unit.DomainScene().GetComponent<ActionHandlerComponent>().Run(unit, resetPosByUnit, actionId, delayTime, selectHandle, actionContext).Coroutine();
        }

        public static bool DoActionTriggerHandler(Unit triggerUnit, Unit actionUnit, float delayTime, string actionId, List<SequenceUnitCondition> actionCondition1, List<SequenceUnitCondition> actionCondition2, SelectHandle curSelectHandle, Unit resetPosByUnit, ref ActionContext actionContext)
        {
            if (curSelectHandle == null)
            {
#if UNITY_EDITOR
                Log.Error($"curSelectHandle == null");
#endif
                return false;
            }
            if (curSelectHandle.selectHandleType == SelectHandleType.SelectUnits && curSelectHandle.unitIds.Count == 0)
            {
#if UNITY_EDITOR
                //Log.Error($"curSelectHandle.selectHandleType == SelectHandleType.SelectUnits && curSelectHandle.unitIds.Count == 0");
#endif
                return false;
            }
            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = UnitConditionHandleHelper.ChkCondition(triggerUnit, curSelectHandle, actionCondition1, ref actionContext);
            if (isChgSelect1)
            {
                curSelectHandle = newSelectHandle1;
            }

            if (bRet1)
            {
                (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = UnitConditionHandleHelper.ChkCondition(triggerUnit, curSelectHandle, actionCondition2, ref actionContext);
                if (isChgSelect2)
                {
                    curSelectHandle = newSelectHandle2;
                }
                if (bRet1 && bRet2)
                {
                    ActionHandlerHelper.CreateAction(actionUnit, resetPosByUnit, actionId, delayTime, curSelectHandle, ref actionContext);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}