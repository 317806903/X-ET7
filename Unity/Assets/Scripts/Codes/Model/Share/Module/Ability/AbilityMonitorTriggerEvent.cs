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
        BuffOnTick1,
        BuffOnTick2,
        BuffOnTick3,

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
        /// 监听 unit切换保持的选择目标
        /// </summary>
        UnitChgSaveSelectObj,

        /// <summary>
        /// 监听 bullet攻击到Mesh
        /// </summary>
        BulletOnHitMesh,

        /// <summary>
        /// 监听 unit攻击到Mesh
        /// </summary>
        UnitOnHitMesh,

        /// <summary>
        /// 监听 bullet攻击到特定位置
        /// </summary>
        BulletOnHitPos,

        /// <summary>
        /// 监听 unit攻击到特定位置
        /// </summary>
        UnitOnHitPos,

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

        /// <summary>
        /// 监听 附近unit被创建的事件
        /// </summary>
        NearUnitOnCreate,
        /// <summary>
        /// 监听 附近unit产生攻击的事件
        /// </summary>
        NearUnitOnHit,
        /// <summary>
        /// 监听 附近unit销毁的事件
        /// </summary>
        NearUnitOnRemoved,
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
        /// 监听 子弹达到Mesh的事件
        /// </summary>
        BulletOnHitMesh,

        /// <summary>
        /// 监听 子弹达到特定位置的事件
        /// </summary>
        BulletOnHitPos,

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
        AoeOnTick1,
        AoeOnTick2,
        AoeOnTick3,

        /// <summary>
        /// 监听 当有unit进入aoe范围的时候
        /// </summary>
        AoeOnEnter,

        /// <summary>
        /// 监听 当有unit离开aoe范围的时候
        /// </summary>
        AoeOnExist,
    }

    public enum AbilityGameMonitorTriggerEvent
    {
        /// <summary>
        /// 监听 GlobalBuff在每个工作周期会执行的函数，如果这个函数为空，或者tickTime<=0，都不会发生周期性工作
        /// </summary>
        GlobalBuffOnTick1,
        GlobalBuffOnTick2,
        GlobalBuffOnTick3,

        /// <summary>
        /// 监听 附近unit被创建的事件
        /// </summary>
        NearUnitOnCreate,

        /// <summary>
        /// 监听 附近unit产生攻击的事件
        /// </summary>
        NearUnitOnHit,

        /// <summary>
        /// 监听 附近unit销毁的事件
        /// </summary>
        NearUnitOnRemoved,

        /// <summary>
        /// 监听 放置塔的事件
        /// </summary>
        GamePlayTowerDefense_PutTower,

        /// <summary>
        /// 监听 出售塔的事件
        /// </summary>
        GamePlayTowerDefense_ScaleTower,

        /// <summary>
        /// 监听 回收塔的事件
        /// </summary>
        GamePlayTowerDefense_ReclaimTower,

        /// <summary>
        /// 监听 升级塔的事件
        /// </summary>
        GamePlayTowerDefense_UpgradeTower,

        /// <summary>
        /// 监听 塔击杀monster的事件
        /// </summary>
        GamePlayTowerDefense_TowerKillMonster,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_PutHome 的事件
        /// </summary>
        GamePlayTowerDefense_Status_PutHomeBegin,
        GamePlayTowerDefense_Status_PutHomeEnd,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_PutMonsterPoint 的事件
        /// </summary>
        GamePlayTowerDefense_Status_PutMonsterPointBegin,
        GamePlayTowerDefense_Status_PutMonsterPointEnd,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_ShowStartEffect 的事件
        /// </summary>
        GamePlayTowerDefense_Status_ShowStartEffectBegin,
        GamePlayTowerDefense_Status_ShowStartEffectEnd,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_RestTime 的事件
        /// </summary>
        GamePlayTowerDefense_Status_RestTimeBegin,
        GamePlayTowerDefense_Status_RestTimeEnd,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_InTheBattle 的事件
        /// </summary>
        GamePlayTowerDefense_Status_InTheBattleBegin,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_InTheBattleEnd 的事件
        /// </summary>
        GamePlayTowerDefense_Status_InTheBattleEnd,

        /// <summary>
        /// 监听  GamePlayTowerDefense_Status_GameEnd 的事件
        /// </summary>
        GamePlayTowerDefense_Status_GameEnd,

    }
}