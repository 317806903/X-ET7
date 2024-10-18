using System;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class M2M_EnterMapHandler : AMActorRpcHandler<Scene, M2M_EnterMapRequest, M2M_EnterMapResponse>
	{
		protected override async ETTask Run(Scene scene, M2M_EnterMapRequest request, M2M_EnterMapResponse response)
		{
			long playerId = request.PlayerId;
			// 通知客户端开始切场景
			M2C_StartSceneChange m2CStartSceneChange = new () {SceneInstanceId = scene.InstanceId, SceneName = scene.Name};
			MessageHelper.SendToClient(playerId, m2CStartSceneChange, scene.InstanceId, false);

			string gamePlayBattleLevelCfgId = request.GamePlayBattleLevelCfgId;
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);

			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			if (gamePlayBattleLevelCfg.IsGlobalMode)
			{
				if (gamePlayComponent == null)
				{
					gamePlayComponent = scene.AddComponent<GamePlayComponent>();
					await gamePlayComponent.InitWhenGlobal(scene.InstanceId, gamePlayBattleLevelCfgId);
					gamePlayComponent.roomTypeInfo.seasonIndex = await SeasonHelper.GetSeasonIndex(scene);
				}
				gamePlayComponent.AddPlayerWhenGlobal(playerId, 1);
			}
			else
			{
				if (gamePlayComponent == null)
				{
					Log.Error($"M2M_EnterMapHandler gamePlayComponent == null");
				}
			}

			UnitComponent unitComponent = scene.GetComponent<UnitComponent>();

			bool bExistUnit = false;
			if (unitComponent.Get(playerId) != null)
			{
				bExistUnit = true;
			}

			Unit observerUnit = null;
			if (bExistUnit)
			{
				observerUnit = unitComponent.Get(playerId);
				gamePlayComponent.ReNoticeToClient(playerId);
			}
			else
			{
				int playerLevel = request.PlayerLevel;
				GamePlayMode gamePlayMode = gamePlayComponent.gamePlayMode;

				observerUnit = ET.GamePlayHelper.CreateObserverUnit(gamePlayMode, scene, playerId);
				unitComponent.Add(observerUnit);
				observerUnit.AddComponent<MailBoxComponent>();

				await TimerComponent.Instance.WaitFrameAsync();

				float3 position = gamePlayComponent.GetPlayerBirthPos(playerId);
				float3 forward = new float3(0, 0, 1);
				Unit playerUnit = ET.GamePlayHelper.CreatePlayerUnit(gamePlayMode, scene, playerId, playerLevel, position, forward);
				if (playerUnit != null)
				{
					unitComponent.Add(playerUnit);
				}

				Unit cameraPlayerUnit = ET.GamePlayHelper.CreateCameraPlayerUnit(gamePlayMode, scene, playerId, playerLevel, position, forward);
				if (cameraPlayerUnit != null)
				{
					await ET.Server.GamePlayHelper.LoadCameraPlayerSkillList(playerId, cameraPlayerUnit);
					unitComponent.Add(cameraPlayerUnit);
				}
			}

			// 解锁location，可以接收发给Unit的消息
			await LocationProxyComponent.Instance.Add(LocationType.Unit, observerUnit.Id, observerUnit.InstanceId);

			// 通知客户端创建My Unit
			M2C_CreateMyUnit m2CCreateUnits = new ();
			m2CCreateUnits.Unit = ET.Ability.UnitHelper.CreateUnitInfo(observerUnit);
			MessageHelper.SendToClient(observerUnit, m2CCreateUnits);

			// if (playerUnit != null)
			// {
			// 	M2C_CreateMyUnit m2CCreatePlayerUnits = new ();
			// 	m2CCreatePlayerUnits.Unit = ET.Ability.UnitHelper.CreateUnitInfo(playerUnit);
			// 	MessageHelper.SendToClient(observerUnit, m2CCreatePlayerUnits);
			// }

			if (bExistUnit)
			{
				observerUnit.GetComponent<AOIEntity>().ReNotice();
			}
			else
			{
			}

		}
	}
}