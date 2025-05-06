using System;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_GetArcadeCoinQrCodeHandler : AMRpcHandler<C2G_GetArcadeCoinQrCode, G2C_GetArcadeCoinQrCode>
	{
		protected override async ETTask Run(Session session, C2G_GetArcadeCoinQrCode request, G2C_GetArcadeCoinQrCode response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			int arcadeCoinNum = request.ArcadeCoinNum;

			StartSceneConfig paySceneConfig = StartSceneConfigCategory.Instance.GetPayManager(session.DomainZone());

			P2G_GetArcadeCoinQrCode _P2G_GetArcadeCoinQrCode = (P2G_GetArcadeCoinQrCode) await ActorMessageSenderComponent.Instance.Call(paySceneConfig.InstanceId, new G2P_GetArcadeCoinQrCode()
			{
				PlayerId = playerId,
				ArcadeCoinNum = arcadeCoinNum,
			});

			response.Error = _P2G_GetArcadeCoinQrCode.Error;
			response.Message = _P2G_GetArcadeCoinQrCode.Message;
			response.PayComponentBytes = _P2G_GetArcadeCoinQrCode.PayComponentBytes;

			await ETTask.CompletedTask;
		}
	}
}