using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    namespace EventType
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

        public struct AppStartInitFinish
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

        public struct NoticeUnitBuffStatusChg
        {
            public Unit Unit;
        }

        public struct SyncNoticeUnitAdds
        {
            public Unit beNoticeUnit;
            public List<Unit> units;
        }

        public struct SyncNoticeUnitRemoves
        {
            public Unit beNoticeUnit;
            public List<long> unitIds;
        }

        public struct SyncDataList
        {
            public long playerId;
            public List<byte[]> syncDataList;
        }

        public struct SyncPlayAudio
        {
            public Unit unit;
            public string playAudioActionId;
            public bool isOnlySelfShow;
        }

        public struct SyncGetCoinShow
        {
            public long playerId;
            public Unit unit;
            public CoinType coinType;
            public int chgValue;
        }

        public struct SyncUnitEffects
        {
            public Unit unit;
            public bool isAddEffect;
            public long effectObjId;
            public ET.Ability.EffectObj effectObj;
            public bool isOnlySelfShow;
        }

        public struct NoticeGameBegin2Server
        {
            public GamePlayComponent gamePlayComponent;
        }

        public struct NoticeGameEnd2Server
        {
            public GamePlayComponent gamePlayComponent;
        }

        public struct WaitNoticeGamePlayToClient
        {
            public long playerId;
            public GamePlayComponent gamePlayComponent;
        }

        public struct WaitNoticeGamePlayPlayerListToClient
        {
            public long playerId;
            public GetCoinType getCoinType;
            public GamePlayPlayerListComponent gamePlayPlayerListComponent;
        }

        public struct WaitNoticeGamePlayStatisticalToClient
        {
            public long playerId;
            public GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent;
        }

        public struct WaitNoticeGamePlayModeToClient
        {
            public long playerId;
            public GamePlayComponent gamePlayComponent;
        }

        public struct NoticeGamePlayToClient
        {
            public HashSet<long> playerIds;
            public GamePlayComponent gamePlayComponent;
            public bool needSendSuccess;
        }

        public struct NoticeGamePlayPlayerListToClient
        {
            public HashSet<long> playerIds;
            public GetCoinType getCoinType;
            public GamePlayPlayerListComponent gamePlayPlayerListComponent;
            public bool needSendSuccess;
        }

        public struct NoticeGamePlayStatisticalToClient
        {
            public long playerId;
            public GamePlayStatisticalDataComponent gamePlayStatisticalDataComponent;
        }

        public struct NoticeGamePlayModeToClient
        {
            public HashSet<long> playerIds;
            public GamePlayModeComponent gamePlayModeComponent;
            public bool needSendSuccess;
        }

        public struct NoticeGameEndToRoom
        {
            public long roomId;
            public bool isReady;
            public Dictionary<long, bool> playerWinResult;
        }

        public struct StopMove
        {
            public Unit unit;
            public int error;
        }

        public struct MovePointList
        {
            public Unit unit;
            public List<float3> pointList;
        }

        public struct NoticeAdmobSDKStatus
        {
            public bool IsAdmobAvailable;
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
        }

        public struct NoticeUIHideCommonLoading
        {
            public bool bForceHide;
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
    }
}