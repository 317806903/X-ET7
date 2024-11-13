using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_BuyItemHandler : AMRpcHandler<C2G_BuyItem, G2C_BuyItem>
	{
		protected override async ETTask Run(Session session, C2G_BuyItem request, G2C_BuyItem response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			string itemCfgId = request.ItemCfgId;

			(bool bRet, string msg) = await ItemHelper.BuyItem(scene, playerId, itemCfgId);
			if (bRet == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = msg;
			}

			await ETTask.CompletedTask;
		}
	}
}