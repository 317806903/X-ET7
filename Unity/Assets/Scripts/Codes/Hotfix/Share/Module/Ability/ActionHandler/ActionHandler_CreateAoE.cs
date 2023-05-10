namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_CreateAoE: AEvent<Scene, AbilityEventType.CreateAoE>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.CreateAoE args)
		{
			await ETTask.CompletedTask;
		}
	}
}
