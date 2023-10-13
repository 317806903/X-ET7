using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using SkillSlotType = ET.Ability.SkillSlotType;

namespace ET
{
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayComponent GetGamePlay(Unit unit)
		{
			Scene scene = unit.DomainScene();
			return GetGamePlay(scene);
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayTowerDefenseComponent>();
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(Unit unit)
		{
			return GetGamePlay(unit)?.GetComponent<GamePlayTowerDefenseComponent>();
		}

		//-----------------------------------------------------------

		public static Unit CreateObserverUnit(GamePlayMode gamePlayMode, Scene scene, long playerId)
		{
			float3 position = float3.zero;
			float3 forward = float3.zero;

			string unitCfgId = "Unit_Observer";
			int level = 1;
			Unit unit = UnitHelper_Create.CreateWhenServer_CommonPlayerUnit(scene, playerId, unitCfgId, level, UnitType.ObserverUnit, position, forward);

			GamePlayHelper.AddUnitPathfinding(unit);
			GamePlayHelper.AddUnitInfo(playerId, unit);
			return unit;
		}

		public static Unit CreatePlayerUnit(GamePlayMode gamePlayMode, Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			if (gamePlayMode == GamePlayMode.TowerDefense)
			{
				if (GamePlayHelper.ChkIsAR(scene))
				{
					return null;
				}
				Unit unit = CreatePlayerUnit(scene, playerId, playerLevel, position, forward);
				return unit;
			}
			else if (gamePlayMode == GamePlayMode.PK)
			{
				Unit unit = CreatePlayerUnit(scene, playerId, playerLevel, position, forward);
				return unit;
			}

			return null;
		}

		public static Unit CreatePlayerUnit(Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			long playerUnitId = -1;
			Unit playerUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_PlayerUnit(scene, playerUnitId, playerLevel, position, forward);

			GamePlayHelper.AddUnitPathfinding(playerUnit);
			GamePlayHelper.AddUnitInfo(playerId, playerUnit);
			GamePlayHelper.AddPlayerUnitTeamFlag(playerId, playerUnit);

			UnitCfg unitCfg = playerUnit.model;
			int count = unitCfg.SkillList.Count;
			for (int i = 0; i < count; i++)
			{
				SkillHelper.LearnSkill(playerUnit, unitCfg.SkillList[i], 1, SkillSlotType.NormalAttack);
			}

			return playerUnit;
		}

		public static Unit CreateBulletByUnit(Scene scene, Unit unitCaster, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle,
		ActionContext actionContext)
		{
			Unit bulletUnit = UnitHelper_Create.CreateWhenServer_Bullet(scene, unitCaster, actionCfgFireBullet, selectHandle, actionContext);

			GamePlayHelper.AddUnitTeamFlagByParent(unitCaster, bulletUnit);
			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != -1)
			{
				GamePlayHelper.AddUnitInfo(playerId, bulletUnit);
			}

			return bulletUnit;
		}

		public static Unit CreateActorByUnit(Scene scene, Unit unitCaster, ActionCfg_CallActor actionCfgCallActor, SelectHandle selectHandle, ActionContext actionContext)
		{
			Unit actorUnit = UnitHelper_Create.CreateWhenServer_CallActorUnit(scene, unitCaster, actionCfgCallActor, selectHandle, actionContext);

			if (actionCfgCallActor.Duration > 0)
			{
				actorUnit.AddComponent<UnitWaitDestroyComponent, float>(actionCfgCallActor.Duration);
			}

			GamePlayHelper.AddUnitTeamFlagByParent(unitCaster, actorUnit);
			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != -1)
			{
				GamePlayHelper.AddUnitInfo(playerId, actorUnit);
			}

			return actorUnit;
		}

		public static Unit CreateAoeByUnit(Scene scene, Unit unitCaster, ActionCfg_CallAoe actionCfg_CallAoe, SelectHandle selectHandle, ActionContext actionContext)
		{
			Unit aoeUnit = UnitHelper_Create.CreateWhenServer_Aoe(scene, unitCaster, actionCfg_CallAoe, selectHandle, actionContext);

			if (actionCfg_CallAoe.Duration > 0)
			{
				aoeUnit.AddComponent<UnitWaitDestroyComponent, float>(actionCfg_CallAoe.Duration);
			}

			GamePlayHelper.AddUnitTeamFlagByParent(unitCaster, aoeUnit);
			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != -1)
			{
				GamePlayHelper.AddUnitInfo(playerId, aoeUnit);
			}

			return aoeUnit;
		}

		public static Unit CreateSceneEffect(Scene scene, float3 position, float3 forward)
		{
			Unit unit = UnitHelper_Create.CreateWhenServer_SceneEffect(scene, position, forward);

			GamePlayHelper.AddUnitTeamFlag(unit, TeamFlagType.SceneEffect);

			return unit;
		}

		public static Unit CreateNPC(Scene scene, string unitCfgId, float3 position, float3 forward)
		{
			Unit unit = UnitHelper_Create.CreateWhenServer_NPC(scene, unitCfgId, position, forward);

			GamePlayHelper.AddUnitTeamFlag(unit, TeamFlagType.NPC);

			return unit;
		}

		/// <summary>
		/// 当unit创建时， 存储unitId 和 playerId 的关系
		/// </summary>
		/// <param name="unit"></param>
		public static void AddUnitInfo(long playerId, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			bool isPlayerUnit = UnitHelper.ChkIsPlayer(unit);
			gamePlayPlayerListComponent.AddUnitInfo(playerId, unit.Id, isPlayerUnit);
		}

		/// <summary>
		/// 当unit销毁时， 去掉一些已存储的的关系
		/// </summary>
		/// <param name="unit"></param>
		public static void RemoveUnitInfo(Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return;
			}
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			bool isPlayerUnit = UnitHelper.ChkIsPlayer(unit);
			gamePlayPlayerListComponent.RemoveUnitInfo(unit.Id, isPlayerUnit);

			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.RemoveUnitTeamFlag(unit);
		}

		public static Unit GetPlayerUnit(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetPlayerUnit(playerId);
		}

		public static Unit GetPlayerUnit(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetPlayerUnit(playerId);
		}

		public static long GetPlayerIdByUnitId(Unit unitCaster)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unitCaster);
			long playerId = gamePlayComponent.GetPlayerIdByUnitId(unitCaster.Id);
			return playerId;
		}

		public static bool ChkIsFriend(Unit curUnit, Unit targetUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(curUnit);
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			return gamePlayFriendTeamFlagCompent.ChkIsFriend(curUnit, targetUnit);
		}

		/// <summary>
		/// 指定玩家的阵营,例如塔
		/// </summary>
		/// <param name="self"></param>
		/// <param name="playerId"></param>
		/// <param name="unit"></param>
		public static void AddPlayerUnitTeamFlag(long playerId, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddPlayerUnitTeamFlag(playerId, unit);
		}

		/// <summary>
		/// 直接指定阵营信息,例如 大本营，怪物
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unit"></param>
		/// <param name="teamFlag"></param>
		public static void AddUnitTeamFlag(Unit unit, TeamFlagType teamFlag)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitTeamFlag(unit, teamFlag);
		}

		/// <summary>
		/// 有召唤者的设置阵营信息，例如子弹
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitParent"></param>
		/// <param name="unit"></param>
		public static void AddUnitTeamFlagByParent(Unit unitParent, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitTeamFlagByParent(unitParent, unit);
		}

		public static string GetPathfindingMapName(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			return gamePlayComponent.GetPathfindingMapName();
		}

		public static int GetAOIDis(Scene scene)
		{
			if (ChkIsAR(scene))
			{
				return 100;
			}
			return 60;
		}

		public static bool ChkIsAR(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			return gamePlayComponent.IsAR();
		}

		public static void AddUnitPathfinding(Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitPathfinding(unit);
		}

		public static void SetPlayerCoin(Scene scene, long playerId, CoinType coinType, int setValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.SetPlayerCoin(playerId, coinType, setValue, GetCoinType.Normal);
		}

		public static bool ChkCanDoCoinAdd(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = gamePlayComponent.GetComponent<GamePlayTowerDefenseComponent>();
				if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (gamePlayComponent.gamePlayMode == GamePlayMode.PK)
			{
				return false;
			}

			return false;
		}

		/// <summary>
		/// 修改player的代币
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="playerId"></param>
		/// <param name="coinType"></param>
		/// <param name="chgValue"></param>
		public static void ChgPlayerCoin(Scene scene, long playerId, CoinType coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgPlayerCoin(playerId, coinType, chgValue, GetCoinType.Normal);
		}

		public static int GetPlayerCoin(Scene scene, long playerId, CoinType coinType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerCoin(playerId, coinType);
		}

		/// <summary>
		/// 修改team的代币
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="playerId"></param>
		/// <param name="coinType"></param>
		/// <param name="chgValue"></param>
		public static void ChgTeamCoin(Scene scene, long playerId, CoinType coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgTeamCoin(teamFlagType, coinType, chgValue, GetCoinType.Normal);
		}

	}
}