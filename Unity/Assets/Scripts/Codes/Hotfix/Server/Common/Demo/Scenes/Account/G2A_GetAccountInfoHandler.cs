using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Account)]
	public class G2A_GetAccountInfoHandler : AMActorRpcHandler<Scene, G2A_GetAccountInfo, A2G_GetAccountInfo>
	{
		protected override async ETTask Run(Scene scene, G2A_GetAccountInfo request, A2G_GetAccountInfo response)
		{
			string accountId = request.AccountId;
			AccountComponent accountComponent = ET.Server.AccountHelper.GetAccountComponent(scene, accountId);
			if (accountComponent == null)
			{
				response.Error = ErrorCode.ERR_LogicError;
			}
			else
			{
				response.AccountComponentBytes = accountComponent.ToBson();
			}
			await ETTask.CompletedTask;
		}
	}
}