namespace ET.Server
{
	[ComponentOf(typeof(Player))]
	public class PlayerStatusComponent : Entity, IAwake
	{
		public PlayerStatus PlayerStatus { get; set; }
		public long RoomId { get; set; }
	}
}