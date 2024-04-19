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

        // 框架层100-200，逻辑层的timer type 200-300
        public const int DynamicMapChecker = 201;
        public const int RoomGetDynamicMapCountChecker = 202;
        public const int ReamGetGatePlayerCountChecker = 203;
        public const int MoveTimer = 204;
        public const int AITimer = 205;
        public const int SessionAcceptTimeout = 206;
        public const int BattleFrameTimer = 207;
        public const int BattleTowerFrameTimer = 208;
        public const int BattleTowerARFrameTimer = 209;
        public const int GamePlayChkMonsterWaveCallAllClear = 210;
        public const int HallTimer = 211;
        public const int LoadingTimer = 212;
        public const int DataCacheClearChkTimer = 213;
        public const int DataCacheWriteChkTimer = 214;
        public const int BattleDragItemFrameTimer = 215;
        public const int BattleTowerHUDShowFrameTimer = 216;
        public const int PhysicalStrengthTimer = 217;
        public const int GameModeARTimer = 218;
        public const int LoginTimer = 219;
        public const int UIAudioManagerTimer = 220;
        public const int DlgARSceneSliderFrameTimer = 221;
        public const int DlgARSceneSliderSimpleFrameTimer = 222;
        public const int DlgVideoShowFrameTimer = 223;
        public const int DlgBattleDeckFrameTimer = 224;
        public const int DlgFixedMenuFrameTimer = 225;
        public const int DlgFixedMenuHighestFrameTimer = 226;
    }
}