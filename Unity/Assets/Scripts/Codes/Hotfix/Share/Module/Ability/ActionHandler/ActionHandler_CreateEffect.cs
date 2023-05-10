namespace ET.Ability
{
	[Event(SceneType.Current)]
	public class ActionHandler_CreateEffect: AEvent<Scene, AbilityEventType.CreateEffect>
	{
		protected override async ETTask Run(Scene scene, AbilityEventType.CreateEffect args)
		{
            EffectHelper.AddEffect(UnitHelper.GetUnit(args.unitId), args.cfgId);
			await ETTask.CompletedTask;
		}
	}
}
