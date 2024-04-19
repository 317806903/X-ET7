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
            self.View.E_Logout_SdkButton.AddListener(self.OnLogout);
            self.View.E_GoogleLoginButton.AddListenerAsync(self.OnClickBindAccount);
            self.View.E_IphoneLoginButton.AddListenerAsync(self.OnClickBindAccount);
            self.View.E_SaveButton.AddListenerAsync(self.OnSave);
            self.View.E_BG_ClickButton.AddListener(self.OnBGClick);
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
            self.oldName = playerBaseInfoComponent.PlayerName;
            self.View.E_InputFieldTMP_InputField.text = self.oldName;
            self.oldIconIndex = playerBaseInfoComponent.IconIndex;
            self.curSelectedIconIndex = self.oldIconIndex;
            self.View.ELabel_IDTextMeshProUGUI.text = $"ID:{playerBaseInfoComponent.GetPlayerId()}";
            self.View.E_SaveButton.SetVisible(false);

            self.View.E_GoogleLoginButton.SetVisible(Application.platform == RuntimePlatform.Android && playerBaseInfoComponent.BindLoginType == LoginType.Editor);
            self.View.E_IphoneLoginButton.SetVisible(Application.platform == RuntimePlatform.IPhonePlayer && playerBaseInfoComponent.BindLoginType == LoginType.Editor);
            self.View.E_AccountButton.SetVisible(playerBaseInfoComponent.BindLoginType != LoginType.Editor);
            self.View.E_Account_TextTextMeshProUGUI.text = playerBaseInfoComponent.BindEmail;
            if(playerBaseInfoComponent.BindLoginType == LoginType.GoogleSDK){
                self.View.E_Account_TitleTextMeshProUGUI.text = "Google Account:";
            }else if(playerBaseInfoComponent.BindLoginType == LoginType.AppleSDK){
                self.View.E_Account_TitleTextMeshProUGUI.text = "Apple ID:";
            }else{
                self.View.E_Account_TitleTextMeshProUGUI.text = "Account:";
            }
            await self.CreateAvatarScrollItem();

            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.RefreshCells();
        }

        public static void OnLogout(this DlgPersonalInformation self)
        {
            self.Logout().Coroutine();
        }

        public static async ETTask Logout(this DlgPersonalInformation self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);


            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());

            string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Des", playerBaseInfoComponent.PlayerName);
            string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Confirm");
            string cancelTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Cancel");
            string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Dialog_AreYouLeaving_Title");
            ET.Client.UIManagerHelper.ShowConfirm(self.DomainScene(), msgTxt, () => { LoginHelper.LoginOut(self.ClientScene(), true).Coroutine(); }, null,
                sureTxt, cancelTxt, titleTxt);
        }

        public static async ETTask OnSave(this DlgPersonalInformation self)
        {
            if (!self.DetermineNameLength(self.curName))
            {
                return;
            }

            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            PlayerBaseInfoComponent playerBaseInfoComponent =
                await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(self.DomainScene());
            playerBaseInfoComponent.PlayerName = self.curName;
            playerBaseInfoComponent.IconIndex = self.curSelectedIconIndex;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(self.DomainScene(), PlayerModelType.BaseInfo, new (){"PlayerName", "IconIndex"});

            self.oldName = self.curName;
            self.oldIconIndex = self.curSelectedIconIndex;
            self.View.E_SaveButton.SetVisible(false);

            string tipMsg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_PersonalInfo_Update");
            ET.Client.UIManagerHelper.ShowTip(self.DomainScene(), tipMsg);
            //self.HidePersonalInfo().Coroutine();
        }

        public static void OnBGClick(this DlgPersonalInformation self)
        {
            if (self.ChkInfoChanged())
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
            self.curName = name;
            //TODO 判断name是否合法
            self.ChkInfoChanged();
        }

        public static async ETTask CreateAvatarScrollItem(this DlgPersonalInformation self)
        {
            int count = ET.Client.PlayerStatusHelper.GetAvatarIconList().Count;
            self.AddUIScrollItems(ref self.ScrollItemAvatarIcons, count);
            self.View.ELoopScrollList_AvatarLoopHorizontalScrollRect.SetVisible(true, count);
        }

        public static async ETTask AddAvatarItemRefreshListener(this DlgPersonalInformation self, Transform transform, int index)
        {
            Scroll_Item_AvatarIcon itemAvatar = self.ScrollItemAvatarIcons[index].BindTrans(transform);
            List<string> avatarIconList = ET.Client.PlayerStatusHelper.GetAvatarIconList();
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

            self.ChkInfoChanged();
        }

        public static async ETTask OnClickBindAccount(this DlgPersonalInformation self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "BindClick",
            });
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				eventName = "BindEnded",
			});
            string AccountKey = "AccountId_Guest";
            string accountId = "";
            if (PlayerPrefs.HasKey(AccountKey))
            {
                accountId = PlayerPrefs.GetString(AccountKey);
            }
            await ET.Client.LoginSDKManagerComponent.Instance.SDKLoginIn(async () =>
            {
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
                await TimerComponent.Instance.WaitAsync(1000);
                string bindAccountId = ET.Client.LoginSDKManagerComponent.Instance.GetClientRecordAccountId();
                string accountName = ET.Client.LoginSDKManagerComponent.Instance.GetClientRecordAccountName();
                string token = ET.Client.LoginSDKManagerComponent.Instance.GetSDKToken();
                string email = ET.Client.LoginSDKManagerComponent.Instance.GetSDKEmail();
                LoginType loginType = ET.Client.LoginSDKManagerComponent.Instance.GetLoginType();
                ET.Client.LoginSDKManagerComponent.Instance.SetClientRecordAccountLoginTime();
                (bool bRet, string msg) = await LoginHelper.BindAccountWithAuth(self.ClientScene(), accountId, bindAccountId, loginType, accountName, token, email);
                if (bRet == false)
                {
                    string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_Title");
                    string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_BindAccount_FailBtn");
                    UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, () =>
                    {
                        ET.Client.LoginSDKManagerComponent.Instance.SetClientRecordAccountLoginTimeNone();
                        ET.Client.LoginSDKManagerComponent.Instance.SDKLoginOut(false).Coroutine();
                    }, sureTxt, titleTxt);
                }else{
                    self.View.E_InputFieldTMP_InputField.text = accountName;
                    self.View.E_GoogleLoginButton.SetVisible(Application.platform == RuntimePlatform.Android && loginType == LoginType.Editor);
                    self.View.E_IphoneLoginButton.SetVisible(Application.platform == RuntimePlatform.IPhonePlayer && loginType == LoginType.Editor);
                    self.View.E_AccountButton.SetVisible(loginType != LoginType.Editor);
                    self.View.E_Account_TextTextMeshProUGUI.text = email;
                    if(loginType == LoginType.GoogleSDK){
                        self.View.E_Account_TitleTextMeshProUGUI.text = "Google Account:";
                    }else if(loginType == LoginType.AppleSDK){
                        self.View.E_Account_TitleTextMeshProUGUI.text = "Apple ID:";
                    }else{
                        self.View.E_Account_TitleTextMeshProUGUI.text = "Account:";
                    }
                }
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "BindEnded",
                    properties = new()
                    {
                        {"success", bRet},
                    }
                });
            });
        }

        public static bool ChkInfoChanged(this DlgPersonalInformation self)
        {
            if (self.curSelectedIconIndex != self.oldIconIndex || self.curName != self.oldName)
            {
                self.View.E_SaveButton.SetVisible(true);
                return true;
            }
            self.View.E_SaveButton.SetVisible(false);
            return false;
        }
    }
}