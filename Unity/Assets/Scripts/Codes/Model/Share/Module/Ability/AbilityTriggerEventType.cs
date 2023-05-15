namespace ET.Ability
{
    namespace AbilityTriggerEventType
    {
        /// <summary>
        /// 释放技能
        /// </summary>
        public struct SkillOnCast
        {
            public Unit unit;
            public int skillId;
            public TimelineObj timeline;
        }

        /// <summary>
        /// buff在创建但还未生效时候触发的事件
        /// </summary>
        public struct BuffOnAwake
        {
            public Unit unit;
            public BuffObj buff;
        }
        
        /// <summary>
        /// buff在创建且已生效时候触发的事件
        /// </summary>
        public struct BuffOnStart
        {
            public Unit unit;
            public BuffObj buff;
        }
        
        /// <summary>
        /// buff在被添加、改变层数、改变持续时间 时候触发的事件
        /// </summary>
        public struct BuffOnRefresh
        {
            public Unit unit;
            public BuffObj buff;
            public int modifyStack;
        }

        /// <summary>
        /// 在这个buffObj被移除之前要做的事情，如果运行之后buffObj又不足以被删除了就会被保留
        /// </summary>
        public struct BuffOnRemoved
        {
            public Unit unit;
            public BuffObj buff;
        }

        /// <summary>
        /// 在这个buffObj被移除之后要做的事情
        /// </summary>
        public struct BuffOnDestroy
        {
            public Unit unit;
            public BuffObj buff;
        }

        /// <summary>
        /// 在伤害流程中, 触发伤害之前要做的初步事件
        /// </summary>
        public struct DamageBeforeOnHit
        {
            public Unit attackerUnit;
            public Unit defenderUnit;
            public long damageInfoId;
        }
        
        /// <summary>
        /// 在伤害流程中, 触发伤害之前要做的最终事件
        /// </summary>
        public struct DamageAfterOnHit
        {
            public Unit attackerUnit;
            public Unit defenderUnit;
            public long damageInfoId;
        }
        
        /// <summary>
        /// 在伤害流程中, 击杀目标前触发
        /// </summary>
        public struct DamageBeforeOnKill
        {
            public Unit attackerUnit;
            public Unit defenderUnit;
            public long damageInfoId;
        }
        
        /// <summary>
        /// 在伤害流程中, 击杀目标后触发
        /// </summary>
        public struct DamageAfterOnKill
        {
            public Unit attackerUnit;
            public Unit defenderUnit;
            public long damageInfoId;
        }
        
        //=========================================================
        
        /// <summary>
        /// unit被创建的事件
        /// </summary>
        public struct UnitOnCreate
        {
            public Unit unit;
            public Unit createUnit;
        }
        
        /// <summary>
        /// 攻击触发的事件
        /// </summary>
        public struct UnitOnHit
        {
            public Unit attackerUnit;
            public Unit defenderUnit;
        }
        
        /// <summary>
        /// unit销毁的事件
        /// </summary>
        public struct UnitOnRemoved
        {
            public Unit unit;
        }
        
        /// <summary>
        ///子弹的轨迹函数，传入一个时间点，返回出一个Vector3，作为这个时间点的速度和方向，这是个相对于正在飞行的方向的一个偏移（*speed的）
        ///正在飞行的方向按照z轴，来算，也就是说，当你只需要子弹匀速行动的时候，你可以让这个函数只做一件事情——return Vector3.forward。
        /// </summary>
        public struct BulletTween
        {
            public Unit bulletUnit;
            public Unit targetUnit;
            public float time;
        }
        
        /// <summary>
        /// 子弹在发射瞬间，可以捕捉一个GameObject作为目标，并且将这个目标传递给BulletTween，作为移动参数
        /// </summary>
        public struct BulletTargettingFunction
        {
            public Unit bulletUnit;
            public Unit[] targetUnitIds;
        }
        
        //=========================================================

        /// <summary>
        /// 当有角色进入aoe范围的时候触发
        /// </summary>
        public struct AoeOnCharacterEnter
        {
            public Unit unit;
            public Unit[] targetUnits;
        }

        /// <summary>
        /// 当有角色离开aoe范围的时候
        /// </summary>
        public struct AoeOnCharacterLeave
        {
            public Unit unit;
            public Unit[] targetUnits;
        }

        /// <summary>
        /// 当有子弹进入aoe范围的时候
        /// </summary>
        public struct AoeOnBulletEnter
        {
            public Unit unit;
            public Unit[] targetUnits;
        }

        /// <summary>
        /// 当有子弹离开aoe范围的时候
        /// </summary>
        public struct AoeOnBulletLeave
        {
            public Unit unit;
            public Unit[] targetUnits;
        }

        /// <summary>
        /// aoe的移动轨迹函数
        /// </summary>
        public struct AoeTween
        {
            public Unit unit;
            public float time;
        }

    }
}