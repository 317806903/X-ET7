
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ForceAddGameGoldWhenDebugHandler : AMActorLocationHandler<Unit, C2M_ForceAddGameGoldWhenDebug>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ForceAddGameGoldWhenDebug message)
		{
			// Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
			// if (playerUnit == null)
			// {
			// 	return;
			// }

			if (observerUnit == null)
			{
				return;
			}

			long playerId = observerUnit.Id;

			ET.GamePlayHelper.ChgPlayerCoin(observerUnit.DomainScene(), playerId, CoinTypeInGame.Gold, 1000);

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			gamePlayTowerDefenseComponent.NoticeToClient(playerId);

			await ETTask.CompletedTask;
		}
	}
}