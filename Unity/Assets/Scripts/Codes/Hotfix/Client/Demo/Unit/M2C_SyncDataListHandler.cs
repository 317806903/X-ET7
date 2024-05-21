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

			int i = 0;
			foreach (byte[] syncData in message.SyncDataList)
			{
				try
				{
					i++;
					Entity entity = MongoHelper.Deserialize<Entity>(syncData);
					System.Type type = entity.GetType();
					if (type == typeof (ET.SyncData_UnitPosInfo))
					{
						ET.SyncData_UnitPosInfo _SyncData_UnitPosInfo = (ET.SyncData_UnitPosInfo)entity;
						await _SyncData_UnitPosInfo.DealByBytes(unitComponent);
					}
					else if (type == typeof (ET.SyncData_UnitNumericInfo))
					{
						ET.SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = (ET.SyncData_UnitNumericInfo)entity;

						await _SyncData_UnitNumericInfo.DealByBytes(unitComponent);
					}
					else if (type == typeof (ET.SyncData_UnitPlayAudio))
					{
						ET.SyncData_UnitPlayAudio _SyncData_UnitPlayAudio = (ET.SyncData_UnitPlayAudio)entity;
						await _SyncData_UnitPlayAudio.DealByBytes(unitComponent);
					}
					else if (type == typeof (ET.SyncData_UnitComponent))
					{
						ET.SyncData_UnitComponent _SyncData_UnitComponent = (ET.SyncData_UnitComponent)entity;
						await _SyncData_UnitComponent.DealByBytes(unitComponent);
					}
					else if (type == typeof (ET.SyncData_UnitGetCoinShow))
					{
						ET.SyncData_UnitGetCoinShow _SyncData_UnitGetCoinShow = (ET.SyncData_UnitGetCoinShow)entity;
						await _SyncData_UnitGetCoinShow.DealByBytes(unitComponent);
					}
					else if (type == typeof (ET.SyncData_DamageShow))
					{
						ET.SyncData_DamageShow _SyncData_DamageShow = (ET.SyncData_DamageShow)entity;
						await _SyncData_DamageShow.DealByBytes(unitComponent);
					}
					else if (type == typeof (ET.SyncData_UnitEffects))
					{
						ET.SyncData_UnitEffects _SyncData_UnitEffects = (ET.SyncData_UnitEffects)entity;
						await _SyncData_UnitEffects.DealByBytes(unitComponent);
					}

					entity.Dispose();
					if (i > 100)
					{
						await TimerComponent.Instance.WaitFrameAsync();
					}
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
