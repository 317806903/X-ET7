using System;
using MongoDB.Bson;
using Unity.Mathematics;

namespace ET.Client
{
    public static class ItemHelper
    {
        public static async ETTask<bool> BuyItem(Scene clientScene, string itemCfgId)
        {
            try
            {
                C2G_BuyItem _C2GBuyItem = new ();
                _C2GBuyItem.ItemCfgId = itemCfgId;
                G2C_BuyItem _G2C_BuyItem = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2GBuyItem) as G2C_BuyItem;
                if (_G2C_BuyItem.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeUITip()
                    {
                        tipMsg = _G2C_BuyItem.Message,
                    });
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return false;
        }

    }
}