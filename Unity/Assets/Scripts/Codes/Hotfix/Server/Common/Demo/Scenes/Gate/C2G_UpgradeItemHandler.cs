using System;


namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_UpgradeItemHandler : AMRpcHandler<C2G_UpgradeItem, G2C_UpgradeItem>
	{
		protected override async ETTask Run(Session session, C2G_UpgradeItem request, G2C_UpgradeItem response)
		{
			Scene scene = session.DomainScene();
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			long playerId = player.Id;
			string itemCfgId = request.ItemCfgId;

			if (ET.ItemHelper.ChkIsTower(itemCfgId))
			{
			}
			else if (ET.ItemHelper.ChkIsSkill(itemCfgId))
			{
			}
			else
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = $"itemCfgId[{itemCfgId}] type not support";
				return;
			}

			PlayerBackPackComponent playerBackPackComponent = await PlayerCacheHelper.GetPlayerBackPackByPlayerId(scene, playerId, true);
			if (scene.IsDisposed)
			{
				return;
			}
			bool isExist = playerBackPackComponent.ChkItemExist(itemCfgId);
			if (isExist == false)
			{
				response.Error = ErrorCode.ERR_LogicError;
				response.Message = $"itemCfgId[{itemCfgId}] type not Exist";
				return;
			}

			int maxUpgradeLevel = ET.ItemHelper.GetItemMaxUpgradeLevel(itemCfgId);
			int curLevel = playerBackPackComponent.GetItemLevelWhenStack(itemCfgId);
			if (curLevel == maxUpgradeLevel)
			{
				string msg = $"it is max level";
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = msg;
				return;
			}

			int curDiamond = await PlayerCacheHelper.GetTokenDiamondByPlayerId(scene, playerId);
			(bool bRet, int costDiamond) = playerBackPackComponent.ChkUpgradeItem(itemCfgId, curDiamond);
			if (bRet)
			{
				playerBackPackComponent.UpgradeItem(itemCfgId);
				await PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.BackPack, null, PlayerModelChgType.PlayerBackPack_UpgradeItem);

				await PlayerCacheHelper.ReduceTokenDiamond(scene, playerId, costDiamond);

			}
			else
			{
				response.Error = ET.ErrorCode.ERR_LogicError;
				response.Message = $"Diamond not enough";
				return;
			}
			await ETTask.CompletedTask;
		}
	}
}