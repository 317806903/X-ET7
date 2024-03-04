namespace ET.Client
{
	[ComponentOf(typeof(Session))]
	public class ReLoginComponent: Entity, IAwake, IDestroy
	{
		[StaticField]
		public static ReLoginComponent Instance;

		public long lastPauseTime;
		public bool isReLogining;
		public bool isNeedReCreateSession;
		public bool isReCreateSessioning;
	}
}
