using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [ActionFromHttpHandler(ActionFromHttpStatus.AddItem)]
    public class ActionFromHttpHandler_AddItem: IActionFromHttpHandler
    {
        public List<long> playerList;
        public Dictionary<string, int> itemCfgList;

        public override async ETTask<(bool, string)> ChkParam(Scene scene, Dictionary<string, string> paramDic)
        {
            paramDic.TryGetValue("playerList", out string playerListStr);
            this.playerList = new List<long>();
            foreach (string playerIdStr in playerListStr.Split(new char[] { ',', ';', '|' }))
            {
                long playerId = long.Parse(playerIdStr);
                bool isValid = await ET.Server.ActionFromHttpHelper.ChkPlayerIsValid(scene, playerId);
                if (isValid)
                {
                    this.playerList.Add(playerId);
                }
                else
                {
                    return (false, $"param playerList[{playerListStr}][{playerIdStr}] error");
                }
            }

            paramDic.TryGetValue("itemCfgList", out string itemCfgListStr);
            this.itemCfgList = new();
            foreach (string tmp1 in itemCfgListStr.Split(new char[] { '|' }))
            {
                var tmp2 = tmp1.Split(new char[] { ';' });
                string itemCfgId = tmp2[0];
                int num = int.Parse(tmp2[1]);
                if (num <= 0)
                {
                    return (false, $"param itemCfgId[{itemCfgId}] num[{num}] error");
                }
                this.itemCfgList.Add(itemCfgId, num);
            }

            return (true, "");
        }

        public override async ETTask<(bool bRet, string msg)> Run(Scene scene, Dictionary<string, string> paramDic)
        {
            await ETTask.CompletedTask;
            (bool bRet, string msg) = await ChkParam(scene, paramDic);
            if (bRet == false)
            {
                return (bRet, msg);
            }

            foreach (long playerId in playerList)
            {
                await PlayerCacheHelper.AddItems(scene, playerId, this.itemCfgList);
            }
            return (true, "");
        }
    }
}