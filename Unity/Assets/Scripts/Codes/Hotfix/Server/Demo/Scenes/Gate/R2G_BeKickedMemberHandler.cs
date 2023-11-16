using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class R2G_BeKickedMemberHandler : AMActorRpcHandler<Player, R2G_BeKickedMember, G2R_BeKickedMember>
	{
		protected override async ETTask Run(Player player, R2G_BeKickedMember request, G2R_BeKickedMember response)
		{
			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			int isKickWhenBattle = 0;
			if (playerStatusComponent.PlayerStatus == PlayerStatus.Battle)
			{
				isKickWhenBattle = 1;
			}
			playerStatusComponent.SubRoomType = SubRoomType.None;
			playerStatusComponent.PlayerStatus = PlayerStatus.Hall;
			playerStatusComponent.RoomId = 0;

			await playerStatusComponent.NoticeClient();

			G2C_BeKickMemberOutRoom _G2C_BeKickMemberOutRoom = new()
			{
				IsKickWhenBattle = isKickWhenBattle,
			};
			player?.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_BeKickMemberOutRoom);
		}
	}
}