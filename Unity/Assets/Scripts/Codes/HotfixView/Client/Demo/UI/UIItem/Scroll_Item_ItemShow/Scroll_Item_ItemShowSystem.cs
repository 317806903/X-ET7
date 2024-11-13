using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(Scroll_Item_ItemShow))]
    public static class Scroll_Item_ItemShowSystem
    {
        public static void RegisterUIEvent(this Scroll_Item_ItemShow self)
        {
        }

        public static void HideItem(this Scroll_Item_ItemShow self)
        {
        }

        public static async ETTask Init(this Scroll_Item_ItemShow self, string itemCfgId, bool needClickShowDetail, int num = 0)
        {
            self.itemCfgId = itemCfgId;
            if (string.IsNullOrEmpty(itemCfgId))
            {
                self.ES_CommonItem.View.uiTransform.SetVisible(false);
                return;
            }

            await self.ES_CommonItem.Init(itemCfgId, needClickShowDetail, false, false);
            self.ES_CommonItem.SetItemNum(num);
            self.ShowCheck(false);
        }

        public static GameObject GetActionButton(this Scroll_Item_ItemShow self)
        {
            return self.ES_CommonItem.GetActionButton();
        }

        public static void SetItemNum(this Scroll_Item_ItemShow self, int num)
        {
            self.ES_CommonItem.SetItemNum(num);
        }

        public static void ShowCheck(this Scroll_Item_ItemShow self, bool isNeedChoose)
        {
            if (string.IsNullOrEmpty(self.itemCfgId))
            {
                self.EG_CheckRectTransform.SetVisible(false);
                return;
            }

            if (ET.ItemHelper.ChkIsToken(self.itemCfgId))
            {
                self.EG_CheckRectTransform.SetVisible(false);
            }
            else
            {
                self.EG_CheckRectTransform.SetVisible(isNeedChoose);
            }
        }

    }
}