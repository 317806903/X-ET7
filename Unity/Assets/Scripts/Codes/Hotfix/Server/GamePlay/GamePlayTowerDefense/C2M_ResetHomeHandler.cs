using System;
using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ResetHomeHandler : AMActorLocationRpcHandler<Unit, C2M_ResetHome, M2C_ResetHome>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ResetHome request, M2C_ResetHome response)
		{
			Unit playerUnit = ET.GamePlayHelper.GetPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;

			GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(observerUnit.DomainScene());

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			if (putHomeComponent != null)
			{
				putHomeComponent.ResetByPlayer(playerId);
			}
			PutMonsterCallComponent putMonsterCallComponent = gamePlayTowerDefenseComponent.GetComponent<PutMonsterCallComponent>();
			if (putMonsterCallComponent != null)
			{
				List<long> playerList = gamePlayComponent.GetPlayerList();

				foreach (long playerIdTmp in playerList)
				{
					if (ET.GamePlayHelper.ChkIsFriend(observerUnit.DomainScene(), playerId, playerIdTmp))
					{
						putMonsterCallComponent.ResetByPlayer(playerIdTmp);
					}
				}
			}

			await gamePlayTowerDefenseComponent.TransToPutHome();

			await ETTask.CompletedTask;
		}
	}
}