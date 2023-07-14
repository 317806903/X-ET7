using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ChgRoomBattleLevelCfgHandler : AMRpcHandler<C2G_ChgRoomBattleLevelCfg, G2C_ChgRoomBattleLevelCfg>
	{
		protected override async ETTask Run(Session session, C2G_ChgRoomBattleLevelCfg request, G2C_ChgRoomBattleLevelCfg response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			long playerId = player.Id;
			long roomId = playerStatusComponent.RoomId;
			string newBattleCfgId = request.NewBattleCfgId;

			StartSceneConfig roomSceneConfig = StartSceneConfigCategory.Instance.GetRoomManager(session.DomainZone());

			R2G_ChgRoomBattleLevelCfg _R2G_ChgRoomBattleLevelCfg = (R2G_ChgRoomBattleLevelCfg) await ActorMessageSenderComponent.Instance.Call(roomSceneConfig.InstanceId, new G2R_ChgRoomBattleLevelCfg()
			{
				PlayerId = playerId,
				RoomId = roomId,
				NewBattleCfgId = newBattleCfgId,
			});
			
			response.Error = _R2G_ChgRoomBattleLevelCfg.Error;
			response.Message = _R2G_ChgRoomBattleLevelCfg.Message;

			await ETTask.CompletedTask;
		}
	}
}