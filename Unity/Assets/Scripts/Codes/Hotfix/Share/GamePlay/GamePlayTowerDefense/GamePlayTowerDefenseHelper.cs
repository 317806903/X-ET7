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
        public static Unit CreateHome(Scene scene, string unitCfgId, float3 pos, int maxHp, int hp, TeamFlagType teamFlagType)
        {
            float3 forward = new float3(0, 0, 1);

            Unit headQuarterUnit = UnitHelper_Create.CreateWhenServer_HomeUnit(scene, unitCfgId, pos, forward, "");

            HomeComponent homeComponent = headQuarterUnit.AddComponent<HomeComponent>();

            NumericComponent numericComponent = headQuarterUnit.GetComponent<NumericComponent>();
            numericComponent.SetAsInt(NumericType.MaxHpBase, maxHp);
            numericComponent.SetAsInt(NumericType.HpBase, hp);

            //GamePlayHelper.AddUnitPathfinding(headQuarterUnit);
            GamePlayHelper.AddUnitTeamFlag(headQuarterUnit, teamFlagType);

            return headQuarterUnit;
        }

        public static Unit CreateMonsterCall(Scene scene, string unitCfgId, float3 pos, float3 forward, TeamFlagType teamFlagType)
        {
            Unit monsterCallUnit = UnitHelper_Create.CreateWhenServer_NPC(scene, unitCfgId, pos, forward);

            GamePlayHelper.AddUnitTeamFlag(monsterCallUnit, teamFlagType);

            return monsterCallUnit;
        }

        public static Unit CreateMonster(Scene scene, long playerId, string monsterCfgId, int level, float3 pos, float3 forward, TeamFlagType teamFlagType, int rewardGold, int waveIndex, int circleWaveIndex)
        {
            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            Unit monsterUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, monsterCfg.UnitId, level, pos, forward, monsterCfg.AiCfgId);

            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
            monsterComponent.monsterCfgId = monsterCfgId;
            monsterComponent.rewardGold = rewardGold;
            monsterComponent.waveIndex = waveIndex;
            monsterComponent.circleWaveIndex = circleWaveIndex;

            GamePlayHelper.AddUnitPathfinding(monsterUnit);
            GamePlayHelper.AddUnitTeamFlag(monsterUnit, teamFlagType);

            UnitHelper_Create.ActorUnitLearnSkillWhenCreate(monsterUnit);

            ET.GamePlayHelper.DoCreateActions(monsterUnit, monsterCfg.CreateActionIds);

            return monsterUnit;
        }

        public static List<Unit> CreateTower(Scene scene, long playerId, string towerCfgId, float3 pos)
        {
            float3 forward = new float3(0, 0, 1);

            ListComponent<Unit> unitList = ListComponent<Unit>.Create();
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            bool isAttackTower = ItemHelper.ChkIsAttackTower(towerCfgId);
            bool isTrap = ItemHelper.ChkIsTrap(towerCfgId);
            bool isCallMonster = ItemHelper.ChkIsCallMonster(towerCfgId);
            int count = towerCfg.UnitId.Count;
            for (int i = 0; i < count; i++)
            {
                string unitCfgId = "";
                string monsterCfgId = "";
                if (isAttackTower || isTrap)
                {
                    unitCfgId = towerCfg.UnitId[i];
                }
                else if (isCallMonster)
                {
                    monsterCfgId = towerCfg.UnitId[i];
                    TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
                    unitCfgId = monsterCfg.UnitId;
                }

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

                    if (isAttackTower || isTrap)
                    {
                        TowerComponent towerComponent = towerUnit.AddComponent<TowerComponent>();
                        towerComponent.towerCfgId = towerCfgId;
                        towerComponent.playerId = playerId;
                    }

                    GamePlayHelper.AddUnitPathfinding(towerUnit);
                    if (isCallMonster)
                    {
                        GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
                        TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetPlayerCallMonsterTeamFlagTypeByPlayer(playerId, pos);
                        GamePlayHelper.AddUnitTeamFlag(towerUnit, teamFlagType);

                        int rewardGold = 0;
                        if (towerCfg.RewardGold.Count > i)
                        {
                            rewardGold = towerCfg.RewardGold[i];
                            Unit monsterUnit = towerUnit;
                            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
                            monsterComponent.monsterCfgId = monsterCfgId;
                            monsterComponent.rewardGold = rewardGold;
                        }
                    }
                    else
                    {
                        GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                        GamePlayHelper.AddUnitInfo(playerId, towerUnit);
                    }

                    UnitHelper_Create.ActorUnitLearnSkillWhenCreate(towerUnit);
                    ET.GamePlayHelper.DoCreateActions(towerUnit, towerCfg.CreateActionIds);

                    if (isAttackTower || isTrap)
                    {
                        unitList.Add(towerUnit);
                    }
                }
            }
            return unitList;
        }

        public static TeamFlagType GetHomeTeamFlagType(TeamFlagType teamFlagType)
        {
            return GamePlayHelper.GetHomeTeamFlagType(teamFlagType);
        }

        public static TeamFlagType GetMonsterTeamFlagType(TeamFlagType teamFlagType)
        {
            return GamePlayHelper.GetMonsterTeamFlagType(teamFlagType);
        }

    }
}