using ET.Ability;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public static class GamePlayHelper
	{
		public static GamePlayComponent GetGamePlay(Scene scene)
		{
			if (scene == null)
			{
				return null;
			}
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

		public static GamePlayPkComponent GetGamePlayPk(Scene scene)
		{
			return GetGamePlay(scene)?.GetComponent<GamePlayPkComponent>();
		}

		public static GamePlayPkComponent GetGamePlayPk(Unit unit)
		{
			return GetGamePlay(unit)?.GetComponent<GamePlayPkComponent>();
		}

		public static bool ChkIsClientScene(Scene scene)
		{
			bool isClient = scene.SceneType == SceneType.Client || scene.SceneType == SceneType.Current;
			return isClient;
		}

		public static float GetGameMapScale(Scene scene, bool isClient = true)
		{
			float gameMapScale = _GetGameMapOrResScale(true, scene, isClient);
			return gameMapScale;
		}

		public static float GetGameResScale(Scene scene, bool isClient = true)
		{
			float gameResScale = _GetGameMapOrResScale(false, scene, isClient);
			return gameResScale;
		}

		public static float _GetGameMapOrResScale(bool isGetMapScale, Scene scene, bool isClient = true)
		{
			if (scene != null)
			{
				isClient = ChkIsClientScene(scene);
				if (isClient)
				{
					scene = ET.Client.SceneHelper.GetCurrentScene(scene);
				}
			}
			float gameScale = EventSystem.Instance.Invoke<ET.GetGameMapOrResScale, float>(new ET.GetGameMapOrResScale()
			{
				isGetMapScale = isGetMapScale,
				scene = scene,
				isClient = isClient,
			});
			return gameScale;
		}

		public static Unit CreateObserverUnit(GamePlayMode gamePlayMode, Scene scene, long playerId)
		{
			float3 position = float3.zero;
			float3 forward = new float3(0, 0, 1);

			Unit unit = UnitHelper_Create.CreateWhenServer_ObserverUnit(scene, playerId, position, forward);

			GamePlayHelper.AddUnitPathfinding(unit);
			GamePlayHelper.AddUnitInfo(playerId, unit);
			GamePlayHelper.AddPlayerUnitTeamFlag(playerId, unit);
			return unit;
		}

		public static Unit CreateCameraPlayerUnit(GamePlayMode gamePlayMode, Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			Unit unit = CreateCameraPlayerUnit(scene, playerId, playerLevel, position, forward);
			if (gamePlayMode == GamePlayMode.TowerDefense)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
				ET.GamePlayHelper.DoCreateActions(unit, gamePlayTowerDefenseComponent.model.CameraPlayerUnitCreateActionIds).Coroutine();
			}
			else if (gamePlayMode == GamePlayMode.PK)
			{
			}
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

				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.GamePlayHelper.GetGamePlayTowerDefense(scene);
				ET.GamePlayHelper.DoCreateActions(unit, gamePlayTowerDefenseComponent.model.PlayerUnitCreateActionIds).Coroutine();

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
			Unit playerUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_PlayerUnit(scene, playerId, playerLevel, position, forward);

			GamePlayHelper.AddUnitPathfinding(playerUnit);
			GamePlayHelper.AddUnitInfo(playerId, playerUnit);
			GamePlayHelper.AddPlayerUnitTeamFlag(playerId, playerUnit);

			UnitCfg unitCfg = playerUnit.model;
			foreach (var item in unitCfg.SkillList)
			{
				string skillCfgId = item.Key;
				ET.AbilityConfig.SkillSlotType skillSlotType = item.Value;
				SkillHelper.LearnSkill(playerUnit, skillCfgId, 1, skillSlotType);
			}

			return playerUnit;
		}

		public static Unit CreateCameraPlayerUnit(Scene scene, long playerId, int playerLevel, float3 position, float3 forward)
		{
			Unit cameraPlayerUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_CameraPlayerUnit(scene, playerId, playerLevel, position, forward);

			GamePlayHelper.AddUnitPathfinding(cameraPlayerUnit);
			GamePlayHelper.AddUnitInfo(playerId, cameraPlayerUnit);
			// GamePlayHelper.AddPlayerUnitTeamFlag(playerId, cameraPlayerUnit);

			TeamFlagType playerSkillTeamFlagType = GamePlayHelper.GetPlayerSkillTeamFlagTypeByPlayer(scene, playerId);
			GamePlayHelper.AddUnitTeamFlag(cameraPlayerUnit, playerSkillTeamFlagType, playerId);

			UnitCfg unitCfg = cameraPlayerUnit.model;
			foreach (var item in unitCfg.SkillList)
			{
				string skillCfgId = item.Key;
				ET.AbilityConfig.SkillSlotType skillSlotType = item.Value;
				SkillHelper.LearnSkill(cameraPlayerUnit, skillCfgId, 1, skillSlotType);
			}

			return cameraPlayerUnit;
		}

		public static Unit CreateSkillCasterUnit(long playerId, Unit cameraPlayerUnit)
		{
			Scene scene = cameraPlayerUnit.DomainScene();
			int playerLevel = cameraPlayerUnit.level;
			float3 position = cameraPlayerUnit.Position;
			float3 forward = cameraPlayerUnit.Forward;

			Unit skillCasterUnit = ET.Ability.UnitHelper_Create.CreateWhenServer_SkillCasterUnit(scene, playerId, playerLevel, position, forward);

			GamePlayHelper.AddUnitInfo(playerId, skillCasterUnit);
			GamePlayHelper.AddUnitTeamFlagByParent(playerId, cameraPlayerUnit, skillCasterUnit);

			return skillCasterUnit;
		}

		public static Unit CreateBulletByUnit(Scene scene, Unit unitCaster, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle,
		ActionContext actionContext)
		{
			Unit bulletUnit = UnitHelper_Create.CreateWhenServer_Bullet(scene, unitCaster, actionCfgFireBullet, selectHandle, actionContext);

			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != (long)ET.PlayerId.PlayerNone)
			{
				GamePlayHelper.AddUnitInfo(playerId, bulletUnit);
			}
			GamePlayHelper.AddUnitTeamFlagByParent(playerId, unitCaster, bulletUnit);

			return bulletUnit;
		}

		public static Unit CreateActorByUnit(Scene scene, Unit unitCaster, ActionCfg_CallActor actionCfgCallActor, SelectHandle selectHandle, ref ActionContext actionContext, float3? position)
		{
			Unit actorUnit = UnitHelper_Create.CreateWhenServer_CallActorUnit(scene, unitCaster, actionCfgCallActor, selectHandle, ref actionContext, position);

			if (actionCfgCallActor.Duration > 0)
			{
				actorUnit.AddComponent<UnitWaitDestroyComponent, float>(actionCfgCallActor.Duration);
			}

			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != (long)ET.PlayerId.PlayerNone)
			{
				GamePlayHelper.AddUnitInfo(playerId, actorUnit);
			}

			if (actionCfgCallActor.CallActorTeamFlagType == CallActorTeamFlagType.ByParentTeam)
			{
				GamePlayHelper.AddUnitTeamFlagByParent(playerId, unitCaster, actorUnit);
			}
			else if (actionCfgCallActor.CallActorTeamFlagType == CallActorTeamFlagType.TeamGlobal)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamGlobal, playerId);
			}
			else if (actionCfgCallActor.CallActorTeamFlagType == CallActorTeamFlagType.TeamPlayer)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamPlayer, playerId);
			}
			else if (actionCfgCallActor.CallActorTeamFlagType == CallActorTeamFlagType.TeamPlayerSkill)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamPlayerSkill, playerId);
			}
			else if (actionCfgCallActor.CallActorTeamFlagType == CallActorTeamFlagType.TeamMonster)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.Monster, playerId);
			}
			else if (actionCfgCallActor.CallActorTeamFlagType == CallActorTeamFlagType.TeamWildMonster)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamWildMonster, playerId);
			}

			if (actionCfgCallActor.BeCallActorActionId.Count > 0)
			{
				SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(actorUnit);
				foreach (var actionId in actionCfgCallActor.BeCallActorActionId)
				{
					ActionHandlerHelper.CreateAction(actorUnit, null, actionId, 0.1f, selectHandleSelf, ref actionContext);
				}
			}
			return actorUnit;
		}

		public static Unit CreateAoeByUnit(Scene scene, Unit unitCaster, ActionCfg_CallAoe actionCfgCallAoe, SelectHandle selectHandle, ref ActionContext actionContext)
		{
			Unit aoeUnit = UnitHelper_Create.CreateWhenServer_Aoe(scene, unitCaster, actionCfgCallAoe, selectHandle, actionContext);

			if (actionCfgCallAoe.Duration > 0)
			{
				aoeUnit.AddComponent<UnitWaitDestroyComponent, float>(actionCfgCallAoe.Duration);
			}

			long playerId = GamePlayHelper.GetPlayerIdByUnitId(unitCaster);
			if (playerId != (long)ET.PlayerId.PlayerNone)
			{
				GamePlayHelper.AddUnitInfo(playerId, aoeUnit);
			}
			GamePlayHelper.AddUnitTeamFlagByParent(playerId, unitCaster, aoeUnit);

			if (actionCfgCallAoe.BeCallActorActionId.Count > 0)
			{
				SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(aoeUnit);
				foreach (var actionId in actionCfgCallAoe.BeCallActorActionId)
				{
					ActionHandlerHelper.CreateAction(unitCaster, null, actionId, 0.1f, selectHandleSelf, ref actionContext);
				}
			}
			return aoeUnit;
		}

		public static Unit CreateActorByGlobal(Scene scene, ActionGameCfg_CreateGlobalUnit actionGameCfgCreateGlobalUnit, ref ActionContext actionContext, float3? position)
		{
			Unit actorUnit = UnitHelper_Create.CreateWhenServer_CallGlobalActorUnit(scene, actionGameCfgCreateGlobalUnit, ref actionContext, position);

			if (actionGameCfgCreateGlobalUnit.Duration > 0)
			{
				actorUnit.AddComponent<UnitWaitDestroyComponent, float>(actionGameCfgCreateGlobalUnit.Duration);
			}

			long playerId = actionContext.playerId;
			if (actionGameCfgCreateGlobalUnit.CallActorTeamFlagType == CallActorTeamFlagType.ByParentTeam)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamWildMonster, playerId);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorTeamFlagType == CallActorTeamFlagType.TeamGlobal)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamGlobal, playerId);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorTeamFlagType == CallActorTeamFlagType.TeamPlayer)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamPlayer, playerId);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorTeamFlagType == CallActorTeamFlagType.TeamPlayerSkill)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamPlayerSkill, playerId);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorTeamFlagType == CallActorTeamFlagType.TeamMonster)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.Monster, playerId);
			}
			else if (actionGameCfgCreateGlobalUnit.CallActorTeamFlagType == CallActorTeamFlagType.TeamWildMonster)
			{
				GamePlayHelper.AddUnitTeamFlag(actorUnit, TeamFlagType.TeamWildMonster, playerId);
			}

			if (actionGameCfgCreateGlobalUnit.BeCallActorActionId.Count > 0)
			{
				SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(actorUnit);
				foreach (var actionId in actionGameCfgCreateGlobalUnit.BeCallActorActionId)
				{
					ActionHandlerHelper.CreateAction(actorUnit, null, actionId, 0.1f, selectHandleSelf, ref actionContext);
				}
			}
			return actorUnit;
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
			gamePlayPlayerListComponent.AddUnitInfo(playerId, unit.Id, unit.Type);
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

			gamePlayPlayerListComponent.RemoveUnitInfo(unit.Id, unit.Type);

			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			gamePlayFriendTeamFlagCompent.RemoveUnitTeamFlag(unit.Id);
		}

		public static Unit GetCurPlayerUnit(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetCurPlayerUnit(playerId);
		}

		public static Unit GetCurPlayerUnit(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetCurPlayerUnit(playerId);
		}

		public static Unit GetCurPlayerUnit(Scene scene, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetCurPlayerUnit(playerId);
		}

		public static List<Unit> GetPlayerUnitList(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetPlayerUnitList(playerId);
		}

		public static List<Unit> GetPlayerUnitList(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetPlayerUnitList(playerId);
		}

		public static Unit GetCameraPlayerUnit(Unit observerUnit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(observerUnit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			long playerId = observerUnit.Id;
			return gamePlayComponent.GetCameraPlayerUnit(playerId);
		}

		public static Unit GetCameraPlayerUnit(Unit unit, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetCameraPlayerUnit(playerId);
		}

		public static Unit GetCameraPlayerUnit(Scene scene, long playerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return null;
			}

			return gamePlayComponent.GetCameraPlayerUnit(playerId);
		}

		public static long GetPlayerIdByUnitId(Unit unit)
		{
			if (unit == null)
			{
				return (long)PlayerId.PlayerNone;
			}
			long playerId = TeamFlagHelper.GetPlayerId(unit);
			return playerId;
		}

		public static bool ChkIsFriend(Unit curUnit, Unit targetUnit, bool needSamePlayer = false)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(curUnit);
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			return gamePlayFriendTeamFlagCompent.ChkIsFriend(curUnit.Id, targetUnit.Id, needSamePlayer);
		}

		public static bool ChkIsFriend(Scene scene, long curPlayerId, long targetPlayerId)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayFriendTeamFlagCompent gamePlayFriendTeamFlagCompent = gamePlayComponent.GetComponent<GamePlayFriendTeamFlagCompent>();
			TeamFlagType curTeamFlagType = gamePlayFriendTeamFlagCompent.GetTeamFlagByPlayerId(curPlayerId);
			TeamFlagType targetTeamFlagType = gamePlayFriendTeamFlagCompent.GetTeamFlagByPlayerId(targetPlayerId);
			return ChkIsFriend(scene, curTeamFlagType, targetTeamFlagType);
		}

		public static bool ChkIsFriend(Scene scene, TeamFlagType curTeamFlagType, TeamFlagType targetTeamFlagType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			return gamePlayComponent.ChkIsFriend(curTeamFlagType, targetTeamFlagType);
		}

		public static bool ChkIsSelectObjectTeamFlagType(Scene scene, SelectObjectTeamFlagType selectObjectTeamFlagType, Unit curUnit, Unit unit)
		{
			TeamFlagType curTeamFlagType = ET.Ability.TeamFlagHelper.GetTeamFlag(curUnit);
			TeamFlagType teamFlagType = ET.Ability.TeamFlagHelper.GetTeamFlag(unit);
			switch (selectObjectTeamFlagType)
			{
				case SelectObjectTeamFlagType.None:
					return true;
				case SelectObjectTeamFlagType.SameTeam:
					return curTeamFlagType == teamFlagType;
				case SelectObjectTeamFlagType.FriendTeam:
				{
					GamePlayComponent gamePlayComponent = GetGamePlay(scene);
					bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curTeamFlagType);
					return isFriend;
				}
				case SelectObjectTeamFlagType.HostileTeam:
				{
					GamePlayComponent gamePlayComponent = GetGamePlay(scene);
					bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curTeamFlagType);
					return !isFriend;
				}
				case SelectObjectTeamFlagType.TeamGlobal:
					return ET.GamePlayHelper.ChkIsTeamGlobal(teamFlagType);
				case SelectObjectTeamFlagType.TeamPlayer:
					return ET.GamePlayHelper.ChkIsTeamPlayer(teamFlagType);
				case SelectObjectTeamFlagType.TeamPlayerSkill:
					return ET.GamePlayHelper.ChkIsTeamPlayerSkill(teamFlagType);
				case SelectObjectTeamFlagType.TeamMonster:
					return ET.GamePlayHelper.ChkIsTeamMonster(teamFlagType);
				case SelectObjectTeamFlagType.TeamWildMonster:
					return ET.GamePlayHelper.ChkIsTeamWildMonster(teamFlagType);
			}
			return true;
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
		public static void AddUnitTeamFlag(Unit unit, TeamFlagType teamFlag, long playerId = (long)ET.PlayerId.PlayerNone)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitTeamFlag(playerId, unit, teamFlag);
		}

		/// <summary>
		/// 有召唤者的设置阵营信息，例如子弹
		/// </summary>
		/// <param name="self"></param>
		/// <param name="unitParent"></param>
		/// <param name="unit"></param>
		public static void AddUnitTeamFlagByParent(long playerId, Unit unitParent, Unit unit)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(unit);
			gamePlayComponent.AddUnitTeamFlagByParent(playerId, unitParent, unit);
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

		public static void SetPlayerCoin(Scene scene, long playerId, CoinTypeInGame coinType, int setValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.SetPlayerCoin(playerId, coinType, setValue, GetCoinType.Normal);
		}

		public static bool ChkCanDoCoinAdd(Scene scene, bool isAddWhenBattleOnly)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
			{
				GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = gamePlayComponent.GetComponent<GamePlayTowerDefenseComponent>();
				GamePlayTowerDefenseStatus gamePlayTowerDefenseStatus = gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus;
				if (gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
				{
					return true;
				}
				else if (isAddWhenBattleOnly == false && gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
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
		public static void ChgPlayerCoin(Scene scene, long playerId, CoinTypeInGame coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgPlayerCoin(playerId, coinType, chgValue, GetCoinType.Normal);
		}

		public static void ChgPlayerCoinShare(Scene scene, long playerId, CoinTypeInGame coinType, int chgValue, Unit showGetCoinUnit, bool isTotalFix)
		{
			if (chgValue == 0)
			{
				return;
			}
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			TeamFlagType curTeamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
			using ListComponent<long> teamPlayerList = ListComponent<long>.Create();
			foreach (long playerIdTmp in gamePlayComponent.GetPlayerList())
			{
				TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerIdTmp);
				if (teamFlagType == curTeamFlagType)
				{
					teamPlayerList.Add(playerIdTmp);
				}
			}

			if (teamPlayerList.Count == 0)
			{
				return;
			}
			int goldOne = 0;
			if (isTotalFix)
			{
				goldOne = (int)(1f*chgValue / teamPlayerList.Count);
				if (goldOne == 0)
				{
					goldOne = 1;
				}
			}
			else
			{
				goldOne = (int)chgValue;
			}
			foreach (long playerIdTmp in teamPlayerList)
			{
				ET.GamePlayHelper.ChgPlayerCoin(scene, playerIdTmp, coinType, goldOne);

				ET.Ability.UnitHelper.AddSyncData_UnitGetCoinShow(playerIdTmp, showGetCoinUnit, coinType, chgValue);
			}
		}

		public static float GetPlayerCoin(Scene scene, long playerId, CoinTypeInGame coinType)
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
		public static void ChgTeamCoin(Scene scene, long playerId, CoinTypeInGame coinType, int chgValue)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			TeamFlagType teamFlagType = gamePlayComponent.GetTeamFlagByPlayerId(playerId);
			GamePlayPlayerListComponent gamePlayPlayerListComponent = gamePlayComponent.GetComponent<GamePlayPlayerListComponent>();
			gamePlayPlayerListComponent.ChgTeamCoin(teamFlagType, coinType, chgValue, GetCoinType.Normal);
		}

		public static float GetAudioMaxDis(Scene scene)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			if (gamePlayComponent == null)
			{
				return 300;
			}
			if (ChkIsAR(scene))
			{
				float gameMapScale = gamePlayComponent.GetGameMapScale();
				return gameMapScale * 10;
			}
			else
			{
				return 300;
			}
		}

		public static string GetBattleCfgId(RoomTypeInfo roomTypeInfo)
		{
			RoomType RoomTypeIn = roomTypeInfo.roomType;
			SubRoomType SubRoomTypeIn = roomTypeInfo.subRoomType;
			int seasonCfgId = roomTypeInfo.seasonCfgId;
			int pveIndex = roomTypeInfo.pveIndex;
			PVELevelDifficulty pveLevelDifficulty = roomTypeInfo.pveLevelDifficulty;

			string battleCfgId = "";
			if (RoomTypeIn == RoomType.Normal)
			{
				if (SubRoomTypeIn == SubRoomType.NormalRoom)
				{
					battleCfgId = "GamePlayBattleLevel_Room11";
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
					battleCfgId = "GamePlayBattleLevel_ARRoom";
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVE)
				{
					if (seasonCfgId > 0)
					{
						battleCfgId = SeasonChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(seasonCfgId, pveIndex, pveLevelDifficulty, false);
					}
					else
					{
						battleCfgId = TowerDefense_ChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(pveIndex, pveLevelDifficulty, false);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVP)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeNoARPVPCfgId;
					}
					else
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.NoARPVPCfgId;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalEndlessChallenge)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeNoAREndlessChallengeCfgId;
					}
					else
					{
						if (seasonCfgId > 0)
						{
							SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
							if (seasonInfoCfg != null)
							{
								battleCfgId = seasonInfoCfg.NoAREndlessChallengeCfgId;
							}
						}
						else
						{
							battleCfgId = GlobalSettingCfgCategory.Instance.NoAREndlessChallengeCfgId;
						}
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalScanMesh)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeNoARScanMeshCfgId;
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARScanCode)
				{
					return "";
				}
			}
			else if (RoomTypeIn == RoomType.AR)
			{
				if (SubRoomTypeIn == SubRoomType.ARPVE)
				{
					if (seasonCfgId > 0)
					{
						battleCfgId = SeasonChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(seasonCfgId, pveIndex, pveLevelDifficulty, true);
					}
					else
					{
						battleCfgId = TowerDefense_ChallengeLevelCfgCategory.Instance.GetCurChallengeGamePlayBattleLevelCfgId(pveIndex, pveLevelDifficulty, true);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARPVP)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeARPVPCfgId;
					}
					else
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.ARPVPCfgId;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					if (ET.SceneHelper.ChkIsGameModeArcade())
					{
						battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeAREndlessChallengeCfgId;
					}
					else
					{
						if (seasonCfgId > 0)
						{
							SeasonInfoCfg seasonInfoCfg = SeasonInfoCfgCategory.Instance.Get(seasonCfgId);
							if (seasonInfoCfg != null)
							{
								battleCfgId = seasonInfoCfg.AREndlessChallengeCfgId;
							}
						}
						else
						{
							battleCfgId = GlobalSettingCfgCategory.Instance.AREndlessChallengeCfgId;
						}
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.ARTutorialFirstCfgId;
				}
				else if (SubRoomTypeIn == SubRoomType.ArcadeScanMesh)
				{
					battleCfgId = GlobalSettingCfgCategory.Instance.GameModeArcadeARScanMeshCfgId;
				}
				else if (SubRoomTypeIn == SubRoomType.ARScanCode)
				{
					return "";
				}
			}

			if (string.IsNullOrEmpty(battleCfgId))
			{
				Log.Error($"string.IsNullOrEmpty({battleCfgId}) when roomTypeInfo=[{roomTypeInfo}]");
				return "";
			}
			if (GamePlayBattleLevelCfgCategory.Instance.Contain(battleCfgId) == false)
			{
				Log.Error($"GamePlayBattleLevelCfgCategory.Instance.Contain({battleCfgId}) == false");
				return "";
			}

			return battleCfgId;
		}

		public static RoomTypeInfo GetRoomTypeInfo(RoomType RoomTypeIn, SubRoomType SubRoomTypeIn, int seasonCfgId = -1, int pveIndex = -1, PVELevelDifficulty pveLevelDifficulty = PVELevelDifficulty.Easy, string gamePlayBattleLevelCfgId = "")
		{
			RoomTypeInfo roomTypeInfo = new();
			roomTypeInfo.roomType = RoomTypeIn;
			roomTypeInfo.subRoomType = SubRoomTypeIn;
			//roomTypeInfo.seasonIndex = seasonCfgId;
			roomTypeInfo.seasonCfgId = seasonCfgId;
			roomTypeInfo.pveIndex = pveIndex;
			roomTypeInfo.pveLevelDifficulty = pveLevelDifficulty;
			if (RoomTypeIn == RoomType.AR && SubRoomTypeIn == SubRoomType.ARScanCode)
			{
				return roomTypeInfo;
			}
			else if (RoomTypeIn == RoomType.Normal && SubRoomTypeIn == SubRoomType.NormalARScanCode)
			{
				return roomTypeInfo;
			}

			if (string.IsNullOrEmpty(gamePlayBattleLevelCfgId))
			{
				roomTypeInfo.gamePlayBattleLevelCfgId = ET.GamePlayHelper.GetBattleCfgId(roomTypeInfo);
			}
			else
			{
				roomTypeInfo.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
			}

			if (RoomTypeIn == RoomType.Normal)
			{
				switch (SubRoomTypeIn)
				{
					case SubRoomType.None:
						Log.Error($"RoomTypeIn == RoomType.Normal SubRoomTypeIn==SubRoomType.None");
						return roomTypeInfo;
				}
			}
			else if(RoomTypeIn == RoomType.AR)
			{

			}

			return roomTypeInfo;
		}

		public static RoomTypeInfo ResetRoomTypeInfo(RoomTypeInfo roomTypeInfo, string gamePlayBattleLevelCfgId)
		{
			if (string.IsNullOrEmpty(gamePlayBattleLevelCfgId))
			{
				return roomTypeInfo;
			}
			else
			{
				roomTypeInfo.gamePlayBattleLevelCfgId = gamePlayBattleLevelCfgId;
			}

			if (roomTypeInfo.roomType == RoomType.Normal)
			{
				GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
				if (roomTypeInfo.subRoomType == SubRoomType.NormalPVP && gamePlayBattleLevelCfg.TeamMode is AllPlayersOneGroup)
				{
					roomTypeInfo.subRoomType = SubRoomType.NormalRoom;
				}
				else if(roomTypeInfo.subRoomType == SubRoomType.NormalRoom && gamePlayBattleLevelCfg.TeamMode is PlayerTeam)
				{
					roomTypeInfo.subRoomType = SubRoomType.NormalPVP;
				}
			}
			return roomTypeInfo;
		}

        public static bool IsEndlessChallengeMode(RoomTypeInfo roomTypeInfo)
        {
            if (roomTypeInfo.roomType == RoomType.Normal)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.NormalEndlessChallenge)
                {
                    return true;
                }
            }
            else if (roomTypeInfo.roomType == RoomType.AR)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.AREndlessChallenge)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPVEMode(RoomTypeInfo roomTypeInfo)
        {
            if (roomTypeInfo.roomType == RoomType.Normal)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.NormalPVE)
                {
                    return true;
                }
            }
            else if (roomTypeInfo.roomType == RoomType.AR)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.ARPVE)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPVPMode(RoomTypeInfo roomTypeInfo)
        {
            if (roomTypeInfo.roomType == RoomType.Normal)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.NormalPVP)
                {
                    return true;
                }
            }
            else if (roomTypeInfo.roomType == RoomType.AR)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.ARPVP)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsTutorialFirstModel(RoomTypeInfo roomTypeInfo)
        {
            if (roomTypeInfo.roomType == RoomType.Normal)
            {
                return false;
            }
            else if (roomTypeInfo.roomType == RoomType.AR)
            {
                if (roomTypeInfo.subRoomType == SubRoomType.ARTutorialFirst)
                {
                    return true;
                }
            }
            return false;
        }

		public static int GetPhysicalCostPVE(bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			if (isOwner)
			{
				return GlobalSettingCfgCategory.Instance.OwnerARPVECfgTakePhsicalStrength;
			}
			else
			{
				return GlobalSettingCfgCategory.Instance.OtherARPVECfgTakePhsicalStrength;
			}
		}

		public static int GetPhysicalCostPVESeason(bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			if (isOwner)
			{
				return GlobalSettingCfgCategory.Instance.OwnerARPVESeasonCfgTakePhsicalStrength;
			}
			else
			{
				return GlobalSettingCfgCategory.Instance.OtherARPVESeasonCfgTakePhsicalStrength;
			}
		}

		public static int GetPhysicalCostPVP(bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			if (isOwner)
			{
				return GlobalSettingCfgCategory.Instance.OwnerARPVPCfgTakePhsicalStrength;
			}
			else
			{
				return GlobalSettingCfgCategory.Instance.OtherARPVPCfgTakePhsicalStrength;
			}
		}

		public static int GetPhysicalCostPVPSeason(bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}
			if (isOwner)
			{
				return GlobalSettingCfgCategory.Instance.OwnerARPVPSeasonCfgTakePhsicalStrength;
			}
			else
			{
				return GlobalSettingCfgCategory.Instance.OtherARPVPSeasonCfgTakePhsicalStrength;
			}
		}

		public static int GetPhysicalCostEndlessChallenge(bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}

			if (isOwner)
			{
				return GlobalSettingCfgCategory.Instance.OwnerAREndlessChallengeTakePhsicalStrength;
			}
			else
			{
				return GlobalSettingCfgCategory.Instance.OtherAREndlessChallengeTakePhsicalStrength;
			}
		}

		public static int GetPhysicalCostEndlessChallengeSeason(bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			if (ET.SceneHelper.ChkIsDemoShow())
			{
				return 1;
			}

			if (isOwner)
			{
				return GlobalSettingCfgCategory.Instance.OwnerAREndlessChallengeSeasonTakePhsicalStrength;
			}
			else
			{
				return GlobalSettingCfgCategory.Instance.OtherAREndlessChallengeSeasonTakePhsicalStrength;
			}
		}

		public static int GetPhysicalCost(RoomTypeInfo roomTypeInfo, bool isOwner = true)
		{
#if Platform_Quest
			return 0;
#endif
			if (ET.SceneHelper.ChkIsGameModeArcade())
			{
				return 0;
			}
			RoomType RoomTypeIn = roomTypeInfo.roomType;
			SubRoomType SubRoomTypeIn = roomTypeInfo.subRoomType;
			int physicalCost = 0;
			if (RoomTypeIn == RoomType.Normal)
			{
				if (SubRoomTypeIn == SubRoomType.NormalRoom)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVE)
				{
					if (roomTypeInfo.seasonCfgId > 0)
					{
						physicalCost = GetPhysicalCostPVESeason(isOwner);
					}
					else
					{
						physicalCost = GetPhysicalCostPVE(isOwner);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVP)
				{
					physicalCost = GetPhysicalCostPVP(isOwner);
				}
				else if (SubRoomTypeIn == SubRoomType.NormalEndlessChallenge)
				{
					if (roomTypeInfo.seasonCfgId > 0)
					{
						physicalCost = GetPhysicalCostEndlessChallengeSeason(isOwner);
					}
					else
					{
						physicalCost = GetPhysicalCostEndlessChallenge(isOwner);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalScanMesh)
				{
					physicalCost = 0;
				}
			}
			else if (RoomTypeIn == RoomType.AR)
			{
				if (SubRoomTypeIn == SubRoomType.ARPVE)
				{
					if (roomTypeInfo.seasonCfgId > 0)
					{
						physicalCost = GetPhysicalCostPVESeason(isOwner);
					}
					else
					{
						physicalCost = GetPhysicalCostPVE(isOwner);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARPVP)
				{
					physicalCost = GetPhysicalCostPVP(isOwner);
				}
				else if (SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					if (roomTypeInfo.seasonCfgId > 0)
					{
						physicalCost = GetPhysicalCostEndlessChallengeSeason(isOwner);
					}
					else
					{
						physicalCost = GetPhysicalCostEndlessChallenge(isOwner);
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.ArcadeScanMesh)
				{
					physicalCost = 0;
				}
			}

			return physicalCost;
		}


		public static int GetArcadeCoinCost(RoomTypeInfo roomTypeInfo, bool isRecover)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade() == false)
			{
				return 0;
			}
			RoomType RoomTypeIn = roomTypeInfo.roomType;
			SubRoomType SubRoomTypeIn = roomTypeInfo.subRoomType;
			int arcadeCoinCost = 0;
			if (RoomTypeIn == RoomType.Normal)
			{
				if (SubRoomTypeIn == SubRoomType.NormalRoom)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalARCreate)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVE)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.NormalPVP)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalEndlessChallenge)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.NormalScanMesh)
				{
					arcadeCoinCost = 0;
				}
			}
			else if (RoomTypeIn == RoomType.AR)
			{
				if (SubRoomTypeIn == SubRoomType.ARPVE)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.ARPVP)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.AREndlessChallenge)
				{
					if (isRecover)
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
					}
					else
					{
						arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart;
					}
				}
				else if (SubRoomTypeIn == SubRoomType.ARTutorialFirst)
				{
				}
				else if (SubRoomTypeIn == SubRoomType.ArcadeScanMesh)
				{
					arcadeCoinCost = 0;
				}
			}

			return arcadeCoinCost;
		}

		public static int GetArcadeCoinCost(GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent, bool isRecover)
		{
			if (ET.SceneHelper.ChkIsGameModeArcade() == false)
			{
				return 0;
			}
			int arcadeCoinCost = 0;
			if (gamePlayTowerDefenseComponent.IsPVPMode())
			{
				if (isRecover)
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenRevive;
				}
				else
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadePVPCostWhenStart;
				}
			}
			else if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
			{
				if (isRecover)
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenRevive;
				}
				else
				{
					arcadeCoinCost = GlobalSettingCfgCategory.Instance.GameModeArcadeEndlessChallengeCostWhenStart;
				}
			}
			return arcadeCoinCost;
		}

		public static async ETTask DoCreateActions(Unit unit, List<string> createActionIds, long playerId = (long)ET.PlayerId.PlayerNone)
		{
			if (createActionIds == null || createActionIds.Count == 0)
			{
				return;
			}

			bool isReady = await ET.AOIHelper.ChkAOIReady(null, unit);
			if (isReady == false)
			{
				return;
			}
			if (unit.IsDisposed)
			{
				return;
			}

			SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(unit);
			ActionContext actionContext = new ActionContext()
			{
				playerId = playerId,
				unitId = unit.Id,
			};
			foreach (var actionId in createActionIds)
			{
				if (string.IsNullOrEmpty(actionId))
				{
					continue;
				}
				ActionHandlerHelper.CreateAction(unit, null, actionId, 0.1f, selectHandleSelf, ref actionContext);
			}
		}

		public static float GetGamePlayNumericValueByPlayerId(Scene scene, long playerId, GameNumericType gameNumericType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			return gamePlayNumericComponent.GetPlayerNumeric(playerId, gameNumericType);
		}

		public static float GetGamePlayNumericValueByHomeTeamFlagType(Scene scene, TeamFlagType homeTeamFlagType, GameNumericType gameNumericType)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			return gamePlayNumericComponent.GetHomeTeamFlagTypeNumeric(homeTeamFlagType, gameNumericType);
		}

		public static void ChgGamePlayNumericValueByPlayerId(Scene scene, long playerId, GameNumericType gameNumericType, float chgValue, bool isReset)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			gamePlayNumericComponent.ChgPlayerNumeric(playerId, gameNumericType, chgValue, isReset);
		}

		public static void ChgGamePlayNumericValueByHomeTeamFlagType(Scene scene, TeamFlagType homeTeamFlagType, GameNumericType gameNumericType, float chgValue, bool isReset)
		{
			GamePlayComponent gamePlayComponent = GetGamePlay(scene);
			GamePlayNumericComponent gamePlayNumericComponent = gamePlayComponent.GetComponent<GamePlayNumericComponent>();
			gamePlayNumericComponent.ChgHomeTeamFlagTypeNumeric(homeTeamFlagType, gameNumericType, chgValue, isReset);
		}

        public static TeamFlagType GetHomeTeamFlagType(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.TeamPlayer1
                || teamFlagType == TeamFlagType.TeamPlayerSkill1
                || teamFlagType == TeamFlagType.Monster1
                || teamFlagType == TeamFlagType.TeamGlobal1)
            {
	            return TeamFlagType.TeamGlobal1;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer2
                     || teamFlagType == TeamFlagType.TeamPlayerSkill2
                     || teamFlagType == TeamFlagType.Monster2
                     || teamFlagType == TeamFlagType.TeamGlobal2)
            {
	            return TeamFlagType.TeamGlobal2;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer3
                     || teamFlagType == TeamFlagType.TeamPlayerSkill3
                     || teamFlagType == TeamFlagType.Monster3
                     || teamFlagType == TeamFlagType.TeamGlobal3)
            {
	            return TeamFlagType.TeamGlobal3;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer4
                     || teamFlagType == TeamFlagType.TeamPlayerSkill4
                     || teamFlagType == TeamFlagType.Monster4
                     || teamFlagType == TeamFlagType.TeamGlobal4)
            {
	            return TeamFlagType.TeamGlobal4;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer5
                     || teamFlagType == TeamFlagType.TeamPlayerSkill5
                     || teamFlagType == TeamFlagType.Monster5
                     || teamFlagType == TeamFlagType.TeamGlobal5)
            {
	            return TeamFlagType.TeamGlobal5;
            }
            else
            {
	            return TeamFlagType.None;
            }
        }

        public static TeamFlagType GetPlayerSkillTeamFlagType(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.TeamPlayer1
                || teamFlagType == TeamFlagType.TeamPlayerSkill1
                || teamFlagType == TeamFlagType.TeamGlobal1)
            {
	            return TeamFlagType.TeamPlayerSkill1;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer2
                     || teamFlagType == TeamFlagType.TeamPlayerSkill2
                     || teamFlagType == TeamFlagType.TeamGlobal2)
            {
	            return TeamFlagType.TeamPlayerSkill2;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer3
                     || teamFlagType == TeamFlagType.TeamPlayerSkill3
                     || teamFlagType == TeamFlagType.TeamGlobal3)
            {
	            return TeamFlagType.TeamPlayerSkill3;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer4
                     || teamFlagType == TeamFlagType.TeamPlayerSkill4
                     || teamFlagType == TeamFlagType.TeamGlobal4)
            {
	            return TeamFlagType.TeamPlayerSkill4;
            }
            else if (teamFlagType == TeamFlagType.TeamPlayer5
                     || teamFlagType == TeamFlagType.TeamPlayerSkill5
                     || teamFlagType == TeamFlagType.TeamGlobal5)
            {
	            return TeamFlagType.TeamPlayerSkill5;
            }
            else
            {
	            return TeamFlagType.None;
            }
        }

        public static TeamFlagType GetMonsterTeamFlagType(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.Monster1
                || teamFlagType == TeamFlagType.TeamPlayer1
                || teamFlagType == TeamFlagType.TeamPlayerSkill1
                || teamFlagType == TeamFlagType.TeamGlobal1)
            {
                return TeamFlagType.Monster1;
            }
            else if (teamFlagType == TeamFlagType.Monster2
                     || teamFlagType == TeamFlagType.TeamPlayer2
                     || teamFlagType == TeamFlagType.TeamPlayerSkill2
                     || teamFlagType == TeamFlagType.TeamGlobal2)
            {
                return TeamFlagType.Monster2;
            }
            else if (teamFlagType == TeamFlagType.Monster3
                     || teamFlagType == TeamFlagType.TeamPlayer3
                     || teamFlagType == TeamFlagType.TeamPlayerSkill3
                     || teamFlagType == TeamFlagType.TeamGlobal3)
            {
                return TeamFlagType.Monster3;
            }
            else if (teamFlagType == TeamFlagType.Monster4
                     || teamFlagType == TeamFlagType.TeamPlayer4
                     || teamFlagType == TeamFlagType.TeamPlayerSkill4
                     || teamFlagType == TeamFlagType.TeamGlobal4)
            {
                return TeamFlagType.Monster4;
            }
            else if (teamFlagType == TeamFlagType.Monster5
                     || teamFlagType == TeamFlagType.TeamPlayer5
                     || teamFlagType == TeamFlagType.TeamPlayerSkill5
                     || teamFlagType == TeamFlagType.TeamGlobal5)
            {
                return TeamFlagType.Monster5;
            }
            else
            {
                return TeamFlagType.Monster1;
            }
        }

        public static bool ChkIsTeamGlobal(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.TeamGlobal
                || teamFlagType == TeamFlagType.TeamGlobal1
                || teamFlagType == TeamFlagType.TeamGlobal2
                || teamFlagType == TeamFlagType.TeamGlobal3
                || teamFlagType == TeamFlagType.TeamGlobal4
                || teamFlagType == TeamFlagType.TeamGlobal5)
            {
                return true;
            }
            else
			{
				return false;
			}
        }

        public static bool ChkIsTeamPlayer(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.TeamPlayer
                || teamFlagType == TeamFlagType.TeamPlayer1
                || teamFlagType == TeamFlagType.TeamPlayer2
                || teamFlagType == TeamFlagType.TeamPlayer3
                || teamFlagType == TeamFlagType.TeamPlayer4
                || teamFlagType == TeamFlagType.TeamPlayer5)
            {
                return true;
            }
            else
			{
				return false;
			}
        }

        public static bool ChkIsTeamPlayerSkill(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.TeamPlayerSkill
                || teamFlagType == TeamFlagType.TeamPlayerSkill1
                || teamFlagType == TeamFlagType.TeamPlayerSkill2
                || teamFlagType == TeamFlagType.TeamPlayerSkill3
                || teamFlagType == TeamFlagType.TeamPlayerSkill4
                || teamFlagType == TeamFlagType.TeamPlayerSkill5)
            {
                return true;
            }
            else
			{
				return false;
			}
        }

        public static bool ChkIsTeamMonster(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.Monster
                || teamFlagType == TeamFlagType.Monster1
                || teamFlagType == TeamFlagType.Monster2
                || teamFlagType == TeamFlagType.Monster3
                || teamFlagType == TeamFlagType.Monster4
                || teamFlagType == TeamFlagType.Monster5)
            {
                return true;
            }
            else
			{
				return false;
			}
        }

        public static bool ChkIsTeamWildMonster(TeamFlagType teamFlagType)
        {
            if (teamFlagType == TeamFlagType.TeamWildMonster)
            {
                return true;
            }
            else
			{
				return false;
			}
        }

        public static TeamFlagType GetHomeTeamFlagTypeByPlayer(Scene scene, long playerId)
        {
	        TeamFlagType teamFlagType = GetGamePlay(scene)?.GetTeamFlagByPlayerId(playerId) ?? TeamFlagType.None;

	        return GetHomeTeamFlagType(teamFlagType);
        }

        public static TeamFlagType GetPlayerSkillTeamFlagTypeByPlayer(Scene scene, long playerId)
        {
	        TeamFlagType teamFlagType = GetGamePlay(scene)?.GetTeamFlagByPlayerId(playerId) ?? TeamFlagType.None;

	        return GetPlayerSkillTeamFlagType(teamFlagType);
        }

        public static TeamFlagType GetMonsterTeamFlagTypeByPlayer(Scene scene, long playerId)
        {
	        TeamFlagType teamFlagType = GetGamePlay(scene).GetTeamFlagByPlayerId(playerId);

	        return GetMonsterTeamFlagType(teamFlagType);
        }

        public static float3 GetRandomPoint(Scene scene, CallActorPositionType callActorPositionType)
        {
	        GamePlayComponent gamePlayComponent = GetGamePlay(scene);
	        if (gamePlayComponent == null)
	        {
		        return float3.zero;
	        }
	        RandomPointManagerComponent randomPointManagerComponent = gamePlayComponent.GetComponent<RandomPointManagerComponent>();
	        return randomPointManagerComponent.GetRandomPoint(callActorPositionType);
        }

        public static List<float3> GetRandomPointList(Scene scene, CallActorPositionType callActorPositionType, int num)
        {
	        GamePlayComponent gamePlayComponent = GetGamePlay(scene);
	        if (gamePlayComponent == null)
	        {
		        return null;
	        }
	        RandomPointManagerComponent randomPointManagerComponent = gamePlayComponent.GetComponent<RandomPointManagerComponent>();
	        return randomPointManagerComponent.GetRandomPointList(callActorPositionType, num);
        }

        #region 文件操作
        public static async Task<byte[]> ReadMeshFileBytes(string fileName)
        {
	        await ETTask.CompletedTask;
            string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalMeshSavePath, string>(new ConfigComponent.GetLocalMeshSavePath());

            string file = System.IO.Path.Combine(savePath, fileName);
            (bool bRet, byte[] bytes) = FileHelper.ReadFileBytes(file);
            return bytes;
        }

        public static async Task<string> ReadMeshFileText(string fileName)
        {
	        await ETTask.CompletedTask;
            string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalMeshSavePath, string>(new ConfigComponent.GetLocalMeshSavePath());

            string file = System.IO.Path.Combine(savePath, fileName);
            (bool bRet, string content) = FileHelper.ReadFileText(file);
            return content;
        }

        public static async Task WriteMeshFileBytes(string fileName, byte[] data)
        {
	        await ETTask.CompletedTask;
            string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalMeshSavePath, string>(new ConfigComponent.GetLocalMeshSavePath());

            string file = System.IO.Path.Combine(savePath, fileName);
            FileHelper.WriteFileBytes(file, data, false);
        }

        public static async Task WriteMeshFileText(string fileName, string content)
        {
	        await ETTask.CompletedTask;
            string savePath = EventSystem.Instance.Invoke<ConfigComponent.GetLocalMeshSavePath, string>(new ConfigComponent.GetLocalMeshSavePath());

            string file = System.IO.Path.Combine(savePath, fileName);
            FileHelper.WriteFileText(file, content, false);
        }

        public static async Task<byte[]> DownloadFileBytesAsync(Entity entity, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Error("Empty URL, return an empty task.");
                return null;
            }

            int retryNum = 5;
            for (int i = 0; i < retryNum; i++)
            {
	            (bool bRet, byte[] bytes) = await ET.HttpHelper.DownloadFileBytesAsync(url);
	            if (bRet)
	            {
		            return bytes;
	            }
	            else
	            {
		            await TimerComponent.Instance.WaitAsync(1000);
		            if (entity.IsDisposed)
		            {
			            return null;
		            }
	            }
            }
            return null;
        }

        public static async Task<string> DownloadFileTextAsync(Entity entity, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Info("Empty URL, return an empty task.");
                return null;
            }

            int retryNum = 30;
            for (int i = 0; i < retryNum; i++)
            {
	            (bool bRet, string content) = await ET.HttpHelper.DownloadFileTextAsync(url);
	            if (bRet)
	            {
		            return content;
	            }
	            else
	            {
		            await TimerComponent.Instance.WaitAsync(1000);
		            if (entity.IsDisposed)
		            {
			            return null;
		            }
	            }
            }
            return null;

        }
        #endregion

	}
}