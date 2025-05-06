using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_UpgradeItemUnitHandler : AMActorLocationRpcHandler<Unit, C2M_UpgradeItemUnit, M2C_UpgradeItemUnit>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_UpgradeItemUnit request, M2C_UpgradeItemUnit response)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			Scene scene = observerUnit.DomainScene();
			long playerId = observerUnit.Id;

			long itemUnitId = request.ItemUnitId;
			int nextLevel = request.NextLevel;
			string itemGiftCfgId = request.ItemGiftCfgId;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());

			(bool bRet, string msg) = gamePlayTowerDefenseComponent.ChkUpgradeItemUnit(playerId, itemUnitId, nextLevel, itemGiftCfgId);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;

				gamePlayTowerDefenseComponent.NoticeToClient(playerId);
			}
			else
			{
				bool success = gamePlayTowerDefenseComponent.UpgradeItemUnit(playerId, itemUnitId, nextLevel, itemGiftCfgId);
				if (success == false)
				{
					response.Error = ErrorCode.ERR_LogicError;
					response.Message = "UpgradeItemUnit Err";

					gamePlayTowerDefenseComponent.NoticeToClient(playerId);
				}
			}
			await ETTask.CompletedTask;
		}
	}
}