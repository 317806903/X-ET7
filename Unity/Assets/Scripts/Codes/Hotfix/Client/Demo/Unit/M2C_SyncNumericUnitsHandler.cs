namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncNumericUnitsHandler : AMHandler<M2C_SyncNumericUnits>
	{
		protected override async ETTask Run(Session session, M2C_SyncNumericUnits message)
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
			foreach (UnitNumericInfo unitInfo in message.Units)
			{
				Unit unit = unitComponent.Get(unitInfo.UnitId);
				if (unit == null)
				{
					continue;
				}

				NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
				if (numericComponent == null)
				{
					numericComponent = unit.AddComponent<NumericComponent>();
				}
				foreach (var kv in unitInfo.KV)
				{
					numericComponent.SetAsLong(kv.Key, kv.Value);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}
