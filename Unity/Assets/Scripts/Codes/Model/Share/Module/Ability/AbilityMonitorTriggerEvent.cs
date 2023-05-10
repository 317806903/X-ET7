namespace ET.Ability
{
    public enum AbilityBuffMonitorTriggerEvent
    {
        /// <summary>
        /// 监听 释放技能
        /// </summary>
        SkillOnCast,

        /// <summary>
        /// 监听 buff在创建但还未生效时候触发的事件
        /// </summary>
        BuffOnAwake,
        
        /// <summary>
        /// 监听 buff在创建且已生效时候触发的事件
        /// </summary>
        BuffOnStart,
        
        /// <summary>
        /// 监听 buff在被添加、改变层数、改变持续时间 时候触发的事件
        /// </summary>
        BuffOnRefresh,
        
        /// <summary>
        /// 监听 buff在每个工作周期会执行的函数，如果这个函数为空，或者tickTime<=0，都不会发生周期性工作
        /// </summary>
        BuffOnTick,
        
        /// <summary>
        /// 监听 在这个buffObj被移除之前要做的事情，如果运行之后buffObj又不足以被删除了就会被保留
        /// </summary>
        BuffOnRemoved,
        
        /// <summary>
        /// 监听 在这个buffObj被移除之后要做的事情
        /// </summary>
        BuffOnDestroy,
        
        /// <summary>
        /// 监听 在伤害流程中，触发伤害之前要做的初步事件, 持有这个buff的人作为攻击者会发生的事情
        /// </summary>
        DamageBeforeOnHit,
        
        /// <summary>
        /// 监听 在伤害流程中，触发伤害之前要做的初步事件, 持有这个buff的人作为挨打者会发生的事情
        /// </summary>
        DamageBeforeOnBeHurt,
        
        /// <summary>
        /// 监听 在伤害流程中，触发伤害之前要做的最终事件, 持有这个buff的人作为攻击者会发生的事情
        /// </summary>
        DamageAfterOnHit,
        
        /// <summary>
        /// 监听 在伤害流程中，触发伤害之前要做的最终事件, 持有这个buff的人作为挨打者会发生的事情
        /// </summary>
        DamageAfterOnBeHurt,
        
        /// <summary>
        /// 监听 在伤害流程中，击杀目标前触发，如果击杀目标，则会触发的啥事情
        /// </summary>
        DamageBeforeOnKill,
        
        /// <summary>
        /// 监听 在伤害流程中，击杀目标前触发，持有这个buff的人被杀死了，会触发的事情
        /// </summary>
        DamageBeforeOnBeKilled,
        
        /// <summary>
        /// 监听 在伤害流程中，击杀目标后触发
        /// </summary>
        DamageAfterOnKill,
        
        /// <summary>
        /// 监听 在伤害流程中，击杀目标后触发
        /// </summary>
        DamageAfterOnBeKilled,
        
        /// <summary>
        /// 监听 unit被创建的事件
        /// </summary>
        UnitOnCreate,
        
        /// <summary>
        /// 监听 unit产生攻击的事件
        /// </summary>
        UnitOnHit,
        
        /// <summary>
        /// 监听 unit被攻击的事件
        /// </summary>
        UnitOnBeHurt,
        
        /// <summary>
        /// 监听 unit销毁的事件
        /// </summary>
        UnitOnRemoved,
    }
    
    public enum AbilityBulletMonitorTriggerEvent
    {
        /// <summary>
        /// 监听 子弹被创建的事件
        /// </summary>
        BulletOnCreate,

        /// <summary>
        /// 监听 子弹产生攻击的事件
        /// </summary>
        BulletOnHit,
        
        /// <summary>
        /// 监听 子弹被攻击的事件
        /// </summary>
        BulletOnBeHurt,

        /// <summary>
        /// 监听 子弹被销毁的事件
        /// </summary>
        BulletOnRemoved,
    }
    
    public enum AbilityAoeMonitorTriggerEvent
    {
        /// <summary>
        /// 监听 Aoe被创建的事件
        /// </summary>
        AoeOnCreate,
        
        /// <summary>
        /// 监听 Aoe被销毁的事件
        /// </summary>
        AoeOnRemoved,

        /// <summary>
        /// 监听 在每个工作周期会执行的函数，如果这个函数为空，或者tickTime<=0，都不会发生周期性工作
        /// </summary>
        AoeOnTick,

        /// <summary>
        /// 监听 当有角色进入aoe范围的时候触发
        /// </summary>
        AoeOnCharacterEnter,
        
        /// <summary>
        /// 监听 当有角色离开aoe范围的时候
        /// </summary>
        AoeOnCharacterLeave,

        /// <summary>
        /// 监听 当有子弹进入aoe范围的时候
        /// </summary>
        AoeOnBulletEnter,
        
        /// <summary>
        /// 监听 当有子弹离开aoe范围的时候
        /// </summary>
        AoeOnBulletLeave,
    }
}