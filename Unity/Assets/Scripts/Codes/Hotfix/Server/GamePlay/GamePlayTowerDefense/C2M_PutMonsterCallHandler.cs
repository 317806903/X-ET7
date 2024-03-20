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
			(bool canPut, float3 forward) = ChkPosition(gamePlayTowerDefenseComponent, observerUnit, pos);
			if (canPut == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Tip_NoWayToConnectHome");
			}
			else
			{
				PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
				if (putMonsterCallComponent == null)
				{
					putMonsterCallComponent = gamePlayTowerDefenseComponent.AddComponent<PutMonsterCallComponent>();
				}

				forward.y = 0;
				putMonsterCallComponent.Init(playerId, unitCfgId, pos, forward);
			}

			await ETTask.CompletedTask;
		}

		public (bool, float3) ChkPosition(GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent, Unit unit, float3 putMonsterCallPos)
		{
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			Unit homeUnit = putHomeComponent.GetHomeUnit(unit);
			return putHomeComponent.ChkHomeUnitPositionAndForward(homeUnit, putMonsterCallPos);
		}
	}
}