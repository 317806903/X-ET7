using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Services.PlayerAccounts;
using Unity.Services.Authentication;
using Unity.Services.Core;

namespace ET.Client
{
    [FriendOf(typeof(LoginUnitySDKComponent))]
    public static class LoginUnitySDKComponentSystem
    {
        [ObjectSystem]
        public class LoginUnitySDKComponentAwakeSystem : AwakeSystem<LoginUnitySDKComponent>
        {
            protected override void Awake(LoginUnitySDKComponent self)
            {
                self.loginType = LoginType.UnitySDK;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class LoginUnitySDKComponentDestroySystem : DestroySystem<LoginUnitySDKComponent>
        {
            protected override void Destroy(LoginUnitySDKComponent self)
            {
                self.Destroy().Coroutine();
            }
        }

        public static async ETTask Awake(this LoginUnitySDKComponent self)
        {
            try
            {
                await UnityServices.InitializeAsync();
                self.RegisterLoginCallBack();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void RegisterLoginCallBack(this LoginUnitySDKComponent self)
        {
            PlayerAccountService.Instance.SignedIn += self.LoginCallBack;
        }

        public static void UnRegisterLoginCallBack(this LoginUnitySDKComponent self)
        {
            PlayerAccountService.Instance.SignedIn -= self.LoginCallBack;
        }

        public static void LoginCallBack(this LoginUnitySDKComponent self)
        {
            self.UpdateAuthentication().Coroutine();
        }

        public static async ETTask UpdateAuthentication(this LoginUnitySDKComponent self)
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);

            if (!string.IsNullOrEmpty(AuthenticationService.Instance.PlayerId))
            {
                await AuthenticationService.Instance.GetPlayerNameAsync();

                self.SetClientRecordAccountId(AuthenticationService.Instance.PlayerId);
                self.SetClientRecordAccountName(AuthenticationService.Instance.PlayerName);

                self.finishCallBack?.Invoke();
            }
            else
            {
                await self.SDKLoginOut(true);
            }
        }

        public static string GetSDKToken(this LoginUnitySDKComponent self)
        {
            return "";
        }

        public static void SetClientRecordAccountId(this LoginUnitySDKComponent self, string accountId)
        {
            string key = $"UnityPlayerId_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountId(this LoginUnitySDKComponent self)
        {
            string key = $"UnityPlayerId_{self.loginType.ToString()}";
            return PlayerPrefs.GetString(key);
        }

        public static void SetClientRecordAccountLoginTime(this LoginUnitySDKComponent self)
        {
            string key = $"UnityPlayerLoginTime_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, TimeHelper.ClientNowSeconds().ToString());
        }

        public static void SetClientRecordAccountLoginTimeNone(this LoginUnitySDKComponent self)
        {
            string key = $"UnityPlayerLoginTime_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, "0");
        }

        public static long GetClientRecordAccountLoginTime(this LoginUnitySDKComponent self)
        {
            string key = $"UnityPlayerLoginTime_{self.loginType.ToString()}";
            string value = PlayerPrefs.GetString(key);
            long lastLoginTime = long.Parse(value);
            return lastLoginTime;
        }

        public static void SetClientRecordAccountName(this LoginUnitySDKComponent self, string accountId)
        {
            string key = $"UnityPlayerName_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountName(this LoginUnitySDKComponent self)
        {
            string key = $"UnityPlayerName_{self.loginType.ToString()}";
            return PlayerPrefs.GetString(key);
        }

        public static async ETTask Destroy(this LoginUnitySDKComponent self)
        {
            self.UnRegisterLoginCallBack();
            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> ChkSDKLoginDone(this LoginUnitySDKComponent self)
        {
            string accountId = self.GetClientRecordAccountId();
            string accountName = self.GetClientRecordAccountName();
            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(accountName))
            {
                return false;
            }

            long lastLoginTime = self.GetClientRecordAccountLoginTime();
            if (TimeHelper.ClientNowSeconds() - lastLoginTime > 7 * 24 * 60 * 60)
            {
                return false;
            }

            return true;
        }

        public static async ETTask SDKLoginIn(this LoginUnitySDKComponent self, Action finishCallBack)
        {
            self.finishCallBack = finishCallBack;

            if (!PlayerAccountService.Instance.IsSignedIn)
                await PlayerAccountService.Instance.StartSignInAsync();
            else
                _ = UpdateAuthentication(self);

        }

        public static async ETTask SDKLoginOut(this LoginUnitySDKComponent self, bool needReLogin)
        {
            if (AuthenticationService.Instance.IsSignedIn)
                AuthenticationService.Instance.SignOut();
            if (PlayerAccountService.Instance.IsSignedIn)
                PlayerAccountService.Instance.SignOut();
            if (needReLogin)
            {
                await PlayerAccountService.Instance.StartSignInAsync();
            }
        }

    }
}