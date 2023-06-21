using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(RoomManagerComponent))]
    public static class RoomManagerComponentSystem
    {
        [ObjectSystem]
        public class RoomManagerComponentAwakeSystem : AwakeSystem<RoomManagerComponent>
        {
            protected override void Awake(RoomManagerComponent self)
            {
                self.IdleRoomList = new();
                self.EnterBattleRoomList = new();
                self.InTheBattleRoomList = new();
                self.player2Room = new();
            }
        }

        public static RoomComponent CreateRoom(this RoomManagerComponent self, long playerId, RoomTeamMode roomTeamMode, string sceneName)
        {
            RoomComponent roomComponent = self.AddChild<RoomComponent>();
            roomComponent.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
            roomComponent.Init(playerId, roomTeamMode, sceneName);

            self.IdleRoomList.Add(roomComponent.Id);
            self.player2Room.Add(playerId, roomComponent.Id);

            return roomComponent;
        }
        
        public static void JoinRoom(this RoomManagerComponent self, long playerId, long roomId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            if (roomComponent.ownerRoomMemberId == -1)
            {
                roomComponent.ownerRoomMemberId = playerId;
                roomComponent.AddRoomMember(playerId, true, RoomTeamId.Red, 0);
            }
            else
            {
                roomComponent.AddRoomMember(playerId, false, RoomTeamId.Red, -1);
            }
            self.player2Room.Add(playerId, roomComponent.Id);
        }
        
        public static void QuitRoom(this RoomManagerComponent self, long playerId, long roomId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            bool isEmptyMember = roomComponent.RemoveRoomMember(playerId);
            self.player2Room.Remove(playerId);
            if (isEmptyMember)
            {
                //self.DestroyRoom(roomId);
            }
        }
        
        public static void ChgRoomStatus(this RoomManagerComponent self, long roomId, RoomStatus roomStatus)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            roomComponent.ChgRoomStatus(roomStatus);

            self.IdleRoomList.Remove(roomId);
            self.EnterBattleRoomList.Remove(roomId);
            self.InTheBattleRoomList.Remove(roomId);
            switch (roomStatus)
            {
                case RoomStatus.Idle:
                    self.IdleRoomList.Add(roomId);
                    self.EnterBattleRoomList.Remove(roomId);
                    self.InTheBattleRoomList.Remove(roomId);
                    break;
                case RoomStatus.EnterBattle:
                    self.IdleRoomList.Remove(roomId);
                    self.EnterBattleRoomList.Add(roomId);
                    self.InTheBattleRoomList.Remove(roomId);
                    break;
                case RoomStatus.InTheBattle:
                    self.IdleRoomList.Remove(roomId);
                    self.EnterBattleRoomList.Remove(roomId);
                    self.InTheBattleRoomList.Add(roomId);
                break;
            }
        }

        public static RoomComponent GetRoom(this RoomManagerComponent self, long roomId)
        {
            return self.GetChild<RoomComponent>(roomId);
        }

        public static void DestroyRoom(this RoomManagerComponent self, long roomId)
        {
            self.IdleRoomList.Remove(roomId);
            self.EnterBattleRoomList.Remove(roomId);
            self.InTheBattleRoomList.Remove(roomId);
            RoomComponent roomComponent = self.GetRoom(roomId);
            List<RoomMember> roomMemberList = roomComponent.GetRoomMemberList();
            foreach (RoomMember roomMember in roomMemberList)
            {
                self.player2Room.Remove(roomMember.Id);
            }
            roomComponent.Dispose();
        }
        
        //获取当前房间列表
        public static HashSet<long> GetIdleRoomList(this RoomManagerComponent self)
        {
            return self.IdleRoomList;
        }

        public static RoomComponent GetRoomByPlayerId(this RoomManagerComponent self, long playerId)
        {
            if (self.player2Room.ContainsKey(playerId))
            {
                return self.GetRoom(self.player2Room[playerId]);
            }
            return null;
        }

    }
}