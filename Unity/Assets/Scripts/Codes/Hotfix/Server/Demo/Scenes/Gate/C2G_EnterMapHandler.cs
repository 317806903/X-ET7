using System;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_EnterMapHandler : AMRpcHandler<C2G_EnterMap, G2C_EnterMap>
	{
		protected override async ETTask Run(Session session, C2G_EnterMap request, G2C_EnterMap response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;

			// 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
			player.RemoveComponent<GateMapComponent>();
			GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();

			
			string gamePlayBattleLevelCfgId = request.GamePlayBattleLevelCfgId;
			GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
			StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), gamePlayBattleLevelCfg.SceneMap);

			PlayerStatusComponent playerStatusComponent = player.GetComponent<PlayerStatusComponent>();
			playerStatusComponent.PlayerGameMode = PlayerGameMode.SingleMap;
			playerStatusComponent.PlayerStatus = PlayerStatus.Battle;
			
			await playerStatusComponent.NoticeClient();

			G2C_EnterBattleNotice _G2C_EnterBattleNotice = new() { };
			player.GetComponent<PlayerSessionComponent>()?.Session?.Send(_G2C_EnterBattleNotice);

			TransferHelper.EnterMap(startSceneConfig.InstanceId, player.Id, 1, request.GamePlayBattleLevelCfgId).Coroutine();

		}
	}
}