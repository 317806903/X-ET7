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
        Red,
        Green,
        Blue,
    }

    [ChildOf(typeof(RoomManagerComponent))]
    public class RoomComponent : Entity, IAwake, IDestroy
    {
        public bool isARRoom;
        public string arSceneId;
        public RoomStatus roomStatus;
        public RoomTeamMode roomTeamMode;
        public long ownerRoomMemberId;
        public long dynamicMapInstanceId;
        public string gamePlayBattleLevelCfgId;
        public List<long> roomMemberSeat;
    }
}