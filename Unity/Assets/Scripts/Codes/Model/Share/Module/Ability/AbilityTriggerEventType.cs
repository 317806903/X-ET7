﻿using Unity.Mathematics;

namespace ET.Ability
{
    namespace AbilityTriggerEventType
    {
        /// <summary>
        /// 释放技能
        /// </summary>
        public struct SkillOnCast
        {
            public ActionContext actionContext;
            public Unit unit;
        }

        /// <summary>
        /// 在伤害流程中, 触发伤害之前要做的初步事件
        /// </summary>
        public struct DamageBeforeOnHit
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        /// <summary>
        /// 在伤害流程中, 触发伤害之前要做的最终事件
        /// </summary>
        public struct DamageAfterOnHit
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        /// <summary>
        /// 在伤害流程中, 击杀目标前触发
        /// </summary>
        public struct DamageBeforeOnKill
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        /// <summary>
        /// 在伤害流程中, 击杀目标后触发
        /// </summary>
        public struct DamageAfterOnKill
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        //=========================================================

        /// <summary>
        /// 监听 unit切换保持的选择目标
        /// </summary>
        public struct UnitChgSaveSelectObj
        {
            public ActionContext actionContext;
            public Unit unit;
            public SelectHandle selectHandle;
        }

        /// <summary>
        /// unit被创建的事件
        /// </summary>
        public struct UnitOnCreate
        {
            public ActionContext actionContext;
            public Unit unit;
            public Unit createUnit;
        }

        /// <summary>
        /// 攻击触发的事件(这里只是判断碰撞到，并没有进入伤害流程)
        /// </summary>
        public struct BulletOnHit
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        /// <summary>
        /// 子弹碰到Mesh
        /// </summary>
        public struct BulletOnHitMesh
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
        }

        /// <summary>
        /// unit攻击到Mesh
        /// </summary>
        public struct UnitOnHitMesh
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
        }

        /// <summary>
        /// 子弹碰到特定位置
        /// </summary>
        public struct BulletOnHitPos
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
        }

        /// <summary>
        /// unit攻击到特定位置
        /// </summary>
        public struct UnitOnHitPos
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
        }

        /// <summary>
        /// 攻击触发的事件(这里只是判断碰撞到，并没有进入伤害流程)
        /// </summary>
        public struct UnitOnHit
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        /// <summary>
        /// unit销毁的事件
        /// </summary>
        public struct UnitOnRemoved
        {
            public ActionContext actionContext;
            public Unit unit;
        }

        /// <summary>
        ///子弹的轨迹函数，传入一个时间点，返回出一个Vector3，作为这个时间点的速度和方向，这是个相对于正在飞行的方向的一个偏移（*speed的）
        ///正在飞行的方向按照z轴，来算，也就是说，当你只需要子弹匀速行动的时候，你可以让这个函数只做一件事情——return Vector3.forward。
        /// </summary>
        public struct BulletTween
        {
            public ActionContext actionContext;
            public Unit bulletUnit;
            public Unit targetUnit;
            public float time;
        }

        /// <summary>
        /// 子弹在发射瞬间，可以捕捉一个GameObject作为目标，并且将这个目标传递给BulletTween，作为移动参数
        /// </summary>
        public struct BulletTargettingFunction
        {
            public ActionContext actionContext;
            public Unit bulletUnit;
            public Unit[] targetUnitIds;
        }

        //=========================================================

        /// <summary>
        /// 当有unit进入aoe范围的时候触发
        /// </summary>
        public struct AoeOnEnter
        {
            public ActionContext actionContext;
            public Unit unit;
            public Unit[] targetUnits;
        }

        /// <summary>
        /// 当有unit离开aoe范围的时候
        /// </summary>
        public struct AoeOnExist
        {
            public ActionContext actionContext;
            public Unit unit;
            public Unit[] targetUnits;
        }

        //=========================================================

        public struct NearUnitOnCreate
        {
            public ActionContext actionContext;
            public Unit unit;
        }

        public struct NearUnitOnHit
        {
            public ActionContext actionContext;
            public Unit attackerUnit;
            public Unit defenderUnit;
        }

        public struct NearUnitOnRemoved
        {
            public ActionContext actionContext;
            public Unit unit;
        }

        public struct GamePlayTowerDefense_PutTower
        {
            public ActionContext actionContext;
            public long playerId;
            public Unit towerUnit;
            public string towerCfgId;
        }

        public struct GamePlayTowerDefense_ScaleTower
        {
            public ActionContext actionContext;
            public long playerId;
            public Unit towerUnit;
            public string towerCfgId;
        }

        public struct GamePlayTowerDefense_ReclaimTower
        {
            public ActionContext actionContext;
            public long playerId;
            public Unit towerUnit;
            public string towerCfgId;
        }

        public struct GamePlayTowerDefense_UpgradeTower
        {
            public ActionContext actionContext;
            public long playerId;
            public Unit oldTowerUnit;
            public string oldTowerCfgId;
            public Unit newTowerUnit;
            public string newTowerCfgId;
        }

        public struct GamePlayTowerDefense_TowerBeKill
        {
            public ActionContext actionContext;
            public long playerId;
            public Unit towerUnit;
            public string towerCfgId;
            public Unit killer;
        }

        public struct GamePlayTowerDefense_TowerKillMonster
        {
            public ActionContext actionContext;
            public long playerId;
            public Unit towerUnit;
            public string towerCfgId;
            public Unit monsterUnit;
        }

        public struct GamePlayTowerDefense_RefreshTowerBuyPool
        {
            public ActionContext actionContext;
            public long playerId;
        }

        public struct GamePlay_Status_GameWaitForStart
        {
            public ActionContext actionContext;
        }

        public struct GamePlay_Status_GameStart
        {
            public ActionContext actionContext;
        }

        public struct GamePlay_Status_GameStartShow
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_PutHomeBegin
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_PutHomeEnd
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_PutMonsterPointBegin
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_PutMonsterPointEnd
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_ShowStartEffectBegin
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_ShowStartEffectEnd
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_RestTimeBegin
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_RestTimeEnd
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_InTheBattleBegin
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_Status_InTheBattleEnd
        {
            public ActionContext actionContext;
        }

        public struct GamePlay_Status_GameEnd
        {
            public ActionContext actionContext;
        }

        public struct GamePlayTowerDefense_AddRestoreEnergy
        {
            public ActionContext actionContext;
            public SkillComponent skillComponent;
            public SkillObj skillObj;
        }

        //=========================================================

    }
}