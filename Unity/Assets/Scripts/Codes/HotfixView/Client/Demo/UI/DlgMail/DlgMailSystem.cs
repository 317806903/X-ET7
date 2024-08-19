using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using System.Linq;
using TMPro;

namespace ET.Client
{
    [FriendOf(typeof(DlgMail))]
    public static class DlgMailSystem
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="self"></param>
        public static void RegisterUIEvent(this DlgMail self)
        {
            //下列列表
            self.View.EMailDropdownTMP_Dropdown.AddListener(self.ClickDropDown);

            //一键收集按钮
            self.View.EBtnCollectAllButton.AddListenerAsync(self.AllCollectBtnClick);

            //返回按钮
            self.View.EQuitBattleButton.AddListenerAsync(self.Back);

            //背景图片按钮
            self.View.E_BG_ClickButton.AddListener(()=>
            {
                if (self.ChkCanClickBg() == false)
                {
                    return;
                }
                self.Back().Coroutine();
            });

            //邮件循环列表
            self.View.ELoopScrollList_MailLoopVerticalScrollRect.prefabSource.prefabName = "Item_Mail_Inbox";
            self.View.ELoopScrollList_MailLoopVerticalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_MailLoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddMailListener(transform, i));

            

        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="self"></param>
        /// <param name="contextData"></param>
        public static async ETTask ShowWindow(this DlgMail self, ShowWindowData contextData = null)
        {
            self.dlgShowTime = TimeHelper.ClientNow();
            self._ShowWindow().Coroutine();
           
        }


        public static bool ChkCanClickBg(this DlgMail self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 真正的显示逻辑
        /// </summary>
        /// <param name="self"></param>
        /// <param name="contextData"></param>
        public static async ETTask _ShowWindow(this DlgMail self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            await self.SetEloopNumber();
            //self.View.ELoopScrollList_MailLoopVerticalScrollRect.RefreshCells();
            await self.RefreshGetAllGiftInMailBox();

            //控制邮箱为空时图标
            if(self.MailInfoAndStatus.Count == 0)
            {
                self.View.ELoopScrollList_MailLoopVerticalScrollRect.gameObject.SetActive(false);
                self.View.ELabel_InboxEmptyImage.gameObject.SetActive(true);
            }
            else
            {
                self.View.ELoopScrollList_MailLoopVerticalScrollRect.gameObject.SetActive(true);
                self.View.ELabel_InboxEmptyImage.gameObject.SetActive(false);
            }

            //控制收集按钮状态
            if (self.AllHavaGiftMailInfoId.Count!= 0 && self.kvpAllItemCfgNumList.Count!= 0)
            {
                self.View.EBtnCollectAllButton.SetVisible(true);
                self.View.EBtnCollectAll_NoneButton.SetVisible(false);
            }
            else
            {
                self.View.EBtnCollectAllButton.SetVisible(false);
                self.View.EBtnCollectAll_NoneButton.SetVisible(true);
            }
        }

        /// <summary>
        /// 重新获取数据
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bClear"></param>
        public static async ETTask SetEloopNumber(this DlgMail self)
        {
            self.GetDropIndexByPlayerPrefs();
            if(self.View.EMailDropdownTMP_Dropdown.options.Count<=0)
            {
                await self.SetDrapdownLocalizeText();
            }
            self.View.EMailDropdownTMP_Dropdown.value =self.DropDownIndex;
            await self.ReGetMailInfoAndStatusListSort();
            self.AddUIScrollItems(ref self.ScrollMailDic, self.MailInfoAndStatus.Count);
            self.View.ELoopScrollList_MailLoopVerticalScrollRect.SetVisible(true, self.MailInfoAndStatus.Count);
        }

        /// <summary>
        /// 重新获取当前邮箱中所有的奖励
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask RefreshGetAllGiftInMailBox(this DlgMail self)
        {
            List<long> allHavaGiftMailInfoId = new List<long>();
            Dictionary<string, int> allItemCfgList = new Dictionary<string, int> { };
            foreach ((MailInfoComponent, MailStatus) mailInfo in self.MailInfoAndStatus)
            {
                if (mailInfo.Item1.itemCfgList!=null &&
                    (mailInfo.Item2 == MailStatus.UnRead ||
                        mailInfo.Item2 == MailStatus.ReadAndNotGetItem))
                {
                    allHavaGiftMailInfoId.Add(mailInfo.Item1.Id);
                    foreach (KeyValuePair<string, int> kvp in mailInfo.Item1.itemCfgList)
                    {
                        if (allItemCfgList.ContainsKey(kvp.Key))
                        {
                            allItemCfgList[kvp.Key] += kvp.Value;
                        }
                        else
                        {
                            allItemCfgList[kvp.Key] = kvp.Value;
                        }
                    }
                }
            }
            self.kvpAllItemCfgNumList = allItemCfgList.ToList();
            self.AllHavaGiftMailInfoId = allHavaGiftMailInfoId;
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 重新刷新面板
        /// </summary>
        /// <param name="self"></param>
        /// <param name="contextData"></param>
        /// <returns></returns>
        public static async ETTask RefreshDlgMail(this DlgMail self, ShowWindowData contextData = null)
        {
            self.ShowWindow(contextData).Coroutine();
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="self"></param>
        public static void HideWindow(this DlgMail self)
        {
        }

        #region  控件事件监听函数
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask Back(this DlgMail self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgRankPowerupSeason>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
        }

        /// <summary>
        /// 背景
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ShowBg(this DlgMail self)
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
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 邮件的刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static async ETTask AddMailListener(this DlgMail self, Transform transform, int index)
        {
            transform.name = $"Item_Mail_Inbox{index}";
            Scroll_Item_Mail_Inbox scrollItemMail = self.ScrollMailDic[index].BindTrans(transform);

            (MailInfoComponent, MailStatus) MailInfo = self.MailInfoAndStatus[index];

            await scrollItemMail.Init(MailInfo);

            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 点击下拉列表选项监听事件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="index"></param>
        public static async void ClickDropDown(this DlgMail self,int index)
        {
            self.DropDownIndex = index;
            await self.SaveIndexByPlayerPrefs();

            await self.RefreshDlgMail();
        }

        /// <summary>
        /// 一键收集按钮监听事件
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask AllCollectBtnClick(this DlgMail self)
        {
            DlgMailSettlement_ShowWindowData showdata = new()
            {
                kvpItemCfgNumList = self.kvpAllItemCfgNumList,
            };

            await ET.Client.MailHelper.DealMyMail(self.DomainScene(), -1, DealMailType.ReadAndGetItem);

            UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMailSettlement>(showdata).Coroutine();
            await ETTask.CompletedTask;
        }
        #endregion

        /// <summary>
        /// 重新获取所有邮件列表保存到该类中
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ReGetMailInfoAndStatusListSort(this DlgMail self)
        {
            switch (self.DropDownIndex)
            {
                //First to expire
                case 0:
                    await self._ReGetMailInfoAndStatusListSort(MailSortRule.LimitTime, true);
                    break;
                //last to expire
                case 1:
                    await self._ReGetMailInfoAndStatusListSort(MailSortRule.LimitTime, false);
                    break;
                //first received
                case 2:
                    await self._ReGetMailInfoAndStatusListSort(MailSortRule.ReceivedTime, true);
                    break;
                //Last received
                case 3:
                    await self._ReGetMailInfoAndStatusListSort(MailSortRule.ReceivedTime, false);
                    break;
            }
        }

        /// <summary>
        /// 真正的重新获取所有邮件列表保存到该类中
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask _ReGetMailInfoAndStatusListSort(this DlgMail self,MailSortRule mailSortRule,bool isDescendingOrder)
        {
            if(self.MailInfoAndStatus != null)
            {
                self.MailInfoAndStatus.Clear();
            }
            PlayerMailComponent playerMailComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerMail(self.DomainScene(), true);
            self.MailInfoAndStatus = playerMailComponent.GetPlayerMailListBySort(mailSortRule, isDescendingOrder);
        }

        /// <summary>
        /// 设置下拉列表索引
        /// </summary>
        public static async ETTask SaveIndexByPlayerPrefs(this DlgMail self)
        {
            PlayerPrefs.SetInt("DlgMail_DropDownIndex", self.DropDownIndex);
            PlayerPrefs.Save();
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// 获取下拉列表索引
        /// </summary>
        public static void GetDropIndexByPlayerPrefs(this DlgMail self)
        {
            self.DropDownIndex = PlayerPrefs.GetInt("DlgMail_DropDownIndex", 2);
        }

        /// <summary>
        /// 设置下拉列表的多语言
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask SetDrapdownLocalizeText(this DlgMail self)
        {         
            self.View.EMailDropdownTMP_Dropdown.options.Clear();
            string textKey1 = "TextCode_Key_MailSortRule_ReceivedTimeUp";
            var tempData = new TMP_Dropdown.OptionData();
            tempData.text = LocalizeComponent.Instance.GetTextValue(textKey1);
            tempData.image = await UIManagerHelper.LoadSprite("Assets/ResAB/UI/Mail/Mail_icon_expire_02.png");
            self.View.EMailDropdownTMP_Dropdown.options.Add(tempData);

            string textKey2 = "TextCode_Key_MailSortRule_ReceivedTimeDown";
            var tempData2 = new TMP_Dropdown.OptionData();
            tempData2.text = LocalizeComponent.Instance.GetTextValue(textKey2);
            tempData2.image = await UIManagerHelper.LoadSprite("Assets/ResAB/UI/Mail/Mail_icon_expire_01.png");
            self.View.EMailDropdownTMP_Dropdown.options.Add(tempData2);

            string textKey3 = "TextCode_Key_MailSortRule_LimitTimeUp";
            var tempData3 = new TMP_Dropdown.OptionData();
            tempData3.text = LocalizeComponent.Instance.GetTextValue(textKey3);
            tempData3.image = await UIManagerHelper.LoadSprite("Assets/ResAB/UI/Mail/Mail_icon_received_02.png");
            self.View.EMailDropdownTMP_Dropdown.options.Add(tempData3);

            string textKey4 = "TextCode_Key_MailSortRule_LimitTimeDown";
            var tempData4 = new TMP_Dropdown.OptionData();
            tempData4.text = LocalizeComponent.Instance.GetTextValue(textKey4);
            tempData4.image = await UIManagerHelper.LoadSprite("Assets/ResAB/UI/Mail/Mail_icon_received_01.png");
            self.View.EMailDropdownTMP_Dropdown.options.Add(tempData4);

            await ETTask.CompletedTask; 
            
        }
    }
}
