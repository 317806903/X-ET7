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
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUITip()
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

        public static async ETTask<bool> ReplaceBattleDeck(Scene clientScene, int replaceIndex, string itemCfgId)
        {
            try
            {
                C2G_ReplaceBattleDeck _C2GReplaceBattleDeck = new ();
                _C2GReplaceBattleDeck.ReplaceIndex = replaceIndex;
                _C2GReplaceBattleDeck.ItemCfgId = itemCfgId;
                G2C_ReplaceBattleDeck _G2C_ReplaceBattleDeck = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2GReplaceBattleDeck) as G2C_ReplaceBattleDeck;
                if (_G2C_ReplaceBattleDeck.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUITip()
                    {
                        tipMsg = _G2C_ReplaceBattleDeck.Message,
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

        public static async ETTask<bool> UpgradeItem(Scene clientScene, string itemCfgId)
        {
            try
            {
                C2G_UpgradeItem _C2GUpgradeItem = new ();
                _C2GUpgradeItem.ItemCfgId = itemCfgId;
                G2C_UpgradeItem _G2C_UpgradeItem = await ET.Client.SessionHelper.GetSession(clientScene).Call(_C2GUpgradeItem) as G2C_UpgradeItem;
                if (_G2C_UpgradeItem.Error != ET.ErrorCode.ERR_Success)
                {
                    EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUITip()
                    {
                        tipMsg = _G2C_UpgradeItem.Message,
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