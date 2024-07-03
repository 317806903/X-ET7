using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;
using UnityEngine.SocialPlatforms.Impl;

namespace ET.Client
{
	[FriendOf(typeof(Scroll_Item_PowerUps))]
	public static class Scroll_Item_PowerUpsSystem
	{
        /// <summary>
        /// 初始化-
        /// </summary>
        public static async ETTask Init(this Scroll_Item_PowerUps self, string bringUpNameStr, int playBringUpLevel, bool isSmooth = false)
		{
            self.SetMainIcon(bringUpNameStr,playBringUpLevel);
            self.SetOutline(bringUpNameStr);            
            self.SetItemNameTxt(bringUpNameStr, playBringUpLevel);
            await self.SetColorOutLine(bringUpNameStr, playBringUpLevel, isSmooth);
            self.SetIconUP(bringUpNameStr, playBringUpLevel).Coroutine();

        }

        /// <summary>
        ///	设置中心图标的图片
        /// </summary>
        public static void SetMainIcon(this Scroll_Item_PowerUps self,string bringUpNameStr,int playerLevel)
		{
            SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(bringUpNameStr, playerLevel);
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(seasonBringUpCfg.Icon);
            self.EMainIconImage.SetImageByPath(resIconCfg.ResName).Coroutine(); 
        }

        /// <summary>
        /// 设置进度条
        /// </summary>
        public static async ETTask SetColorOutLine(this Scroll_Item_PowerUps self, string bringUpNameStr, int playerBringUpLevel,bool isSmooth=false)
        {
            int maxLevel = SeasonBringUpCfgCategory.Instance.SeasonBringUpCfgMaxLevel[bringUpNameStr];
            int _playerBringUpLevel = playerBringUpLevel;

            if (isSmooth)
            {
                // 计算填充比例
                float fromValue = (_playerBringUpLevel - 1) / (float)maxLevel;
                float toValue = _playerBringUpLevel / (float)maxLevel;

                await self.SmoothFillAmountChange(fromValue, toValue, 0.5f);
            }
            else
            {
                self.EOutlineColorImage.fillAmount = playerBringUpLevel/(float)maxLevel;
            }
           
        }

        /// <summary>
        ///	控制三种类型的outline
        /// </summary>
        public static void SetOutline(this Scroll_Item_PowerUps self,string bringUpNameStr)
		{
			int maxLevel=SeasonBringUpCfgCategory.Instance.GetMaxLevel(bringUpNameStr);

			//切换不同的Line图片显隐
			switch (maxLevel)
			{
                case 1:
                    self.ELevelline2Image.SetVisible(false);
                    self.ELevelline3Image.SetVisible(false);
                    self.ELevelline4Image.SetVisible(false);
                    break;
                case 2: 
					self.ELevelline2Image.SetVisible(true);
					self.ELevelline3Image.SetVisible(false);
					self.ELevelline4Image.SetVisible(false);
					break; 
				case 3:
                    self.ELevelline2Image.SetVisible(false);
                    self.ELevelline3Image.SetVisible(true);
                    self.ELevelline4Image.SetVisible(false);
                    break; 
				case 4:
                    self.ELevelline2Image.SetVisible(false);
                    self.ELevelline3Image.SetVisible(false);
                    self.ELevelline4Image.SetVisible(true);
                    break; 
			}
        }

        /// <summary>
        ///	设置是否可以升级的Icon及颜色
        /// </summary>
        public static async ETTask  SetIconUP(this Scroll_Item_PowerUps self, string bringUpNameStr, int playerBringUpLevel)
        {
            int maxLevel = SeasonBringUpCfgCategory.Instance.GetMaxLevel(bringUpNameStr);
            bool isEnoughDiamond = await self.IsPlayerMoneyEnough(bringUpNameStr, playerBringUpLevel);

            bool isUpgrade=playerBringUpLevel < maxLevel&& isEnoughDiamond;

			self.EIconUpImage.SetVisible(isUpgrade);

            bool isMaxLevel = playerBringUpLevel >= maxLevel;

            if (isMaxLevel)
			    self.EOutlineColorImage.color = Color.red;
            else
                self.EOutlineColorImage.color = Color.white;


        }

        /// <summary>
        ///	设置Item底部的name
        /// </summary>
        public static void SetItemNameTxt(this Scroll_Item_PowerUps self, string bringUpNameStr, int playerBringupLevel)
        {
            SeasonBringUpCfg seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(bringUpNameStr, playerBringupLevel);
            string text = LocalizeComponent.Instance.GetTextValue(seasonBringUpCfg.Name);
            self.ETxtItemDesTextMeshProUGUI.SetText(text);
        }

        /// <summary>
        ///平滑过度函数
        /// </summary>
        public static async ETTask SmoothFillAmountChange(this Scroll_Item_PowerUps self, float fromValue, float toValue, float duration)
        {
            float elapsed = 0f;

            // 获取初始值
            float startValue = self.EOutlineColorImage.fillAmount;
            while (elapsed < duration)
            {

                elapsed += Time.deltaTime;

                // 计算进度
                float progress = elapsed / duration;
                float newValue = Mathf.Lerp(fromValue, toValue, progress);

                // 进行平滑过渡
                self.EOutlineColorImage.fillAmount = Mathf.Lerp(startValue, newValue, progress);
                await TimerComponent.Instance.WaitFrameAsync();
            }
            self.EOutlineColorImage.fillAmount = toValue;
        }

        /// <summary>
        /// 玩家金钱是否足够
        /// </summary>
        public static async ETTask<bool> IsPlayerMoneyEnough(this Scroll_Item_PowerUps self, string bringUpNameStr, int playerBringupLevel)
        {
            int playerDiamond = await PlayerCacheHelper.GetTokenDiamond(self.DomainScene());
            SeasonBringUpCfg seasonBringUpCfg = null;
            if (bringUpNameStr != null)
            {
                seasonBringUpCfg = SeasonBringUpCfgCategory.Instance.GetSeasonBringUpCfg(bringUpNameStr, playerBringupLevel);
            }
            else
            {
                return false;
            }
            return (playerDiamond >= seasonBringUpCfg.Cost);
        }

    }
}
