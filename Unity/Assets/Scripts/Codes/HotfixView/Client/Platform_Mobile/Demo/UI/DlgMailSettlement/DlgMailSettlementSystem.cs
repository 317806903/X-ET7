﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(DlgMailSettlement))]
	public static class DlgMailSettlementSystem
	{
		public static void RegisterUIEvent(this DlgMailSettlement self)
		{
            //背景按钮
            self.View.E_BG_ClickButton.AddListenerAsync(self.Back);

            //循环列表
            self.View.ELoopScrollList_LoopVerticalScrollRect.prefabSource.prefabName = "Item_ItemShow";
            self.View.ELoopScrollList_LoopVerticalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_LoopVerticalScrollRect.AddItemRefreshListener((transform, i) =>
                self.AddGiftListener(transform, i).Coroutine());
        }

        public static async ETTask ShowWindow(this DlgMailSettlement self, ShowWindowData contextData = null)
		{
            self.dlgShowTime = TimeHelper.ClientNow();
            DlgMailSettlement_ShowWindowData showData = contextData as DlgMailSettlement_ShowWindowData;
            self.kvpItemCfgNumList = showData.kvpItemCfgNumList;

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.GetItem);

            self.ShowBg().Coroutine();
            await self.SetEloopNumber(true);
        }


        public static bool ChkCanClickBg(this DlgMailSettlement self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 背景
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ShowBg(this DlgMailSettlement self)
        {
            bool isARCameraEnable = ET.Client.ARSessionHelper.ChkARCameraEnable(self.DomainScene());
            isARCameraEnable = false;
            if (isARCameraEnable)
            {
                self.View.EGbgARRectTransform.SetVisible(true);
                self.View.EG_bgRectTransform.SetVisible(false);
            }
            else
            {
                self.View.EGbgARRectTransform.SetVisible(false);
                self.View.EG_bgRectTransform.SetVisible(true);
            }
            await ETTask.CompletedTask;
        }


        public static void HideWindow(this DlgMailSettlement self)
		{
		}


        #region 事件监听函数

        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask Back(this DlgMailSettlement self)
        {
            if (self.ChkCanClickBg() == false)
            {
                return;
            }
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Back);
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgMailSettlement>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgMail>();
        }

        /// <summary>
        /// 礼物列表的刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        public static async ETTask AddGiftListener(this DlgMailSettlement self, Transform transform, int index)
        {
            transform.name = $"Item_TowerBattleBuy{index}";
            Scroll_Item_ItemShow itemGift = self.ScrollItemGiftDic[index].BindTrans(transform);

            string itemcfg = self.kvpItemCfgNumList[index].Key;
            int itemNum = self.kvpItemCfgNumList[index].Value;
            await itemGift.Init(itemcfg, true, itemNum);
            await ETTask.CompletedTask;
        }
        #endregion

        /// <summary>
        /// 设置循环列表数量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bClear"></param>
        public static async ETTask SetEloopNumber(this DlgMailSettlement self, bool bClear)
        {
            self.AddUIScrollItems(ref self.ScrollItemGiftDic, self.kvpItemCfgNumList.Count);
            self.View.ELoopScrollList_LoopVerticalScrollRect.SetVisible(true, self.kvpItemCfgNumList.Count);
            await ETTask.CompletedTask;
        }


    }
}
