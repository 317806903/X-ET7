using System;
using System.Collections.Generic;

namespace ET.Server
{
	[ActorMessageHandler(SceneType.Map)]
	public class R2M_ChkIsBattleEndHandler : AMActorRpcHandler<Scene, R2M_ChkIsBattleEnd, M2R_ChkIsBattleEnd>
	{
		protected override async ETTask Run(Scene scene, R2M_ChkIsBattleEnd request, M2R_ChkIsBattleEnd response)
		{
			GamePlayComponent gamePlayComponent = scene.GetComponent<GamePlayComponent>();
			if (gamePlayComponent == null)
			{
				response.IsBattleEnd = 1;
				return;
			}

			response.IsBattleEnd = gamePlayComponent.ChkIsGameEnd() ? 1:0;

			await ETTask.CompletedTask;
		}
	}
}