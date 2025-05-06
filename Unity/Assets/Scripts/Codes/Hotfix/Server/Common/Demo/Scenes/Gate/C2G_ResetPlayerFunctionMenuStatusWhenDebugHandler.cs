using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
	[MessageHandler(SceneType.Gate)]
	public class C2G_ResetPlayerFunctionMenuStatusWhenDebugHandler : AMHandler<C2G_ResetPlayerFunctionMenuStatusWhenDebug>
	{
		protected override async ETTask Run(Session session, C2G_ResetPlayerFunctionMenuStatusWhenDebug message)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().Player;
			//long playerId = player.Id;
			Scene scene = session.DomainScene();

			long playerId = message.PlayerId;
			if (playerId == (long)ET.PlayerId.PlayerNone)
			{
				playerId = player.Id;
			}

			// 1 强制打开
			// 2 强制关闭
			// 3 按照条件调整
			int operateType = message.OperateType;

			// All 全部
			// AllLock 全部被锁的
			// AllOpenned 全部已打开的
			// 具体 menu1;menu2;...
			string functionMenuCfgIds = message.FunctionMenuCfgIds;

			PlayerFunctionMenuComponent playerFunctionMenuComponent = await PlayerCacheHelper.GetPlayerFunctionMenuByPlayerId(scene, playerId, true);

			bool isNeedChkByCondition = false;
			FunctionMenuStatus functionMenuStatus = FunctionMenuStatus.Lock;
			if (operateType == 1)
			{
				functionMenuStatus = FunctionMenuStatus.Openning;
			}
			else if (operateType == 2)
			{
				functionMenuStatus = FunctionMenuStatus.Lock;
			}
			else
			{
				isNeedChkByCondition = true;
			}

			List<string> functionMenuCfgList = ListComponent<string>.Create();
			if (functionMenuCfgIds == "All")
			{
				functionMenuCfgList = playerFunctionMenuComponent.GetFunctionMenuList();
			}
			else if (functionMenuCfgIds == "AllLock")
			{
				functionMenuCfgList = playerFunctionMenuComponent.GetLockFunctionMenuList();
			}
			else if (functionMenuCfgIds == "AllOpenned")
			{
				functionMenuCfgList = playerFunctionMenuComponent.GetOpennedFunctionMenuList();
			}
			else
			{
				var list = functionMenuCfgIds.Split(";");
				foreach (string cfgId in list)
				{
					string cfgIdTmp = cfgId.Trim();
					if (string.IsNullOrEmpty(cfgIdTmp))
					{
						continue;
					}
					functionMenuCfgList.Add(cfgIdTmp);
				}
			}

			foreach (string functionMenuCfgId in functionMenuCfgList)
			{
				FunctionMenuStatus functionMenuStatusTmp = functionMenuStatus;
				FunctionMenuCfg functionMenuCfg = FunctionMenuCfgCategory.Instance.Get(functionMenuCfgId);
				if (isNeedChkByCondition)
				{
					bool bRet = await ET.Server.PlayerCacheHelper.DealPlayerFunctionMenuOne(scene, playerId, functionMenuCfg, null);
					if (bRet)
					{
						functionMenuStatusTmp = FunctionMenuStatus.Openning;
					}
					else
					{
						functionMenuStatusTmp = FunctionMenuStatus.Lock;
					}
				}

				if (functionMenuCfg.OpenCondition is FunctionMenuConditionDefault)
				{
					functionMenuStatusTmp = FunctionMenuStatus.Openned;
				}

				FunctionMenuStatus curFunctionMenuStatus = playerFunctionMenuComponent.GetStatus(functionMenuCfgId);
				if (curFunctionMenuStatus == FunctionMenuStatus.Openned && functionMenuStatusTmp == FunctionMenuStatus.Openning)
				{
					continue;
				}
				if (curFunctionMenuStatus == functionMenuStatusTmp)
				{
					continue;
				}
				playerFunctionMenuComponent.ChgStatus(functionMenuCfgId, functionMenuStatusTmp);
			}

			await ET.Server.PlayerCacheHelper.SavePlayerModel(scene, playerId, PlayerModelType.FunctionMenu, null, PlayerModelChgType.PlayerFunctionMenu_ResetStatusWhenDebug);

            await ETTask.CompletedTask;
		}
	}
}