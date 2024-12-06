using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_PKMoveTowerHandler : AMActorLocationRpcHandler<Unit, C2M_PKMoveTower, M2C_PKMoveTower>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_PKMoveTower request, M2C_PKMoveTower response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			long towerUnitId = request.TowerUnitId;
			float3 position = request.Position;

			Unit curTownUnit = ET.Ability.UnitHelper.GetUnit(observerUnit.DomainScene(), towerUnitId);

			List<Unit> downTowerList = null;
			float curUnitHeight = 0;
			GamePlayPkComponent GetGamePlayPK = ET.GamePlayHelper.GetGamePlayPk(observerUnit.DomainScene());
			bool isNeedDownTower = GetGamePlayPK.ChkMovePlayerTowerNeedDownTower(towerUnitId, position);
			if (isNeedDownTower)
			{
				Unit unit = curTownUnit;
				downTowerList = GetGamePlayPK.GetTowerListWhenStackedOnTop(unit);
				curUnitHeight = ET.Ability.UnitHelper.GetBodyHeight(unit);
			}

			ET.Ability.UnitHelper.ResetPos(curTownUnit, position, float3.zero);

			if (true)
			{
				if (downTowerList != null)
				{
					foreach (Unit towerUnit in downTowerList)
					{
						Ability.UnitHelper.ResetPos(towerUnit, towerUnit.Position - new float3(0, curUnitHeight, 0), float3.zero);
					}
				}
			}

			await ETTask.CompletedTask;
		}
	}
}