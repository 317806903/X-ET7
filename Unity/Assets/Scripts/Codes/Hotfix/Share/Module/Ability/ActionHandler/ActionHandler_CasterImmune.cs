namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_CasterImmune: AEvent<Scene, AbilityEventType.CasterImmune>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.CasterImmune args)
		{
			await ETTask.CompletedTask;
		}
	}
}
