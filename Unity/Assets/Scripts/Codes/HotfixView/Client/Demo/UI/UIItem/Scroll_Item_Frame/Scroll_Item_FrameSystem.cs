using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_Frame))]
	public static class Scroll_Item_FrameSystem
	{
        public static void RegisterUIEvent(this Scroll_Item_Frame self)
        {

        }

        public static void HideItem(this Scroll_Item_Frame self)
        {

        }


        public static void ShowFrameItem(this Scroll_Item_Frame self, string itemCfgId, bool needClickShowDetail)
        {
            if (needClickShowDetail)
            {
                ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onClick.AddListener((go, xx) =>
                {
                    UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

                    self.ShowDetails(itemCfgId);
                });
            }
        }

        public static void ShowDetails(this Scroll_Item_Frame self, string itemCfgId)
        {
            if (string.IsNullOrEmpty(itemCfgId))
            {
                return;
            }

            Vector3 pos = ET.Client.EUIHelper.GetRectTransformMidTop(self.uiTransform.GetComponent<RectTransform>());
            ET.Client.UIManagerHelper.ShowItemInfoWnd(self.DomainScene(), itemCfgId, pos, false, false);
        }


    }
}
