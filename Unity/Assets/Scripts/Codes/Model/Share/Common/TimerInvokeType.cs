﻿namespace ET
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
        public const int DlgFixedMenuFrameTimer = 225;
        public const int DlgFixedMenuHighestFrameTimer = 226;
        public const int GameModeArcadeTimer = 227;
        public const int DlgCommonChooseTimer = 228;
        public const int ARSessionComponentTimer = 229;
        public const int DlgTutorialsFrameTimer = 230;
        public const int DlgTutorialOneFrameTimer = 231;
        public const int DlgSkillDetailsFrameTimer = 233;
        public const int DlgBattleCameraPlayerSkillFrameTimer = 234;
        public const int DlgBattlePlayerSkillFrameTimer = 235;
        public const int EPage_BattleDeckTowerFrameTimer = 236;
        public const int EPage_BattleDeckSkillFrameTimer = 237;
        public const int DlgTowerDetailsFrameTimer = 238;
        public const int DlgBattleWaveTimer = 239;
        public const int DlgBattleCameraPlayerSkillMRFrameTimer = 240;
        public const int DlgBattlePlacementFrameTimer = 241;
        public const int DlgBattleTowerListFrameTimer = 242;
        public const int DlgDescTipsFrameTimer = 243;
    }
}