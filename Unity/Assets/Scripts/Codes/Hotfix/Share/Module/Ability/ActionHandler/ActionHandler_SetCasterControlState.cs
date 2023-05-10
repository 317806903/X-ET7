namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_SetCasterControlState: AEvent<Scene, AbilityEventType.SetCasterControlState>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.SetCasterControlState args)
		{
			await ETTask.CompletedTask;
		}
	}
}
