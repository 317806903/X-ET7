using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(DlgMailInfo))]
	public static class DlgMailInfoSystem
	{
        /*
         WJTODO:
            该类逻辑代码与 Scroll_Item_Mail_Inbox 代码存在冗余
         */
        public static void RegisterUIEvent(this DlgMailInfo self)
		{
            self.View.E_CollectButton.AddListenerAsync(self.CollectBtnClick);
            self.View.E_BG_ClickButton.AddListenerAsync(self.OnBGClick);
            //礼物列表
            self.View.ELoopScrollList_LoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.View.ELoopScrollList_LoopHorizontalScrollRect.prefabSource.poolSize = 5;
            self.View.ELoopScrollList_LoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddGiftListener(transform, i));
        }

        #region Show相关
        public static async ETTask ShowWindow(this DlgMailInfo self, ShowWindowData contextData = null)
        {
            self.dlgShowTime = TimeHelper.ClientNow();
            DlgMailInfo_ShowWindowData showData = contextData as DlgMailInfo_ShowWindowData;
            self.SetMailData(showData.mainInfo).Coroutine();

            self.ShowBg().Coroutine();
            self.SetEloopNumber();
            self.SetAllTextAndAvatar();
            self.CollectBtnShow().Coroutine();
            //self.View.ELoopScrollList_LoopHorizontalScrollRect.RefreshCells();

        }

        public static bool ChkCanClickBg(this DlgMailInfo self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 控制Ar或者普通的背景
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ShowBg(this DlgMailInfo self)
        {
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
            isARCameraEnable = false;
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


        /// <summary>
        /// 设置礼物循环列表数量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bClear"></param>
        public static async ETTask SetEloopNumber(this DlgMailInfo self)
        {
            if (self.kvpItemCfgNumList != null)
            {
                self.AddUIScrollItems(ref self.ScrollGiftDic, self.kvpItemCfgNumList.Count);
                self.View.EMainContentGiftsImage.gameObject.SetActive(true);
                self.View.ELoopScrollList_LoopHorizontalScrollRect.SetVisible(true, self.kvpItemCfgNumList.Count);
            }
            else
            {
                self.AddUIScrollItems(ref self.ScrollGiftDic, 0);
                self.View.EMainContentGiftsImage.gameObject.SetActive(false);
            }
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 设置数据域
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mainInfo"></param>
        public static async ETTask SetMailData(this DlgMailInfo self, (MailInfoComponent, MailStatus) mainInfo)
        {
            self.mailInfoComponent = mainInfo.Item1;
            self.mailStatus = mainInfo.Item2;
            if (self.mailInfoComponent.itemCfgList != null)
            {
                self.kvpItemCfgNumList = new List<KeyValuePair<string, int>>(self.mailInfoComponent.itemCfgList);
            }
            else
            {
                self.kvpItemCfgNumList = null;
            }

            await ETTask.CompletedTask;

        }

        /// <summary>
        /// 设置所有有关Item的文字和头像
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mailInfoComponent"></param>
        public static async ETTask SetAllTextAndAvatar(this DlgMailInfo self)
        {
            string mailTitle = self.mailInfoComponent.mailTitle;
            string mailContent = self.mailInfoComponent.mailContent;
            self.View.ETxtDetailsTextMeshProUGUI.SetText(mailContent);
            string dataTime = TimeHelper.ToDateTime(self.mailInfoComponent.receiveTime, false, false, true).ToString();
            self.View.ETxtSendDateTextMeshProUGUI.SetText(dataTime);
            string mailLeftTime = ET.Client.MailHelper.GetMailLeftTime(self.mailInfoComponent.limitTime);
            self.View.ETxtLimintDataTextMeshProUGUI.SetText(mailLeftTime);
            self.View.ETxttitleTextMeshProUGUI.SetText(mailTitle);

            string path = self.mailInfoComponent.GetMailTypeIcon();
            if (!string.IsNullOrEmpty(path))
            {
                await self.View.ES_AvatarShow.SetAvatarIcon(path);
            }

            self.View.ES_AvatarShow.View.E_PlayerNameTextMeshProUGUI.text = self.mailInfoComponent.GetMailTypeName();
        }

        /// <summary>
        /// 根据数据控制控件的显影
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask CollectBtnShow(this DlgMailInfo self)
        {
            if (self.kvpItemCfgNumList != null)
            {
                //设置收集按钮状态
                switch (self.mailStatus)
                {
                    case MailStatus.UnRead:
                        self.View.E_CollectButton.SetVisible(true);
                        self.View.E_CollectUnSelectImage.SetVisible(false);
                        break;
                    case MailStatus.ReadAndGetItem:
                        self.View.E_CollectButton.SetVisible(false);
                        self.View.E_CollectUnSelectImage.SetVisible(true);
                        break;
                    case MailStatus.ReadAndNotGetItem:
                        self.View.E_CollectButton.SetVisible(true);
                        self.View.E_CollectUnSelectImage.SetVisible(false);
                        break;
                    case MailStatus.ReadAndNoItem:
                        Debug.Log("Error");
                        self.View.E_CollectButton.gameObject.SetActive(false);
                        self.View.E_CollectUnSelectImage.SetVisible(true);
                        break;
                    default:
                        Debug.Log("Error");
                        break;
                }
            }
            else
            {
                self.View.E_CollectButton.SetVisible(false);
			    self.View.E_CollectUnSelectImage.SetVisible(false);
            }

            await ETTask.CompletedTask;
        }

        #endregion

        public static void HideWindow(this DlgMailInfo self)
		{
		}

        #region
        /// <summary>
        ///礼物列表的刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static async ETTask AddGiftListener(this DlgMailInfo self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBuy{index}";
            Scroll_Item_TowerBuy scrollItemGift = self.ScrollGiftDic[index].BindTrans(transform);

            string itemcfg = self.kvpItemCfgNumList[index].Key;
            int itemNum = self.kvpItemCfgNumList[index].Value;

            await scrollItemGift.ShowBagItem(itemcfg, true, itemNum);
            await ETTask.CompletedTask;

        }

        /// <summary>
        /// 收集按钮监听事件
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask CollectBtnClick(this DlgMailInfo self)
        {
            DlgMailSettlement_ShowWindowData showdata = new()
            {
                kvpItemCfgNumList = self.kvpItemCfgNumList,
            };

            await ET.Client.MailHelper.DealMyMail(self.DomainScene(), self.mailInfoComponent.Id, DealMailType.ReadAndGetItem);

            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgMailInfo>();
            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMailSettlement>(showdata).Coroutine();

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 背景图点击事件
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask OnBGClick(this DlgMailInfo self)
        {
            if (self.ChkCanClickBg() == false)
            {
                return;
            }
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgMailInfo>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMail>();
        }

        #endregion

    }
}
