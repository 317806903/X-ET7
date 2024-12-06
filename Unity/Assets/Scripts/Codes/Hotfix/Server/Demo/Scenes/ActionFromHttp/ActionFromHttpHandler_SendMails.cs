using System;
using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Server
{
    [ActionFromHttpHandler(ActionFromHttpStatus.SendMails)]
    public class ActionFromHttpHandler_SendMails: IActionFromHttpHandler
    {
        public MailToPlayerType mailToPlayerType;
        public List<long> waitSendPlayerList;
        public string mailType;
        public string mailTitle;
        public string mailContent;
        public Dictionary<string, int> itemCfgList;
        public int mailEffectiveDay;

        public override async ETTask<(bool, string)> ChkParam(Scene scene, Dictionary<string, string> paramDic)
        {
            paramDic.TryGetValue("mailToPlayerType", out string mailToPlayerTypeStr);
            bool result = Enum.TryParse<MailToPlayerType>(mailToPlayerTypeStr, out var mailToPlayerType);
            if (result)
            {
                this.mailToPlayerType = mailToPlayerType;
            }
            else
            {
                return (false, $"param mailToPlayerType[{mailToPlayerTypeStr}] error");
            }

            if (this.mailToPlayerType == MailToPlayerType.PlayerList)
            {
                paramDic.TryGetValue("waitSendPlayerList", out string waitSendPlayerListStr);
                this.waitSendPlayerList = new List<long>();
                foreach (string playerIdStr in waitSendPlayerListStr.Split(new char[] { ',', ';', '|' }))
                {
                    long playerId = long.Parse(playerIdStr);
                    bool isValid = await ET.Server.ActionFromHttpHelper.ChkPlayerIsValid(scene, playerId);
                    if (isValid)
                    {
                        this.waitSendPlayerList.Add(playerId);
                    }
                    else
                    {
                        return (false, $"param waitSendPlayerList[{waitSendPlayerListStr}][{playerIdStr}] error");
                    }
                }
            }

            paramDic.TryGetValue("mailType", out this.mailType);
            MailTypeCfg mailTypeCfg = MailTypeCfgCategory.Instance.Get(this.mailType);
            if (mailTypeCfg == null)
            {
                return (false, $"param mailType[{mailType}] error");
            }

            paramDic.TryGetValue("mailTitle", out mailTitle);
            paramDic.TryGetValue("mailContent", out mailContent);

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
            paramDic.TryGetValue("mailEffectiveDay", out string mailEffectiveDayStr);
            if (string.IsNullOrEmpty(mailEffectiveDayStr))
            {
                return (false, $"param mailEffectiveDay[{mailEffectiveDay}] error");
            }
            this.mailEffectiveDay = int.Parse(mailEffectiveDayStr);

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

            long receiveTime = TimeHelper.ServerNow();
            DateTime limitTimeTmp = TimeHelper.DateTimeNow().AddDays(this.mailEffectiveDay);
            long limitTime = TimeHelper.ToTimeStamp(limitTimeTmp);

            await ET.Server.MailHelper.InsertMailToCenter(scene, -1, mailType, mailTitle, mailContent, itemCfgList, receiveTime, limitTime, mailToPlayerType, waitSendPlayerList, null);

            if (this.mailToPlayerType == MailToPlayerType.PlayerList)
            {
                foreach (long playerId in this.waitSendPlayerList)
                {
                    PlayerCacheHelper.NoticeClientPlayerCacheChg(scene, playerId, PlayerModelType.Mails).Coroutine();
                }
            }

            return (true, "");
        }
    }
}