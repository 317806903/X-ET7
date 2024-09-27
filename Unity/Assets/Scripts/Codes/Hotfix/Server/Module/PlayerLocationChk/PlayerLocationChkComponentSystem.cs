using ET.Ability;
using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof(PlayerLocationChkComponent))]
    public static class PlayerLocationChkComponentSystem
	{
		[ObjectSystem]
		public class PlayerLocationChkComponentFixedUpdateSystem: FixedUpdateSystem<PlayerLocationChkComponent>
		{
			protected override void FixedUpdate(PlayerLocationChkComponent self)
			{
				if (self.IsDisposed)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this PlayerLocationChkComponent self, float fixedDeltaTime)
		{
			if (++self.curFrameChk >= self.waitFrameChk)
			{
				self.curFrameChk = 0;

				self.ChkPlayerOffline().Coroutine();
			}
		}

		public static async ETTask ChkPlayerOffline(this PlayerLocationChkComponent self)
		{
			if (self.isChking)
			{
				return;
			}
			try
			{
				self.isChking = true;
				await self._ChkPlayerOffline();
			}
			finally
			{
				self.isChking = false;
			}
		}

		public static async ETTask _ChkPlayerOffline(this PlayerLocationChkComponent self)
		{
			if (self.playerListIn == null || self.playerListIn.Count == 0)
			{
				return;
			}
			self.existPlayerListTmp.Clear();

			List<long> notExistPlayerListTmp = await LocationProxyComponent.Instance.ChkObjectListExist(LocationType.Player, self.playerListIn, self.DomainScene().InstanceId);
			foreach (long playerId in self.playerListIn)
			{
				self.existPlayerListTmp.Add(playerId);
			}
			if (notExistPlayerListTmp != null)
			{
				foreach (long playerId in notExistPlayerListTmp)
				{
					self.existPlayerListTmp.Remove(playerId);
				}

				foreach (long playerId in notExistPlayerListTmp)
				{
					if (self.playerWaitQuitTimeTmp.ContainsKey(playerId) == false)
					{
						self.playerWaitQuitTimeTmp[playerId] = TimeHelper.ServerNow() + 5000;
					}
				}
			}
			foreach (long playerId in self.existPlayerListTmp)
			{
				if (self.playerWaitQuitTimeTmp.ContainsKey(playerId))
				{
					self.playerWaitQuitTimeTmp.Remove(playerId);
				}
			}

			while (true)
			{
				bool bWhile = false;
				foreach (var playerWaitQuitTime in self.playerWaitQuitTimeTmp)
				{
					long playerId = playerWaitQuitTime.Key;
					long time = playerWaitQuitTime.Value;
					if (time != -1 && time < TimeHelper.ServerNow())
					{
						self.playerWaitQuitTimeTmp[playerId] = -1;
						bWhile = true;

						self.notExistPlayerList.Add(playerId);

						break;
					}
				}

				if (bWhile == false)
				{
					break;
				}
			}
		}

		public static void SetChkPlayerOfflineList(this PlayerLocationChkComponent self, List<long> playerList)
		{
			if (self.isChking)
			{
				return;
			}
			self.playerListIn.Clear();
			foreach (long playerId in playerList)
			{
				if (self.notExistPlayerList.Contains(playerId))
				{
					continue;
				}
				self.playerListIn.Add(playerId);
			}
		}

		public static HashSet<long> GetPlayerOfflineList(this PlayerLocationChkComponent self)
		{
			return self.notExistPlayerList;
		}

		public static void ClearPlayerOfflineList(this PlayerLocationChkComponent self)
		{
			self.notExistPlayerList.Clear();
		}
	}
}