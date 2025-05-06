namespace ET.Server
{
	[ComponentOf(typeof(Session))]
	public class SessionPlayerComponent : Entity, IAwake, IDestroy
	{
		private EntityRef<Player> player;

		public Player Player
		{
			get
			{
				return player;
			}
			set
			{
				this.player = value;
			}
		}

		public int fps;
		public long pingTime;
	}
}