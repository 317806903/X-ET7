using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Gate)]
	public class R2G_GetLoginKeyHandler : AMActorRpcHandler<Scene, R2G_GetLoginKey, G2R_GetLoginKey>
	{
		protected override async ETTask Run(Scene scene, R2G_GetLoginKey request, G2R_GetLoginKey response)
		{
			StartSceneConfig accountSceneConfig = StartSceneConfigCategory.Instance.GetAccountManager(scene.DomainZone());
			A2G_LoginByAccount _A2G_GetPlayerIdByAccount = (A2G_LoginByAccount)await ActorMessageSenderComponent.Instance.Call(
				accountSceneConfig.InstanceId, new G2A_LoginByAccount()
				{
					Account = request.Account,
					Password = request.Password,
					LoginType = request.LoginType,
				});

			if (_A2G_GetPlayerIdByAccount.Error == ErrorCode.ERR_LogicError)
			{
				response.Error = ErrorCode.ERR_LogicError;
				return;
			}

			long key = scene.GetComponent<GateSessionKeyComponent>().Add(request.Account);
			response.Key = key;
			response.GateId = scene.Id;
			response.IsFirstLogin = _A2G_GetPlayerIdByAccount.IsFirstLogin;

			await ETTask.CompletedTask;
		}
	}
}