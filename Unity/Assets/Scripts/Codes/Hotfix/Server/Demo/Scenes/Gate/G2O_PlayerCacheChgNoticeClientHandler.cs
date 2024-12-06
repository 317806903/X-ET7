using System.Xml.Schema;
using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class O2G_PlayerCacheChgNoticeClientHandler : AMActorRpcHandler<Player, O2G_PlayerCacheChgNoticeClient, G2O_PlayerCacheChgNoticeClient>
	{
		protected override async ETTask Run(Player player, O2G_PlayerCacheChgNoticeClient request, G2O_PlayerCacheChgNoticeClient response)
		{
			Scene scene = player.DomainScene();
			long playerId = player.Id;

			PlayerModelType playerModelType = (PlayerModelType)request.PlayerModelType;
			long sceneInstanceId = request.SceneInstanceId;
			if (sceneInstanceId != scene.InstanceId)
			{
				PlayerCacheHelper.ClearPlayerModel(scene, playerId, playerModelType);
			}

			G2C_PlayerCacheChgNotice _G2C_PlayerCacheChgNotice = new()
			{
				PlayerModelType = (int)playerModelType,
			};
			player?.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_PlayerCacheChgNotice);
			await TimerComponent.Instance.WaitFrameAsync();
		}
	}
}