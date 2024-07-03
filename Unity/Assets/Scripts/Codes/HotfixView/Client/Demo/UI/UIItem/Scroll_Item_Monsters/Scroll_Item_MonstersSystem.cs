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

		public static void ShowMonsterItem(this Scroll_Item_Monsters self, string itemCfgId, bool needClickShowDetail,Vector3 detailsOffset = default)
		{
			ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).RemoveAllListeners();
			if (needClickShowDetail)
			{
				ET.EventTriggerListener.Get(self.EButton_SelectButton.gameObject).onClick.AddListener((go, xx) =>
				{
					UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
					self.ShowDetails(itemCfgId,detailsOffset);
				});
			}

			if (string.IsNullOrEmpty(itemCfgId))
			{
				return;
			}
			string iconPath = ItemHelper.GetItemIcon(itemCfgId);
			if (string.IsNullOrEmpty(iconPath) == false)
			{
				self.EImage_MonsterImage.SetImageByPath(iconPath).Coroutine();
			}
		}

		public static void ShowDetails(this Scroll_Item_Monsters self, string itemCfgId, Vector3 detailsOffset)
		{
			if (string.IsNullOrEmpty(itemCfgId))
			{
				return;
			}

			ET.Client.UIManagerHelper.ShowItemInfoWnd(self.DomainScene(), itemCfgId, self.uiTransform.position+ detailsOffset);
		}

	}
}
