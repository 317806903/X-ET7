using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[Invoke(TimerInvokeType.GamePlayChkTimer)]
	public class GamePlayComponentTimer: ATimer<GamePlayComponent>
	{
		protected override void Run(GamePlayComponent self)
		{
			try
			{
				self.Update();
			}
			catch (Exception e)
			{
				Log.Error($"move timer error: {self.Id}\n{e}");
			}
		}
	}
	
    [FriendOf(typeof(GamePlayComponent))]
    public static class GamePlayComponentSystem
	{
		[ObjectSystem]
		public class GamePlayComponentAwakeSystem : AwakeSystem<GamePlayComponent>
		{
			protected override void Awake(GamePlayComponent self)
			{
				self.Timer = TimerComponent.Instance.NewRepeatedTimer(5000, TimerInvokeType.GamePlayChkTimer, self);
			}
		}
	
		[ObjectSystem]
		public class GamePlayComponentDestroySystem : DestroySystem<GamePlayComponent>
		{
			protected override void Destroy(GamePlayComponent self)
			{
				TimerComponent.Instance?.Remove(ref self.Timer);
			}
		}

		public static void Update(this GamePlayComponent self)
		{
			bool willDestroy = self.ChkGamePlayWaitDestroy();
			if (willDestroy == false)
			{
				self.ChkPlayerWaitDestroy();
			}
		}

		public static bool ChkGamePlayWaitDestroy(this GamePlayComponent self)
		{
			if (self.gamePlayWaitDestroyTime == 0)
			{
				if (self.ChkIsNeedDestroy())
				{
					self.gamePlayWaitDestroyTime = TimeHelper.ServerNow() + 5000;
					return true;
				}
				return false;
			}
			else if(self.gamePlayWaitDestroyTime < TimeHelper.ServerNow())
			{
				self.TrigDestroyGamePlay().Coroutine();
			}
			return true;
		}

		public static async ETTask TrigDestroyGamePlay(this GamePlayComponent self)
		{
			// foreach (long playerId in self.GetPlayerList())
			// {
			// 	await self.TrigDestroyPlayer(playerId);
			// }
			
			long dynamicMapInstanceId = self.dynamicMapInstanceId;
			DynamicMapManagerComponent dynamicMapManagerComponent = self.GetParent<Scene>().GetParent<DynamicMapManagerComponent>();
			if (dynamicMapManagerComponent != null)
			{
				await dynamicMapManagerComponent.DestroyDynamicMap(dynamicMapInstanceId);
			}
            await ETTask.CompletedTask;
        }
		
		public static async ETTask TrigDestroyPlayer(this GamePlayComponent self, long playerId)
		{
			M2G_MemberQuitBattle _M2G_MemberQuitBattle = new();
			ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			await oneTypeLocationType.Call(playerId, _M2G_MemberQuitBattle);

			Unit unit = ET.Ability.UnitHelper.GetUnit(self.DomainScene(), playerId);
			if (unit != null)
			{
				await unit.RemoveLocation(LocationType.Unit);
            }
            await ETTask.CompletedTask;
        }
		
		public static bool ChkIsNeedDestroy(this GamePlayComponent self)
		{
			// if (self.ChkIsGameEnd())
			// {
			// 	return true;
			// }
			if (self.GetPlayerList().Count == 0)
			{
				return true;
			}

			return false;
		}

		public static void ChkPlayerWaitDestroy(this GamePlayComponent self)
		{
			if (self.playerWaitQuitTime == null)
			{
				self.playerWaitQuitTime = new();
			}
			
			ActorLocationSenderOneType oneTypeLocationType = ActorLocationSenderComponent.Instance.Get(LocationType.Player);
			foreach (long playerId in self.GetPlayerList())
			{
				if (oneTypeLocationType.GetChild<Entity>(playerId) == null)
				{
					if (self.playerWaitQuitTime.ContainsKey(playerId) == false)
					{
						self.playerWaitQuitTime[playerId] = TimeHelper.ServerNow() + 5000;
					}
				}
				else
				{
					if (self.playerWaitQuitTime.ContainsKey(playerId))
					{
						self.playerWaitQuitTime.Remove(playerId);
					}
				}
			}

			foreach (var playerWaitQuitTime in self.playerWaitQuitTime)
			{
				long playerId = playerWaitQuitTime.Key;
				long time = playerWaitQuitTime.Value;
				if (time != -1 && time < TimeHelper.ServerNow())
				{
					self.playerWaitQuitTime[playerId] = -1;
					self.TrigDestroyPlayer(playerId).Coroutine();
					break;
				}
			}

		}
	}
}