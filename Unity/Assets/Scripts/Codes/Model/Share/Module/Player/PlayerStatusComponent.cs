namespace ET
{
	[ComponentOf(typeof(Player))]
	public class PlayerStatusComponent : Entity, IAwake
	{
		public PlayerStatus PlayerStatus { get; set; }
		public RoomType RoomType { get; set; }
		public SubRoomType SubRoomType { get; set; }
		public long RoomId { get; set; }
		public string LastBattleCfgId { get; set; }
		//0 未结束; 1 胜利; -1 失败
		public int LastBattleResult { get; set; }
	}
}