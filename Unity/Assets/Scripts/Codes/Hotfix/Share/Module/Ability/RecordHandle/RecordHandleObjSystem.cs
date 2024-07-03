using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (RecordHandleObj))]
    public static class RecordHandleObjSystem
    {
        [ObjectSystem]
        public class RecordHandleObjAwakeSystem: AwakeSystem<RecordHandleObj>
        {
            protected override void Awake(RecordHandleObj self)
            {
            }
        }

        [ObjectSystem]
        public class RecordHandleObjDestroySystem: DestroySystem<RecordHandleObj>
        {
            protected override void Destroy(RecordHandleObj self)
            {
                self.RecordIntDic?.Clear();
                self.RecordStringDic?.Clear();
            }
        }

        public static void DoRecordInt(this RecordHandleObj self, RecordKeyInt recordKey, ValueOperation valueOperation, float recordValue)
        {
            if (self.RecordIntDic == null)
            {
                self.RecordIntDic = new();
            }

            if (self.RecordIntDic.TryGetValue(recordKey, out float orgValue) == false)
            {
                orgValue = 0;
            }

            float newValue = 0;
            if (valueOperation == ValueOperation.Add)
            {
                newValue = orgValue + recordValue;
            }
            else if (valueOperation == ValueOperation.Reduce)
            {
                newValue = orgValue - recordValue;
            }
            else if (valueOperation == ValueOperation.Set)
            {
                newValue = recordValue;
            }

            self.RecordIntDic[recordKey] = newValue;
        }

        public static void DoRecordString(this RecordHandleObj self, RecordKeyString recordKey, ValueOperation valueOperation, string recordValue)
        {
            if (self.RecordStringDic == null)
            {
                self.RecordStringDic = new();
            }

            if (valueOperation == ValueOperation.Set)
            {
                self.RecordStringDic[recordKey] = recordValue;
            }
            else
            {
                Log.Error($"recordKey[{recordKey.ToString()}] valueOperation[{valueOperation}] != ValueOperation.Set");
            }
        }

        public static float GetRecordInt(this RecordHandleObj self, RecordKeyInt recordKey)
        {
            if (self.RecordIntDic == null)
            {
                return 0;
            }

            if (self.RecordIntDic.TryGetValue(recordKey, out float orgValue) == false)
            {
                orgValue = 0;
            }

            return orgValue;
        }

        public static string GetRecordString(this RecordHandleObj self, RecordKeyString recordKey)
        {
            if (self.RecordStringDic == null)
            {
                return "";
            }

            if (self.RecordStringDic.TryGetValue(recordKey, out string orgValue) == false)
            {
                orgValue = "";
            }

            return orgValue;
        }
    }
}