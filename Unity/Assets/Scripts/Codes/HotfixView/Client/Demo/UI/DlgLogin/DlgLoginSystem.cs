using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
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
        }

        public static void ShowWindow(this DlgLogin self, ShowWindowData contextData = null)
        {
            self.PrintSystemInfo();
            self.SetGameInfo();

            self._ShowWindow().Coroutine();
        }

        public static async ETTask _ShowWindow(this DlgLogin self)
        {
            await self.ChkIsShowDebugUI();
            await self.InitAccount();
            await self.InitDebugMode();

            await ETTask.CompletedTask;
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

            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
        }

        public static async ETTask ChkIsShowDebugUI(this DlgLogin self)
        {
            await ETTask.CompletedTask;
            if (Application.isEditor)
            {
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
            }
            else
            {
                self.IsShowDebugMode = ResConfig.Instance.IsShowDebugMode;
                self.IsShowEditorLoginMode = ResConfig.Instance.IsShowEditorLoginMode;

                self.IsDebugMode = DebugConnectComponent.Instance.IsDebugMode;
                self.IsEditorLoginMode = DebugConnectComponent.Instance.IsEditorLoginMode;
            }
        }

        public static async ETTask InitAccount(this DlgLogin self)
        {
            self.View.EG_LoginAccountRootRectTransform.SetVisible(false);
            self.View.EG_LoginWhenSDKRectTransform.SetVisible(false);
            self.View.EG_LoginWhenEditorRectTransform.SetVisible(false);
            if (self.IsEditorLoginMode == false)
            {
                bool bSDKLoginDone = await ET.Client.LoginSDKComponent.Instance.ChkSDKLoginDone();
                self.View.EG_LoginAccountRootRectTransform.SetVisible(true);
                self.View.EG_LoginWhenSDKRectTransform.SetVisible(true);
                if (bSDKLoginDone)
                {
                    await self.LoginWhenSDK_LoginDone();
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
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            await ET.Client.LoginSDKComponent.Instance.SDKLoginOut(true);
        }

        public static async ETTask LoginWhenEditor(this DlgLogin self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

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
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, null);
            }
        }

        public static async ETTask LoginWhenGuest(this DlgLogin self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

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

            self.RecordIsDebugMode();

            (bool bRet, string msg) = await LoginHelper.Login(self.ClientScene(), accountId, accountPassWord, LoginType.Editor);
            if (bRet == false)
            {
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, null);
            }
        }

        public static async ETTask LoginWhenSDK(this DlgLogin self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            await ET.Client.LoginSDKComponent.Instance.SDKLoginIn(() =>
            {
                self.LoginWhenSDK_LoginDone().Coroutine();
            });
        }

        public static async ETTask LoginWhenSDK_LoginDone(this DlgLogin self)
        {
            UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            self.RecordIsDebugMode();

            string accountId = ET.Client.LoginSDKComponent.Instance.GetClientRecordAccountId();
            ET.Client.LoginSDKComponent.Instance.SetClientRecordAccountLoginTime();
            (bool bRet, string msg) = await LoginHelper.Login(self.ClientScene(), accountId, "", LoginType.UnitySDK);
            if (bRet == false)
            {
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), msg, null);
            }
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