using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Account)]
	public class G2A_LoginByAccountHandler : AMActorRpcHandler<Scene, G2A_LoginByAccount, A2G_LoginByAccount>
	{
		protected override async ETTask Run(Scene scene, G2A_LoginByAccount request, A2G_LoginByAccount response)
		{
			string account = request.Account;
			string password = request.Password;
			ET.LoginType loginType = (ET.LoginType)request.LoginType;
			string loginIP = request.LoginIP;
			(long playerId, bool isFirstLogin) = await ET.Server.AccountHelper.AccountLogin(scene, account, password, loginType, loginIP);
			response.PlayerId = playerId;
			response.IsFirstLogin = isFirstLogin?1:0;
			if (playerId == (long)ET.PlayerId.PlayerNone)
			{
				response.Error = ErrorCode.ERR_LogicError;
			}

			await ETTask.CompletedTask;
		}
	}
}