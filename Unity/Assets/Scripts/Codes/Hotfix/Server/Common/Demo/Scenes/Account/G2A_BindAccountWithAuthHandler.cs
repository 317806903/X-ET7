using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Account)]
	public class G2A_BindAccountWithAuthHandler : AMActorRpcHandler<Scene, G2A_BindAccountWithAuth, A2G_BindAccountWithAuth>
	{
		protected override async ETTask Run(Scene scene, G2A_BindAccountWithAuth request, A2G_BindAccountWithAuth response)
		{
			string account = request.Account;
			string bindAccountId = request.BindAccount;
			ET.LoginType loginType = (ET.LoginType)request.LoginType;
            bool isBindSuccess = await ET.Server.AccountHelper.AccountBind(scene, account, bindAccountId, loginType);
            response.IsBindSuccess = isBindSuccess?1:0;
			// if (isBindSuccess == false)
			// {
			// 	response.Error = ErrorCode.ERR_LogicError;
            //     response.Message = $"bind fail: account already exist";
			// }

			await ETTask.CompletedTask;
		}
	}
}