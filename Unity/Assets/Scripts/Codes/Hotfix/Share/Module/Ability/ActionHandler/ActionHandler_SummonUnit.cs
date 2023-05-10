namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_SummonUnit: AEvent<Scene, AbilityEventType.SummonUnit>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.SummonUnit args)
		{
			await ETTask.CompletedTask;
		}
	}
}
