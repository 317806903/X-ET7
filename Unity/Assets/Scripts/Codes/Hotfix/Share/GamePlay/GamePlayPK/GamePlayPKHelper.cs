using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPKComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayPKHelper
	{
		public static Unit CreateMonster(Scene scene, string monsterCfgId, int level, float3 pos, float3 forward)
		{
			TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
			Unit monsterUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, monsterCfg.UnitId, level, pos, forward, monsterCfg.AiCfgId);

            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
            monsterComponent.monsterCfgId = monsterCfgId;
            monsterComponent.rewardGold = 0;
            monsterComponent.waveIndex = 1;
            monsterComponent.circleWaveIndex = 0;

			GamePlayHelper.AddUnitPathfinding(monsterUnit);
			GamePlayHelper.AddUnitTeamFlag(monsterUnit, TeamFlagType.Monster1);

            UnitHelper_Create.ActorUnitLearnSkillWhenCreate(monsterUnit);
            ET.GamePlayHelper.DoCreateActions(monsterUnit, monsterCfg.CreateActionIds);

			return monsterUnit;
		}

		public static List<Unit> CreateTower(Scene scene, long playerId, string towerId, float3 pos)
        {
            float3 forward = new float3(0, 0, 1);

            List<Unit> unitList = new();
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerId);
            bool isTower = towerCfg.Type is PlayerTowerType.Tower;
            bool isCallMonster = towerCfg.Type is PlayerTowerType.CallMonster;
            int count = towerCfg.UnitId.Count;
            for (int i = 0; i < count; i++)
            {
                string unitCfgId = towerCfg.UnitId[i];
                int unitNum = 1;
                if (towerCfg.Num.Count > i)
                {
                    unitNum = towerCfg.Num[i];
                }
                int unitLevel = 1;
                if (towerCfg.Level.Count > i)
                {
                    unitLevel = towerCfg.Level[i];
                }
                float3 releativePos = float3.zero;
                if (towerCfg.RelativePosition.Count > i)
                {
                    releativePos = new float3(towerCfg.RelativePosition[i].X, towerCfg.RelativePosition[i].Y, towerCfg.RelativePosition[i].Z);
                }

                for (int j = 0; j < unitNum; j++)
                {
                    Unit towerUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, unitCfgId, unitLevel, pos + releativePos, forward, towerCfg.AiCfgId);

                    if (isTower)
                    {
                        TowerComponent towerComponent = towerUnit.AddComponent<TowerComponent>();
                        towerComponent.towerCfgId = towerId;
                        towerComponent.playerId = playerId;
                    }

                    GamePlayHelper.AddUnitPathfinding(towerUnit);
                    // if (isCallMonster)
                    // {
                    //     GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
                    //     TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetPlayerCallMonsterTeamFlagTypeByPlayer(playerId, pos);
                    //     GamePlayHelper.AddUnitTeamFlag(towerUnit, teamFlagType);
                    //
                    //     MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
                    //     int rewardGold = 0;
                    //     if (towerCfg.RewardGold.Count > i)
                    //     {
                    //         rewardGold = towerCfg.RewardGold[i];
                    //         monsterWaveCallComponent.RecordUnit2Monster(towerUnit.Id, "", rewardGold);
                    //     }
                    // }
                    // else
                    // {
                    //     GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                    //     GamePlayHelper.AddUnitInfo(playerId, towerUnit);
                    // }
                    GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                    GamePlayHelper.AddUnitInfo(playerId, towerUnit);

                    UnitHelper_Create.ActorUnitLearnSkillWhenCreate(towerUnit);
                    ET.GamePlayHelper.DoCreateActions(towerUnit, towerCfg.CreateActionIds);


                    unitList.Add(towerUnit);
                }
            }
            return unitList;
        }

    }
}