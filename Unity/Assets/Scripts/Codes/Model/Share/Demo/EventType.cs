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

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }
    }
}