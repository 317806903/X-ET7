using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.Services.PlayerAccounts;
using Unity.Services.Authentication;
using Unity.Services.Core;

namespace ET.Client
{
    [FriendOf(typeof(LoginSDKComponent))]
    public static class LoginSDKComponentSystem
    {
        [ObjectSystem]
        public class LoginSDKComponentAwakeSystem : AwakeSystem<LoginSDKComponent>
        {
            protected override void Awake(LoginSDKComponent self)
            {
                LoginSDKComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class LoginSDKComponentDestroySystem : DestroySystem<LoginSDKComponent>
        {
            protected override void Destroy(LoginSDKComponent self)
            {
                self.Destroy().Coroutine();
                LoginSDKComponent.Instance = null;
            }
        }

        public static async ETTask Awake(this LoginSDKComponent self)
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

        public static void RegisterLoginCallBack(this LoginSDKComponent self)
        {
            PlayerAccountService.Instance.SignedIn += self.LoginCallBack;
        }

        public static void UnRegisterLoginCallBack(this LoginSDKComponent self)
        {
            PlayerAccountService.Instance.SignedIn -= self.LoginCallBack;
        }

        public static void LoginCallBack(this LoginSDKComponent self)
        {
            self.UpdateAuthentication().Coroutine();
        }

        public static async ETTask UpdateAuthentication(this LoginSDKComponent self)
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

        public static void SetClientRecordAccountId(this LoginSDKComponent self, string accountId)
        {
            string key = "UnityPlayerId";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountId(this LoginSDKComponent self)
        {
            string key = "UnityPlayerId";
            return PlayerPrefs.GetString(key);
        }

        public static void SetClientRecordAccountLoginTime(this LoginSDKComponent self)
        {
            string key = "UnityPlayerLoginTime";
            PlayerPrefs.SetString(key, TimeHelper.ClientNowSeconds().ToString());
        }

        public static void SetClientRecordAccountLoginTimeNone(this LoginSDKComponent self)
        {
            string key = "UnityPlayerLoginTime";
            PlayerPrefs.SetString(key, "0");
        }

        public static long GetClientRecordAccountLoginTime(this LoginSDKComponent self)
        {
            string key = "UnityPlayerLoginTime";
            string value = PlayerPrefs.GetString(key);
            long lastLoginTime = long.Parse(value);
            return lastLoginTime;
        }

        public static void SetClientRecordAccountName(this LoginSDKComponent self, string accountId)
        {
            string key = "UnityPlayerName";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountName(this LoginSDKComponent self)
        {
            string key = "UnityPlayerName";
            return PlayerPrefs.GetString(key);
        }

        public static async ETTask Destroy(this LoginSDKComponent self)
        {
            self.UnRegisterLoginCallBack();
            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> ChkSDKLoginDone(this LoginSDKComponent self)
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

        public static async ETTask SDKLoginIn(this LoginSDKComponent self, Action finishCallBack)
        {
            self.finishCallBack = finishCallBack;

            if (!PlayerAccountService.Instance.IsSignedIn)
                await PlayerAccountService.Instance.StartSignInAsync();
            else
                _ = UpdateAuthentication(self);

        }

        public static async ETTask SDKLoginOut(this LoginSDKComponent self, bool needReLogin)
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