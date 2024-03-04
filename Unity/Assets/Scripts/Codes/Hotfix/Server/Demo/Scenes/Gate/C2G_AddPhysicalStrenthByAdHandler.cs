using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_AddPhysicalStrenthByAdHandler : AMRpcHandler<C2G_AddPhysicalStrenthByAd, G2C_AddPhysicalStrenthByAd>
	{
		protected override async ETTask Run(Session session, C2G_AddPhysicalStrenthByAd request, G2C_AddPhysicalStrenthByAd response)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			if (playerId != request.PlayerId)
			{
				string msg = $"playerId[{playerId}] != request.PlayerId[{request.PlayerId}]";
				Log.Error(msg);
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			await ET.Server.PlayerCacheHelper.AddPhysicalStrenth(session.DomainScene(), playerId, GlobalSettingCfgCategory.Instance.RecoverIncreaseOfPhysicalStrengthByAd, PlayerModelChgType.PlayerBaseInfo_111);

			await ETTask.CompletedTask;
		}
	}
}