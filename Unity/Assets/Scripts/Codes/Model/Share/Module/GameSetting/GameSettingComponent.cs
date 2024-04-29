namespace ET
{
	public enum GameSettingType
	{
		Music,
		Audio,
		DamageShow,
	}

	[ComponentOf(typeof(Scene))]
	public class GameSettingComponent : Entity, IAwake
	{
		[StaticField]
		public static GameSettingComponent Instance;

	}
}