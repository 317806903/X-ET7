using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(PayManagerComponent))]
    public static class PayManagerComponentSystem
    {
        [ObjectSystem]
        public class PayManagerComponentAwakeSystem : AwakeSystem<PayManagerComponent>
        {
            protected override void Awake(PayManagerComponent self)
            {
            }
        }

        public static PayComponent GetNewPayOrder(this PayManagerComponent self, long playerId, int coinNum)
        {
            PayComponent payComponent = self.AddChild<PayComponent>();
            payComponent.Init(playerId, coinNum);
            payComponent.SetDataCacheAutoWrite();
            return payComponent;
        }

        public static async ETTask<PayComponent> GetPayOrder(this PayManagerComponent self, long orderId)
        {
            PayComponent payComponent = self.GetChild<PayComponent>(orderId);
            if (payComponent == null)
            {
                payComponent = await ET.Server.DBHelper.LoadDBWithParent2Child<PayComponent>(self, orderId, false);
            }
            return payComponent;
        }

        public static async ETTask<(bool bRet, string msg)> GetWXUrlCallBack(this PayManagerComponent self, long orderId, string sWXUrl)
        {
            PayComponent payComponent = await self.GetPayOrder(orderId);
            if (payComponent == null)
            {
                Log.Error($"ET.Server.PayManagerComponentSystem.GetWXUrlCallBack orderId[{orderId}] payComponent == null");
                string msg = $"The OrderId[{orderId}] not found";
                return (false, msg);
            }
            return payComponent.GetWXUrlCallBack(sWXUrl);
        }

        public static async ETTask<(bool bRet, string msg)> ConfirmCallBack(this PayManagerComponent self, long orderId, bool paySucessed, string payResultMsg)
        {
            PayComponent payComponent = await self.GetPayOrder(orderId);
            if (payComponent == null)
            {
                Log.Error($"ET.Server.PayManagerComponentSystem.ConfirmCallBack orderId[{orderId}] payComponent == null");
                string msg = $"The OrderId[{orderId}] not found";
                return (false, msg);
            }
            return await payComponent.ConfirmCallBack(paySucessed, payResultMsg);
        }
    }
}