using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(PayComponent))]
    public static class PayComponentSystem
    {
        public static void Init(this PayComponent self, long playerId, int coinNum)
        {
            self.orderId = self.Id;
            self.playerId = playerId;
            self.createOrderTime = TimeHelper.ServerNow();
            self.payStatus = PayStatus.Default;
            self.coinNum = coinNum;
            self.moneyValue = ET.Server.PayHelper.GetMoneyValue(coinNum);
        }

        public static (bool bRet, string msg) GetWXUrlCallBack(this PayComponent self, string sWXUrl)
        {
            if (self.payStatus != PayStatus.Default)
            {
                string msg = $"The OrderId[{self.orderId}] is not PayStatus.Default";
                return (false, msg);
            }

            self.sWXUrl = sWXUrl;
            self.payStatus = PayStatus.GetWXUrlSuccessed;
            return (true, "");
        }

        public static async ETTask<(bool bRet, string msg)> ConfirmCallBack(this PayComponent self, bool paySucessed, string payResultMsg)
        {
            if (self.payStatus == PayStatus.Default)
            {
                string msg = $"The OrderId[{self.orderId}] not get WXUrl";
                return (false, msg);
            }

            if (self.payStatus == PayStatus.PaySuccessed)
            {
                string msg = $"The OrderId[{self.orderId}] has been pay successful";
                return (false, msg);
            }

            if (self.payStatus == PayStatus.AddCoinNumSuccessed)
            {
                string msg = $"The OrderId[{self.orderId}] has been AddCoinNumSuccessed successful";
                return (false, msg);
            }

            if (self.payStatus == PayStatus.PayFailed)
            {
                string msg = $"The OrderId[{self.orderId}] has been pay failed";
                return (false, msg);
            }

            {
                self.confirmOrderTime = TimeHelper.ServerNow();
                self.payResultMsg = payResultMsg;

                if (paySucessed)
                {
                    self.payStatus = PayStatus.PaySuccessed;
                    self.SetDataCacheAutoWrite();
                    await self.AddPlayerArcadeCoinNum();
                    self.payStatus = PayStatus.AddCoinNumSuccessed;
                }
                else
                {
                    self.payStatus = PayStatus.PayFailed;
                }
                self.SetDataCacheAutoWrite();
                return (true, "");
            }
        }

        public static async ETTask AddPlayerArcadeCoinNum(this PayComponent self)
        {
            await ET.Server.PlayerCacheHelper.AddArcadeCoin(self.DomainScene(), self.playerId, self.coinNum);
        }

    }
}