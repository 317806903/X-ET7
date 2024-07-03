using System.Collections.Generic;
using Unity.Mathematics;
using ET.AbilityConfig;
namespace ET.Ability
{
    public static class RecordHandleHelper
    {
        public static void DealRecordInt(Unit unit, ActionCfg_SetRecordInt actionCfgSetRecordInt, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                DoRecordInt(list[i], actionCfgSetRecordInt);
            }
            list.Dispose();
        }

        public static void DealRecordString(Unit unit, ActionCfg_SetRecordString actionCfgSetRecordString, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                DoRecordString(list[i], actionCfgSetRecordString);
            }
            list.Dispose();
        }

        public static void DoRecordInt(Unit unit, ActionCfg_SetRecordInt actionCfgSetRecordInt)
        {
            RecordKeyInt recordKey = actionCfgSetRecordInt.RecordKey;
            ValueOperation valueOperation = actionCfgSetRecordInt.ValueOperation;
            float recordValue = actionCfgSetRecordInt.RecordValue;

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

        public static float GetRecordInt(Unit unit, RecordKeyInt recordKey)
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