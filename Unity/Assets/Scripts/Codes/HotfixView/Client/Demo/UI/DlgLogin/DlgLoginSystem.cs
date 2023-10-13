using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgLogin))]
    public static class DlgLoginSystem
    {
        public static void RegisterUIEvent(this DlgLogin self)
        {
            self.View.E_LoginButton.AddListenerAsync(self.OnLoginClickHandler);
        }

        public static void ShowWindow(this DlgLogin self, ShowWindowData contextData = null)
        {
            self.PrintSystemInfo();

            self.InitAccount();
            self.InitDebugMode();

            self.SetGameInfo();
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
            }else if(Application.platform == RuntimePlatform.IPhonePlayer)
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

        public static void InitAccount(this DlgLogin self)
        {
            string AccountKey = "AccountId";
            string accountId = "";
            if (PlayerPrefs.HasKey(AccountKey))
            {
                accountId = PlayerPrefs.GetString(AccountKey);
            }
            if(string.IsNullOrEmpty(accountId))
            {
                accountId = (100000 + RandomGenerator.RandInt32()).ToString();
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

        public static void InitDebugMode(this DlgLogin self)
        {
            // if (Application.isEditor == false)
            // {
            //     return;
            // }

#if UNITY_EDITOR
            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            playerComponent.IsDebugMode = true;
            self.View.E_ToggleDebugModeToggle.isOn = playerComponent.IsDebugMode;
            self.View.E_ToggleDebugModeToggle.gameObject.SetActive(true);
#else
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                self.View.E_ToggleDebugModeToggle.isOn = true;
                self.View.E_ToggleDebugModeToggle.gameObject.SetActive(true);
            }
            else
            {
                self.View.E_ToggleDebugModeToggle.isOn = false;
                self.View.E_ToggleDebugModeToggle.gameObject.SetActive(false);
                //self.View.E_ToggleDebugModeToggle.gameObject.SetActive(true);
            }
#endif
        }

        public static async ETTask OnLoginClickHandler(this DlgLogin self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            string accountId = self.View.E_AccountInputField.text;
            string accountPassWord = self.View.E_PasswordInputField.text;

            string AccountKey = "AccountId";
            string AccountPassWordKey = "AccountPassWord";

            PlayerPrefs.SetString(AccountKey, accountId);
            PlayerPrefs.SetString(AccountPassWordKey, accountPassWord);

            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(self.DomainScene());
            playerComponent.IsDebugMode = self.View.E_ToggleDebugModeToggle.isOn;

            await LoginHelper.Login(self.ClientScene(), accountId.ToString(), accountPassWord);
        }
    }
}