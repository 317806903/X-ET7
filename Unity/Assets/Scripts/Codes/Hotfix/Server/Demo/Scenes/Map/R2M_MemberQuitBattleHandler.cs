using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class R2M_MemberQuitBattleHandler : AMActorRpcHandler<Scene, R2M_MemberQuitBattle, M2R_MemberQuitBattle>
	{
		protected override async ETTask Run(Scene scene, R2M_MemberQuitBattle request, M2R_MemberQuitBattle response)
		{
			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			long playerId = request.PlayerId;

			gamePlayComponent?.PlayerQuitBattle(playerId, true);

			await ETTask.CompletedTask;
		}
	}
}