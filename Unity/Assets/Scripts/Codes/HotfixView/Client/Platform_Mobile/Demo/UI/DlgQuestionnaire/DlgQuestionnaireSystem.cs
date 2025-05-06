using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ET.Client
{
	[FriendOf(typeof(DlgQuestionnaire))]
	public static class DlgQuestionnaireSystem
	{
		public static void RegisterUIEvent(this DlgQuestionnaire self)
		{
            self.View.E_CloseComplainButton.AddListenerAsync(self.Back);
            self.View.E_StartButton.AddListenerAsync(self.ClickStart);
            self.View.E_BGButton.AddListenerAsync(self.ClickBg);

            self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.prefabSource.prefabName = "Item_ItemShow";
            self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.prefabSource.poolSize = 5;
        }

        public static async ETTask ShowWindow(this DlgQuestionnaire self, ShowWindowData contextData = null)
		{
			self.dlgShowTime = TimeHelper.ClientNow();
            await self.SetQuestionnaireInfo();
            await self.ShowAwardItems();
        }

		public static bool ChkCanClickBg(this DlgQuestionnaire self)
		{
			if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
			{
				return true;
			}
			return false;
		}

		public static void HideWindow(this DlgQuestionnaire self)
		{

		}


        public static async ETTask Back(this DlgQuestionnaire self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgQuestionnaire>();
            await ETTask.CompletedTask;
        }

        public static async ETTask ClickStart(this DlgQuestionnaire self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
	            eventName = "SurveyOpened",
	            properties = new()
	            {
		            {"survey_id", self.questionnaireCfg.Id},
		            {"survey_url", self.questionnaireCfg.Url},
	            }
            });

            //打开问卷
            string urlQuestionnaire = self.questionnaireCfg.Url;
            UIManagerHelper.ShowUrl(self.DomainScene(), urlQuestionnaire);
            Log.Debug(urlQuestionnaire);

            ET.Client.PlayerCacheHelper.SetQuestionnaireFinished(self.DomainScene(), self.questionnaireCfg.Id).Coroutine();

            await self.Back();
        }

        public static async ETTask ClickBg(this DlgQuestionnaire self)
        {
            if (!self.ChkCanClickBg())
                return;
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgQuestionnaire>();
            await ETTask.CompletedTask;
        }

        public static async ETTask OnAddItemRefreshHandler(this DlgQuestionnaire self, Transform transform, int index )
        {
            Scroll_Item_ItemShow gifts = self.Scroll_Item_GiftsDict[index].BindTrans(transform);

            List<KeyValuePair<string, int>> list = self.questionnaireCfg.RewardItemListShow.ToList();
            string ItemcfgId = list[index].Key;
            int count = list[index].Value;
            await gifts.Init(ItemcfgId, true, count);
        }

        public static async ETTask ShowAwardItems(this DlgQuestionnaire self)
        {
            int count = self.questionnaireCfg.RewardItemListShow.Count;
            //在字典容器中添加对象
            self.AddUIScrollItems(ref self.Scroll_Item_GiftsDict, count);
            //显示列表容器
            self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.SetVisible(true, count);
            await ETTask.CompletedTask;
        }

        public static async ETTask SetQuestionnaireInfo(this DlgQuestionnaire self)
        {
            self.questionnaireCfg = await ET.Client.PlayerCacheHelper.GetNextQuestionnaire(self.DomainScene());
            self.View.E_titleTextMeshProUGUI.text = self.questionnaireCfg.Title;
            self.View.E_infoTextMeshProUGUI.text = self.questionnaireCfg.Content;
            //添加列表内容回调
            self.View.ELoopScrollList_ItemLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) => { self.OnAddItemRefreshHandler(transform, i).Coroutine(); });


            EventSystem.Instance.Publish(self.DomainScene(), new ClientEventType.NoticeEventLogging()
            {
	            eventName = "SurveyClicked",
	            properties = new()
	            {
		            {"survey_id", self.questionnaireCfg.Id},
		            {"survey_url", self.questionnaireCfg.Url},
	            }
            });

            await ETTask.CompletedTask;
        }
    }
}

