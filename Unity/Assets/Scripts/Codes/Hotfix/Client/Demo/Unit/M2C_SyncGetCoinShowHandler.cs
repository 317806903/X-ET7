using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_SyncGetCoinShowHandler : AMHandler<M2C_SyncGetCoinShow>
	{
		protected override async ETTask Run(Session session, M2C_SyncGetCoinShow message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			if (currentScene == null)
			{
				return;
			}

			long unitId = message.UnitId;
			CoinType coinType = (CoinType)message.CoinType;
			int chgValue = message.ChgValue;

			UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
			if (unitComponent == null)
			{
				return;
			}
			Unit unit = unitComponent.Get(unitId);
			if (unit == null)
			{
				return;
			}
			EventType.SyncGetCoinShow _SyncGetCoinShow = new ()
			{
				unit = unit,
				coinType = coinType,
				chgValue = chgValue,
			};
			EventSystem.Instance.Publish(unit.DomainScene(), _SyncGetCoinShow);

			await ETTask.CompletedTask;
		}
	}
}
