using ET.Ability;
using Unity.Mathematics;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class R2G_StartBattleHandler : AMActorRpcHandler<Player, R2G_StartBattle, G2R_StartBattle>
	{
		protected override async ETTask Run(Player player, R2G_StartBattle request, G2R_StartBattle response)
		{
			// 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
			player.RemoveComponent<GateMapComponent>();
			GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();

			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			playerStatusComponent.PlayerStatus = PlayerStatus.Battle;
			
			G2C_EnterBattleNotice _G2C_EnterBattleNotice = new() { };
			player.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_EnterBattleNotice);

			TransferHelper.EnterMap(request.DynamicMapId, player.Id, player.Level, request.GamePlayBattleLevelCfgId).Coroutine();


            await ETTask.CompletedTask;
        }
	}
}