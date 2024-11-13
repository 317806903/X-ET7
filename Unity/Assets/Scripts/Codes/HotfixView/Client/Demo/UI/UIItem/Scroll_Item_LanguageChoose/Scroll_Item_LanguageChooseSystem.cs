using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_LanguageChoose))]
	public static class Scroll_Item_LanguageChooseSystem
	{
		public static void RegisterUIEvent(this Scroll_Item_LanguageChoose self)
		{

		}

		public static void HideItem(this Scroll_Item_LanguageChoose self)
		{

		}

		public static void Init(this Scroll_Item_LanguageChoose self, LanguageType languageType, Action callback)
		{
			self.SetItemSelectStatus(false);

			string text = LocalizeComponent.Instance.GetTextValue($"TextCode_key_LanguageChoose_{languageType.ToString()}");
			self.E_TextTextMeshProUGUI.text = text;
			self.E_SelectButton.AddListener(() =>
			{
				callback?.Invoke();
			});
		}

		public static void SetItemSelectStatus(this Scroll_Item_LanguageChoose self, bool selected)
		{
			self.EG_SelectedRectTransform.SetVisible(selected);
		}

	}
}
