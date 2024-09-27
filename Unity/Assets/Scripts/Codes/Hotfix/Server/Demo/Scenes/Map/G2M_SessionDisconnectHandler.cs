

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class G2M_SessionDisconnectHandler : AMActorLocationHandler<Unit, G2M_SessionDisconnect>
	{
		protected override async ETTask Run(Unit observerUnit, G2M_SessionDisconnect message)
		{
			//Unit playerUnit = ET.GamePlayHelper.GetCurPlayerUnit(observerUnit);

			long playerId = observerUnit.Id;
			await LocationProxyComponent.Instance.RemoveLocation(playerId, LocationType.Player);

			await ETTask.CompletedTask;
		}
    }

}