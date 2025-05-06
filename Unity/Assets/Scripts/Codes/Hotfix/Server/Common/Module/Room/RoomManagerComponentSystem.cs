using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(RoomManagerComponent))]
    public static class RoomManagerComponentSystem
	{
		[ObjectSystem]
		public class RoomManagerComponentFixedUpdateSystem: FixedUpdateSystem<RoomManagerComponent>
		{
			protected override void FixedUpdate(RoomManagerComponent self)
			{
				if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Room)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this RoomManagerComponent self, float fixedDeltaTime)
		{
			if (++self.curFrameChk >= self.waitFrameChk)
			{
				self.curFrameChk = 0;

				self.ChkPlayerOffline().Coroutine();
			}
		}

		public static async ETTask ChkPlayerOffline(this RoomManagerComponent self)
		{
			PlayerLocationChkComponent playerLocationChkComponent = self.GetComponent<PlayerLocationChkComponent>();

			using ListComponent<long> playerList = ListComponent<long>.Create();
			playerList.AddRange(self.player2Room.Keys);
			playerLocationChkComponent.SetChkPlayerOfflineList(playerList);

			HashSet<long> notExistPlayerList = playerLocationChkComponent.GetPlayerOfflineList();
			foreach (long playerId in notExistPlayerList)
			{
				if (self.IsDisposed)
				{
					return;
				}
				if (self.player2Room.ContainsKey(playerId))
				{
					await ET.Server.M2R_MemberQuitRoomHandler.KickMember(self.DomainScene(), playerId, self.player2Room[playerId]);
				}
			}
			playerLocationChkComponent.ClearPlayerOfflineList();
		}
	}
}