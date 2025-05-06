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

        public static bool ChkActionCondition(Unit triggerUnit, List<SequenceUnitCondition> chkCondition1, List<SequenceUnitCondition> chkCondition2, SelectObjectCfg chkCondition1SelectObj, SelectObjectCfg chkCondition2SelectObj, ref ActionContext actionContext)
        {
            bool chkRet1 = _ChkActionCondition(triggerUnit, chkCondition1, chkCondition1SelectObj, ref actionContext);
            bool chkRet2 = _ChkActionCondition(triggerUnit, chkCondition2, chkCondition2SelectObj, ref actionContext);
            return chkRet1 && chkRet2;
        }

        public static bool _ChkActionCondition(Unit triggerUnit, List<SequenceUnitCondition> chkCondition, SelectObjectCfg chkConditionSelectObj, ref ActionContext actionContext)
        {
            if (chkConditionSelectObj == null || chkCondition == null || chkCondition.Count == 0)
            {
                return true;
            }

            SelectHandle chkCondition1SelectHandle = SelectHandleHelper.CreateSelectHandle(triggerUnit, null, chkConditionSelectObj, ref actionContext);
            if (chkCondition1SelectHandle == null)
            {
                return false;
            }
            if (chkCondition1SelectHandle.selectHandleType == SelectHandleType.SelectUnits && chkCondition1SelectHandle.unitIds.Count == 0)
            {
                return false;
            }

            bool bRet = UnitConditionHandleHelper.ChkConditionWhenChk(triggerUnit, chkCondition1SelectHandle, chkCondition, ref actionContext);

            if (bRet == false)
            {
                return false;
            }

            return true;
        }

        public static bool DoActionTriggerHandler(Unit triggerUnit, Unit actionUnit, List<float> delayTimeList, List<string> actionIdList,
        List<SequenceUnitCondition> filterCondition1, List<SequenceUnitCondition> filterCondition2, SelectHandle curSelectHandle, Unit resetPosByUnit,
        ref ActionContext actionContext)
        {
            bool bRetAll = true;
            float delayTime = 0;
            for (int i = 0; i < actionIdList.Count; i++)
            {
                string actionId = actionIdList[i];
                if (delayTimeList.Count > i)
                {
                    delayTime = delayTimeList[i];
                }
                bool bRet = DoActionTriggerHandler(triggerUnit, actionUnit, delayTime, actionId, filterCondition1, filterCondition2, curSelectHandle, resetPosByUnit, ref actionContext);
                if (bRet == false)
                {
                    bRetAll = false;
                }
            }
            return bRetAll;
        }

        public static bool DoActionTriggerHandler(Unit triggerUnit, Unit actionUnit, float delayTime, string actionId, List<SequenceUnitCondition> filterCondition1, List<SequenceUnitCondition> filterCondition2, SelectHandle curSelectHandle, Unit resetPosByUnit, ref ActionContext actionContext)
        {
            if (curSelectHandle == null)
            {
#if UNITY_EDITOR
                //Log.Error($"curSelectHandle == null");
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
            (bool bRet1, bool isChgSelect1, SelectHandle newSelectHandle1) = UnitConditionHandleHelper.ChkConditionWhenFilter(triggerUnit, curSelectHandle, filterCondition1, ref actionContext);
            if (isChgSelect1)
            {
                curSelectHandle = newSelectHandle1;
            }

            if (bRet1)
            {
                (bool bRet2, bool isChgSelect2, SelectHandle newSelectHandle2) = UnitConditionHandleHelper.ChkConditionWhenFilter(triggerUnit, curSelectHandle, filterCondition2, ref actionContext);
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