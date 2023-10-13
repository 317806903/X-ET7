namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class PlayerComponent: Entity, IAwake
    {
        public long MyId { get; set; }
        public PlayerGameMode PlayerGameMode { get; set; }
        public PlayerStatus PlayerStatus { get; set; }
        public ARRoomType ARRoomType { get; set; }
        public long RoomId { get; set; }
        public bool IsDebugMode { get; set; }
    }
}