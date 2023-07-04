namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncPosUnitsHandler : AMHandler<M2C_SyncPosUnits>
	{
		protected override async ETTask Run(Session session, M2C_SyncPosUnits message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			if (currentScene == null)
			{
				return;
			}
			UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
			if (unitComponent == null)
			{
				return;
			}
			foreach (UnitPosInfo unitInfo in message.Units)
			{
				Unit unit = unitComponent.Get(unitInfo.UnitId);
				if (unit == null)
				{
					continue;
				}

				unit.Position = unitInfo.Position;
				unit.Forward = unitInfo.Forward;
			}
			await ETTask.CompletedTask;
		}
	}
}
