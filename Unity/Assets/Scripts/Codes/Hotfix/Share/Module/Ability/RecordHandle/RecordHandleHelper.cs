using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class RecordHandleHelper
    {
        public static void DealRecordInt(Unit unit, ActionCfg_SetRecordInt actionCfgSetRecordInt, SelectHandle selectHandle, ActionContext actionContext)
        {
            List<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                DoRecordInt(list[i], actionCfgSetRecordInt);
            }
        }

        public static void DealRecordString(Unit unit, ActionCfg_SetRecordString actionCfgSetRecordString, SelectHandle selectHandle, ActionContext actionContext)
        {
            List<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                DoRecordString(list[i], actionCfgSetRecordString);
            }
        }

        public static void DoRecordInt(Unit unit, ActionCfg_SetRecordInt actionCfgSetRecordInt)
        {
            RecordKeyInt recordKey = actionCfgSetRecordInt.RecordKey;
            ValueOperation valueOperation = actionCfgSetRecordInt.ValueOperation;
            int recordValue = actionCfgSetRecordInt.RecordValue;

            RecordHandleObj recordHandleObj = unit.GetComponent<RecordHandleObj>();
            if (recordHandleObj == null)
            {
                recordHandleObj = unit.AddComponent<RecordHandleObj>();
            }
            recordHandleObj.DoRecordInt(recordKey, valueOperation, recordValue);
        }

        public static void DoRecordString(Unit unit, ActionCfg_SetRecordString actionCfgSetRecordString)
        {
            RecordKeyString recordKey = actionCfgSetRecordString.RecordKey;
            ValueOperation valueOperation = actionCfgSetRecordString.ValueOperation;
            string recordValue = actionCfgSetRecordString.RecordValue;

            RecordHandleObj recordHandleObj = unit.GetComponent<RecordHandleObj>();
            if (recordHandleObj == null)
            {
                recordHandleObj = unit.AddComponent<RecordHandleObj>();
            }
            recordHandleObj.DoRecordString(recordKey, valueOperation, recordValue);
        }

        public static int GetRecordInt(Unit unit, RecordKeyInt recordKey)
        {
            RecordHandleObj recordHandleObj = unit.GetComponent<RecordHandleObj>();
            if (recordHandleObj == null)
            {
                return 0;
            }
            return recordHandleObj.GetRecordInt(recordKey);
        }

        public static string GetRecordString(Unit unit, RecordKeyString recordKey)
        {
            RecordHandleObj recordHandleObj = unit.GetComponent<RecordHandleObj>();
            if (recordHandleObj == null)
            {
                return "";
            }
            return recordHandleObj.GetRecordString(recordKey);
        }

    }
}