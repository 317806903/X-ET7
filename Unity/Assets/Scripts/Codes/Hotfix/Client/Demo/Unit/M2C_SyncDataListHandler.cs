using System;
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
				try
				{

					Entity entity = MongoHelper.Deserialize<Entity>(syncData);
					System.Type type = entity.GetType();
					if (type == typeof (ET.SyncData_UnitPosInfo))
					{
						ET.SyncData_UnitPosInfo _SyncData_UnitPosInfo = (ET.SyncData_UnitPosInfo)entity;
						Unit unit = unitComponent.Get(_SyncData_UnitPosInfo.unitId);
						if (unit == null)
						{
							continue;
						}

						_SyncData_UnitPosInfo.DealByBytes(unit);
					}
					else if (type == typeof (ET.SyncData_UnitNumericInfo))
					{
						ET.SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = (ET.SyncData_UnitNumericInfo)entity;
						Unit unit = unitComponent.Get(_SyncData_UnitNumericInfo.unitId);
						if (unit == null)
						{
							continue;
						}

						_SyncData_UnitNumericInfo.DealByBytes(unit);
					}
					else if (type == typeof (ET.SyncData_UnitPlayAudio))
					{
						ET.SyncData_UnitPlayAudio _SyncData_UnitPlayAudio = (ET.SyncData_UnitPlayAudio)entity;
						Unit unit = unitComponent.Get(_SyncData_UnitPlayAudio.unitId);
						if (unit == null)
						{
							continue;
						}

						_SyncData_UnitPlayAudio.DealByBytes(unit);
					}
					else if (type == typeof (ET.SyncData_UnitComponent))
					{
						ET.SyncData_UnitComponent _SyncData_UnitComponent = (ET.SyncData_UnitComponent)entity;
						Unit unit = unitComponent.Get(_SyncData_UnitComponent.unitId);
						if (unit == null)
						{
							continue;
						}

						_SyncData_UnitComponent.DealByBytes(unit);
					}

					entity.Dispose();
				}
				catch (Exception e)
				{
					Log.Error($"ET.Client.M2C_SyncDataListHandler {e}");
				}
			}
			await ETTask.CompletedTask;
		}
	}
}
