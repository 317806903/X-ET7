namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_PlayAnimator: AEvent<Scene, AbilityEventType.PlayAnimator>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.PlayAnimator args)
		{
			await ETTask.CompletedTask;
		}
	}
}
