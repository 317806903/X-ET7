using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ReturnBackBattleHandler : AMRpcHandler<C2G_ReturnBackBattle, G2C_ReturnBackBattle>
	{
		protected override async ETTask Run(Session session, C2G_ReturnBackBattle request, G2C_ReturnBackBattle response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_ReturnBackBattle _R2G_ReturnBackBattle = (R2G_ReturnBackBattle) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_ReturnBackBattle()
			{
				PlayerId = playerId,
			});

			await ETTask.CompletedTask;
		}
	}
}