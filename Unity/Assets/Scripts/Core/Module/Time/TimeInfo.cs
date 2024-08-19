using System;

namespace ET
{
    public class TimeInfo: Singleton<TimeInfo>, ISingletonUpdate
    {
        private int timeZone;

        public int TimeZone
        {
            get
            {
                return this.timeZone;
            }
            set
            {
                this.timeZone = value;
                dt = dt1970.AddHours(TimeZone);
            }
        }

        private DateTime dt1970;
        private DateTime dt;
        private DateTime dtLocal;

        public long ServerMinusClientTime { private get; set; }

        public long FrameTime;

        public TimeInfo()
        {
            this.dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            this.dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            this.dtLocal = this.dt1970.ToLocalTime();
            this.FrameTime = this.ClientNow();
        }

        public void Update()
        {
            this.FrameTime = this.ClientNow();
        }

        /// <summary>
        /// 根据时间戳获取时间 , 单位 毫秒
        /// </summary>
        public DateTime ToDateTime(long timeStamp, bool isUtc = true, bool isZone = false, bool isLocal = false)
        {
            if (isUtc)
            {
                return this.dt1970.AddTicks(timeStamp * 10000);
            }
            if (isZone)
            {
                return this.dt.AddTicks(timeStamp * 10000);
            }
            if (isLocal)
            {
                return this.dtLocal.AddTicks(timeStamp * 10000);
            }

            return default;
        }

        // 线程安全, 单位 毫秒
        public long ClientNow()
        {
            return (DateTime.UtcNow.Ticks - this.dt1970.Ticks) / 10000;
        }

        // 单位 毫秒
        public long ServerNow()
        {
            return ClientNow() + Instance.ServerMinusClientTime;
        }

        // 单位 毫秒
        public long ClientFrameTime()
        {
            return this.FrameTime;
        }

        // 单位 毫秒
        public long ServerFrameTime()
        {
            return this.FrameTime + Instance.ServerMinusClientTime;
        }

        // 单位 毫秒
        public long Transition(DateTime d, bool isUtc = true, bool isZone = false, bool isLocal = false)
        {
            if (isUtc)
            {
                return (d.Ticks - this.dt1970.Ticks) / 10000;
            }
            if (isZone)
            {
                return (d.Ticks - dt.Ticks) / 10000;
            }
            if (isLocal)
            {
                return (d.Ticks - this.dtLocal.Ticks) / 10000;
            }
            return 0;
        }
    }
}