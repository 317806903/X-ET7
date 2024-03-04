using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_IOS
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Interfaces;
using AppleAuth.Native;
#endif
using System.Text;

namespace ET.Client
{
    [FriendOf(typeof(LoginAppleSDKComponent))]
    public static class LoginAppleSDKComponentSystem
    {
        [ObjectSystem]
        public class LoginAppleSDKComponentAwakeSystem : AwakeSystem<LoginAppleSDKComponent>
        {
            protected override void Awake(LoginAppleSDKComponent self)
            {
                self.loginType = LoginType.AppleSDK;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class LoginAppleSDKComponentDestroySystem : DestroySystem<LoginAppleSDKComponent>
        {
            protected override void Destroy(LoginAppleSDKComponent self)
            {
                self.Destroy().Coroutine();
            }
        }

        [ObjectSystem]
        public class LoginAppleSDKComponentUpdateSystem : UpdateSystem<LoginAppleSDKComponent>
        {
            protected override void Update(LoginAppleSDKComponent self)
            {
#if UNITY_IOS
                if(self.m_AppleAuthManager != null){
                    self.m_AppleAuthManager.Update();
                }
#endif
            }
        }

        public static async ETTask Awake(this LoginAppleSDKComponent self)
        {
            try
            {
                self.Initialize();
                self.RegisterLoginCallBack();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Initialize(this LoginAppleSDKComponent self)
        {
#if UNITY_IOS
            var deserializer = new PayloadDeserializer();
            self.m_AppleAuthManager = new AppleAuthManager(deserializer);
#endif
        }

        public static void RegisterLoginCallBack(this LoginAppleSDKComponent self)
        {
        }

        public static void UnRegisterLoginCallBack(this LoginAppleSDKComponent self)
        {
        }

        public static void LoginCallBack(this LoginAppleSDKComponent self)
        {
        }

        public static void SetClientRecordAccountId(this LoginAppleSDKComponent self, string accountId)
        {
            string key = $"ApplePlayerId_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountId(this LoginAppleSDKComponent self)
        {
            string key = $"ApplePlayerId_{self.loginType.ToString()}";
            return PlayerPrefs.GetString(key);
        }

        public static void SetClientRecordAccountLoginTime(this LoginAppleSDKComponent self)
        {
            string key = $"ApplePlayerLoginTime_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, TimeHelper.ClientNowSeconds().ToString());
        }

        public static void SetClientRecordAccountLoginTimeNone(this LoginAppleSDKComponent self)
        {
            string key = $"ApplePlayerLoginTime_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, "0");
        }

        public static long GetClientRecordAccountLoginTime(this LoginAppleSDKComponent self)
        {
            string key = $"ApplePlayerLoginTime_{self.loginType.ToString()}";
            string value = PlayerPrefs.GetString(key);
            long lastLoginTime = long.Parse(value);
            return lastLoginTime;
        }

        public static void SetClientRecordAccountName(this LoginAppleSDKComponent self, string accountId)
        {
            string key = $"ApplePlayerName_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountName(this LoginAppleSDKComponent self)
        {
            string key = $"ApplePlayerName_{self.loginType.ToString()}";
            return PlayerPrefs.GetString(key);
        }

        public static async ETTask Destroy(this LoginAppleSDKComponent self)
        {
            self.UnRegisterLoginCallBack();
            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> ChkSDKLoginDone(this LoginAppleSDKComponent self)
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

        public static string GetSDKToken(this LoginAppleSDKComponent self)
        {
            return self.Token;
        }

        public static string GetSDKEmail(this LoginAppleSDKComponent self)
        {
            return self.Email;
        }

        public static async ETTask SDKLoginIn(this LoginAppleSDKComponent self, Action finishCallBack)
        {
            self.finishCallBack = finishCallBack;
#if UNITY_IOS
            // Initialize the Apple Auth Manager
            if (self.m_AppleAuthManager == null)
            {
                self.Initialize();
            }

            // Set the login arguments
            var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

            // Perform the login
            self.m_AppleAuthManager.LoginWithAppleId(
                loginArgs,
                credential =>
                {
                    var appleIDCredential = credential as IAppleIDCredential;
                    if (appleIDCredential != null)
                    {
                        var idToken = Encoding.UTF8.GetString(
                            appleIDCredential.IdentityToken,
                            0,
                            appleIDCredential.IdentityToken.Length);
                        self.Token = idToken;
                        self.Email = appleIDCredential.Email;
                        Log.Info("Sign-in with Apple successfully done.  User: " + appleIDCredential.User);
                        if(appleIDCredential.FullName != null)
                        {
                            self.UpdateAuthentication(appleIDCredential.User, appleIDCredential.FullName.GivenName);
                        }
                        else
                        {
                            self.UpdateAuthentication(appleIDCredential.User, "");
                        }
                    }
                    else
                    {
                        Log.Info("Sign-in with Apple error. Message: appleIDCredential is null");
                        self.Error = "Retrieving Apple Id Token failed.";
                    }
                },
                error =>
                {
                    Log.Info("Sign-in with Apple error. Message: " + error);
                    self.Error = "Retrieving Apple Id Token failed.";
                }
            );
#endif
        }

        public static async ETTask SDKLoginOut(this LoginAppleSDKComponent self, bool needReLogin)
        {

        }

        public static void UpdateAuthentication(this LoginAppleSDKComponent self, string accountId, string accountName)
        {
            self.SetClientRecordAccountId($"ApplePlayer_{accountId}");
            if (!string.IsNullOrEmpty(accountName)){
                self.SetClientRecordAccountName(accountName);
            }

            self.finishCallBack?.Invoke();
        }
    }
}