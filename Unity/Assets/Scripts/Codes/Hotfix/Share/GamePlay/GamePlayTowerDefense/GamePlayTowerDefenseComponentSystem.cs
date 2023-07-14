using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayTowerDefenseComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayTowerDefenseComponentSystem
	{
		[ObjectSystem]
		public class GamePlayTowerDefenseComponentAwakeSystem : AwakeSystem<GamePlayTowerDefenseComponent>
		{
			protected override void Awake(GamePlayTowerDefenseComponent self)
			{
			}
		}
	
		[ObjectSystem]
		public class GamePlayTowerDefenseComponentDestroySystem : DestroySystem<GamePlayTowerDefenseComponent>
		{
			protected override void Destroy(GamePlayTowerDefenseComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayTowerDefenseComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayTowerDefenseComponent>
		{
			protected override void FixedUpdate(GamePlayTowerDefenseComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayTowerDefenseComponent self, float fixedDeltaTime)
		{
		}

		public static Unit GetHomeUnit(this GamePlayTowerDefenseComponent self)
		{
			return self.GetComponent<PutHomeComponent>().GetHomeUnit();
		}
		
		public static float3 GetHomePosition(this GamePlayTowerDefenseComponent self)
		{
			return self.GetComponent<PutHomeComponent>().GetPosition();
		}

		public static float3 GetCallMonsterPosition(this GamePlayTowerDefenseComponent self, long playerId)
		{
			return self.GetComponent<PutMonsterCallComponent>().GetPosition(playerId);
		}

		public static List<long> GetPlayerList(this GamePlayTowerDefenseComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			return gamePlayComponent.GetPlayerList();
		}

		public static void NoticeToClientAll(this GamePlayTowerDefenseComponent self)
		{
			List<long> playerList = self.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				self.NoticeToClient(playerList[i]);
			}
		}

		public static void NoticeToClient(this GamePlayTowerDefenseComponent self, long playerId)
		{
			EventType.NoticeGamePlayModeToClient _NoticeGamePlayModeChgToClient = new ()
			{
				playerId = playerId,
				gamePlayModeComponent = self,
			};
			EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayModeChgToClient);
		}

		public static void Init(this GamePlayTowerDefenseComponent self, long ownerPlayerId, string gamePlayModeCfgId)
		{
			self.gamePlayModeCfgId = gamePlayModeCfgId;
			self.ownerPlayerId = ownerPlayerId;
			self.AddComponent<PlayerOwnerTowersComponent>();
			MonsterWaveCallComponent monsterWaveCallComponent = self.AddComponent<MonsterWaveCallComponent>();
			monsterWaveCallComponent.Init(self.model.MonsterWaveCallRuleCfgId);

			self.InitPlayerCoin();
			
			self.TransToPutHome();
		}

		public static void InitPlayerCoin(this GamePlayTowerDefenseComponent self)
		{
			int initGold = self.model.PlayerInitGold;
			List<long> playerList = self.GetPlayerList();
			for (int i = 0; i < playerList.Count; i++)
			{
				ET.GamePlayHelper.SetPlayerCoin(self.DomainScene(), playerList[i], CoinType.Gold, initGold);
			}
		}

		public static void Start(this GamePlayTowerDefenseComponent self)
		{
			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			gamePlayComponent.Start();

			self.TransToRestTime();
		}

		public static async ETTask DownloadMapRecast(this GamePlayTowerDefenseComponent self)
        {
            await ETTask.CompletedTask;
        }

		public static void TransToPutHome(this GamePlayTowerDefenseComponent self)
		{
			self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.PutHome;
			self.NoticeToClientAll();
		}

		public static void TransToPutMonsterPoint(this GamePlayTowerDefenseComponent self)
		{
			self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.PutMonsterPoint;
			self.NoticeToClientAll();
		}

		public static void TransToBattle(this GamePlayTowerDefenseComponent self)
		{
			self.RemoveComponent<RestTimeComponent>();
			self.GetComponent<MonsterWaveCallComponent>().DoNextMonsterWaveCall();
			self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.InTheBattle;
			self.NoticeToClientAll();
		}

		public static void TransToRestTime(this GamePlayTowerDefenseComponent self)
		{
			RestTimeComponent restTimeComponent = self.GetComponent<RestTimeComponent>();
			if (restTimeComponent == null)
			{
				restTimeComponent = self.AddComponent<RestTimeComponent>();
			}
			restTimeComponent.Init(self.model.ResTime);
			
			self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.RestTime;
			self.NoticeToClientAll();
		}

		public static void TransToGameSuccess(this GamePlayTowerDefenseComponent self)
		{
			self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameSuccess;

			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			gamePlayComponent.GameEnd();
			
			self.NoticeToClientAll();
		}

		public static void TransToGameFailed(this GamePlayTowerDefenseComponent self)
		{
			self.GamePlayTowerDefenseStatus = GamePlayTowerDefenseStatus.GameFailed;

			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			gamePlayComponent.GameEnd();

			self.NoticeToClientAll();
		}

		public static bool ChkIsGameEnd(this GamePlayTowerDefenseComponent self)
		{
			if (self.GamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameSuccess
				|| self.GamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.GameFailed)
			{
				return true;
			}

			return false;
		}

		public static GamePlayTowerDefenseStatus GetGamePlayTowerDefenseStatus(this GamePlayTowerDefenseComponent self)
		{
			return self.GamePlayTowerDefenseStatus;
		}

		public static bool BuyPlayerTower(this GamePlayTowerDefenseComponent self, Unit unit, int index)
		{
			return self.GetComponent<PlayerOwnerTowersComponent>().BuyPlayerTower(unit.Id, index);
		}
		
		public static bool RefreshBuyPlayerTower(this GamePlayTowerDefenseComponent self, Unit unit)
		{
			return self.GetComponent<PlayerOwnerTowersComponent>().RefreshTowerPool(unit.Id);
		}
		
		public static void CallPlayerTower(this GamePlayTowerDefenseComponent self, Unit unit, string towerId, float3 position)
		{
			self.GetComponent<PlayerOwnerTowersComponent>().CallPlayerTower(unit.Id, towerId, position);
		}

		public static void DealFriendTeamFlagType(this GamePlayTowerDefenseComponent self)
		{
			ListComponent<TeamFlagType> teamFlagTypes = ListComponent<TeamFlagType>.Create();
			teamFlagTypes.Add(TeamFlagType.TeamGlobal1);
			GamePlayComponent gamePlayComponent = self.GetParent<GamePlayComponent>();
			gamePlayComponent.DealFriendTeamFlag(teamFlagTypes, true, true);
		}

		public static void DealEscape(this GamePlayTowerDefenseComponent self, Unit unit)
		{
			NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

			long attackValue = numericComponent.GetAsInt(NumericType.PhysicalAttack);
			Damage damage = new(NumericType.PhysicalAttack, attackValue);
			ET.Ability.DamageHelper.CreateDamageInfo(unit, self.GetHomeUnit(), damage, 0, 0, null);
			unit.Dispose();
		}

		public static void DealUnitBeKill(this GamePlayTowerDefenseComponent self, Unit attackerUnit, Unit beKillUnit)
		{
			long attackerPlayerId = GamePlayHelper.GetPlayerIdByUnitId(attackerUnit);
			if (attackerPlayerId == -1)
			{
				return;
			}
			long beKillUnitPlayerId = GamePlayHelper.GetPlayerIdByUnitId(beKillUnit);
			if (beKillUnitPlayerId != -1)
			{
				return;
			}

			MonsterWaveCallComponent monsterWaveCallComponent = self.GetComponent<MonsterWaveCallComponent>();
			int rewardGold = monsterWaveCallComponent.GetMonsterRewardGoldByUnitId(beKillUnit.Id);
			if (rewardGold > 0)
			{
				ET.GamePlayHelper.ChgPlayerCoin(self.DomainScene(), attackerPlayerId, CoinType.Gold, rewardGold);
			}
		}

	}
}