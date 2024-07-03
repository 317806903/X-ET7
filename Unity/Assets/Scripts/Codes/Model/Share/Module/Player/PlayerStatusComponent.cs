using ET.Ability;

namespace ET
{
	public enum BattleResult
	{
		Default,
		Successed,
		Failed,
	}

	[ComponentOf(typeof(Player))]
	public class PlayerStatusComponent : Entity, IAwake
	{
		public PlayerStatus PlayerStatus { get; set; }
		public RoomTypeInfo RoomTypeInfo { get; set; }

		public long RoomId { get; set; }
		public string LastBattleCfgId { get; set; }
		public BattleResult LastBattleResult { get; set; }
	}
}