using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    namespace ClientEventType
    {
        public struct SwitchLanguage
        {
            public LanguageType languageType;
        }

        public struct EnterLoginSceneStart
        {
            public bool isFromInit;
        }

        public struct EnterHallSceneStart
        {
            public bool isFromLogin;
            public bool isRelogin;
        }

        public struct BattleSceneEnterStart
        {
        }

        public struct BattleSceneEnterFinish
        {
        }

        public struct AfterCreateClientScene
        {
        }

        public struct AfterCreateCurrentScene
        {
        }

        public struct LoginFinish
        {
        }

        public struct ReLoginFinish
        {
        }

        public struct LoginOutFinish
        {
            public bool isNeedLoginOutAccount;
        }

        public struct EnterMapFinish
        {
        }

        public struct RoomInfoChg
        {
        }

        public struct BattleCfgIdChoose
        {
            public string gamePlayBattleLevelCfgId;
        }

        public struct BeKickedRoom
        {
        }

        public struct GamePlayChg
        {
        }

        public struct GamePlayCoinChg
        {
            public GetCoinType getCoinType;
            public Dictionary<string, float> myCoinListChg;
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct NoticeGuideConditionStatus
        {
            public string guideConditionStaticMethodType;
        }

        public struct NoticeRedrawAllPaths
        {
            public M2C_DrawAllMonsterCall2HeadQuarterPath pathToDraw;
        }

        public struct NoticeAdvertStatusChg
        {
            public bool IsAvailable;
        }

        public struct ShowAdvert
        {
            public System.Action rewardCallback;
            public System.Action failCallback;
        }

        public struct NoticeUITip
        {
            public string tipMsg;
        }

        public struct NoticeNetDisconnected
        {
            public bool bReLogin;
        }

        public struct NoticeApplicationStatus
        {
            public bool isPause;
        }

        public struct NoticeUIReconnect
        {
        }

        public struct NoticeUILoginInAtOtherWhere
        {
        }

        public struct NoticeUIShowCommonLoading
        {
            public bool bForce;
        }

        public struct NoticeUIHideCommonLoading
        {
            public bool bForce;
        }

        public struct NoticeEventLoggingLoginIn
        {
            public long playerId;
        }

        public struct NoticeEventLoggingStart
        {
            public string eventName;
            public string timerKey;
        }

        public struct TutorialCompleted
        {
            public bool isTutorialCompleted;
            public string tutorialId;
            public string tutorialName;
        }

        public struct NoticeEventLogging
        {
            public string eventName;
            public Dictionary<string, object> properties;
            public string timerKey;
        }

        public struct NoticeEventLoggingSetCommonProperties
        {
            public Dictionary<string, object> properties;
        }

        public struct NoticeEventLoggingSetUserProperties
        {
            public Dictionary<string, object> properties;
        }

        public struct NoticePlayerStatusChg
        {
        }

        public struct NoticePlayerCacheChg
        {
            public PlayerModelType playerModelType;
        }

        public struct NoticeUISeasonIndexChg
        {
        }

        public struct NoticeUISeasonRemainChg
        {
        }

        public struct NoticeShowBattleNotice
        {
            public string tutorialCfgId;
        }

        public struct NoticeGamePlayTowerDefenseStatusWhenClient
        {
            public GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus;
        }

        public struct NoticeGamePlayPKStatusWhenClient
        {
        }

        public struct NoticeResetSpatialAnchor
        {
            public float3 position;
            public quaternion rotation;
        }

        public struct NoticeResetOVRTrackingSpaceTrans
        {
        }

    }
}