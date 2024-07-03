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
    }

    [ChildOf(typeof(RoomManagerComponent))]
    public class RoomComponent : Entity, IAwake, IDestroy, IFixedUpdate
    {
        public RoomTypeInfo roomTypeInfo;

        public string arSceneId;
        public float arMapScale;
        public RoomStatus roomStatus;
        public RoomTeamMode roomTeamMode;
        public long ownerRoomMemberId;
        public long dynamicMapInstanceId;

        public List<long> roomMemberSeat;

        public int MaxMemberCount = 10;
        [BsonIgnore]
        public int waitFrameChk = 900;
        [BsonIgnore]
        public int curFrameChk = 0;
        [BsonIgnore]
        public Dictionary<long, long> playerWaitQuitTime;
    }
}