namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_CreateMyUnitHandler : AMHandler<M2C_CreateMyUnit>
	{
		protected override async ETTask Run(Session session, M2C_CreateMyUnit message)
		{
			Scene clientScene = session.ClientScene();
			clientScene.GetComponent<PlayerComponent>().PlayerStatus = PlayerStatus.Battle;
			clientScene.GetComponent<PlayerComponent>().MyId = message.Unit.UnitId;
			// 通知场景切换协程继续往下走
			session.DomainScene().GetComponent<ObjectWait>().Notify(new Wait_CreateMyUnit() {Message = message});
			await ETTask.CompletedTask;
		}
	}
}
