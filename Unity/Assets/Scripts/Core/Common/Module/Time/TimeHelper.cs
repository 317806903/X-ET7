using System;

namespace ET
{
    public static class TimeHelper
    {
        public const long OneDay = 86400000;
        public const long Hour = 3600000;
        public const long Minute = 60000;

        public const int LogicFrame = 20;
        [StaticField]
        public static float FixedDetalTime = (1f / LogicFrame);
        [StaticField]
        public static long FixedDetalTimeLong = (long)(1000 * FixedDetalTime);

        /// <summary>
        /// 客户端时间
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return TimeInfo.Instance.ClientNow();
        }

        public static long ClientNowSeconds()
        {
            return ClientNow() / 1000;
        }

        public static DateTime DateTimeNow(bool isUtc = true, bool isZone = false, bool isLocal = false)
        {
            if (isUtc)
            {
                return DateTime.UtcNow;
            }
            if (isZone)
            {
                return DateTime.UtcNow;
            }
            if (isLocal)
            {
                return DateTime.Now;
            }
            return DateTime.UtcNow;
        }

        public static long ServerNow()
        {
            return TimeInfo.Instance.ServerNow();
        }

        public static long ClientFrameTime()
        {
            return TimeInfo.Instance.ClientFrameTime();
        }

        public static long ServerFrameTime()
        {
            return TimeInfo.Instance.ServerFrameTime();
        }

        public static DateTime ToDateTime(long timeStamp, bool isUtc = true, bool isZone = false, bool isLocal = false)
        {
            timeStamp = ChgToMillisecondTimeStamp(timeStamp);
            return TimeInfo.Instance.ToDateTime(timeStamp, isUtc, isZone, isLocal);
        }

        public static long ToTimeStamp(DateTime dateTime, bool isUtc = true, bool isZone = false, bool isLocal = false)
        {
            return TimeInfo.Instance.Transition(dateTime, isUtc, isZone, isLocal);
        }

        public static bool ChkIsAfter(long timeStampA, long timeStampB)
        {
            timeStampA = ChgToMillisecondTimeStamp(timeStampA);
            timeStampB = ChgToMillisecondTimeStamp(timeStampB);
            return timeStampA > timeStampB;
        }

        public static long ChgToMillisecondTimeStamp(long timeStamp)
        {
            if (timeStamp < 1000000000000)
            {
                timeStamp *= 1000;
            }
            return timeStamp;
        }
    }
}