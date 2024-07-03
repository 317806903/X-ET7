using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using System.Security;

namespace ET.Client
{
    [FriendOf(typeof(EPage_Powerup))]
	public static class EPage_PowerupSystem
	{
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="self"></param>
		public static void RegisterUIEvent(this EPage_Powerup self)
		{
			//重置按钮
			self.View.EBtnResetButton.AddListenerAsync(self.ResetBtnHandel);

            //Item列表
            self.View.ELoopScrollList_LoopVerticalScrollRect.prefabSource.prefabName = "Item_PowerUps";
            self.View.ELoopScrollList_LoopVerticalScrollRect.prefabSource.poolSize = 10;
            self.View.ELoopScrollList_LoopVerticalScrollRect.AddItemRefreshListener((async(transform, i) =>
                await self.AddListItemRefreshListener(transform, i)));

             //升级按钮
            self.View.EBtnUpdateButton.AddListenerAsync(self.UpdateBtnHandel);

        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="self"></param>
        /// <param name="contextData"></param>
        public static async void ShowPage(this EPage_Powerup self, ShowWindowData contextData = null)
		{
            self.BottomtItemCfg = null;
            self.View.uiTransform.SetVisible(true);

            SeasonComponent seasonComponent = SeasonHelper.GetSeasonComponent(self.DomainScene());
            int count = seasonComponent.cfg.BringUpList.Count;
            self.AddUIScrollItemsPage(ref self.ScrollItemDic, count);

            self.View.ELoopScrollList_LoopVerticalScrollRect.SetVisible(true, count);
            self.View.ELoopScrollList_LoopVerticalScrollRect.RefreshCells();

            int resetCost = seasonComponent.cfg.BringUpResetCost;
            self.View.ETxtResetNumTextMeshProUGUI.SetText(resetCost.ToString());
            bool isEnoughReset = await self.IsPlayeEnoughReset();
            self.View.EBtnResetButton.interactable = isEnoughReset;
        }

        /// <summary>
        /// 当钻石改变时重新刷新面板
        /// </summary>
        /// <param name="self"></param>
        public static async void RefreshWhenDiamondChg(this EPage_Powerup self)
        {
            if (self.isUpadaeting)
            {
                return;
            }              
            self.View.ELoopScrollList_LoopVerticalScrollRect.RefreshCells();
            bool isEnoughReset = await self.IsPlayeEnoughReset();
            self.View.EBtnResetButton.interactable = isEnoughReset;
            await self.UpdateBottomUI(self.BottomtItemCfg);
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="self"></param>
		public static void HidePage(this EPage_Powerup self)
		{
			self.View.uiTransform.SetVisible(false);
		}

        #region  控件事件监听函数
        /// <summary>
        /// 重置按钮
        /// </summary>
        /// <param name="self"></param>
		public static async ETTask ResetBtnHandel(this EPage_Powerup self)
		{        
            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PowerBringUp_Reset_Des");
            ET.Client.UIManagerHelper.ShowConfirm(
                self.DomainScene(),
                msgTxt, 
                async () => {
                    bool isSuccesful = await PlayerCacheHelper.ResetAllPowerUp(self.DomainScene());
                    if (isSuccesful)
                    {
                        self.BottomtItemCfg = null;
                        self.ShowPage();
                    }
                    else
                    {
                        //WJTODO 消息错误处理 
                        self.ShowPage();
                    }                                     
                }, 
                null
                );
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// Item刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static async ETTask AddListItemRefreshListener(this EPage_Powerup self, Transform transform, int index)
        {
            transform.name = $"Item_PowerUP_{index}";
            Scroll_Item_PowerUps PowerupsItem = self.ScrollItemDic[index].BindTrans(transform);

            //获取赛季配置中的养成Item对应的名字
            List<string> bringupStrList = SeasonHelper.GetSeasonComponent(self.DomainScene()).cfg.BringUpList;
            string itemName = bringupStrList[index];

            int playerBringupLevel = await self.GetBringUpLevel(itemName);

            //当玩家等级中没有满级的时候就把这个Item放到底部
            bool isMax = await self.IsPlayerPowerupMax(itemName);
            if( !isMax && string.IsNullOrEmpty(self.BottomtItemCfg))
            {
                self.BottomtItemCfg = itemName;
                self.CurrentItemIndex = index;
                self.UpdateBottomUI(self.BottomtItemCfg).Coroutine();
            }

            //若当前的Item与底部Item相同
            if (self.CurrentItemIndex == index)
            {
                PowerupsItem.EImgSelectIconImage.gameObject.SetActive(true);
            }
            else
            {
                PowerupsItem.EImgSelectIconImage.gameObject.SetActive(false);
            }


            await PowerupsItem.Init(itemName, playerBringupLevel);
            PowerupsItem.EItemBtnButton.AddListener( ()=> {
                self.ClickItem(itemName,index);
            });
        }

        /// <summary>
        /// 点击Item按钮
        /// </summary>
        public static void ClickItem(this EPage_Powerup self, string bringUpNameStr, int index)
        {
            //记录当前的选择
            if (self.BottomtItemCfg != bringUpNameStr)
            {
                self.CurrentItemIndex = index;
                self.BottomtItemCfg = bringUpNameStr;
                self.UpdateBottomUI(self.BottomtItemCfg).Coroutine();
                /*
                 * 下面这行代码为了控制Item选中的图片
                 */
                self.View.ELoopScrollList_LoopVerticalScrollRect.RefreshCells();
            }
        }

        /// <summary>
        /// 更新按钮
        /// </summary>
        /// <param name="self"></param>
        public static async ETTask UpdateBtnHandel(this EPage_Powerup self)
        {
            if (self.isUpadaeting)
                return;
            /*创建临时变量是因为
                 * 避免玩家在升级时频繁切换Item，
                 * 会导致self.CurrentItemIndex和self.BottomtItemCfg改变
                 导致刷新指定的索引Item出现错误*/
            int tempIndex = self.CurrentItemIndex;
            string tempCfgName = self.BottomtItemCfg;

            int playerBringupLevel = await self.GetBringUpLevel(tempCfgName);
            int maxLevel = SeasonBringUpCfgCategory.Instance.GetMaxLevel(tempCfgName);
            bool isCanUpdate = await self.IsPlayerCanUpdate(tempCfgName, playerBringupLevel);

            if (isCanUpdate)
            {
                // 计算填充比例
                int _playerBringupLevel = playerBringupLevel;
                float fromValue = _playerBringupLevel / (float)maxLevel;
                float toValue = (_playerBringupLevel + 1) / (float)maxLevel;

                self.SmoothFillAmountChange(fromValue, toValue, 0.3f).Coroutine();
                //刷新指定的Item
                self.ScrollItemDic[tempIndex].Init(tempCfgName, playerBringupLevel + 1,true).Coroutine();

                //WJTODO 这里进行等待
                bool isUpdateSucces = await PlayerCacheHelper.UpdatePowerup(self.DomainScene(), tempCfgName);
                if (isUpdateSucces)
                {
                    await self.UpdateBottomUI(self.BottomtItemCfg);
                    self.View.ELoopScrollList_LoopVerticalScrollRect.RefreshCells();
                    bool isEnoughReset = await self.IsPlayeEnoughReset();
                    self.View.EBtnResetButton.interactable = isEnoughReset;
                }
                else
                {
                    //WJTODO 消息异常处理
                }
                self.isUpadaeting = false;
            }
        }
        #endregion

        /// <summary>
        /// 刷新页面下面的UI显示
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask UpdateBottomUI(this EPage_Powerup self,string cfg)
        {
            /*
             * 以下函数跟养成ITem（Scroll_Item_PowerUps）存在冗余
             */
            int maxLevel = SeasonBringUpCfgCategory.Instance.GetMaxLevel(cfg);
            int playerBringupLevel = await self.GetBringUpLevel(cfg);
            bool isPlayerDiamondEnough = await self.IsPlayerDiamondEnough(cfg, playerBringupLevel);
            bool isMax = (playerBringupLevel >= maxLevel);
            //控制三个按钮的显影
            if (isMax)
            {
                self.View.EBtnUpdateButton.SetVisible(false);
                self.View.EBtnUpdate_maxButton.SetVisible(true);
                self.View.EBtnUpdate_nullButton.SetVisible(false);
            }
            else
            {
                if (isPlayerDiamondEnough)
                {
                    self.View.EBtnUpdateButton.SetVisible(true);
                    self.View.EBtnUpdate_maxButton.SetVisible(false);
                    self.View.EBtnUpdate_nullButton.SetVisible(false);
                }
                else
                {
                    self.View.EBtnUpdateButton.SetVisible(false);
                    self.View.EBtnUpdate_maxButton.SetVisible(false);
                    self.View.EBtnUpdate_nullButton.SetVisible(true);
                }
            }

            //底部的刷新逻辑 1.Item名字 2：Item的描述 3:Item升级的钻石花费 4：切换养成的Icon图 5:不同的Line图片显隐 6:NextLevel名字
            SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(cfg, playerBringupLevel);
            string text = LocalizeComponent.Instance.GetTextValue(seasonBringUpCfg.NameExtend);
            self.View.ETxtBottomItemNameTextMeshProUGUI.SetText(text);

            SeasonBringUpCfg nextSeasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetNextSeasonBringUpCfg(cfg, playerBringupLevel);
            if (nextSeasonBringUpCfg != null)
            {
                text = LocalizeComponent.Instance.GetTextValue(nextSeasonBringUpCfg.NameExtend);
                self.View.ETxtBottomExtendNameTextMeshProUGUI.SetVisible(true);
                self.View.ETxtBottomExtendNameTextMeshProUGUI.SetText(text);
                self.View.ETxtNextLevelTextMeshProUGUI.SetVisible(true);
            }
            else
            {
                self.View.ETxtBottomExtendNameTextMeshProUGUI.SetVisible(false);
                self.View.ETxtNextLevelTextMeshProUGUI.SetVisible(false);
            }

            text = LocalizeComponent.Instance.GetTextValue(seasonBringUpCfg.Desc);
            self.View.ETxtBottomItemDesTextMeshProUGUI.SetText(text);

            self.View.ETxtUpgradeNumTextMeshProUGUI.SetText(seasonBringUpCfg.Cost.ToString());
            self.View.ETxtUpgradeNumNullTextMeshProUGUI.SetText(seasonBringUpCfg.Cost.ToString());

            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(seasonBringUpCfg.Icon);
            self.View.EMainIconImage.SetImageByPath(resIconCfg.ResName).Coroutine();
            switch (maxLevel)
             {
                case 1:
                     self.View.ELevelline2Image.SetVisible(false);
                     self.View.ELevelline3Image.SetVisible(false);
                    self.View.ELevelline4Image.SetVisible(false);
                    break;
                case 2:
                     self.View.ELevelline2Image.SetVisible(true);
                     self.View.ELevelline3Image.SetVisible(false);
                     self.View.ELevelline4Image.SetVisible(false);
                     break;
                 case 3:
                     self.View.ELevelline2Image.SetVisible(false);
                     self.View.ELevelline3Image.SetVisible(true);
                     self.View.ELevelline4Image.SetVisible(false);
                     break;
                 case 4:
                     self.View.ELevelline2Image.SetVisible(false);
                     self.View.ELevelline3Image.SetVisible(false);
                     self.View.ELevelline4Image.SetVisible(true);
                     break;
             }

            //颜色
            self.View.EOutlineColorImage.fillAmount = playerBringupLevel / (float)maxLevel;
            if (playerBringupLevel == maxLevel)
            {
                self.View.EOutlineColorImage.color = Color.red;
            }
            else
            {
                self.View.EOutlineColorImage.color = Color.white;
            }
        }

        /// <summary>
        ///平滑过度函数
        /// </summary>
        /// <param name="self"></param>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static async ETTask SmoothFillAmountChange(this EPage_Powerup self, float fromValue, float toValue, float duration)
        {
            float elapsed = 0f;

            // 获取初始值
            float startValue = self.View.EOutlineColorImage.fillAmount;
            self.isUpadaeting = true;
            while (elapsed < duration )
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / duration;
                float newValue = Mathf.Lerp(fromValue, toValue, progress);
                self.View.EOutlineColorImage.fillAmount = Mathf.Lerp(startValue, newValue, progress);
                await TimerComponent.Instance.WaitFrameAsync();
            }
            self.View.EOutlineColorImage.fillAmount = toValue;
            
            
        }

        /// <summary>
        /// 通过配置得到玩家的等级
        /// </summary>
        public static async ETTask<int> GetBringUpLevel(this EPage_Powerup self,string cfgName)
        {
            PlayerSeasonInfoComponent playerSeasonInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());
            int playerBringupLevel;
            playerBringupLevel=playerSeasonInfoComponent.GetSeasonBringUpLevel(cfgName);
            return playerBringupLevel;
        }

        /// <summary>
        /// 玩家钻石是否足够
        /// </summary>
        public static async ETTask<bool> IsPlayerDiamondEnough(this EPage_Powerup self, string bringUpNameStr, int playerBringupLevel)
        {
            int playerDiamond = await PlayerCacheHelper.GetTokenDiamond(self.DomainScene());
            SeasonBringUpCfg seasonBringUpCfg=null;
            if (bringUpNameStr != null)
            {
                seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(bringUpNameStr, playerBringupLevel);
            }
            else
            {
                return false;
            }
            await ETTask.CompletedTask;
            return playerDiamond >= seasonBringUpCfg.Cost;
        }

        /// <summary>
        /// 玩家养成是否能升级
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask<bool> IsPlayerCanUpdate(this EPage_Powerup self, string bringUpNameStr, int playerBringupLevel)
        {
            bool isMax = await self.IsPlayerPowerupMax(bringUpNameStr);
            bool isEnough = await self.IsPlayerDiamondEnough(bringUpNameStr, playerBringupLevel);
            return !isMax&&isEnough;
        }

        /// <summary>
        /// 玩家是否足够重置
        /// </summary>
        public static async ETTask<bool> IsPlayeEnoughReset(this EPage_Powerup self)
        {            
            PlayerSeasonInfoComponent playerSeasonInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerSeasonInfo(self.DomainScene());
            Dictionary<string, int> seasonBringUpDic = playerSeasonInfoComponent.GetSeasonBringUpDic();
            bool isPlayerBringupIsNone = true;
            foreach (KeyValuePair<string, int> kvp in seasonBringUpDic)
            {
                if (kvp.Value != 0)
                {
                    isPlayerBringupIsNone = false;
                }
            }

            int playerDiamond = await PlayerCacheHelper.GetTokenDiamond(self.DomainScene());      
            SeasonComponent seasonComponent = ET.Client.SeasonHelper.GetSeasonComponent(self.DomainScene());
            int reset = seasonComponent.cfg.BringUpResetCost;
            return playerDiamond >= reset&& !isPlayerBringupIsNone;
        }

        /// <summary>
        /// 玩家养成是否满级
        /// </summary>
        /// <param name="self"></param>
        /// <param name="bringUpNameStr"></param>
        /// <returns>是否满级</returns>
        public static async ETTask<bool> IsPlayerPowerupMax(this EPage_Powerup self, string bringUpNameStr)
        {
            int playerBringupLevel = await self.GetBringUpLevel(bringUpNameStr);

            int maxLevel = SeasonBringUpCfgCategory.Instance.GetMaxLevel(bringUpNameStr);

            return playerBringupLevel >= maxLevel;
        }
    }
}
