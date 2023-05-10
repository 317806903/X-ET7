namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_PlayAudio: AEvent<Scene, AbilityEventType.PlayAudio>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.PlayAudio args)
		{
			await ETTask.CompletedTask;
		}
	}
}
