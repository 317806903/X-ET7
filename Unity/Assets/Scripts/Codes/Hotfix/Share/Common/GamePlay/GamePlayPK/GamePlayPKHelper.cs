using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayPkComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayPKHelper
	{
		public static Unit CreateMonster(Scene scene, string monsterCfgId, int level, float3 pos, float3 forward)
		{
			TowerDefense_MonsterCfg monsterCfg = TowerDefense_MonsterCfgCategory.Instance.Get(monsterCfgId);
			Unit monsterUnit = UnitHelper_Create.CreateWhenServer_ActorUnit(scene, UnitType.ActorUnit, monsterCfg.UnitId, level, pos, forward, monsterCfg.AiCfgId);

            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
            monsterComponent.monsterCfgId = monsterCfgId;
            monsterComponent.rewardGold = 0;
            monsterComponent.playerId = (long)ET.PlayerId.PlayerNone;
            monsterComponent.waveIndex = 1;
            monsterComponent.circleWaveIndex = 0;
            monsterComponent.stageWaveIndex = 0;

			GamePlayHelper.AddUnitPathfinding(monsterUnit);
			GamePlayHelper.AddUnitTeamFlag(monsterUnit, TeamFlagType.Monster1);

            UnitHelper_Create.ActorUnitLearnSkillWhenCreate(monsterUnit);
            ET.GamePlayHelper.DoCreateActions(monsterUnit, monsterCfg.CreateActionIds).Coroutine();

			return monsterUnit;
		}

		public static List<Unit> CreateTower(Scene scene, long playerId, string towerCfgId, float3 pos, float3 forward, bool isCreateByMonster)
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
                    if (isCreateByMonster)
                    {
                        towerComponent.playerId = (long)ET.PlayerId.PlayerNone;
                    }
                    else
                    {
                        towerComponent.playerId = playerId;
                    }

                    towerUnit.AddComponent<AttackTargetComponent>();

                    ItemUpgradeComponent itemUpgradeComponent = towerUnit.AddComponent<ItemUpgradeComponent>();
                    itemUpgradeComponent.Init(playerId, towerCfgId, 1);
                }

                GamePlayHelper.AddUnitPathfinding(towerUnit);

                if (isCreateByMonster)
                {
                    GamePlayHelper.AddUnitTeamFlag(towerUnit, TeamFlagType.Monster1);
                }
                else
                {
                    if (isCallMonster)
                    {
                        GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                        GamePlayHelper.AddUnitInfo(playerId, towerUnit);
                    }
                    else
                    {
                        GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                        GamePlayHelper.AddUnitInfo(playerId, towerUnit);
                    }
                }

                UnitHelper_Create.ActorUnitLearnSkillWhenCreate(towerUnit);
                ET.GamePlayHelper.DoCreateActions(towerUnit, towerCfg.CreateActionIds).Coroutine();

                if (isAttackTower || isTrap || isCollider)
                {
                    unitList.Add(towerUnit);
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


                        {
                            Unit monsterUnit = towerUnit;
                            MonsterComponent monsterComponent = monsterUnit.AddComponent<MonsterComponent>();
                            monsterComponent.monsterCfgId = monsterCfgId;
                            monsterComponent.rewardGold = rewardGold;
                            monsterComponent.playerId = playerId;
                        }


                        GamePlayHelper.AddUnitPathfinding(towerUnit);

                        if (isCreateByMonster)
                        {
                            GamePlayHelper.AddUnitTeamFlag(towerUnit, TeamFlagType.Monster1);
                        }
                        else
                        {
                            if (isCallMonster)
                            {
                                GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                                GamePlayHelper.AddUnitInfo(playerId, towerUnit);
                            }
                            else
                            {
                                GamePlayHelper.AddPlayerUnitTeamFlag(playerId, towerUnit);
                                GamePlayHelper.AddUnitInfo(playerId, towerUnit);
                            }
                        }

                        UnitHelper_Create.ActorUnitLearnSkillWhenCreate(towerUnit);
                        ET.GamePlayHelper.DoCreateActions(towerUnit, towerCfg.CreateActionIds).Coroutine();

                        if (isAttackTower || isTrap || isCollider)
                        {
                            unitList.Add(towerUnit);
                        }
                    }
                }
            }
            return unitList;
        }

    }
}