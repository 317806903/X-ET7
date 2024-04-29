using System;


namespace ET.Server
{
	[ActorMessageHandler(SceneType.Pay)]
	public class G2P_GetArcadeCoinQrCodeHandler : AMActorRpcHandler<Scene, G2P_GetArcadeCoinQrCode, P2G_GetArcadeCoinQrCode>
	{
		protected override async ETTask Run(Scene scene, G2P_GetArcadeCoinQrCode request, P2G_GetArcadeCoinQrCode response)
		{
			long playerId = request.PlayerId;
			int arcadeCoinNum = request.ArcadeCoinNum;

#if UNITY_EDITOR
			(bool bRet, string msg, PayComponent payComponent) = await ET.Server.PayHelper.GetNewPayOrder_Editor(scene, playerId, arcadeCoinNum);
			ConfirmWhenEditor(scene, payComponent.orderId).Coroutine();
			response.PayComponentBytes = payComponent.ToBson();
#else
			(bool bRet, string msg, PayComponent payComponent) = await ET.Server.PayHelper.GetNewPayOrder(scene, playerId, arcadeCoinNum);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;
			}
			else
			{
				response.PayComponentBytes = payComponent.ToBson();
			}
#endif

			await ETTask.CompletedTask;
		}

		protected async ETTask ConfirmWhenEditor(Scene scene, long orderId)
		{
			await TimerComponent.Instance.WaitAsync(5000);
			await ET.Server.PayHelper.ConfirmCallBack(scene, orderId, true, "hhh");
		}
	}
}