using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public enum RoomStatus: byte
    {
        Idle,
        EnteringBattle,
        InTheBattle,
    }
    public enum RoomTeamMode: byte
    {
        //单人一队
        Single,
        //组队对抗
        TeamConfrontation,
        //统一一队
        OneTeam,
    }

    public enum RoomTeamId: byte
    {
        Green,
        Red,
        Blue,
        Yellow,
    }

    [ChildOf(typeof(RoomManagerComponent))]
    public class RoomComponent : Entity, IAwake, IDestroy
    {
        public RoomTypeInfo roomTypeInfo;

        public string arSceneId;
        
        // 手机和MirrorScene场景中，这个值不需要，因为arSceneId除了代表一个共享的session，也代表了一个session使用的mesh。
        // 在Quest场景中，这个值用来代表一个主机本地扫的mesh，而arSceneId代表一个共享的session，这两个值不一样。
        public string arSceneMeshId; 
        
        public float mapScale;
        public RoomStatus roomStatus;
        public RoomTeamMode roomTeamMode;
        public long ownerRoomMemberId;
        public long dynamicMapInstanceId;

        public List<long> roomMemberSeat;

        public int MaxMemberCount = 10;
    }
}