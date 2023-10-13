namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_CreateUnitsHandler : AMHandler<M2C_CreateUnits>
	{
		protected override async ETTask Run(Session session, M2C_CreateUnits message)
		{
			UnitComponent unitComponent = ET.Client.UnitHelper.GetUnitComponent(session.DomainScene());
			if (unitComponent == null)
			{
				return;
			}

			foreach (UnitInfo unitInfo in message.Units)
			{
				Unit unit = unitComponent.Get(unitInfo.UnitId);
				if (unit != null)
				{
					UnitFactory.ReplaceComponent(unit, unitInfo);
					continue;
				}
				unit = UnitFactory.Create(unitComponent, unitInfo);
			}
			await ETTask.CompletedTask;
		}
	}
}
