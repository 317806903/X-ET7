using System.Collections.Generic;

namespace ET.Client
{
    [FriendOf(typeof(RoomComponent))]
    public static class RoomComponentSystem
    {
        [ObjectSystem]
        public class RoomComponentAwakeSystem : AwakeSystem<RoomComponent>
        {
            protected override void Awake(RoomComponent self)
            {
            }
        }
        
        public static void CopyFrom(this RoomComponent self, RoomComponent roomComponent)
        {
            self.roomStatus = roomComponent.roomStatus;
            self.roomTeamMode = roomComponent.roomTeamMode;
            self.sceneName = roomComponent.sceneName;
            self.ownerRoomMemberId = roomComponent.ownerRoomMemberId;
            self.sceneMapId = roomComponent.sceneMapId;
            self.roomMemberSeat = roomComponent.roomMemberSeat;
        }
        
        public static void Init(this RoomComponent self, byte[] roomInfo, List<byte[]> roomMemberList)
        {
            if (self == null)
            {
                return;
            }
            RoomComponent roomComponent = MongoHelper.Deserialize<Entity>(roomInfo) as RoomComponent;
            self.CopyFrom(roomComponent);

            while (true)
            {
                bool hasChild = false;
                foreach (var child in self.Children)
                {
                    hasChild = true;
                    child.Value.Dispose();
                    break;
                }

                if (hasChild == false)
                {
                    break;
                }
            }
            for (int i = 0; i < roomMemberList.Count; i++)
            {
                Entity roomMember = MongoHelper.Deserialize<Entity>(roomMemberList[i]);
                self.AddChild(roomMember);
            }
        }
        
        public static RoomMember AddRoomMember(this RoomComponent self, long playerId, bool isOwner, RoomTeamId roomTeamId, bool isReady)
        {
            RoomMember roomMember = self.AddChildWithId<RoomMember>(playerId);
            roomMember.isOwner = isOwner;
            roomMember.isReady = isReady;
            roomMember.roomTeamId = roomTeamId;
            return roomMember;
        }

    }
}