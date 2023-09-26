using Unity.Mathematics;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncDataListHandler : AMHandler<M2C_SyncDataList>
	{
		protected override async ETTask Run(Session session, M2C_SyncDataList message)
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
			foreach (byte[] syncData in message.SyncDataList)
			{
				Entity entity = MongoHelper.Deserialize<Entity>(syncData);
				System.Type type = entity.GetType();
				if (type == typeof (ET.SyncData_UnitPosInfo))
				{
					ET.SyncData_UnitPosInfo unitPosInfo = (ET.SyncData_UnitPosInfo)entity;
					Unit unit = unitComponent.Get(unitPosInfo.unitId);
					if (unit == null)
					{
						continue;
					}
					unit.Position = unitPosInfo.GetPos();
					unit.Forward = unitPosInfo.GetForward();
				}
			}
			await ETTask.CompletedTask;
		}
	}
}
