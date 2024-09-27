
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class C2M_ForceAddHomeHpWhenDebugHandler : AMActorLocationHandler<Unit, C2M_ForceAddHomeHpWhenDebug>
	{
		protected override async ETTask Run(Unit observerUnit, C2M_ForceAddHomeHpWhenDebug message)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			if (observerUnit == null)
			{
				return;
			}
			long playerId = observerUnit.Id;

			GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(observerUnit.DomainScene());
			PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
			Unit homeUnit = putHomeComponent.GetHomeUnit(observerUnit);

			NumericComponent numericComponent = homeUnit.GetComponent<NumericComponent>();
			float curHp = numericComponent.GetAsFloat(NumericType.Hp);
			float maxHp = numericComponent.GetAsFloat(NumericType.MaxHp);
			numericComponent.SetAsFloatToBase(NumericType.MaxHp, maxHp + 200);
			numericComponent.SetAsFloatToBase(NumericType.Hp, curHp + 200);


			await ETTask.CompletedTask;
		}
	}
}