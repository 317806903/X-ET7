using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgPersonalInformation))]
    public static class DlgPersonalInformationSystem
    {
        public static void RegisterUIEvent(this DlgPersonalInformation self)
        {
            self.View.E_LogoutButton.AddListener(self.OnLogout);
            self.View.E_SaveButton.AddListenerAsync(self.OnSave);
            self.View.E_BG_ClickButton.AddListenerAsync(self.OnBGClick);
            self.View.E_InputFieldTMP_InputField.onEndEdit.AddListener(self.OnEndEdit);
            self.View.E_InputFieldTMP_InputField.onValueChanged.AddListener(self.OnValueChanged);

            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.prefabSource.prefabName = "Item_AvatarIcon";
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.prefabSource.poolSize = 6;
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.AddItemRefreshListener((transform, i) =>
                    self.AddAvatarItemRefreshListener(transform, i));
        }

        public static void ShowWindow(this DlgPersonalInformation self, ShowWindowData contextData = null)
        {
            self.ShowBg().Coroutine();
            self._ShowWindow().Coroutine();
        }

        public static async ETTask ShowBg(this DlgPersonalInformation self)
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

        public static async ETTask _ShowWindow(this DlgPersonalInformation self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            self.View.E_InputFieldTMP_InputField.text = playerBaseInfoComponent.PlayerName;
            self.curSelectedIconIndex = playerBaseInfoComponent.IconIndex;

            await self.CreateAvatarScrollItem();

            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.RefreshCells();
        }

        public static void OnLogout(this DlgPersonalInformation self)
        {
            self.Logout().Coroutine();
        }

        public static async ETTask Logout(this DlgPersonalInformation self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());


            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Des", playerBaseInfoComponent.PlayerName);
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { LoginHelper.LoginOut(self.ClientScene()).Coroutine(); }, null,
                sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask OnSave(this DlgPersonalInformation self)
        {
            string Name = self.View.E_InputFieldTMP_InputField.text;
            if (!self.DetermineNameLength(Name))
            {
                return;
            }

            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            if (playerBaseInfoComponent.PlayerName != self.View.E_InputFieldTMP_InputField.text ||
                playerBaseInfoComponent.IconIndex != self.curSelectedIconIndex)
            {
                playerBaseInfoComponent.PlayerName = self.View.E_InputFieldTMP_InputField.text;
                playerBaseInfoComponent.IconIndex = self.curSelectedIconIndex;
                await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new (){"PlayerName", "IconIndex"});
            }

            self.HidePersonalInfo().Coroutine();
        }

        public static async ETTask OnBGClick(this DlgPersonalInformation self)
        {
            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            if (playerBaseInfoComponent.PlayerName != self.View.E_InputFieldTMP_InputField.text ||
                playerBaseInfoComponent.IconIndex != self.curSelectedIconIndex)
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

        public static async ETTask HidePersonalInfo(this DlgPersonalInformation self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPersonalInformation>();
            await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgGameModeAR>();
        }

        public static bool DetermineNameLength(this DlgPersonalInformation self, string name)
        {
            if (name.Length < 1)
            {
                string tipMsg = "Name cannot be empty";
                UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            if (name.Length > self.NameMaxLength)
            {
                string tipMsg = "Name length exceeds limit";
                UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
                return false;
            }

            return true;
        }

        public static void OnEndEdit(this DlgPersonalInformation self, string name)
        {
            self.DetermineNameLength(name);
        }

        public static void OnValueChanged(this DlgPersonalInformation self, string name)
        {
            //判断name是否合法
        }

        public static async ETTask CreateAvatarScrollItem(this DlgPersonalInformation self)
        {
            int count = ET.Client.PlayerHelper.GetAvatarIconList().Count;
            self.AddUIScrollItems(ref self.ScrollItemAvatarIcons, count);
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static async ETTask AddAvatarItemRefreshListener(this DlgPersonalInformation self, Transform transform, int index)
        {
            Scroll_Item_AvatarIcon itemAvatar = self.ScrollItemAvatarIcons[index].BindTrans(transform);
            List<string> avatarIconList = ET.Client.PlayerHelper.GetAvatarIconList();
            await itemAvatar.EButton_IconImage.SetImageByPath(avatarIconList[index]);
            if (self.curSelectedIconIndex == index)
            {
                itemAvatar.EG_SelectedImage.gameObject.SetActive(true);
            }
            else
            {
                itemAvatar.EG_SelectedImage.gameObject.SetActive(false);
            }

            itemAvatar.EButton_IconButton.AddListener(() => { self.IconSelected(index).Coroutine(); });
        }

        public static async ETTask IconSelected(this DlgPersonalInformation self, int index)
        {
            self.curSelectedIconIndex = index;
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.RefreshCells();
        }
    }
}