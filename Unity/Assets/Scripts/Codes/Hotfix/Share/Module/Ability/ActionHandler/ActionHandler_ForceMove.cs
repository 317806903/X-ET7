namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_ForceMove: AEvent<Scene, AbilityEventType.ForceMove>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.ForceMove args)
		{
			await ETTask.CompletedTask;
		}
	}
}
