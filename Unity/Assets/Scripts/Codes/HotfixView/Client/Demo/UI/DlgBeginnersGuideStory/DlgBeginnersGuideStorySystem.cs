using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[FriendOf(typeof(DlgBeginnersGuideStory))]
	public static class DlgBeginnersGuideStorySystem
	{
		public static void RegisterUIEvent(this DlgBeginnersGuideStory self)
		{
			self.View.E_NextButton.AddListenerAsync(self.DoNext);
		}

		public static void ShowWindow(this DlgBeginnersGuideStory self, ShowWindowData contextData = null)
		{
			self.index = 0;
			DlgBeginnersGuideStory_ShowWindowData dlgBeginnersGuideStoryShowWindowData = (DlgBeginnersGuideStory_ShowWindowData)contextData;
			self.finishCallBack = dlgBeginnersGuideStoryShowWindowData.finishCallBack;

			self.DoNext().Coroutine();
		}

		public static async ETTask DoNext(this DlgBeginnersGuideStory self)
		{
			UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

			await self.ShowStory(self.index++);
		}

		public static async ETTask ShowStory(this DlgBeginnersGuideStory self, int index)
		{
			var list = GlobalSettingCfgCategory.Instance.BeginnersGuideImgs;
			if (index < list.Count)
			{
				foreach (var info in list[index])
				{
					string iconKey = info.Key;
					string iconPath = ResIconCfgCategory.Instance.Get(iconKey).ResName;
					string contextKey = info.Value;
					string context = LocalizeComponent.Instance.GetTextValue(contextKey);
					await self.DoShowStory(iconPath, context);
				}
			}
			else
			{
				UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgBeginnersGuideStory>();
				self.finishCallBack?.Invoke();
			}
		}

		public static async ETTask DoShowStory(this DlgBeginnersGuideStory self, string iconPath, string context)
		{
			Image image = self.View.E_StoryImgImage;
			string imgPath = iconPath;
			await image.SetImageByPath(imgPath);

			self.View.E_TextContextTextMeshProUGUI.text = context;
			await ETTask.CompletedTask;
		}

	}
}
