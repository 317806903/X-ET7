namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_AddBuffToCaster: AEvent<Scene, AbilityEventType.AddBuffToCaster>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.AddBuffToCaster args)
		{
			await ETTask.CompletedTask;
		}
	}
}
