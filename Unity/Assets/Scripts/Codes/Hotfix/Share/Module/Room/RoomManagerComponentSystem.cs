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
                self._ARMeshDownLoadUrlDic = new();
            }
        }

        public static RoomComponent CreateRoom(this RoomManagerComponent self, RoomType roomType, SubRoomType subRoomType, long playerId, RoomTeamMode roomTeamMode, string battleCfgId)
        {
            RoomComponent roomComponent = self.AddChild<RoomComponent>();
            roomComponent.Init(roomType, subRoomType, playerId, roomTeamMode, battleCfgId);

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

        public static void SetARMeshDownLoadUrl(this RoomManagerComponent self, long roomId, string _ARMeshDownLoadUrl)
        {
            //     @"http://prod-cn-bj-alicloud-arsession-arsession-deepmirror-s3.oss-cn-beijing.aliyuncs.com/64d0dfbf5252b55795bc8507.space_mesh?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=LTAI5tPk1NHZtLxk3N1nm8nT%2F20230807%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230807T121319Z&X-Amz-Expires=172800&X-Amz-SignedHeaders=host&X-Amz-Signature=040086f469d411292adf293aa8372ddff823450c77304d7c5e701a8e58f440e9";
            self._ARMeshDownLoadUrlDic[roomId] = _ARMeshDownLoadUrl;
        }

        public static string GetARMeshDownLoadUrl(this RoomManagerComponent self, long roomId)
        {
            if (self._ARMeshDownLoadUrlDic.ContainsKey(roomId))
            {
                return self._ARMeshDownLoadUrlDic[roomId];
            }

            return "";
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
            self._ARMeshDownLoadUrlDic.Remove(roomId);
        }

        //获取当前房间列表
        public static List<long> GetIdleRoomList(this RoomManagerComponent self, bool isARRoom)
        {
            ListComponent<long> list = ListComponent<long>.Create();
            foreach (long idleRoomId in self.IdleRoomList)
            {
                RoomComponent roomComponent = self.GetRoom(idleRoomId);
                if (roomComponent.IsARRoom() == isARRoom)
                {
                    list.Add(roomComponent.Id);
                }
            }
            return list;
        }

        public static List<RoomComponent> GetRoomList(this RoomManagerComponent self)
        {
            ListComponent<RoomComponent> list = ListComponent<RoomComponent>.Create();
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