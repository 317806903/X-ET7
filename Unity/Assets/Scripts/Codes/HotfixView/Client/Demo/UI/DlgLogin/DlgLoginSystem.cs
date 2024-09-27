using System.Collections;
using System.Collections.Generic;
using System;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Invoke(TimerInvokeType.LoginTimer)]
    public class DlgLoginTimer: ATimer<DlgLogin>
    {
        protected override void Run(DlgLogin self)
        {
            try
            {
                self.ChkUpdate().Coroutine();
            }
            catch (Exception e)
            {
                Log.Error($"DlgLogin error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof(DlgLogin))]
    public static class DlgLoginSystem
    {
        public static void RegisterUIEvent(this DlgLogin self)
        {
            self.View.E_ToggleDebugModeToggle.AddListener(self.OnToggleDebugModeToggle);
            self.View.E_ToggleLoginEditorModeToggle.AddListener(self.OnToggleLoginModeToggle);
            self.View.E_LoginButton.AddListenerAsync(self.LoginWhenEditor);
            self.View.E_Login_GuestButton.AddListenerAsync(self.LoginWhenGuest);
            self.View.E_Login_SDKButton.AddListenerAsync(self.LoginWhenSDK);
            self.View.E_Login_AppleButton.AddListenerAsync(self.LoginWhenSDK);
        }

        public static async ETTask ShowWindow(this DlgLogin self, ShowWindowData contextData = null)
        {
            self.PrintSystemInfo();
            self.SetGameInfo();

            self._ShowWindow().Coroutine();

            self.Timer = TimerComponent.Instance.NewRepeatedTimer(5000, TimerInvokeType.LoginTimer, self);
        }

        public static async ETTask _ShowWindow(this DlgLogin self)
        {
            await self.LoadBG();
            self.View.ELabel_VersionTextMeshProUGUI.text = $"{ResConfig.Instance.Channel} {Application.version}-{ResComponent.Instance.PackageVersion}";
            await self.ChkIsShowDebugUI();
            await self.InitAccount();
            await self.InitDebugMode();

            await ETTask.CompletedTask;
        }

        public static async ETTask LoadBG(this DlgLogin self)
        {
            self.View.E_BGImage.LoadBG(self).Coroutine();
        }

        public static void HideWindow(this DlgLogin self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
        }

        public static async ETTask ChkUpdate(this DlgLogin self)
        {
            Scene clientScene = self.ClientScene();
            // 热更流程
            bool bRet = await EntryEvent3_InitClient.ChkHotUpdateAsync(clientScene, false);
            if (bRet == false)
            {
                UIComponent uiComponent = UIManagerHelper.GetUIComponent(clientScene);
                uiComponent.HideWindow<DlgLogin>();
                // 打开热更界面
                await uiComponent.ShowWindowAsync<DlgUpdate>();
            }
        }

        public static void OnToggleDebugModeToggle(this DlgLogin self, bool status)
        {
            self.IsDebugMode = !self.IsDebugMode;
        }

        public static void OnToggleLoginModeToggle(this DlgLogin self, bool status)
        {
            self.IsEditorLoginMode = !self.IsEditorLoginMode;
            self.InitAccount().Coroutine();
        }

        public static void PrintSystemInfo(this DlgLogin self)
        {

            //----------------------------------
#if UNITY_EDITOR
            Debug.Log("--[[ PrintSystemInfo UNITY_EDITOR");
#endif

#if UNITY_ANDROID
            Debug.Log("--[[ PrintSystemInfo UNITY_ANDROID");
#endif

#if UNITY_IPHONE
            Debug.Log("--[[ PrintSystemInfo UNITY_IPHONE");
#endif

#if UNITY_STANDALONE
            Debug.Log("--[[ PrintSystemInfo UNITY_STANDALONE");
#endif

#if UNITY_STANDALONE_WIN
            Debug.Log("--[[ PrintSystemInfo UNITY_STANDALONE_WIN");
#endif

#if UNITY_STANDALONE_OSX
            Debug.Log("--[[ PrintSystemInfo UNITY_STANDALONE_OSX");
#endif

            //----------------------------------
            if (Application.platform == RuntimePlatform.Android)
            {
                Debug.Log("--[[ PrintSystemInfo Android");
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Debug.Log("--[[ PrintSystemInfo IOS");
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Debug.Log("--[[ PrintSystemInfo WindowsPlayer");
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Debug.Log("--[[ PrintSystemInfo WindowsEditor");
            }
            else if (Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("--[[ PrintSystemInfo OSXPlayer");
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                Debug.Log("--[[ PrintSystemInfo OSXEditor");
            }

            //----------------------------------
            if (Application.isEditor)
            {
                Debug.Log("--[[ PrintSystemInfo Application.isEditor");
            }
            if (Application.isPlaying)
            {
                Debug.Log("--[[ PrintSystemInfo Application.isPlaying");
            }
            if (Application.isFocused)
            {
                Debug.Log("--[[ PrintSystemInfo Application.isFocused");
            }
            if (Application.isBatchMode)
            {
                Debug.Log("--[[ PrintSystemInfo Application.isBatchMode");
            }
            if (Application.isConsolePlatform)
            {
                Debug.Log("--[[ PrintSystemInfo Application.isConsolePlatform");
            }
            if (Application.isMobilePlatform)
            {
                Debug.Log("--[[ PrintSystemInfo Application.isMobilePlatform");
            }
            //----------------------------------
        }

        public static void SetGameInfo(this DlgLogin self)
        {
            Application.targetFrameRate = 30;

            ET.Client.UIRootManagerComponent.Instance.SetDefaultRotation().Coroutine();
        }

        public static async ETTask ChkIsShowDebugUI(this DlgLogin self)
        {
            await ETTask.CompletedTask;
            if (Application.isEditor)
            {
                self.IsAutoLogining = false;
                if (DebugConnectComponent.Instance.IsFirstShow)
                {
                    DebugConnectComponent.Instance.IsFirstShow = false;
                    self.IsShowDebugMode = true;
                    self.IsShowEditorLoginMode = true;

                    self.IsDebugMode = true;
                    self.IsEditorLoginMode = true;
                }
                else
                {
                    self.IsShowDebugMode = ResConfig.Instance.IsShowDebugMode;
                    self.IsShowEditorLoginMode = ResConfig.Instance.IsShowEditorLoginMode;

                    self.IsDebugMode = DebugConnectComponent.Instance.IsDebugMode;
                    self.IsEditorLoginMode = DebugConnectComponent.Instance.IsEditorLoginMode;
                }

                if (ET.SceneHelper.ChkIsGameModeArcade())
                {
                    self.IsShowDebugMode = false;
                    self.IsShowEditorLoginMode = false;
                    self.IsDebugMode = false;
                    self.IsEditorLoginMode = false;
                }
            }
            else
            {
                self.IsShowDebugMode = ResConfig.Instance.IsShowDebugMode;
                self.IsShowEditorLoginMode = ResConfig.Instance.IsShowEditorLoginMode;

                self.IsDebugMode = DebugConnectComponent.Instance.IsDebugMode;
                self.IsEditorLoginMode = DebugConnectComponent.Instance.IsEditorLoginMode;

                if (self.IsShowDebugMode || self.IsShowEditorLoginMode)
                {
                    self.IsAutoLogining = false;
                }
                else
                {
                    self.IsAutoLogining = true;
                }
            }
        }

        public static async ETTask InitAccount(this DlgLogin self)
        {
            if (ET.SceneHelper.ChkIsGameModeArcade())
            {
                await self.InitAccount_Arcade();
            }
            else
            {
                await self.InitAccount_Normal();
            }
        }

        public static async ETTask InitAccount_Arcade(this DlgLogin self)
        {
            self.View.E_Login_SDKButton.SetVisible(false);
            self.View.E_Login_AppleButton.SetVisible(false);
            self.View.EG_LoginAccountRootRectTransform.SetVisible(false);
            self.View.EG_LoginWhenSDKRectTransform.SetVisible(false);
            self.View.EG_LoginWhenEditorRectTransform.SetVisible(false);
            self.View.E_LoggingTextTextMeshProUGUI.SetVisible(false);

            bool bSDKLoginDone = await ET.Client.LoginSDKManagerComponent.Instance.ChkSDKLoginDone();
            bool bGuestLoginDone = self.ChkGuestLoginDone();
            self.View.EG_LoginAccountRootRectTransform.SetVisible(true);

            if (bSDKLoginDone && self.IsAutoLogining)
            {
                self.LoginWhenSDK().Coroutine();
            }
            else if(bGuestLoginDone && self.IsAutoLogining)
            {
                self.LoginWhenGuest().Coroutine();
            }
            else
            {
                self.View.EG_LoginWhenSDKRectTransform.SetVisible(true);
            }
        }

        public static async ETTask InitAccount_Normal(this DlgLogin self)
        {
            if (ChannelSettingComponent.Instance.ChkIsNeedSDKLogin())
            {
                self.View.E_Login_SDKButton.SetVisible(Application.platform == RuntimePlatform.Android);
                self.View.E_Login_AppleButton.SetVisible(Application.platform == RuntimePlatform.IPhonePlayer);
            }
            else
            {
                self.View.E_Login_SDKButton.SetVisible(false);
                self.View.E_Login_AppleButton.SetVisible(false);
            }

            self.View.EG_LoginAccountRootRectTransform.SetVisible(false);
            self.View.EG_LoginWhenSDKRectTransform.SetVisible(false);
            self.View.EG_LoginWhenEditorRectTransform.SetVisible(false);
            self.View.E_LoggingTextTextMeshProUGUI.SetVisible(false);
            if (self.IsEditorLoginMode == false)
            {
                bool bSDKLoginDone = await ET.Client.LoginSDKManagerComponent.Instance.ChkSDKLoginDone();
                bool bGuestLoginDone = self.ChkGuestLoginDone();
                self.View.EG_LoginAccountRootRectTransform.SetVisible(true);

                if (bSDKLoginDone && self.IsAutoLogining)
                {
                    self.LoginWhenSDK().Coroutine();
                }
                else if(bGuestLoginDone && self.IsAutoLogining)
                {
                    self.LoginWhenGuest().Coroutine();
                }
                else
                {
                    self.View.EG_LoginWhenSDKRectTransform.SetVisible(true);
                }
            }
            else
            {
                self.View.EG_LoginAccountRootRectTransform.SetVisible(true);
                self.View.EG_LoginWhenEditorRectTransform.SetVisible(true);
                self.InitAccountWhenEditor();

            }

        }

        public static void InitAccountWhenEditor(this DlgLogin self)
        {
            string AccountKey = "AccountId";
            string accountId = "";
            if (PlayerPrefs.HasKey(AccountKey))
            {
                accountId = PlayerPrefs.GetString(AccountKey);
            }
            if (string.IsNullOrEmpty(accountId))
            {
                accountId = SystemInfo.deviceUniqueIdentifier;
            }
            if (string.IsNullOrEmpty(accountId))
            {
                accountId = IdGenerater.Instance.GenerateId().ToString();
            }

            string AccountPassWordKey = "AccountPassWord";
            string accountPassWord = "";
            if (PlayerPrefs.HasKey(AccountPassWordKey))
            {
                accountPassWord = PlayerPrefs.GetString(AccountPassWordKey);
            }

            self.View.E_AccountInputField.text = accountId;
            self.View.E_PasswordInputField.text = accountPassWord;

        }

        public static async ETTask InitDebugMode(this DlgLogin self)
        {
            await ETTask.CompletedTask;
            self.View.E_ToggleDebugModeToggle.SetIsOnWithoutNotify(self.IsDebugMode);
            self.View.E_ToggleDebugModeToggle.gameObject.SetActive(self.IsShowDebugMode);

            self.View.E_ToggleLoginEditorModeToggle.SetIsOnWithoutNotify(self.IsEditorLoginMode);
            self.View.E_ToggleLoginEditorModeToggle.gameObject.SetActive(self.IsShowEditorLoginMode);
        }

        public static async ETTask OnSwitchPlayerClickHandler(this DlgLogin self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            await ET.Client.LoginSDKManagerComponent.Instance.SDKLoginOut(true);
        }

        public static async ETTask LoginWhenEditor(this DlgLogin self)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            string accountId = self.View.E_AccountInputField.text;
            string accountPwd = self.View.E_PasswordInputField.text;

            if (string.IsNullOrEmpty(accountId))
            {
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), "account is null", null);
                return;
            }

            string AccountKey = "AccountId";
            string AccountPassWordKey = "AccountPassWord";
            PlayerPrefs.SetString(AccountKey, accountId);
            PlayerPrefs.SetString(AccountPassWordKey, accountPwd);

            self.RecordIsDebugMode();

            (bool bRet, string msg) = await LoginHelper.Login(self.ClientScene(), accountId, accountPwd, LoginType.Editor);
            if (bRet == false)
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Des");
                string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Title");
                string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Btn");
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msgTxt, () =>
                {
                    self.LoginWhenEditor().Coroutine();
                }, sureTxt, titleTxt);
            }
        }

        public static async ETTask LoginWhenGuest(this DlgLogin self)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                eventName = "PlayClicked",
            });
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				eventName = "RoleLoggedIn",
			});

            string AccountKey = "AccountId_Guest";
            string AccountPassWordKey = "AccountPassWord_Guest";

            string accountId = "";
            if (PlayerPrefs.HasKey(AccountKey))
            {
                accountId = PlayerPrefs.GetString(AccountKey);
            }
            if (string.IsNullOrEmpty(accountId))
            {
                accountId = SystemInfo.deviceUniqueIdentifier;
            }
            if (string.IsNullOrEmpty(accountId))
            {
                accountId = IdGenerater.Instance.GenerateId().ToString();
            }

            string accountPassWord = "";
            if (PlayerPrefs.HasKey(AccountPassWordKey))
            {
                accountPassWord = PlayerPrefs.GetString(AccountPassWordKey);
            }

            PlayerPrefs.SetString(AccountKey, accountId);
            PlayerPrefs.SetString(AccountPassWordKey, accountPassWord);
            string key = $"GuestPlayerLoginTime_{LoginType.Editor.ToString()}";
            PlayerPrefs.SetString(key, TimeHelper.ClientNowSeconds().ToString());
            self.RecordIsDebugMode();

            self.ShowLoginTips(true);
            (bool bRet, string msg) = await LoginHelper.Login(self.ClientScene(), accountId, accountPassWord, LoginType.Editor);
            if (bRet == false)
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Des");
                string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Title");
                string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Btn");
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msgTxt, () =>
                {
                    self.LoginWhenGuest().Coroutine();
                }, sureTxt, titleTxt);
            }
        }

        public static bool ChkGuestLoginDone(this DlgLogin self){
            string AccountKey = "AccountId_Guest";
            string accountId = "";
            if (PlayerPrefs.HasKey(AccountKey))
            {
                accountId = PlayerPrefs.GetString(AccountKey);
            }
            if (string.IsNullOrEmpty(accountId))
            {
                return false;
            }

            string key = $"GuestPlayerLoginTime_{LoginType.Editor.ToString()}";
            if (!PlayerPrefs.HasKey(key))
            {
                return false;
            }
            string value = PlayerPrefs.GetString(key);
            long lastLoginTime = long.Parse(value);

            if (TimeHelper.ClientNowSeconds() - lastLoginTime > 7 * 24 * 60 * 60)
            {
                return false;
            }

            return true;
        }

        public static async ETTask LoginWhenSDK(this DlgLogin self)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
			{
				eventName = "RoleLoggedIn",
			});
            await ET.Client.LoginSDKManagerComponent.Instance.SDKLoginIn(async () =>
            {
                //等待0.5s，回调直接修改UI会导致应用崩溃
                await TimerComponent.Instance.WaitAsync(500);
                self.ShowLoginTips(true);
                self.LoginWhenSDK_LoginDone().Coroutine();
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "LoginClicked",
                    properties = new()
                    {
                        {"success", true},
                    }
                });
            },()=>{
                self.ShowLoginTips(false);
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    eventName = "LoginClicked",
                    properties = new()
                    {
                        {"success", false},
                    }
                });
            });
        }

        public static async ETTask LoginWhenSDK_LoginDone(this DlgLogin self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            self.RecordIsDebugMode();

            string accountId = ET.Client.LoginSDKManagerComponent.Instance.GetClientRecordAccountId();
            string accountName = ET.Client.LoginSDKManagerComponent.Instance.GetClientRecordAccountName();
            string token = ET.Client.LoginSDKManagerComponent.Instance.GetSDKToken();
            string email = ET.Client.LoginSDKManagerComponent.Instance.GetSDKEmail();
            LoginType loginType = ET.Client.LoginSDKManagerComponent.Instance.GetLoginType();
            ET.Client.LoginSDKManagerComponent.Instance.SetClientRecordAccountLoginTime();
            (bool bRet, string msg) = await LoginHelper.LoginWithAuth(self.ClientScene(), accountId, "", loginType, accountName, token, email);
            if (bRet == false)
            {
                string msgTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Des");
                string titleTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Title");
                string sureTxt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_ServerFail_Btn");
                UIManagerHelper.ShowOnlyConfirmHighest(self.DomainScene(), msgTxt, () =>
                {
                    self.LoginWhenSDK_LoginDone().Coroutine();
                }, sureTxt, titleTxt);
            }
        }

        public static void ShowLoginTips(this DlgLogin self, bool bShow){
            self.View.E_LoggingTextTextMeshProUGUI.SetVisible(bShow);
            self.View.EG_LoginWhenSDKRectTransform.SetVisible(!bShow);
        }

        public static void RecordIsDebugMode(this DlgLogin self)
        {
            DebugConnectComponent.Instance.IsDebugMode = self.View.E_ToggleDebugModeToggle.isOn;
            DebugConnectComponent.Instance.IsEditorLoginMode = self.View.E_ToggleLoginEditorModeToggle.isOn;

            if (ResConfig.Instance.IsShowDebugMode == false)
            {
                ResConfig.Instance.IsShowDebugMode = self.View.E_ToggleDebugModeToggle.gameObject.activeSelf;
            }

            if (ResConfig.Instance.IsShowEditorLoginMode == false)
            {
                ResConfig.Instance.IsShowEditorLoginMode = self.View.E_ToggleLoginEditorModeToggle.gameObject.activeSelf;
            }
        }

    }
}