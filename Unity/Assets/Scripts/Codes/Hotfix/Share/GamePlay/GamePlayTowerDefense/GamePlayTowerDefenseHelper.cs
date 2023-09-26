using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof (GamePlayTowerDefenseComponent))]
    [FriendOf(typeof (Unit))]
    public static class GamePlayTowerDefenseHelper
    {
        public static Unit CreateHome(Scene scene, string unitCfgId, float3 pos, int hp)
        {
            float3 forward = new float3(0, 0, 1);

            Unit headQuarterUnit = UnitHelper_Create.CreateWhenServer_HomeUnit(scene, unitCfgId, pos, forward, "");

            NumericComponent numericComponent = headQuarterUnit.GetComponent<NumericComponent>();
            numericComponent.SetAsInt(NumericType.MaxHpBase, hp);
            numericComponent.SetAsInt(NumericType.HpBase, hp);

            //GamePlayHelper.AddUnitPathfinding(headQuarterUnit);
            GamePlayHelper.AddUnitTeamFlag(headQuarterUnit, TeamFlagType.TeamGlobal1);

            return headQuarterUnit;
        }

        public static Unit CreateMonsterCall(Scene scene, string unitCfgId, float3 pos)
        {
            float3 forward = new float3(0, 0, 1);
            Unit monsterCallUnit = UnitHelper_Create.CreateWhenServer_NPC(scene, unitCfgId, pos, forward);

            GamePlayHelper.AddUnitTeamFlag(monsterCallUnit, TeamFlagType.Monster);

            return monsterCallUnit;
        }

        public static Unit CreateMonster(Scene scene, string monsterCfgId, int level, float3 pos, float3 forward)
        {
            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            Unit monsterUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, monsterCfg.UnitId, level, pos, forward, monsterCfg.AiCfgId);

            GamePlayHelper.AddUnitPathfinding(monsterUnit);
            GamePlayHelper.AddUnitTeamFlag(monsterUnit, TeamFlagType.Monster);

            return monsterUnit;
        }

        public static Unit CreateTower(Scene scene, long playerId, string towerId, float3 pos)
        {
            float3 forward = new float3(0, 0, 1);

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerId);

            Unit towerUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, towerCfg.UnitId, towerCfg.Level, pos, forward, towerCfg.AiCfgId);

            TowerComponent towerComponent = towerUnit.AddComponent<TowerComponent>();
            towerComponent.towerCfgId = towerId;
            towerComponent.playerId = playerId;

            GamePlayHelper.AddUnitPathfinding(towerUnit);
            GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
            GamePlayHelper.AddUnitInfo(playerId, towerUnit);

            return towerUnit;
        }

    }
}