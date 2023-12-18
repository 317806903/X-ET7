using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(RoomComponent))]
    public static class RoomComponentSystem
	{
		[ObjectSystem]
		public class RoomComponentFixedUpdateSystem: FixedUpdateSystem<RoomComponent>
		{
			protected override void FixedUpdate(RoomComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Room)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this RoomComponent self, float fixedDeltaTime)
		{
			if (++self.curFrameChk >= self.waitFrameChk)
			{
				self.curFrameChk = 0;

				self.ChkPlayerOffline().Coroutine();
			}
		}

		public static async ETTask ChkPlayerOffline(this RoomComponent self)
		{
			if (self.playerWaitQuitTime == null)
			{
				self.playerWaitQuitTime = new();
			}

			foreach (RoomMember roomMember in self.GetRoomMemberList())
			{
				long playerId = roomMember.Id;

				long locationActorId = await LocationProxyComponent.Instance.Get(LocationType.Player, playerId);
				if (locationActorId == 0)
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
				// if (oneTypeLocationType.GetChild<Entity>(playerId) == null)
				// {
				// 	if (self.playerWaitQuitTime.ContainsKey(playerId) == false)
				// 	{
				// 		self.playerWaitQuitTime[playerId] = TimeHelper.ServerNow() + 5000;
				// 	}
				// }
				// else
				// {
				// 	if (self.playerWaitQuitTime.ContainsKey(playerId))
				// 	{
				// 		self.playerWaitQuitTime.Remove(playerId);
				// 	}
				// }
			}

			foreach (var playerWaitQuitTime in self.playerWaitQuitTime)
			{
				long playerId = playerWaitQuitTime.Key;
				long time = playerWaitQuitTime.Value;
				if (time != -1 && time < TimeHelper.ServerNow())
				{
					self.playerWaitQuitTime[playerId] = -1;

					ET.Server.M2R_MemberQuitRoomHandler.KickMember(self.DomainScene(), playerId, self.Id).Coroutine();
				}
			}
		}

	}
}