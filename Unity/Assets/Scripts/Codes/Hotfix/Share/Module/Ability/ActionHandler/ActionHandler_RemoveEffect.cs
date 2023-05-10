namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_RemoveEffect: AEvent<Scene, AbilityEventType.RemoveEffect>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.RemoveEffect args)
		{
			await ETTask.CompletedTask;
		}
	}
}
