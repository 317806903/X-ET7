using System.Collections.Generic;

namespace ET
{
    namespace EventType
    {
        public struct HallSceneEnterStart
        {
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

        public struct BeKickedRoom
        {
        }

        public struct GamePlayChg
        {
        }

        public struct GamePlayCoinChg
        {
        }

        public struct AfterUnitCreate
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
    
        public struct SyncUnitEffects
        {
            public Unit unit;
            public bool isAddEffect;
            public long effectObjId;
            public ET.Ability.EffectObj effectObj;
        }
        
        public struct NoticeGamePlayToClient
        {
            public long playerId;
            public GamePlayComponent gamePlayComponent;
        }
        
        public struct NoticeGamePlayPlayerListToClient
        {
            public long playerId;
            public GamePlayPlayerListComponent gamePlayPlayerListComponent;
        }
        
        public struct NoticeGamePlayModeToClient
        {
            public long playerId;
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

    }
}