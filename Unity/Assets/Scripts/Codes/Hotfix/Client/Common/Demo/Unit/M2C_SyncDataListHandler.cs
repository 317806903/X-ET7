﻿using System;
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
					switch (entity)
					{
						case ET.SyncData_UnitPosInfo _SyncData_UnitPosInfo:
							_SyncData_UnitPosInfo.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitNumericInfo _SyncData_UnitNumericInfo:
							_SyncData_UnitNumericInfo.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitPlayAudio _SyncData_UnitPlayAudio:
							_SyncData_UnitPlayAudio.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitFloatingText _SyncData_UnitFloatingText:
							_SyncData_UnitFloatingText.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitComponent _SyncData_UnitComponent:
							_SyncData_UnitComponent.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitGetCoinShow _SyncData_UnitGetCoinShow:
							_SyncData_UnitGetCoinShow.DealByBytes(unitComponent);
							break;
						case ET.SyncData_DamageShow _SyncData_DamageShow:
							_SyncData_DamageShow.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitEffects _SyncData_UnitEffects:
							await _SyncData_UnitEffects.DealByBytes(unitComponent);
							break;
						case ET.SyncData_UnitBaseInfo _SyncData_UnitBaseInfo:
							_SyncData_UnitBaseInfo.DealByBytes(unitComponent);
							break;
						default:
							// 处理未知类型
							break;
					}

					// System.Type type = entity.GetType();
					// if (type == typeof (ET.SyncData_UnitPosInfo))
					// {
					// 	ET.SyncData_UnitPosInfo _SyncData_UnitPosInfo = (ET.SyncData_UnitPosInfo)entity;
					// 	_SyncData_UnitPosInfo.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitNumericInfo))
					// {
					// 	ET.SyncData_UnitNumericInfo _SyncData_UnitNumericInfo = (ET.SyncData_UnitNumericInfo)entity;
					//
					// 	_SyncData_UnitNumericInfo.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitPlayAudio))
					// {
					// 	ET.SyncData_UnitPlayAudio _SyncData_UnitPlayAudio = (ET.SyncData_UnitPlayAudio)entity;
					// 	_SyncData_UnitPlayAudio.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitFloatingText))
					// {
					// 	ET.SyncData_UnitFloatingText _SyncData_UnitFloatingText = (ET.SyncData_UnitFloatingText)entity;
					// 	_SyncData_UnitFloatingText.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitComponent))
					// {
					// 	ET.SyncData_UnitComponent _SyncData_UnitComponent = (ET.SyncData_UnitComponent)entity;
					// 	_SyncData_UnitComponent.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitGetCoinShow))
					// {
					// 	ET.SyncData_UnitGetCoinShow _SyncData_UnitGetCoinShow = (ET.SyncData_UnitGetCoinShow)entity;
					// 	_SyncData_UnitGetCoinShow.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_DamageShow))
					// {
					// 	ET.SyncData_DamageShow _SyncData_DamageShow = (ET.SyncData_DamageShow)entity;
					// 	_SyncData_DamageShow.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitEffects))
					// {
					// 	ET.SyncData_UnitEffects _SyncData_UnitEffects = (ET.SyncData_UnitEffects)entity;
					// 	await _SyncData_UnitEffects.DealByBytes(unitComponent);
					// }
					// else if (type == typeof (ET.SyncData_UnitBaseInfo))
					// {
					// 	ET.SyncData_UnitBaseInfo _SyncData_UnitBaseInfo = (ET.SyncData_UnitBaseInfo)entity;
					// 	_SyncData_UnitBaseInfo.DealByBytes(unitComponent);
					// }

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
