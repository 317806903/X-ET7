using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_Monsters))]
	public static class Scroll_Item_MonstersSystem
	{
		public static void Init(this Scroll_Item_Monsters self)
		{
		}

		public static async ETTask ShowMonsterItem(this Scroll_Item_Monsters self, string itemCfgId, bool needClickShowDetail)
		{
			ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).RemoveAllListeners();
			if (needClickShowDetail)
			{
				ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onClick.AddListener((go, xx) =>
				{
					UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
					self.ShowDetails(itemCfgId);
				});
			}

			if (string.IsNullOrEmpty(itemCfgId))
			{
				return;
			}

			await self.EImage_MonsterImage.SetImageByItemCfgId(itemCfgId);
		}

		public static void ShowDetails(this Scroll_Item_Monsters self, string itemCfgId)
		{
			if (string.IsNullOrEmpty(itemCfgId))
			{
				return;
			}

			Vector3 pos = ET.Client.EUIHelper.GetRectTransformMidTop(self.uiTransform.GetComponent<RectTransform>());
			ET.Client.UIManagerHelper.ShowItemInfoWnd(self.DomainScene(), itemCfgId, pos);
		}

	}
}
