namespace ET
{
    public static class ConstValue
    {
        public static string RouterHttpHost = "127.0.0.1";
        public static int RouterHttpPort = 30300;
        public const int SessionTimeoutTime = 600 * 1000;
        public const int PingTime = 2 * 1000;
        public const int ReLoginChkTimeoutTime = PingTime + 1 * 1000;
        public const int ReCreateSessionTime = 5 * 1000;
    }
}