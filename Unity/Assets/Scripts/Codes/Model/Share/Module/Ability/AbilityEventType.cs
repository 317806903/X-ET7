namespace ET.Ability
{
    namespace AbilityEventType
    {
        public struct AddBuffToCaster
        {
            public long unitId;
            public string cfgId;
        }

        public struct CasterImmune
        {
            public long unitId;
            public string cfgId;
        }

        public struct CreateAoE
        {
            public long unitId;
            public string cfgId;
        }

        public struct CreateEffect
        {
            public long unitId;
            public string cfgId;
        }

        public struct FireBullet
        {
            public long unitId;
            public string cfgId;
        }

        public struct ForceMove
        {
            public long unitId;
            public string cfgId;
        }

        public struct PlayAnimator
        {
            public long unitId;
            public string cfgId;
        }

        public struct PlayAudio
        {
            public long unitId;
            public string cfgId;
        }

        public struct RemoveEffect
        {
            public long unitId;
            public string cfgId;
        }

        public struct SetCasterControlState
        {
            public long unitId;
            public string cfgId;
        }

        public struct SummonUnit
        {
            public long unitId;
            public string cfgId;
        }
    }
}