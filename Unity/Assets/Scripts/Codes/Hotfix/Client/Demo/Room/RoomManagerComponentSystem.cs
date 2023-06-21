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
        
        public static RoomComponent Get(this RoomManagerComponent self, long roomId)
        {
            return self.GetChild<RoomComponent>(roomId);
        }
        
        public static void Init(this RoomManagerComponent self, long roomId, byte[] roomInfo, List<byte[]> roomMemberList)
        {
            RoomComponent roomComponent = self.Get(roomId);
            if (roomComponent == null)
            {
                roomComponent = self.AddChildWithId<RoomComponent>(roomId);
            }
            roomComponent.Init(roomInfo, roomMemberList);
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
    }
}