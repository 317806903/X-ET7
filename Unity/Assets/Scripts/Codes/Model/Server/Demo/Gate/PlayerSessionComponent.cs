namespace ET.Server
{
	[ComponentOf(typeof(Player))]
	public class PlayerSessionComponent : Entity, IAwake
	{
		private EntityRef<Session> session;

		public Session Session
		{
			get
			{
				return session;
			}
			set
			{
				this.session = value;
			}
		}
	}
}