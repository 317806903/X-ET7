using System;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class M2M_UnitTransferRequestHandler : AMActorRpcHandler<Scene, M2M_UnitTransferRequest, M2M_UnitTransferResponse>
	{
		protected override async ETTask Run(Scene scene, M2M_UnitTransferRequest request, M2M_UnitTransferResponse response)
		{
			UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
			Unit unit = MongoHelper.Deserialize<Unit>(request.Unit);
			bool bExistUnit = false;
			if (unitComponent.Get(unit.Id) != null)
			{
				bExistUnit = true;
			}

			if (bExistUnit)
			{
				unit.Dispose();
				unit = unitComponent.Get(unit.Id);
			}
			else
			{
				unitComponent.AddChild(unit);
				unitComponent.Add(unit);

				foreach (byte[] bytes in request.Entitys)
				{
					Entity entity = MongoHelper.Deserialize<Entity>(bytes);
					unit.AddComponent(entity);
				}

				//unit.AddComponent<MoveByPathComponent>();
				unit.Position = new float3(-10, 0, -10);

				unit.AddComponent<MailBoxComponent>();
			}

			// 通知客户端开始切场景
			M2C_StartSceneChange m2CStartSceneChange = new M2C_StartSceneChange() {SceneInstanceId = scene.InstanceId, SceneName = scene.Name};
			MessageHelper.SendToClient(unit, m2CStartSceneChange, false);

			// 通知客户端创建My Unit
			M2C_CreateMyUnit m2CCreateUnits = new M2C_CreateMyUnit();
			m2CCreateUnits.Unit = ET.Ability.UnitHelper.CreateUnitInfo(unit);
			MessageHelper.SendToClient(unit, m2CCreateUnits);

			if (bExistUnit)
			{
				unit.GetComponent<AOIEntity>().ReNotice();
			}
			else
			{
			}

			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			if (gamePlayComponent == null)
			{
				gamePlayComponent = scene.AddComponent<GamePlayComponent>();
				await gamePlayComponent.InitWhenGlobal(scene.InstanceId, "GamePlayBattleLevel_Global1");
			}
			gamePlayComponent.AddPlayerWhenGlobal(unit.Id, 1);
			ET.GamePlayHelper.AddUnitPathfinding(unit);

			// 解锁location，可以接收发给Unit的消息
			await LocationProxyComponent.Instance.UnLock(LocationType.Unit, unit.Id, request.OldInstanceId, unit.InstanceId);
		}
	}
}