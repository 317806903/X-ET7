using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public enum RoomStatus
    {
        Idle,
        EnterBattle,
        InTheBattle,
    }
    public enum RoomTeamMode
    {
        //单人一队
        Single,
        //组队对抗
        TeamConfrontation,
        //统一一队
        OneTeam,
    }
    
    public enum RoomTeamId
    {
        Red,
        Green,
        Blue,
    }
    
    [ChildOf(typeof(RoomManagerComponent))]
    public class RoomComponent : Entity, IAwake, IDestroy
    {
        public RoomStatus roomStatus;
        public RoomTeamMode roomTeamMode;
        public string sceneName;
        public long ownerRoomMemberId;
        public long sceneMapId;
        public List<long> roomMemberSeat;
    }
}