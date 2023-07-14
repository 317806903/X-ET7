using System.Collections.Generic;
using System.Linq;

namespace ET
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

        public static RoomComponent CreateRoom(this RoomManagerComponent self, bool isARRoom, long playerId, RoomTeamMode roomTeamMode, string battleCfgId)
        {
            RoomComponent roomComponent = self.AddChild<RoomComponent>();
            roomComponent.Init(isARRoom, playerId, roomTeamMode, battleCfgId);

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
        
        public static bool QuitRoom(this RoomManagerComponent self, long playerId, long roomId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            bool isEmptyMember = roomComponent.RemoveRoomMember(playerId);
            if (self.player2Room.ContainsKey(playerId) && self.player2Room[playerId] == roomId)
            {
                self.player2Room.Remove(playerId);
            }
            
            if (isEmptyMember)
            {
                self.DestroyRoom(roomId);
            }

            return isEmptyMember;
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
                case RoomStatus.EnteringBattle:
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
        public static List<long> GetIdleRoomList(this RoomManagerComponent self, bool isARRoom)
        {
            ListComponent<long> list = ListComponent<long>.Create();
            foreach (long idleRoomId in self.IdleRoomList)
            {
                RoomComponent roomComponent = self.GetRoom(idleRoomId);
                if (roomComponent.isARRoom == isARRoom)
                {
                    list.Add(roomComponent.Id);
                }
            }
            return list;
        }

        public static List<RoomComponent> GetRoomList(this RoomManagerComponent self)
        {
            ListComponent<RoomComponent> list =ListComponent<RoomComponent>.Create();
            foreach (var child in self.Children)
            {
                list.Add(child.Value as RoomComponent);
            }
            return list;
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