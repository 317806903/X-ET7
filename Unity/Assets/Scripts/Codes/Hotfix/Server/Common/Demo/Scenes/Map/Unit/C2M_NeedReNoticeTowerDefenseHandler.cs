
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_NeedReNoticeTowerDefenseHandler : AMActorLocationHandler<Unit, C2M_NeedReNoticeTowerDefense>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_NeedReNoticeTowerDefense message)
		{
			// Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);
			// if (playerUnit == null)
			// {
			// 	return;
			// }

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			if (gamePlayTowerDefenseComponent == null)
			{
				return;
			}

			long playerId = observerUnit.Id;
			gamePlayTowerDefenseComponent.NoticeToClient(playerId);

			await ETTask.CompletedTask;
		}
	}
}