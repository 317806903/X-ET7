using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
	[FriendOf(typeof(ES_AvatarShow))]
	public static class ES_AvatarShowSystem
	{
		public static void RegisterUIEvent(this ES_AvatarShow self)
		{
		}

		public static void ShowCommonUI(this ES_AvatarShow self, ShowWindowData contextData = null)
		{
			self.View.uiTransform.SetVisible(true);

		}

		public static void HideCommonUI(this ES_AvatarShow self)
		{
			self.View.uiTransform.SetVisible(false);

		}

        /// <summary>
        /// 通过playerBaseInfoComponent数据设置头像框图片
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ShowAvatarIconByPlayerId(this ES_AvatarShow self, long playerID,bool isNameActive=true)
		{
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetOtherPlayerBaseInfo(self.DomainScene(),playerID);
            if (playerBaseInfoComponent.AvatarFrameItemCfgId == null)
                playerBaseInfoComponent.AvatarFrameItemCfgId = "AvatarFrame_None";

            //设置头像框
            await self.View.E_AvatarIconImage.SetOtherPlayerIcon(self.ClientScene(),playerID);

            //设置头像
            await self.View.EImage_FrameIconImage.SetOtherPlayerFrame(self.ClientScene(), playerID);

            self.SetPlayerName(playerBaseInfoComponent.PlayerName,isNameActive);
        }

        /// <summary>
        /// 设置自身的头像Icon
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isNameActive"></param>
        /// <returns></returns>
        public static async ETTask ShowMyAvatarIcon(this ES_AvatarShow self,bool isNameActive = true)
        {
           await self.ShowAvatarIconByPlayerId(PlayerStatusHelper.GetMyPlayerId(self.ClientScene()),isNameActive);
        }

        /// <summary>
        /// 按下按钮的回调函数
        /// </summary>
        /// <param name="self"></param>
        /// <param name="action">回调的委托</param>
        public static void ClickAvatarIconBtn(this ES_AvatarShow self,Func<ETTask> action)
        {
            self.View.EboxButton.AddListenerAsync(action);
        }


		//设置黄色描边是否激活
		public static void SetActiveLine(this ES_AvatarShow self,bool isActive)
		{
			self.View.E_ImgLineImage.SetVisible(isActive);
		}

        //设置名字
        public static void SetPlayerName(this ES_AvatarShow self, string name, bool isActive = true)
        {
            self.View.E_PlayerNameTextMeshProUGUI.SetText(name);
            self.View.E_PlayerNameTextMeshProUGUI.SetVisible(isActive);
        }

        //设置头像框(给DlgpersonalFrame调用)
        public static async ETTask SetFrameIcon(this ES_AvatarShow self, string resPath)
        {
            await self.View.EImage_FrameIconImage.SetImageByPath(resPath);

        }

        //设置头像图片(给DlgpersonalFrame调用)
        public static async ETTask SetAvatarIcon(this ES_AvatarShow self, string resPath)
		{
			await self.View.E_AvatarIconImage.SetImageByPath(resPath);
		}

    }
}
