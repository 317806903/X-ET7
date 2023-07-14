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

        public static void Init(this RoomManagerComponent self, long roomId, byte[] roomInfo, List<byte[]> roomMemberList)
        {
            self.RemoveChild(roomId);
            RoomComponent roomComponent = MongoHelper.Deserialize<Entity>(roomInfo) as RoomComponent;
            self.AddChild(roomComponent);
            for (int i = 0; i < roomMemberList.Count; i++)
            {
                Entity roomMember = MongoHelper.Deserialize<Entity>(roomMemberList[i]);
                roomComponent.AddChild(roomMember);
            }
        }
        
    }
}