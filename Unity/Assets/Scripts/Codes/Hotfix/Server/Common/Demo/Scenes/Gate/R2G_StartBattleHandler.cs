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
			playerStatusComponent.MapScale = request.MapScale * 0.01f;
			playerStatusComponent.LastBattleCfgId = request.GamePlayBattleLevelCfgId;
			playerStatusComponent.LastBattleResult = BattleResult.Default;
			await playerStatusComponent.NoticeClient();

			if (player.GetComponent<PlayerSessionComponent>() == null)
			{
				Log.Error($"player.GetComponent<PlayerSessionComponent>() == null");
			}
			else
			{
				Session session = player.GetComponent<PlayerSessionComponent>().Session;
				if (session == null)
				{
					Log.Error($"player.GetComponent<PlayerSessionComponent>().Session == null");
				}
				else
				{
					G2C_EnterBattleNotice _G2C_EnterBattleNotice = new() { };
					session.Send(_G2C_EnterBattleNotice);
				}
			}

			TransferHelper.EnterMap(request.DynamicMapInstanceId, player.Id, player.Level, request.GamePlayBattleLevelCfgId, request.MapScale * 0.01f).Coroutine();

            await ETTask.CompletedTask;
        }
	}
}