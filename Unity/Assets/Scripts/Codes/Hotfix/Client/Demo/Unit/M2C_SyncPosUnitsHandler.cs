using Unity.Mathematics;

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

				unit.Position = new float3(unitInfo.PositionX * 0.01f, unitInfo.PositionY * 0.01f, unitInfo.PositionZ * 0.01f);
				unit.Forward = new float3(unitInfo.ForwardX * 0.01f, unitInfo.ForwardY * 0.01f, unitInfo.ForwardZ * 0.01f);
			}
			await ETTask.CompletedTask;
		}
	}
}
