using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class M2G_MemberReturnRoomFromBattleHandler : AMActorRpcHandler<Player, M2G_MemberReturnRoomFromBattle, G2M_MemberReturnRoomFromBattle>
	{
		protected override async ETTask Run(Player player, M2G_MemberReturnRoomFromBattle request, G2M_MemberReturnRoomFromBattle response)
		{
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			playerStatusComponent.PlayerStatus = PlayerStatus.Room;


			await playerStatusComponent.NoticeClient();

			await ETTask.CompletedTask;
		}
	}
}