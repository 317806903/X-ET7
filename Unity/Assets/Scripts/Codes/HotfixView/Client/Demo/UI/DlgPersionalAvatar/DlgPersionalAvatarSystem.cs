using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

namespace ET.Client
{
    [FriendOf(typeof(DlgPersionalAvatar))]
    public static class DlgPersionalAvatarSystem
    {
        public static void RegisterUIEvent(this DlgPersionalAvatar self)
        {
            self.View.E_SaveButton.AddListenerAsync(self.OnSave);
            self.View.E_BG_ClickButton.AddListener(self.OnBGClick);

            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.prefabSource.prefabName = "Item_AvatarIcon";
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.AddItemRefreshListener(async (transform, i) =>
                    await self.AddAvatarItemRefreshListener(transform, i));

            self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.prefabSource.prefabName = "Item_Frame";
            self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.prefabSource.poolSize = 4;
            self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.AddItemRefreshListener(async (transform, i) =>
                     await self.AddFrameItemRefreshListener(transform, i));

            self.View.E_BtnCloseButton.AddListener(self.OnBGClick);
        }

        #region Show相关
        public static async ETTask ShowWindow(this DlgPersionalAvatar self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            self._ShowWindow().Coroutine();
        }

        /// <summary>
        /// 控制Ar或者普通的背景
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask ShowBg(this DlgPersionalAvatar self)
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
        ///  真正的显示逻辑
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask _ShowWindow(this DlgPersionalAvatar self)
        {
            //加载头像框数据
            PlayerBackPackComponent playerBackPackComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBackPack(self.DomainScene());
            List<ItemComponent> avatarFrameList = playerBackPackComponent.GetItemListByItemType(ItemType.AvatarFrame, ItemSubType.None);
            avatarFrameList.Sort((x, y) => x.model.ShowPriority.CompareTo(y.model.ShowPriority));
            self.avatarFrameList = avatarFrameList;

            //加载头像图片数据
            self.avatarIconList = ET.Client.PlayerStatusHelper.GetAvatarIconList();

            PlayerBaseInfoComponent playerBaseInfoComponent =await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.oldAvatarIconIndex = playerBaseInfoComponent.IconIndex;
            self.curSelectedAvatarIconIndex = self.oldAvatarIconIndex;

            self.View.ES_AvatarShow.ShowMyAvatarIcon(false).Coroutine();

            self.oldFrameIcon = playerBaseInfoComponent.AvatarFrameItemCfgId;
            self.curSelectedFrameIcon = self.oldFrameIcon;
            string frameNameDesc = ItemHelper.GetItemDesc(self.curSelectedFrameIcon);
            self.View.ELabelDesFrameTextMeshProUGUI.SetText(frameNameDesc);

            self.ChkInfoIsChanged();

            self.CreateAvatarScrollItem().Coroutine();
            self.CreateFrameScrollItem().Coroutine();

            //若只有一个默认头像框特殊处理
            if (self.avatarFrameList.Count < 2)
            {
                self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.gameObject.SetActive(false);
                self.View.ELabel_NoFrameTipTextMeshProUGUI.gameObject.SetActive(true);
                self.View.ELabelDesFrameTextMeshProUGUI.gameObject.SetActive(false);
                self.View.ELabelDesNOFrameTextMeshProUGUI.gameObject.SetActive(true);
            }
            else
            {
                self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.gameObject.SetActive(true);
                self.View.ELabel_NoFrameTipTextMeshProUGUI.gameObject.SetActive(false);
                self.View.ELabelDesFrameTextMeshProUGUI.gameObject.SetActive(true);
                self.View.ELabelDesNOFrameTextMeshProUGUI.gameObject.SetActive(false);
            }

            //self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.RefreshCells();
            //self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.RefreshCells();
        }
        #endregion

        /// <summary>
        ///  Save按钮点击事件
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask OnSave(this DlgPersionalAvatar self)
        {

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            PlayerBaseInfoComponent playerBaseInfoComponent =await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            playerBaseInfoComponent.IconIndex = self.curSelectedAvatarIconIndex;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new() { "PlayerName", "IconIndex" });

            playerBaseInfoComponent.AvatarFrameItemCfgId = self.curSelectedFrameIcon;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new() { "AvatarFrameItemCfgId" });

            self.oldAvatarIconIndex = self.curSelectedAvatarIconIndex;
            self.oldFrameIcon = self.curSelectedFrameIcon;
            self.ChkInfoIsChanged();

            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PersonalInfo_Update");
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
        }

        /// <summary>
        /// 背景图点击事件
        /// </summary>
        /// <param name="self"></param>
        public static void OnBGClick(this DlgPersionalAvatar self)
        {
            if (self.ChkInfoIsChanged())
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PersonalInfo_Abandon_Des");
                ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { self.HidePersonalInfo().Coroutine(); }, null
                    );
            }
            else
            {
                self.HidePersonalInfo().Coroutine();
            }
        }

        public static async ETTask HidePersonalInfo(this DlgPersionalAvatar self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPersionalAvatar>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersonalInformation>();
        }

        public static async ETTask CreateAvatarScrollItem(this DlgPersionalAvatar self)
        {
            int count = self.avatarIconList.Count;
            self.AddUIScrollItems(ref self.ScrollItemAvatarIcons, count);
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static async ETTask CreateFrameScrollItem(this DlgPersionalAvatar self)
        {
            int count = self.avatarFrameList.Count;
            self.AddUIScrollItems(ref self.ScrollItemFrameIcons, count);
            self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static async ETTask AddAvatarItemRefreshListener(this DlgPersionalAvatar self, Transform transform, int index)
        {
            Scroll_Item_AvatarIcon itemAvatar = self.ScrollItemAvatarIcons[index].BindTrans(transform);
            await itemAvatar.EButton_IconImage.SetImageByPath(self.avatarIconList[index]);
            if (self.curSelectedAvatarIconIndex == index)
            {
                itemAvatar.EG_SelectedImage.gameObject.SetActive(true);
            }
            else
            {
                itemAvatar.EG_SelectedImage.gameObject.SetActive(false);
            }

            itemAvatar.EButton_IconButton.AddListener(() => { self.AvatarIconSelected(index).Coroutine(); });
        }

        public static async ETTask AddFrameItemRefreshListener(this DlgPersionalAvatar self, Transform transform, int index)
        {
            Scroll_Item_Frame itemFrame = self.ScrollItemFrameIcons[index].BindTrans(transform);
            string itemCfgId = null;

            if (index < self.avatarFrameList.Count)
            {
                itemCfgId = self.avatarFrameList[index].CfgId;
            }

            itemFrame.ShowFrameItem(itemCfgId, false);


            await itemFrame.EImage_FrameImage.SetImageByItemCfgId(itemCfgId);
            if (self.curSelectedFrameIcon == self.avatarFrameList[index].CfgId)
            {
                itemFrame.EIcon_SelectedImage.gameObject.SetActive(true);
            }
            else
            {
                itemFrame.EIcon_SelectedImage.gameObject.SetActive(false);
            }

            itemFrame.EButton_SelectButton.AddListener(() => { self.FrameIconSelected(index).Coroutine(); });
        }


        public static async ETTask AvatarIconSelected(this DlgPersionalAvatar self, int index)
        {
            self.curSelectedAvatarIconIndex = index;

            await self.View.ES_AvatarShow.SetAvatarIcon(self.avatarIconList[index]);

            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.RefreshCells();

            self.ChkInfoIsChanged();
        }

        public static async ETTask FrameIconSelected(this DlgPersionalAvatar self, int index)
        {
            self.curSelectedFrameIcon = self.avatarFrameList[index].CfgId;

            ItemCfg itemCfg = ItemCfgCategory.Instance.Get(self.curSelectedFrameIcon);
            ResIconCfg resIconCfg = ResIconCfgCategory.Instance.Get(itemCfg.Icon);
            await self.View.ES_AvatarShow.SetFrameIcon(resIconCfg.ResName);
            string frameNameDesc = ItemHelper.GetItemDesc(self.curSelectedFrameIcon);
            self.View.ELabelDesFrameTextMeshProUGUI.SetText(frameNameDesc);
            self.View.ELoopScrollList_FrameLoopHorizontalScrollRect.RefreshCells();

            self.ChkInfoIsChanged();
        }

        /// <summary>
        /// 改变save preview/Current控件的显示和隐藏
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkInfoIsChanged(this DlgPersionalAvatar self)
        {
            if ((self.curSelectedAvatarIconIndex != self.oldAvatarIconIndex)|| (self.curSelectedFrameIcon != self.oldFrameIcon))
            {
                self.View.E_SaveButton.SetVisible(true);
                self.View.ELabel_PreviewTextMeshProUGUI.SetVisible(true);
                self.View.ELabel_CurrentTextMeshProUGUI.SetVisible(false);
                return true;
            }
            self.View.E_SaveButton.SetVisible(false);
            self.View.ELabel_PreviewTextMeshProUGUI.SetVisible(false);
            self.View.ELabel_CurrentTextMeshProUGUI.SetVisible(true);

            return false;
        }


    }
}