using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    namespace EventType
    {
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

        public struct SyncHealthBar
        {
            public List<Unit> list;
        }

        public struct SyncPlayAudio
        {
            public List<(Unit unit, string playAudioActionId, bool isOnlySelfShow)> list;
        }

        public struct SyncFloatingText
        {
            public List<(Unit unit, string floatingTextActionId, int showNum, bool isOnlySelfShow)> list;
        }

        public struct SyncGetCoinShow
        {
            public List<(Unit unit, CoinTypeInGame coinType, int chgValue)> list;
        }

        public struct SyncDamageShow
        {
            public List<(Unit unit, int damageValue, bool isCrt)> list;
        }

        public struct SendDrawPathsToClients
        {
            public M2C_DrawAllMonsterCall2HeadQuarterPath pathToDraw;
        }

        public struct NoticeGameWaitForStart2Server
        {
            public GamePlayComponent gamePlayComponent;
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
            public bool forceSend;
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
            public GamePlayModeComponentBase GamePlayModeComponentBase;
            public bool needSendSuccess;
        }

        public struct NeedReNoticeTowerDefense
        {
        }

        public struct NoticeGameEndToRoom
        {
            public long roomId;
            public long playerId;
            public bool isReady;
            public Dictionary<long, bool> playerWinResult;
        }

        public struct NoticeGameBattleRemovePlayer
        {
            public long playerId;
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

    }
}