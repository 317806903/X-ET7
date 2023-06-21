using System.Collections.Generic;

namespace ET.Server
{
    [FriendOf(typeof(RoomComponent))]
    public static class RoomComponentSystem
    {
        [ObjectSystem]
        public class RoomComponentAwakeSystem : AwakeSystem<RoomComponent>
        {
            protected override void Awake(RoomComponent self)
            {
                self.roomMemberSeat = new();
                for (int i = 0; i < 10; i++)
                {
                    self.roomMemberSeat.Add(-1);
                }
            }
        }
        
        public static void Init(this RoomComponent self, long playerId, RoomTeamMode roomTeamMode, string sceneName)
        {
            self.roomStatus = RoomStatus.Idle;
            self.ownerRoomMemberId = playerId;
            self.roomTeamMode = roomTeamMode;
            self.sceneName = sceneName;
            self.AddRoomMember(playerId, true, RoomTeamId.Red, 0);
        }
        
        public static void ChgRoomStatus(this RoomComponent self, RoomStatus roomStatus)
        {
            self.roomStatus = roomStatus;
        }
        
        public static void ChgRoomTeamMode(this RoomComponent self, RoomTeamMode roomTeamMode)
        {
            self.roomTeamMode = roomTeamMode;
        }
        
        public static void ChgSceneName(this RoomComponent self, string sceneName)
        {
            self.sceneName = sceneName;
        }
        
        public static bool ChkIsOwner(this RoomComponent self, long playerId)
        {
            return self.ownerRoomMemberId == playerId;
        }

        public static bool ChkIsAllReady(this RoomComponent self)
        {
            foreach (RoomMember roomMember in self.GetRoomMemberList())
            {
                if (self.ownerRoomMemberId != roomMember.Id)
                {
                    if (roomMember.isReady == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static List<RoomMember> GetRoomMemberList(this RoomComponent self)
        {
            ListComponent<RoomMember> list = ListComponent<RoomMember>.Create();
            foreach (var child in self.Children)
            {
                list.Add(child.Value as RoomMember);
            }
            return list;
        }

        public static void ChgOwnerRoomMember(this RoomComponent self, long ownerPlayerId)
        {
            long oldOwnerPlayerId = self.ownerRoomMemberId;
            RoomMember oldOwnerRoomMember = self.GetRoomMember(oldOwnerPlayerId);
            oldOwnerRoomMember.isOwner = false;
            RoomMember ownerRoomMember = self.GetRoomMember(ownerPlayerId);
            ownerRoomMember.isOwner = true;
            ownerRoomMember.isReady = false;
        }

        public static RoomMember GetRoomMember(this RoomComponent self, long playerId)
        {
            return self.GetChild<RoomMember>(playerId);
        }

        public static RoomMember AddRoomMember(this RoomComponent self, long playerId, bool isOwner, RoomTeamId roomTeamId, int seatIndex)
        {
            if (seatIndex == -1)
            {
                int count = self.roomMemberSeat.Count;
                for (int i = 0; i < count; i++)
                {
                    if (self.roomMemberSeat[i] == -1)
                    {
                        seatIndex = i;
                        break;
                    }
                }
            }
            
            RoomMember roomMember = self.AddChildWithId<RoomMember>(playerId);
            roomMember.isOwner = isOwner;
            roomMember.isReady = false;
            roomMember.roomTeamId = roomTeamId;
            roomMember.seatIndex = seatIndex;

            self.roomMemberSeat[seatIndex] = roomMember.Id;

            roomMember.AddLocation(LocationType.RoomMember).Coroutine();
            
            return roomMember;
        }
        
        public static bool RemoveRoomMember(this RoomComponent self, long playerId)
        {
            bool isEmptyMember = false;
            bool isOwner = playerId == self.ownerRoomMemberId;
            if (isOwner)
            {
                RoomMember newOwnRoomMember = null;
                foreach (var child in self.Children)
                {
                    RoomMember roomMember = child.Value as RoomMember;
                    if (roomMember.Id != playerId)
                    {
                        newOwnRoomMember = roomMember;
                        break;
                    }
                }

                if (newOwnRoomMember != null)
                {
                    self.ChgOwnerRoomMember(newOwnRoomMember.Id);
                }
                else
                {
                    isEmptyMember = true;
                }
            }

            RoomMember curRoomMember = self.GetChild<RoomMember>(playerId);
            self.roomMemberSeat[curRoomMember.seatIndex] = -1;
            
            curRoomMember.RemoveLocation(LocationType.RoomMember).Coroutine();

            curRoomMember.Dispose();

            if (isEmptyMember)
            {
                self.ownerRoomMemberId = -1;
            }

            return isEmptyMember;
        }
        
        public static void ChgRoomMemberStatus(this RoomComponent self, long playerId, bool isReady)
        {
            RoomMember ownerRoomMember = self.GetRoomMember(playerId);
            ownerRoomMember.isReady = isReady;
        }

        public static void ChgRoomMemberSeat(this RoomComponent self, long playerId, int seatIndex)
        {
            RoomMember ownerRoomMember = self.GetRoomMember(playerId);
            self.roomMemberSeat[ownerRoomMember.seatIndex] = -1;
            ownerRoomMember.seatIndex = seatIndex;
            self.roomMemberSeat[seatIndex] = playerId;
        }

        //获取房间对应的动态map
        //销毁房间的时候销毁动态map
    }
}