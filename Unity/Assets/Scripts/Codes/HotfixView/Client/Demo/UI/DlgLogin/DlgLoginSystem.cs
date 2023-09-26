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
            self.View.E_LoginButton.onClick.AddListener(self.OnLoginClickHandler);
        }

        public static void ShowWindow(this DlgLogin self, ShowWindowData contextData = null)
        {
            self.InitAccount();

            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
        }

        public static void InitAccount(this DlgLogin self)
        {
            string AccountKey = "AccountId";
            int accountId;
            if (PlayerPrefs.HasKey(AccountKey))
            {
                accountId = PlayerPrefs.GetInt(AccountKey);
            }
            else
            {
                accountId = 100000 + RandomGenerator.RandInt32();
            }
            self.View.E_AccountInputField.text = accountId.ToString();
            self.View.E_PasswordInputField.text = "";
        }

        public static void OnLoginClickHandler(this DlgLogin self)
        {
            ET.Ability.Client.UIAudioManagerHelper.PlayUIAudioConfirm(self.DomainScene());

            string AccountKey = "AccountId";
            int accountId = Convert.ToInt32(self.View.E_AccountInputField.text);
            PlayerPrefs.SetInt(AccountKey, accountId);

            LoginHelper.Login(self.ClientScene(), self.View.E_AccountInputField.text, self.View.E_PasswordInputField.text).Coroutine();
        }
    }
}