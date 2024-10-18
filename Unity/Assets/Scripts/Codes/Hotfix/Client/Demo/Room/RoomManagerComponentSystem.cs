using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof(RoomManagerComponent))]
    public static class RoomManagerComponentSystem
    {
        [ObjectSystem]
        public class RoomManagerComponentAwakeSystem : AwakeSystem<RoomManagerComponent>
        {
            protected override void Awake(RoomManagerComponent self)
            {
            }
        }

        public static void Init(this RoomManagerComponent self, List<byte[]> roomList)
        {
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

            if (roomList == null)
            {
                return;
            }
            for (int i = 0; i < roomList.Count; i++)
            {
                Entity roomComponent = MongoHelper.Deserialize<Entity>(roomList[i]);
                self.AddChild(roomComponent);
            }
        }

        public static RoomComponent Init(this RoomManagerComponent self, long roomId, byte[] roomInfo, List<byte[]> roomMemberList)
        {
            Log.Debug($"self[{self}], roomId[{roomId}], roomInfo[{roomInfo.Length}], roomMemberList[{roomMemberList.Count}]");
            self.RemoveChild(roomId);
            Log.Debug($"roomComponent is MongoHelper.Deserialize ing");
            RoomComponent roomComponent = MongoHelper.Deserialize<RoomComponent>(roomInfo);
            Log.Debug($"roomComponent[{roomComponent}]");
            self.AddChild(roomComponent);
            for (int i = 0; i < roomMemberList.Count; i++)
            {
                RoomMember roomMember = MongoHelper.Deserialize<RoomMember>(roomMemberList[i]);
                roomComponent.AddChild(roomMember);
            }
            Log.Debug($"ET.Client.RoomManagerComponentSystem.Init End");
            return roomComponent;
        }

    }
}