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
                self._ARMeshInfoDic = new();
            }
        }

        public static RoomComponent CreateRoom(this RoomManagerComponent self, RoomTypeInfo roomTypeInfo, long playerId, RoomTeamMode roomTeamMode)
        {
            RoomComponent roomComponent = self.AddChild<RoomComponent>();
            roomComponent.Init(roomTypeInfo, playerId, roomTeamMode);

            self.IdleRoomList.Add(roomComponent.Id);
            self.player2Room.Add(playerId, roomComponent.Id);

            return roomComponent;
        }

        public static bool ChkRoomMemberIsFull(this RoomManagerComponent self, long roomId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            return roomComponent.ChkRoomMemberIsFull();
        }

        public static void JoinRoom(this RoomManagerComponent self, long playerId, long roomId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            if (roomComponent.ownerRoomMemberId == -1)
            {
                roomComponent.ownerRoomMemberId = playerId;
                roomComponent.AddRoomMember(playerId, true, RoomTeamId.Green, 0);
            }
            else
            {
                roomComponent.AddRoomMember(playerId, false, RoomTeamId.Green, -1);
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

        public static void SetARSceneId(this RoomManagerComponent self, long roomId, string ARSceneId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            roomComponent.arSceneId = ARSceneId;
        }

        public static void SetARSceneMeshId(this RoomManagerComponent self, long roomId, string ARSceneMeshId)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            roomComponent.arSceneMeshId = ARSceneMeshId;
        }

        public static void SetMapScale(this RoomManagerComponent self, long roomId, float mapScale)
        {
            RoomComponent roomComponent = self.GetRoom(roomId);
            roomComponent.mapScale = mapScale;
        }

        public static void SetARMeshInfo(this RoomManagerComponent self, long roomId, ARMeshType _ARMeshType, string _ARSceneId, string _ARSceneMeshId, string _ARMeshDownLoadUrl, byte[] _ARMeshBytes)
        {
            self._ARMeshInfoDic[roomId] = (_ARMeshType, _ARSceneId, _ARSceneMeshId, _ARMeshDownLoadUrl, _ARMeshBytes);
        }

        public static (ARMeshType _ARMeshType, string _ARSceneId, string _ARSceneMeshId, string _ARMeshDownLoadUrl, byte[] _ARMeshBytes) GetARMeshInfo(this RoomManagerComponent self, long roomId)
        {
            if (self._ARMeshInfoDic.ContainsKey(roomId))
            {
                return self._ARMeshInfoDic[roomId];
            }

            return default;
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
            self._ARMeshInfoDic.Remove(roomId);
        }

        //获取当前房间列表
        public static List<RoomComponent> GetIdleRoomList(this RoomManagerComponent self, bool needARRoom, bool needNotARRoom)
        {
            ListComponent<RoomComponent> list = ListComponent<RoomComponent>.Create();
            foreach (long idleRoomId in self.IdleRoomList)
            {
                RoomComponent roomComponent = self.GetRoom(idleRoomId);
                if (needARRoom && roomComponent.IsARRoom())
                {
                    list.Add(roomComponent);
                }
                else if (needNotARRoom && roomComponent.IsARRoom() == false)
                {
                    list.Add(roomComponent);
                }
            }
            return list;
        }

        public static List<RoomComponent> GetRoomList(this RoomManagerComponent self, bool needARRoom, bool needNotARRoom)
        {
            ListComponent<RoomComponent> list = ListComponent<RoomComponent>.Create();
            foreach (var child in self.Children)
            {
                RoomComponent roomComponent = child.Value as RoomComponent;
                if (needARRoom && roomComponent.IsARRoom())
                {
                    list.Add(roomComponent);
                }
                else if (needNotARRoom && roomComponent.IsARRoom() == false)
                {
                    list.Add(roomComponent);
                }
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