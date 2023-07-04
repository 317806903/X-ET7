using ET.Ability;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    [FriendOf(typeof(GamePlayComponent))]
    [FriendOf(typeof(Unit))]
    public static class GamePlayComponentSystem
	{
		[ObjectSystem]
		public class GamePlayComponentAwakeSystem : AwakeSystem<GamePlayComponent>
		{
			protected override void Awake(GamePlayComponent self)
			{
				self.GamePlayStatus = GamePlayStatus.ScanMap;
				self.Init();
			}
		}
	
		[ObjectSystem]
		public class GamePlayComponentDestroySystem : DestroySystem<GamePlayComponent>
		{
			protected override void Destroy(GamePlayComponent self)
			{
			}
		}

		[ObjectSystem]
		public class GamePlayComponentFixedUpdateSystem: FixedUpdateSystem<GamePlayComponent>
		{
			protected override void FixedUpdate(GamePlayComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void FixedUpdate(this GamePlayComponent self, float fixedDeltaTime)
		{
		}

		public static Unit GetHomeUnit(this GamePlayComponent self)
		{
			return self.GetComponent<PutHomeComponent>().GetHomeUnit();
		}
		
		public static float3 GetHomePosition(this GamePlayComponent self)
		{
			return self.GetComponent<PutHomeComponent>().GetPosition();
		}

		public static float3 GetCallMonsterPosition(this GamePlayComponent self)
		{
			return self.GetComponent<PutMonsterCallComponent>().GetPosition();
		}

		public static long GetOwnerPlayerId(this GamePlayComponent self)
		{
			return self.Parent.GetComponent<RoomComponent>().ownerRoomMemberId;
		}

		public static List<RoomMember> GetPlayerList(this GamePlayComponent self)
		{
			return self.Parent.GetComponent<RoomComponent>().GetRoomMemberList();
		}

		public static RoomComponent GetRoomComponent(this GamePlayComponent self)
		{
			return self.Parent.GetComponent<RoomComponent>();
		}

		public static void NoticeToClientAll(this GamePlayComponent self)
		{
			List<RoomMember> roomMembers = self.GetPlayerList();
			for (int i = 0; i < roomMembers.Count; i++)
			{
				self.NoticeToClient(roomMembers[i].Id);
			}
		}

		public static void NoticeToClient(this GamePlayComponent self, long playerId)
		{
			EventType.NoticeGamePlayToClient _NoticeGamePlayChgToClient = new ()
			{
				playerId = playerId,
			};
			EventSystem.Instance.Publish(self.DomainScene(), _NoticeGamePlayChgToClient);
		}

		public static void Init(this GamePlayComponent self)
		{
			self.ownerPlayerId = self.GetOwnerPlayerId();
			self.AddComponent<PlayerOwnerTowersComponent>();
			MonsterWaveCallComponent monsterWaveCallComponent = self.AddComponent<MonsterWaveCallComponent>();
			monsterWaveCallComponent.Init("Wave1");
			//monsterWaveCallComponent.Init("WaveTest");
			
			self.TransToPutHome();
		}

		public static void DealFriendTeamFlagType(this GamePlayComponent self)
        {
	        ListComponent<TeamFlagType> teamFlagTypes = ListComponent<TeamFlagType>.Create();
	        foreach (RoomMember roomMember in self.GetPlayerList())
	        {
		        TeamFlagType teamFlagType = ET.Ability.TeamFlagHelper.GetTeamFlagTypeBySeatIndex(roomMember.seatIndex);
		        teamFlagTypes.Add(teamFlagType);
	        }

	        TeamFlagType teamFlagTypeHome = self.GetHomeUnit().GetComponent<TeamFlagObj>().GetTeamFlagType();
	        teamFlagTypes.Add(teamFlagTypeHome);
	        
	        ET.Ability.TeamFlagHelper.AddFriendTeamFlag(self.DomainScene(), teamFlagTypes, true);
        }

		public static async ETTask DownloadMapRecast(this GamePlayComponent self)
        {
            await ETTask.CompletedTask;
        }

		public static void TransToPutHome(this GamePlayComponent self)
		{
			self.GamePlayStatus = GamePlayStatus.PutHome;
			self.NoticeToClientAll();
		}

		public static void TransToPutMonsterPoint(this GamePlayComponent self)
		{
			self.GamePlayStatus = GamePlayStatus.PutMonsterPoint;
			self.NoticeToClientAll();
		}

		public static void TransToBattle(this GamePlayComponent self)
		{
			self.RemoveComponent<RestTimeComponent>();
			self.GetComponent<MonsterWaveCallComponent>().DoNextMonsterWaveCall();
			self.GamePlayStatus = GamePlayStatus.InTheBattle;
			self.NoticeToClientAll();
		}

		public static void TransToRestTime(this GamePlayComponent self)
		{
			self.AddComponent<RestTimeComponent>();
			
			self.GamePlayStatus = GamePlayStatus.RestTime;
			self.NoticeToClientAll();
		}

		public static void TransToGameSuccess(this GamePlayComponent self)
		{
			self.GamePlayStatus = GamePlayStatus.GameSuccess;
			self.NoticeToClientAll();
		}

		public static void TransToGameFailed(this GamePlayComponent self)
		{
			self.GamePlayStatus = GamePlayStatus.GameFailed;
			self.NoticeToClientAll();
		}

	}
}