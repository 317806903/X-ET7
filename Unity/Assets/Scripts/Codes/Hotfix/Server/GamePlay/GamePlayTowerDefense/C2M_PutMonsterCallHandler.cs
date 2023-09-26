using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PutMonsterCallHandler : AMActorLocationRpcHandler<Unit, C2M_PutMonsterCall, M2C_PutMonsterCall>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PutMonsterCall request, M2C_PutMonsterCall response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			string unitCfgId = request.UnitCfgId;
			float3 pos = request.Position;

			pos = ET.RecastHelper.GetNearNavmeshPos(observerUnit, pos);


			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			bool canPut = ChkPosition(gamePlayTowerDefenseComponent, observerUnit, pos);
			if (canPut == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = "当前放置位置 没法到达大本营,请重新选位置";
			}
			else
			{
				PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
				if (putMonsterCallComponent == null)
				{
					putMonsterCallComponent = gamePlayTowerDefenseComponent.AddComponent<PutMonsterCallComponent>();
				}
				putMonsterCallComponent.Init(playerId, unitCfgId, pos);

			}

			await ETTask.CompletedTask;
		}

		public bool ChkPosition(GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent, Unit unit, float3 putMonsterCallPos)
		{
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			float3 homePos = putHomeComponent.GetPosition();
			float3 startPos = putMonsterCallPos;

			List<float3> points = ET.RecastHelper.GetArrivePath(unit, startPos, homePos);
			if (points == null)
			{
				return false;
			}
			if (points.Count == 0)
			{
				return false;
			}
			float3 lastPoint = points[points.Count - 1];
			if (math.abs(homePos.x - lastPoint.x) < 0.3f
			    && math.abs(homePos.y - lastPoint.y) < 0.3f
			    && math.abs(homePos.z - lastPoint.z) < 0.3f)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}