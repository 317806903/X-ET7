using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Schema;
using UnityEditor.TestTools.TestRunner.Api;

namespace ET.Client
{
	[FriendOf(typeof(DlgLanguageChoose))]
	public static class DlgLanguageChooseSystem
	{
		public static void RegisterUIEvent(this DlgLanguageChoose self)
		{
			self.languages = ChannelSettingComponent.Instance.GetLanguageTypeList();

			self.View.E_BG_ClickButton.AddListenerAsync(self.OnClickBG);
			self.View.ELoopScrollList_LanguageLoopVerticalScrollRect.prefabSource.prefabName = "Item_LanguageChoose";
			self.View.ELoopScrollList_LanguageLoopVerticalScrollRect.prefabSource.poolSize = 6;
            self.View.ELoopScrollList_LanguageLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
			{
				self.AddItemRefreshCallBack(transform, i);
			});

		}

		public static async ETTask ShowWindow(this DlgLanguageChoose self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();
			self.ShowBg();

			self.AddUIScrollItems(ref self.languageItemDic, self.languages.Count);
			self.View.ELoopScrollList_LanguageLoopVerticalScrollRect.SetVisible(true, self.languages.Count);
			await self.DefaultLanguage();
		}

		public static bool ChkCanClickBg(this DlgLanguageChoose self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgLanguageChoose self)
		{

        }

		public static void AddItemRefreshCallBack(this DlgLanguageChoose self,Transform transform, int index)
		{
			Scroll_Item_LanguageChoose scroll_Item_LanguageChoose = self.languageItemDic[index].BindTrans(transform);
			LanguageType languageType = self.languages[index];
			scroll_Item_LanguageChoose.Init(languageType, () => {
				self.SetLanguageType(languageType, index);
			});
		}

		// 检查 AR 相机是否启用后设置不同的背景
		public static void ShowBg(this DlgLanguageChoose self)
		{
			bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
			if (isARCameraEnable)
			{
				self.View.EG_bgARRectTransform.SetVisible(true);
				self.View.EG_bgRectTransform.SetVisible(false);
			}
			else
			{
				self.View.EG_bgARRectTransform.SetVisible(false);
				self.View.EG_bgRectTransform.SetVisible(true);
			}
		}

		public static async ETTask OnClickBG(this DlgLanguageChoose self)
		{
			if (!self.ChkCanClickBg())
				return;
			UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgLanguageChoose>();
			await ETTask.CompletedTask;
		}

		//设置容器中item对象的选中状态
		public static void SetItemStatus(this DlgLanguageChoose self, int index)
		{
			if(index==self.frontLanguageIndex)
			{
				self.languageItemDic[index].SetItemSelectStatus(true);
			}
			else
			{
				self.languageItemDic[index].SetItemSelectStatus(true);
				self.languageItemDic[self.frontLanguageIndex].SetItemSelectStatus(false);
				self.frontLanguageIndex = index;
			}
		}

		//选中上次操作选择的语言
		public static async ETTask DefaultLanguage(this DlgLanguageChoose self)
		{
            LanguageType languageType = LocalizeComponent.Instance.CurrentLanguage;
            if (self.languages.Contains(languageType))
            {
                for (int i = 0; i < self.languages.Count; i++)
                {
                    if (self.languages[i] == languageType)
                    {
                        self.frontLanguageIndex = i;
                        break;
                    }

                }
            }
			self.SetItemStatus(self.frontLanguageIndex);
            await ETTask.CompletedTask;
		}

		//切换语言类型
		public static void SetLanguageType(this DlgLanguageChoose self, LanguageType languageType, int index)
		{
            string tipInfo = LocalizeComponent.Instance.GetTextValue("TextCode_key_LanguageChoose_TipInfo");

            UIManagerHelper.ShowConfirm(self.DomainScene(), tipInfo, () =>
            {
#if UNITY_EDITOR
                LocalizeComponent.Instance.IsShowLanguagePre = ResConfig.Instance.IsShowLanguagePre;
#endif
                LocalizeComponent.Instance.SwitchLanguage(languageType, true);
                self.SetItemStatus(index);
                UIManagerHelper.GetUIComponent(self.DomainScene()).GetDlgLogic<DlgGameModeSetting>().SetLanguageText(languageType);
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgLanguageChoose>();
            }, null);

        }
	}
}
