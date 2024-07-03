using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_Gifts))]
	public static class Scroll_Item_GiftsSystem
	{
		public static async ETTask Init(this Scroll_Item_Gifts self , KeyValuePair<string,int> ItemcfgNum)
		{
            //设置图片
            if (!ItemcfgNum.Key.Equals(null)&& ItemcfgNum.Value >= 0)
            {
                await self.EbtnIconImage.SetImageByPath(ItemHelper.GetItemIcon(ItemcfgNum.Key));
                self.ETxtNumTextMeshProUGUI.SetText(ItemcfgNum.Value.ToString());
            }

            //WJTODO根据不同的Item实现点击有不同的效果
            self.ShowGiftItem(ItemcfgNum.Key,true);
        }

        public static void ShowGiftItem(this Scroll_Item_Gifts self, string itemCfgId, bool needClickShowDetail)
        {
            if (needClickShowDetail)
            {
                self.EbtnIconButton.onClick.AddListener(() =>
                {
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
                    self.ShowDetails(itemCfgId);
                });
            }
        }

        public static void ShowDetails(this Scroll_Item_Gifts self, string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }

            ET.Client.UIManagerHelper.ShowItemInfoWnd(self.DomainScene(), itemCfgId, self.uiTransform.position);
        }
    }
}
