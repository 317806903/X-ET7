namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_FireBullet: AEvent<Scene, AbilityEventType.FireBullet>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.FireBullet args)
		{
			await ETTask.CompletedTask;
		}
	}
}
