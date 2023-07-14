using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

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
		
		public static Unit CreatePlayerUnit(GamePlayMode gamePlayMode, Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			if (gamePlayMode == GamePlayMode.TowerDefense)
			{
				Unit unit = GamePlayTowerDefenseHelper.CreatePlayerObserverUnit(scene, playerId, position, forward);
				return unit;
			}
			else if (gamePlayMode == GamePlayMode.PK)
			{
				Unit unit = GamePlayPKHelper.CreatePlayerUnit(scene, playerId, playerLevel, position, forward);
				return unit;
			}

			return null;
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

		public static Unit CreateSceneEffect(Scene scene, float3 position, float3 forward)
		{
			Unit unit = UnitHelper_Create.CreateWhenServer_SceneEffect(scene, position, forward);

			GamePlayHelper.AddUnitTeamFlag(unit, TeamFlagType.TeamGlobal1);

			return unit;
		}

		public static Unit CreateNPC(Scene scene, string unitCfgId, float3 position, float3 forward)
		{
			Unit unit = UnitHelper_Create.CreateWhenServer_NPC(scene, unitCfgId, position, forward);

			GamePlayHelper.AddUnitTeamFlag(unit, TeamFlagType.TeamGlobal1);

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
			gamePlayPlayerListComponent.AddUnitInfo(playerId, unit.Id);
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
			gamePlayPlayerListComponent.RemoveUnitInfo(unit.Id);
			
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.RemoveUnitTeamFlag(unit);
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

		public static void SetPlayerCoin(Scene scene, long playerId, CoinType coinType, int setValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.SetPlayerCoin(playerId, coinType, setValue);
		}

		public static void ChgPlayerCoin(Scene scene, long playerId, CoinType coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgPlayerCoin(playerId, coinType, chgValue);
		}

		public static int GetPlayerCoin(Scene scene, long playerId, CoinType coinType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			return gamePlayPlayerListComponent.GetPlayerCoin(playerId, coinType);
		}

	}
}