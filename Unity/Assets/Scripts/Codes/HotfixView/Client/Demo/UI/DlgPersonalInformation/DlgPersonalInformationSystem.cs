using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using ET.AbilityConfig;

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

            self.View.E_BG_ClickButton.AddListener(()=>
            {
                if (self.ChkCanClickBg() == false)
                {
                    return;
                }
                self.OnBGClick();
            });
            self.View.E_BtnCloseButton.AddListener(self.OnBGClick);

            //改名按钮
            self.View.EBtn_ChgNameButton.AddListener(() => {
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersionalName>().Coroutine();
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPersonalInformation>();
            });

            //改头像按钮
            self.View.EBtn_ChgAvatarButton.AddListener(() => {
                UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgPersionalAvatar>().Coroutine();
                UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPersonalInformation>();
            });
        }

        public static async ETTask ShowWindow(this DlgPersonalInformation self, ShowWindowData contextData = null)
        {
            self.dlgShowTime = TimeHelper.ClientNow();

            self.ShowBg().Coroutine();
            self._ShowWindow().Coroutine();
        }

        public static bool ChkCanClickBg(this DlgPersonalInformation self)
        {
            if (self.dlgShowTime < TimeHelper.ClientNow() - (long)(1000 * 1f))
            {
                return true;
            }
            return false;
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
            self.View.ELabel_NameTextMeshProUGUI.text = playerBaseInfoComponent.PlayerName;
            self.View.ELabel_IDTextMeshProUGUI.text = $"ID:{playerBaseInfoComponent.GetPlayerId()}";

            self.View.ES_AvatarShow.ShowMyAvatarIcon(false).Coroutine();

            Log.Debug($"playerBaseInfoComponent.BindLoginType[{playerBaseInfoComponent.BindLoginType.ToString()}]");
            if (ChannelSettingComponent.Instance.ChkIsNeedSDKLogin())
            {
                if (playerBaseInfoComponent.BindLoginType == LoginType.Editor)
                {
                    self.View.E_GoogleLoginButton.SetVisible(Application.platform == RuntimePlatform.Android);
                    self.View.E_IphoneLoginButton.SetVisible(Application.platform == RuntimePlatform.IPhonePlayer);
                    self.View.E_AccountButton.SetVisible(false);
                }
                else
                {
                    self.View.E_GoogleLoginButton.SetVisible(false);
                    self.View.E_IphoneLoginButton.SetVisible(false);
                    self.View.E_AccountButton.SetVisible(true);
                }

                self.View.E_Account_TextTextMeshProUGUI.text = playerBaseInfoComponent.BindEmail;
                if(playerBaseInfoComponent.BindLoginType == LoginType.GoogleSDK)
                {
                    self.View.E_Account_TitleTextMeshProUGUI.text = "Google Account:";
                }
                else if(playerBaseInfoComponent.BindLoginType == LoginType.AppleSDK)
                {
                    self.View.E_Account_TitleTextMeshProUGUI.text = "Apple ID:";
                }
                else
                {
                    self.View.E_Account_TitleTextMeshProUGUI.text = "Account:";
                }
            }
            else
            {
                self.View.E_AccountButton.SetVisible(false);
                self.View.E_GoogleLoginButton.SetVisible(false);
                self.View.E_IphoneLoginButton.SetVisible(false);
            }

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


        public static void OnBGClick(this DlgPersonalInformation self)
        {
            self.HidePersonalInfo().Coroutine();
        }

        public static async ETTask HidePersonalInfo(this DlgPersonalInformation self)
        {
            UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow<DlgPersonalInformation>();
            await UIManagerHelper.EnterGameModeUI(self.DomainScene());
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
                    //self.View.E_InputFieldTMP_InputField.text = accountName;
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

    }
}