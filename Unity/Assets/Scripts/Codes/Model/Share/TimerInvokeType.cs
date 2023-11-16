namespace ET
{
    [UniqueId(100, 10000)]
    public static class TimerInvokeType
    {
        // 框架层100-200，逻辑层的timer type从200起
        public const int WaitTimer = 100;
        public const int SessionIdleChecker = 101;
        public const int ActorLocationSenderChecker = 102;
        public const int ActorMessageSenderChecker = 103;
        public const int DynamicMapChecker = 104;

        // 框架层100-200，逻辑层的timer type 200-300
        public const int MoveTimer = 201;
        public const int AITimer = 202;
        public const int SessionAcceptTimeout = 203;
        public const int BattleFrameTimer = 204;
        public const int BattleTowerFrameTimer = 205;
        public const int BattleTowerARFrameTimer = 206;
        public const int GamePlayChkMonsterWaveCallAllClear = 207;
        public const int HallTimer = 209;
        public const int LoadingTimer = 210;
        public const int DataCacheClearChkTimer = 211;
        public const int DataCacheWriteChkTimer = 212;
        public const int BattleDragItemFrameTimer = 213;
    }
}