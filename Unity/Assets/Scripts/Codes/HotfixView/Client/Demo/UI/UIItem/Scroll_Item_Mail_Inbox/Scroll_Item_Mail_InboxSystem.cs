using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using Dm.Api.Proto.Services.ArSession;

namespace ET.Client
{
    /*
        * WJTODO
        * 若重新生成此类，需在Scroll_Item_Mail_Inbox类中添加如下数据
            public Dictionary<int, Scroll_Item_TowerBuy> ScrollGiftDic = new Dictionary<int, Scroll_Item_TowerBuy>();
            public MailInfoComponent mailInfoComponent = new MailInfoComponent();
            public MailStatus mailStatus;
            public List<KeyValuePair<string, int>> kvpItemCfgNumList = new List<KeyValuePair<string, int>>();

           在Item中Destroy中加上下面数据
            this.ScrollGiftDic = null;
            this.mailInfoComponent = null;
            this.mailStatus = 0;
            this.kvpItemCfgNumList = null;
   */

    [FriendOf(typeof(Scroll_Item_Mail_Inbox))]
	public static class Scroll_Item_Mail_InboxSystem
	{
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mainInfo"></param>
        public static async ETTask Init(this Scroll_Item_Mail_Inbox self,(MailInfoComponent, MailStatus) mainInfo)
		{
            //数据
            await self.SetMailData(mainInfo);

            await self._ShowItem();

            self.EBtnCollectButton.AddListenerAsync(self.CollectBtnClick);
            self.ELabel_HaveGiftButton.AddListenerAsync(self.ClickDesBtnClick);
            self.ELabel_NohaveGiftButton.AddListenerAsync(self.ClickDesBtnClick);


            //礼物列表
            self.ELoopScrollListGiftLoopHorizontalScrollRect.prefabSource.prefabName = "Item_TowerBuy";
            self.ELoopScrollListGiftLoopHorizontalScrollRect.prefabSource.poolSize = 5;
            self.ELoopScrollListGiftLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddGiftListener(transform, i));
            self.SetEloopNumber();
            //self.ELoopScrollListGiftLoopHorizontalScrollRect.RefreshCells();

            //WJTODO  ESavatarshow

            //三个文字和头像
            self.SetAllTextAndAvatar();

        }

        /// <summary>
        /// 根据数据控制控件的显影
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask _ShowItem(this Scroll_Item_Mail_Inbox self)
        {
            //有奖励
            if (self.kvpItemCfgNumList != null)
            {
                switch (self.mailStatus)
                {
                    case MailStatus.UnRead:
                        self.EBtnCollectButton.SetVisible(true);
                        self.E_CollectUnSelectImage.gameObject.SetActive(false);
                        self.ELabel_HaveReadImage.gameObject.SetActive(false);
                        break;
                    case MailStatus.ReadAndGetItem:
                        self.EBtnCollectButton.SetVisible(false);
                        self.E_CollectUnSelectImage.gameObject.SetActive(true);
                        self.ELabel_HaveReadImage.gameObject.SetActive(true);
                        break;
                    case MailStatus.ReadAndNotGetItem:
                        self.EBtnCollectButton.SetVisible(true);
                        self.E_CollectUnSelectImage.gameObject.SetActive(false);
                        self.ELabel_HaveReadImage.gameObject.SetActive(true);
                        break;
                }
                self.ELabel_HaveGiftButton.gameObject.SetActive(true);
                self.ELabel_NohaveGiftButton.gameObject.SetActive(false);
            }
            //无奖励
            else
            {
                self.EBtnCollectButton.SetVisible(false);
                self.E_CollectUnSelectImage.gameObject.SetActive(false);
                self.ELabel_HaveGiftButton.gameObject.SetActive(false);
                self.ELabel_NohaveGiftButton.gameObject.SetActive(true);
                switch (self.mailStatus)
                {
                    case MailStatus.UnRead:
                        self.ELabel_HaveReadImage.gameObject.SetActive(false);
                        break;
                    case MailStatus.ReadAndNoItem:
                        self.ELabel_HaveReadImage.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 设置所有有关Item的文字和头像
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mailInfoComponent"></param>
        public static async ETTask SetAllTextAndAvatar(this Scroll_Item_Mail_Inbox self)
        {
            string mailTitle = self.mailInfoComponent.mailTitle;
            string mailContent = self.mailInfoComponent.mailContent;
            self.ELabel_NameTextMeshProUGUI.SetText(self.mailInfoComponent.GetMailTypeName());

            string mailLeftTime = ET.Client.MailHelper.GetMailLeftTime(self.mailInfoComponent.limitTime);
            self.ELabelExpireTextMeshProUGUI.SetText(mailLeftTime);
            self.ELabelDescribeTextMeshProUGUI.SetText(mailTitle);

            string path = self.mailInfoComponent.GetMailTypeIcon();;
            if (!string.IsNullOrEmpty(path))
            {
                await self.ES_AvatarShow.SetAvatarIcon(path);
            }
        }

        #region 控件事件监听函数
        /// <summary>
        ///礼物列表的刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static async ETTask AddGiftListener(this Scroll_Item_Mail_Inbox self, Transform transform, int index)
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
        public static async ETTask CollectBtnClick(this Scroll_Item_Mail_Inbox self)
        {
            DlgMailSettlement_ShowWindowData showdata = new()
            {
               kvpItemCfgNumList = self.kvpItemCfgNumList,
            };

            await ET.Client.MailHelper.DealMyMail(self.DomainScene(), self.mailInfoComponent.Id, DealMailType.ReadAndGetItem);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMailSettlement>(showdata).Coroutine();

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 点击列表项打开描述面板监听事件
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ClickDesBtnClick(this Scroll_Item_Mail_Inbox self)
        {
            (MailInfoComponent, MailStatus) _mainInfo = (self.mailInfoComponent, self.mailStatus);

                DlgMailInfo_ShowWindowData showdata = new()
                {
                    mainInfo = _mainInfo,
                };

                await ET.Client.MailHelper.DealMyMail(self.DomainScene(), self.mailInfoComponent.Id, DealMailType.ReadOnly);

                await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMailInfo>(showdata);
        }
        #endregion

        /// <summary>
        /// 设置数据域
        /// </summary>
        /// <param name="self"></param>
        /// <param name="mainInfo"></param>
        public static async ETTask SetMailData(this Scroll_Item_Mail_Inbox self, (MailInfoComponent, MailStatus) mainInfo)
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
        /// 设置礼物循环列表数量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bClear"></param>
        public static async ETTask SetEloopNumber(this Scroll_Item_Mail_Inbox self)
        {
            if (self.kvpItemCfgNumList != null)
            {
                self.AddUIScrollItems(ref self.ScrollGiftDic, self.kvpItemCfgNumList.Count);
                self.ELoopScrollListGiftLoopHorizontalScrollRect.SetVisible(true, self.kvpItemCfgNumList.Count);
            }
            else
            {
                self.AddUIScrollItems(ref self.ScrollGiftDic, 0);
                self.ELoopScrollListGiftLoopHorizontalScrollRect.gameObject.SetActive(false);
            }
            await ETTask.CompletedTask;
        }

    }
}
