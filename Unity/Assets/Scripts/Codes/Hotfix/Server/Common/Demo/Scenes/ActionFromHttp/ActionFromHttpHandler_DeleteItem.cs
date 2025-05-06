using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [ActionFromHttpHandler(ActionFromHttpStatus.DeleteItem)]
    public class ActionFromHttpHandler_DeleteItem: IActionFromHttpHandler
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
                    return (false, $"param playerList[{playerListStr}]error, [{playerIdStr}] is not player");
                }
            }

            paramDic.TryGetValue("itemCfgList", out string itemCfgListStr);
            this.itemCfgList = new();
            foreach (string tmp1 in itemCfgListStr.Split(new char[] { '|' }))
            {
                var tmp2 = tmp1.Split(new char[] { ';' });
                string itemCfgId = tmp2[0];
                if (ItemCfgCategory.Instance.Contain(itemCfgId) == false)
                {
                    return (false, $"param itemCfgListStr[{tmp1}]error, [{itemCfgId}] is not itemCfgId");
                }
                if (int.TryParse(tmp2[1], out int num))
                {
                    if (num <= 0)
                    {
                        return (false, $"param itemCfgId[{itemCfgId}] num[{num}] <= 0");
                    }
                }
                else
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
                foreach (var item in this.itemCfgList)
                {
                    string itemCfgId = item.Key;
                    int num = item.Value;
                    await PlayerCacheHelper.DeleteItem(scene, playerId, itemCfgId, num);
                }
            }
            return (true, "");
        }
    }
}