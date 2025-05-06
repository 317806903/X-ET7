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
        public static Unit CreateHome(Scene scene, string unitCfgId, float3 pos, float3 forward, int maxHp, int hp, TeamFlagType teamFlagType)
        {
            Unit headQuarterUnit = UnitHelper_Create.CreateWhenServer_HomeUnit(scene, unitCfgId, pos, forward, "");

            HomeComponent homeComponent = headQuarterUnit.AddComponent<HomeComponent>();
            homeComponent.homeCfgId = unitCfgId;

            ItemUpgradeComponent itemUpgradeComponent = headQuarterUnit.AddComponent<ItemUpgradeComponent>();
            itemUpgradeComponent.Init((long)PlayerId.PlayerNone, ET.ItemHelper.GetHeadQuarterCfgId(), 1);

            NumericComponent numericComponent = headQuarterUnit.GetComponent<NumericComponent>();
            numericComponent.SetAsInt(NumericType.MaxHpBase, maxHp);
            numericComponent.SetAsInt(NumericType.HpBase, hp);

            //GamePlayHelper.AddUnitPathfinding(headQuarterUnit);
            GamePlayHelper.AddUnitTeamFlag(headQuarterUnit, teamFlagType);

            return headQuarterUnit;
        }

        public static Unit CreateMonsterCall(Scene scene, long playerId, string unitCfgId, float3 pos, float3 forward, TeamFlagType teamFlagType)
        {
            Unit monsterCallUnit = UnitHelper_Create.CreateWhenServer_NPC(scene, unitCfgId, pos, forward);

            MonsterCallComponent monsterCallComponent = monsterCallUnit.AddComponent<MonsterCallComponent>();
            monsterCallComponent.playerId = playerId;
            monsterCallComponent.monsterCallCfgId = unitCfgId;

            GamePlayHelper.AddUnitTeamFlag(monsterCallUnit, teamFlagType);

            return monsterCallUnit;
        }

        public static Unit CreateMonster(Scene scene, long playerId, string monsterCfgId, int level, float3 pos, float3 forward, TeamFlagType teamFlagType, int rewardGold, int waveIndex, int circleWaveIndex, int circleNum, int circleIndex, int stageWaveIndex)
        {
            TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
            Unit monsterUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, UnitType.ActorUnit, monsterCfg.UnitId, level, pos, forward, monsterCfg.AiCfgId);

            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
            ET.GamePlayHelper.DoCreateActions(monsterUnit, gamePlayTowerDefenseComponent.model.MonsterWaveCallCreateActionIds).Coroutine();

            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
            monsterComponent.monsterCfgId = monsterCfgId;
            monsterComponent.rewardGold = rewardGold;
            monsterComponent.playerId = (long)ET.PlayerId.PlayerNone;
            monsterComponent.waveIndex = waveIndex;
            monsterComponent.circleWaveIndex = circleWaveIndex;
            monsterComponent.circleNum = circleNum;
            monsterComponent.circleIndex = circleIndex;
            monsterComponent.stageWaveIndex = stageWaveIndex;

            GamePlayHelper.AddUnitPathfinding(monsterUnit);
            GamePlayHelper.AddUnitTeamFlag(monsterUnit, teamFlagType);

            float3 hitPosition = ET.RecastHelper.GetNearNavmeshPos(monsterUnit, monsterUnit.Position);
            ET.Ability.UnitHelper.ResetPos(monsterUnit, hitPosition, float3.zero);

            UnitHelper_Create.ActorUnitLearnSkillWhenCreate(monsterUnit);

            ET.GamePlayHelper.DoCreateActions(monsterUnit, monsterCfg.CreateActionIds).Coroutine();

            return monsterUnit;
        }

        public static List<Unit> CreateTower(Scene scene, long playerId, string towerCfgId, float3 pos, float3 forward)
        {
            ListComponent<Unit> unitList = ListComponent<Unit>.Create();
            TowerDefense_TowerCfg towerCfg = TowerDefense_TowerCfgCategory.Instance.Get(towerCfgId);
            bool isAttackTower = ItemHelper.ChkIsAttackTower(towerCfgId);
            bool isTrap = ItemHelper.ChkIsTrap(towerCfgId);
            bool isCollider = ItemHelper.ChkIsCollider(towerCfgId);
            bool isCallMonster = ItemHelper.ChkIsCallMonster(towerCfgId);

            if (towerCfg.Tower2UnitInfo is TowerToUnitCfgId towerToUnitCfgId)
            {
                if (isCallMonster)
                {
                    Log.Error($"---ET.GamePlayPKHelper.CreateTower isCallMonster==true when towerCfgId[{towerCfgId}]");
                    return null;
                }
                string unitCfgId = towerToUnitCfgId.CfgId;
                int unitLevel = towerToUnitCfgId.Level;
                float3 releativePos = float3.zero;
                Unit towerUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, UnitType.ActorUnit, unitCfgId, unitLevel, pos + releativePos, forward, towerCfg.AiCfgId);

                if (isAttackTower || isTrap || isCollider)
                {
                    TowerComponent towerComponent = towerUnit.AddComponent<TowerComponent>();
                    towerComponent.towerCfgId = towerCfgId;
                    towerComponent.playerId = playerId;

                    towerUnit.AddComponent<AttackTargetComponent>();

                    ItemUpgradeComponent itemUpgradeComponent = towerUnit.AddComponent<ItemUpgradeComponent>();
                    itemUpgradeComponent.Init(playerId, towerCfgId, 1);
                }

                GamePlayHelper.AddUnitPathfinding(towerUnit);

                GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                GamePlayHelper.AddUnitInfo(playerId, towerUnit);

                UnitHelper_Create.ActorUnitLearnSkillWhenCreate(towerUnit);
                ET.GamePlayHelper.DoCreateActions(towerUnit, towerCfg.CreateActionIds).Coroutine();

                if (isAttackTower || isTrap || isCollider)
                {
                    unitList.Add(towerUnit);
                }
                if (isAttackTower || isTrap || isCollider)
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
                    ET.GamePlayHelper.DoCreateActions(towerUnit, gamePlayTowerDefenseComponent.model.TowerCreateActionIds).Coroutine();
                }
            }
            else if (towerCfg.Tower2UnitInfo is TowerToMonsterCfgId towerToMonsterCfgId)
            {
                if (isCallMonster == false)
                {
                    Log.Error($"---ET.GamePlayPKHelper.CreateTower isCallMonster==false when towerCfgId[{towerCfgId}]");
                    return null;
                }
                int count = towerToMonsterCfgId.CfgId.Count;
                for (int i = 0; i < count; i++)
                {
                    string monsterCfgId = towerToMonsterCfgId.CfgId[i];
                    if (TowerDefense_MonsterCfgCategory.Instance.Contain(monsterCfgId) == false)
                    {
                        Log.Error($"CreateTower towerCfgId[{towerCfgId}] isCallMonster, but TowerDefense_MonsterCfgCategory.Instance.Contain({monsterCfgId}) == false");
                        continue;
                    }
                    TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
                    string unitCfgId = monsterCfg.UnitId;

                    int unitNum = 1;
                    if (towerToMonsterCfgId.Num.Count > i)
                    {
                        unitNum = towerToMonsterCfgId.Num[i];
                    }
                    int unitLevel = 1;
                    if (towerToMonsterCfgId.Level.Count > i)
                    {
                        unitLevel = towerToMonsterCfgId.Level[i];
                    }
                    float3 releativePos = float3.zero;
                    if (towerToMonsterCfgId.RelativePosition.Count > i)
                    {
                        releativePos = new float3(towerToMonsterCfgId.RelativePosition[i].X, towerToMonsterCfgId.RelativePosition[i].Y, towerToMonsterCfgId.RelativePosition[i].Z);
                    }
                    int rewardGold = 0;
                    if (towerToMonsterCfgId.RewardGold.Count > i)
                    {
                        rewardGold = towerToMonsterCfgId.RewardGold[i];
                    }

                    for (int j = 0; j < unitNum; j++)
                    {
                        Unit towerUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, UnitType.ActorUnit, unitCfgId, unitLevel, pos + releativePos, forward, towerCfg.AiCfgId);

                        GamePlayHelper.AddUnitPathfinding(towerUnit);

                        GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
                        TeamFlagType teamFlagType = gamePlayTowerDefenseComponent.GetPlayerCallMonsterTeamFlagTypeByPlayer(playerId, pos);
                        GamePlayHelper.AddUnitTeamFlag(towerUnit, teamFlagType);

                        {
                            Unit monsterUnit = towerUnit;
                            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
                            monsterComponent.monsterCfgId = monsterCfgId;
                            monsterComponent.rewardGold = rewardGold;
                            monsterComponent.playerId = playerId;
                        }

                        UnitHelper_Create.ActorUnitLearnSkillWhenCreate(towerUnit);
                        ET.GamePlayHelper.DoCreateActions(towerUnit, towerCfg.CreateActionIds).Coroutine();

                        if (isAttackTower || isTrap || isCollider)
                        {
                            unitList.Add(towerUnit);
                        }
                        if (isAttackTower || isTrap || isCollider)
                        {
                            ET.GamePlayHelper.DoCreateActions(towerUnit, gamePlayTowerDefenseComponent.model.TowerCreateActionIds).Coroutine();
                        }
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