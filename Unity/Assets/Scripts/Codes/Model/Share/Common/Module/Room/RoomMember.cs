namespace ET
{
    [ChildOf(typeof(RoomComponent))]
    public sealed class RoomMember : Entity, IAwake//, ISerializeToEntity
    {
        public bool isOwner { get; set; }
        public bool isReady { get; set; }
        public RoomTeamId roomTeamId { get; set; }
        public int seatIndex { get; set; }
    }
}