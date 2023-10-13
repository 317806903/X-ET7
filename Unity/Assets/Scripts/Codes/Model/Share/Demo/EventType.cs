using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    namespace EventType
    {
        public struct SwitchLanguage
        {
            public LanguageType languageType;
        }

        public struct LoginSceneEnterStart
        {
            public bool isFromInit;
        }

        public struct HallSceneEnterStart
        {
            public bool isFromLogin;
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

        public struct LoginOutFinish
        {
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
            public Dictionary<string, int> myCoinListChg;
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct NoticeUnitBuffStatusChg
        {
            public Unit Unit;
        }

        public struct SyncPosUnits
        {
            public List<Unit> units;
        }

        public struct SyncNumericUnits
        {
            public List<Unit> units;
        }

        public struct SyncPlayAudio
        {
            public Unit unit;
            public string playAudioActionId;
        }

        public struct SyncPlayAnimator
        {
            public Unit unit;
        }

        public struct SyncUnitEffects
        {
            public Unit unit;
            public bool isAddEffect;
            public long effectObjId;
            public ET.Ability.EffectObj effectObj;
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

        public struct WaitNoticeGamePlayModeToClient
        {
            public long playerId;
            public GamePlayComponent gamePlayComponent;
        }

        public struct NoticeGamePlayToClient
        {
            public HashSet<long> playerIds;
            public GamePlayComponent gamePlayComponent;
        }

        public struct NoticeGamePlayPlayerListToClient
        {
            public HashSet<long> playerIds;
            public GetCoinType getCoinType;
            public GamePlayPlayerListComponent gamePlayPlayerListComponent;
        }

        public struct NoticeGamePlayModeToClient
        {
            public HashSet<long> playerIds;
            public GamePlayModeComponent gamePlayModeComponent;
        }

        public struct NoticeGameEndToRoom
        {
            public long roomId;
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

        public struct NoticeUITip
        {
            public string tipMsg;
        }

        public struct NoticeUIReconnect
        {
        }

    }
}