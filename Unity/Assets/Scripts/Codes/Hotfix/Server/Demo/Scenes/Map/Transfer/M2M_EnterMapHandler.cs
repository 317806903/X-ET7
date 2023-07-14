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
			M2C_StartSceneChange m2CStartSceneChange = new M2C_StartSceneChange() {SceneInstanceId = scene.InstanceId, SceneName = scene.Name};
			MessageHelper.SendToClient(playerId, m2CStartSceneChange, false);

			string gamePlayBattleLevelCfgId = request.GamePlayBattleLevelCfgId;
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);

			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			if (gamePlayBattleLevelCfg.IsGlobalMode)
			{
				if (gamePlayComponent == null)
				{
					gamePlayComponent = scene.AddComponent<GamePlayComponent>();
					gamePlayComponent.InitWhenGlobal(scene.InstanceId, gamePlayBattleLevelCfgId);
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

			Unit unit = null;
			if (bExistUnit)
			{
				unit = unitComponent.Get(playerId);
				gamePlayComponent.ReNoticeToClient(playerId);
			}
			else
			{
				int playerLevel = request.PlayerLevel;
				GamePlayMode gamePlayMode = gamePlayComponent.gamePlayMode;
				
				float3 position = gamePlayComponent.GetPlayerBirthPos(playerId);
				float3 forward = new float3(0, 0, 1);
				unit = ET.GamePlayHelper.CreatePlayerUnit(gamePlayMode, scene, playerId, playerLevel, position, forward);
				
				unitComponent.Add(unit);

				string pathfindingMapName = scene.Name;
				unit.AddComponent<PathfindingComponent, string>(pathfindingMapName);

				unit.AddComponent<MailBoxComponent>();
			}
			
			// 解锁location，可以接收发给Unit的消息
			await LocationProxyComponent.Instance.Add(LocationType.Unit, unit.Id, unit.InstanceId);
		
			// 通知客户端创建My Unit
			M2C_CreateMyUnit m2CCreateUnits = new ();
			m2CCreateUnits.Unit = ET.Ability.UnitHelper.CreateUnitInfo(unit);
			MessageHelper.SendToClient(unit, m2CCreateUnits);

			if (bExistUnit)
			{
				unit.GetComponent<AOIEntity>().ReNotice();
			}
			else
			{
				// 加入aoi
				//unit.AddComponent<AOIEntity, int, float3>(30 * 1000, unit.Position);
			}
			
		}
	}
}