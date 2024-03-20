using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class G2OG_LoginInAtOtherWhereHandler : AMActorRpcHandler<Player, G2OG_LoginInAtOtherWhere, OG2G_LoginInAtOtherWhere>
	{
		protected override async ETTask Run(Player player, G2OG_LoginInAtOtherWhere request, OG2G_LoginInAtOtherWhere response)
		{
			G2C_LoginInAtOtherWhere _G2C_LoginInAtOtherWhere = new();
			player?.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_LoginInAtOtherWhere);
			await TimerComponent.Instance.WaitFrameAsync();
		}
	}
}