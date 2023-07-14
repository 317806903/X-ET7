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
            
            string pathfindingMapName = ET.GamePlayHelper.GetPathfindingMapName(scene);
            Unit headQuarterUnit = UnitHelper_Create.CreateWhenServer_HomeUnit(scene, unitCfgId, pos, forward, "", pathfindingMapName);
            
            NumericComponent numericComponent = headQuarterUnit.GetComponent<NumericComponent>();
            numericComponent.Set(NumericType.MaxHp, hp);
            numericComponent.Set(NumericType.Hp, hp);
            
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
            string pathfindingMapName = ET.GamePlayHelper.GetPathfindingMapName(scene);
            Unit monsterUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, monsterCfg.UnitId, level, pos, forward, monsterCfg.AiCfgId, pathfindingMapName);

            GamePlayHelper.AddUnitTeamFlag(monsterUnit, TeamFlagType.Monster);

            return monsterUnit;
        }

        public static Unit CreateTower(Scene scene, long playerId, string towerId, float3 pos)
        {
            float3 forward = new float3(0, 0, 1);

            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerId);
            string pathfindingMapName = ET.GamePlayHelper.GetPathfindingMapName(scene);
            Unit towerUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, towerCfg.UnitId, towerCfg.Level, pos, forward, towerCfg.AiCfgId, pathfindingMapName);

            GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
            GamePlayHelper.AddUnitInfo(playerId, towerUnit);

            return towerUnit;
        }

        public static Unit CreatePlayerObserverUnit(Scene scene, long playerId, float3 position, float3 forward)
        {
            string unitCfgId = "Unit_Observer";
            int level = 1;
            Unit unit = UnitHelper_Create.CreateWhenServer_CommonPlayerUnit(scene, playerId, unitCfgId, level, UnitType.ObserverUnit, position,
                forward);

            GamePlayHelper.AddUnitInfo(playerId, unit);

            return unit;
        }

    }
}