using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET.Server
{
    [FriendOf(typeof(GameRecoverOnceComponent))]
    public static class GameRecoverOnceComponentSystem
	{
		[ObjectSystem]
		public class GameRecoverOnceComponentFixedUpdateSystem: FixedUpdateSystem<GameRecoverOnceComponent>
		{
			protected override void FixedUpdate(GameRecoverOnceComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void _ClearPlayerRecoverStatusDic(this GameRecoverOnceComponent self)
		{
			foreach (var item in self.playerRecoverStatusDic)
			{
				long playerId = item.Key;
				bool isQuit = self.GetGamePlayTowerDefense().ChkPlayerIsQuit(playerId);
				if (isQuit)
				{
					self.playerRecoverStatusDic.Remove(playerId);
					break;
				}

				PlayerRecoverStatus playerRecoverStatus = item.Value;
				if (playerRecoverStatus == PlayerRecoverStatus.Cancel)
				{
					if (self.playerRecoverStatusDic.Count == 1)
					{
						self.GetGamePlayTowerDefense().TransToGameEnd(false).Coroutine();
						self.Dispose();
						return;
					}
					else
					{
						self.GetGamePlayTowerDefense().DealPlayerCancelRecover(playerId).Coroutine();
						self.playerRecoverStatusDic.Remove(playerId);
						break;
					}
				}
			}
		}

		public static void SetPlayerRecoverStatus(this GameRecoverOnceComponent self, long playerId, PlayerRecoverStatus playerRecoverStatus)
		{
			self.playerRecoverStatusDic[playerId] = playerRecoverStatus;
		}

		public static async ETTask DealNext(this GameRecoverOnceComponent self)
		{
			//触发条件： 如果所有人都做了选择 或 时间到
			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = self.GetGamePlayTowerDefense();
			List<long> confirmPlayerList = ListComponent<long>.Create();
			List<long> defaultPlayerList = ListComponent<long>.Create();
			foreach (var item in self.playerRecoverStatusDic)
			{
				if (item.Value == PlayerRecoverStatus.Confirm)
				{
					confirmPlayerList.Add(item.Key);
				}
				else
				{
					defaultPlayerList.Add(item.Key);
				}
			}

			//如果没人继续，这推动 gamePlay结束
			if (confirmPlayerList.Count == 0)
			{
				await gamePlayTowerDefenseComponent.TransToGameEnd(false);
				return;
			}

			//如果存在Default则踢出战斗，同时发送结算，
			foreach (var playerId in defaultPlayerList)
			{
				await gamePlayTowerDefenseComponent.DealPlayerCancelRecover(playerId);
			}

			//如果有人继续，这推动 gamePlay继续
			await gamePlayTowerDefenseComponent.TransToRecoverSucess();
		}

		public static void FixedUpdate(this GameRecoverOnceComponent self, float fixedDeltaTime)
		{
			self._ClearPlayerRecoverStatusDic();

			if (self.IsDisposed)
			{
				return;
			}

			bool needTransToBattle = false;
			if (self.timeOutTime <= TimeHelper.ServerNow())
			{
				needTransToBattle = true;
			}
			else
			{
				needTransToBattle = true;
				foreach (var isPlayerReady in self.playerRecoverStatusDic)
				{
					if (isPlayerReady.Value == PlayerRecoverStatus.Default)
					{
						needTransToBattle = false;
						break;
					}
				}
			}
			if (needTransToBattle)
			{
				self.DealNext().Coroutine();
				self.Dispose();
				return;
			}
		}

	}
}