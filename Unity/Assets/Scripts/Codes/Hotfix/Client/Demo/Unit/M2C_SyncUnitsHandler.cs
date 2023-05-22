namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncUnitsHandler : AMHandler<M2C_SyncUnits>
	{
		protected override async ETTask Run(Session session, M2C_SyncUnits message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
			
			foreach (UnitInfo unitInfo in message.Units)
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
