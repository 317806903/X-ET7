using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (NumericComponent))]
    public static class NumericComponentSystem
    {
        public static float GetAsFloat(this NumericComponent self, int numericType)
        {
            return (float)(self.GetByKey(numericType) * 0.0001f);
        }

        public static int GetAsInt(this NumericComponent self, int numericType)
        {
            return (int)(self.GetByKey(numericType) * 0.0001f + 0.0001f);
        }

        public static long GetAsLong(this NumericComponent self, int numericType)
        {
            return self.GetByKey(numericType);
        }

        public static void SetAsFloat(this NumericComponent self, int nt, float value)
        {
            self[nt] = (long)(value * 10000);
        }

        public static void SetAsInt(this NumericComponent self, int nt, int value)
        {
            self[nt] = (long)(value * 10000);
        }

        public static void SetAsLong(this NumericComponent self, int nt, long value)
        {
            self[nt] = value;
        }

        public static void SetNoEvent(this NumericComponent self, int numericType, long value, bool isNeedUpdate = true)
        {
            self.Insert(numericType, value, isNeedUpdate, false);
        }

        public static void Insert(this NumericComponent self, int numericType, long value, bool isNeedUpdate = true, bool isPublicEvent = true)
        {
            long oldValue = self.GetByKey(numericType);
            if (oldValue == value)
            {
                return;
            }

            self.NumericDic[numericType] = value;

            if (numericType >= NumericType.Max)
            {
                self.Update(numericType, isPublicEvent);
                return;
            }

            if (isPublicEvent)
            {
                Unit unit = self.GetParent<Unit>();
                if (unit != null)
                {
                    EventSystem.Instance.Publish(self.DomainScene(),
                        new EventType.NumbericChange() { Unit = unit, New = value, Old = oldValue, NumericType = numericType });
                }
            }
        }

        public static void SetAsFloatToBase(this NumericComponent self, int numericType, float valueNew)
        {
            if (numericType >= NumericType.Max)
            {
                return;
            }

            long value = (long)(valueNew * 10000);

            long oldValue = self.GetByKey(numericType);
            if (oldValue == value)
            {
                return;
            }

            self.UpdateBaseByFinal(numericType, value);

            Unit unit = self.GetParent<Unit>();
            if (unit != null)
            {
                EventSystem.Instance.Publish(self.DomainScene(),
                    new EventType.NumbericChange() { Unit = unit, New = value, Old = oldValue, NumericType = numericType });
            }
        }

        public static void KeepHpLessMaxHp(this NumericComponent self)
        {
            long maxHp = self.GetByKey(NumericType.MaxHp);
            if (maxHp <= 0)
            {
                self.SetAsFloatToBase(NumericType.MaxHp, 1);
                maxHp = self.GetByKey(NumericType.MaxHp);
            }
            long hp = self.GetByKey(NumericType.Hp);
            if (hp <= maxHp)
            {
                return;
            }

            self.SetAsFloatToBase(NumericType.Hp, maxHp * 0.0001f);
        }

        public static void SetHpFull(this NumericComponent self)
        {
            long maxHp = self.GetByKey(NumericType.MaxHp);
            long hp = self.GetByKey(NumericType.Hp);
            if (hp < maxHp)
            {
                self.SetAsFloatToBase(NumericType.Hp, maxHp * 0.0001f);
            }
        }

        public static long GetByKey(this NumericComponent self, int key)
        {
            long value = 0;
            self.NumericDic.TryGetValue(key, out value);
            return value;
        }

        public static void Update(this NumericComponent self, int numericType, bool isPublicEvent)
        {
            int final = (int)numericType / 10;
            int bas = final * 10 + 1;
            int add = final * 10 + 2;
            int pct = final * 10 + 3;
            int finalAdd = final * 10 + 4;
            int finalPct = final * 10 + 5;

            // 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
            // final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
            long result = (long)((((self.GetAsFloat(bas) + self.GetAsFloat(add)) * math.max(0, 100 + self.GetAsFloat(pct)) * 0.01f +
                    self.GetAsFloat(finalAdd)) *
                math.max(0, 100 + self.GetAsFloat(finalPct)) * 0.01f + 0.0001f) * 10000);
            self.Insert(final, result, false, isPublicEvent);
        }

        public static void UpdateBaseByFinal(this NumericComponent self, int numericType, long value)
        {
            int final = numericType;
            int bas = final * 10 + 1;
            int add = final * 10 + 2;
            int pct = final * 10 + 3;
            int finalAdd = final * 10 + 4;
            int finalPct = final * 10 + 5;

            long finalLong = self.GetByKey(final);
            long basLong = self.GetByKey(bas);
            if (basLong.Equals(finalLong))
            {
                self.NumericDic[bas] = value;
                self.NumericDic[final] = value;
                return;
            }
            else
            {
                self.NumericDic[final] = value;
                finalLong = self.GetByKey(final);
            }
            long addLong = self.GetByKey(add);
            long pctLong = self.GetByKey(pct);
            long finalAddLong = self.GetByKey(finalAdd);
            long finalPctLong = self.GetByKey(finalPct);

            float finalValue = self.GetAsFloat(final);
            float basValue = self.GetAsFloat(bas);
            float addValue = self.GetAsFloat(add);
            float pctValue = self.GetAsFloat(pct);
            float finalAddValue = self.GetAsFloat(finalAdd);
            float finalPctValue = self.GetAsFloat(finalPct);

            // final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
            // base = (final * 100 / (100 + finalPct) - finalAdd) * 100 / (100 + pct) - add
            var tmp = (finalValue * 100 / (100 + finalPctValue) - finalAddValue) * 100 / (100 + pctValue) - addValue;
            self.NumericDic[bas] = (long)(tmp * 10000);

            // // base = (final * 100 / (100 + finalPct) - finalAdd) * 100 / (100 + pct) - add
            // // base * 1w = (final * 1w * 100 / (100 + finalPct) - finalAdd * 1w) * 100 / (100 + pct) - add * 1w
            // var tmpLong = (finalLong * 100 / (100 + finalPctLong) - finalAddLong) * 100 / (100 + pctLong) - addLong;
            // self.NumericDic[bas] = tmpLong;

            // long result = (long)((((self.GetAsFloat(bas) + self.GetAsFloat(add)) * math.max(0, 100 + self.GetAsFloat(pct)) * 0.01f +
            //         self.GetAsFloat(finalAdd)) *
            //     math.max(0, 100 + self.GetAsFloat(finalPct)) * 0.01f + 0.0001f) * 10000);

        }
    }

    namespace EventType
    {
        public struct NumbericChange
        {
            public Unit Unit;
            public int NumericType;
            public long Old;
            public long New;
        }
    }

    [ComponentOf(typeof (Unit))]
    public class NumericComponent: Entity, IAwake, ITransfer, ITransferClient
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, long> NumericDic = new();

        public long this[int numericType]
        {
            get
            {
                return this.GetByKey(numericType);
            }
            set
            {
                this.Insert(numericType, value, true, true);
            }
        }
    }
}