using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(RestTimeComponent))]
    public static class RestTimeComponentSystem
	{
		[ObjectSystem]
		public class RestTimeComponentAwakeSystem : AwakeSystem<RestTimeComponent>
		{
			protected override void Awake(RestTimeComponent self)
			{
				self.isPlayerReadyForBattle = new();
			}
		}

		[ObjectSystem]
		public class RestTimeComponentDestroySystem : DestroySystem<RestTimeComponent>
		{
			protected override void Destroy(RestTimeComponent self)
			{
				self.isPlayerReadyForBattle.Clear();
			}
		}

		[ObjectSystem]
		public class RestTimeComponentFixedUpdateSystem: FixedUpdateSystem<RestTimeComponent>
		{
			protected override void FixedUpdate(RestTimeComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void Init(this RestTimeComponent self, float duration)
		{
			self.duration = duration;
			self.timeElapsed = 0;

			self.isPlayerReadyForBattle.Clear();
			List<long> playerList = self.GetGamePlayTowerDefense().GetPlayerList();
			foreach (long playerId in playerList)
			{
				self.isPlayerReadyForBattle[playerId] = false;
			}
		}

		public static GamePlayComponent GetGamePlay(this RestTimeComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			GamePlayComponent gamePlayComponent = gamePlayTowerDefenseComponent.GetParent<GamePlayComponent>();
			return gamePlayComponent;
		}

		public static GamePlayTowerDefenseComponent GetGamePlayTowerDefense(this RestTimeComponent self)
		{
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetParent<GamePlayTowerDefenseComponent>();
			return gamePlayTowerDefenseComponent;
		}

		public static void SetReadyWhenRestTime(this RestTimeComponent self, long playerId)
		{
			self.isPlayerReadyForBattle[playerId] = true;
		}

		public static void FixedUpdate(this RestTimeComponent self, float fixedDeltaTime)
		{
			if (self.GetGamePlayTowerDefense().ChkIsGameEnd()
			    || self.GetGamePlayTowerDefense().ChkIsGameWaitRescan()
			    || self.GetGamePlayTowerDefense().ChkIsGameRecover()
			    || self.GetGamePlayTowerDefense().ChkIsGameRecovering())
			{
				return;
			}

			if (self.duration > 0)
			{
				self.duration -= fixedDeltaTime;
			}

			self.timeElapsed += fixedDeltaTime;

			bool needTransToBattle = false;
			if (self.duration <= 0)
			{
				needTransToBattle = true;
			}
			else
			{
				needTransToBattle = true;
				foreach (var isPlayerReady in self.isPlayerReadyForBattle)
				{
					if (isPlayerReady.Value == false)
					{
						needTransToBattle = false;
						break;
					}
				}
			}
			if (needTransToBattle)
			{

				EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.GamePlayTowerDefense_Status_RestTimeEnd());

				self.GetGamePlayTowerDefense().DoNextStep().Coroutine();
				return;
			}
		}

	}
}