
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ForceNextWaveWhenDebugHandler : AMActorLocationHandler<Unit, C2M_ForceNextWaveWhenDebug>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ForceNextWaveWhenDebug message)
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

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());

			if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
			{
				MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();

				if (gamePlayTowerDefenseComponent.IsEndlessChallengeMonster())
				{
					monsterWaveCallComponent.curIndex++;
					gamePlayTowerDefenseComponent.NoticeToClientAll();
				}
				else
				{
					if (monsterWaveCallComponent.curIndex == monsterWaveCallComponent.totalCount - 2)
					{
						gamePlayTowerDefenseComponent.TransToGameResult(true, false).Coroutine();
					}
					else
					{
						monsterWaveCallComponent.curIndex++;
						gamePlayTowerDefenseComponent.NoticeToClientAll();
					}
				}

			}
			else if (gamePlayTowerDefenseComponent.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
			{
				MonsterWaveCallComponent monsterWaveCallComponent = gamePlayTowerDefenseComponent.GetComponent<MonsterWaveCallComponent>();
				monsterWaveCallComponent.ClearMonsterCallWhenDebug();
			}

			await ETTask.CompletedTask;
		}
	}
}