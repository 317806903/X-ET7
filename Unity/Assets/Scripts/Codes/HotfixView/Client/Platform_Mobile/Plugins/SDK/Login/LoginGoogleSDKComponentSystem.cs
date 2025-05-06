using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_ANDROID
using Google;
#endif
using System.Threading.Tasks;

namespace ET.Client
{
    [FriendOf(typeof(LoginGoogleSDKComponent))]
    public static class LoginGoogleSDKComponentSystem
    {
        [ObjectSystem]
        public class LoginGoogleSDKComponentAwakeSystem : AwakeSystem<LoginGoogleSDKComponent>
        {
            protected override void Awake(LoginGoogleSDKComponent self)
            {
                self.loginType = LoginType.GoogleSDK;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class LoginGoogleSDKComponentDestroySystem : DestroySystem<LoginGoogleSDKComponent>
        {
            protected override void Destroy(LoginGoogleSDKComponent self)
            {
                self.Destroy().Coroutine();
            }
        }

        public static async ETTask Awake(this LoginGoogleSDKComponent self)
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

        public static void Initialize(this LoginGoogleSDKComponent self)
        {
#if UNITY_ANDROID
            self.configuration = new GoogleSignInConfiguration
            {
                WebClientId = "168847219175-1l44u59t2cb5rr3q44bsd2fk1nk2p2ij.apps.googleusercontent.com",
                RequestIdToken = true,
                RequestEmail = true
            };
#endif
        }

        public static void RegisterLoginCallBack(this LoginGoogleSDKComponent self)
        {
        }

        public static void UnRegisterLoginCallBack(this LoginGoogleSDKComponent self)
        {
        }

        public static void LoginCallBack(this LoginGoogleSDKComponent self)
        {
        }

        public static void SetClientRecordAccountId(this LoginGoogleSDKComponent self, string accountId)
        {
            string key = $"GooglePlayerId_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountId(this LoginGoogleSDKComponent self)
        {
            string key = $"GooglePlayerId_{self.loginType.ToString()}";
            return PlayerPrefs.GetString(key);
        }

        public static void SetClientRecordAccountLoginTime(this LoginGoogleSDKComponent self)
        {
            string key = $"GooglePlayerLoginTime_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, TimeHelper.ClientNowSeconds().ToString());
        }

        public static void SetClientRecordAccountLoginTimeNone(this LoginGoogleSDKComponent self)
        {
            string key = $"GooglePlayerLoginTime_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, "0");
        }

        public static long GetClientRecordAccountLoginTime(this LoginGoogleSDKComponent self)
        {
            string key = $"GooglePlayerLoginTime_{self.loginType.ToString()}";
            string value = PlayerPrefs.GetString(key);
            long lastLoginTime = long.Parse(value);
            return lastLoginTime;
        }

        public static void SetClientRecordAccountName(this LoginGoogleSDKComponent self, string accountId)
        {
            string key = $"GooglePlayerName_{self.loginType.ToString()}";
            PlayerPrefs.SetString(key, accountId);
        }

        public static string GetClientRecordAccountName(this LoginGoogleSDKComponent self)
        {
            string key = $"GooglePlayerName_{self.loginType.ToString()}";
            return PlayerPrefs.GetString(key);
        }

        public static async ETTask Destroy(this LoginGoogleSDKComponent self)
        {
            self.UnRegisterLoginCallBack();
            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> ChkSDKLoginDone(this LoginGoogleSDKComponent self)
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

        public static string GetSDKToken(this LoginGoogleSDKComponent self)
        {
            return self.Token;
        }

        public static string GetSDKEmail(this LoginGoogleSDKComponent self)
        {
            return self.Email;
        }

        public static async ETTask SDKLoginIn(this LoginGoogleSDKComponent self, Action finishCallBack, Action failCallBack)
        {
            // await self.SDKLoginOut(true);
            self.finishCallBack = finishCallBack;
            self.failCallBack = failCallBack;
#if UNITY_ANDROID
            GoogleSignIn.Configuration = self.configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            Log.Info("Calling SignIn");

            await GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
              self.OnAuthenticationFinished);
            // Social.localUser.Authenticate(self.OnGoogleLogin);
#endif
        }

#if UNITY_ANDROID
        public static void OnAuthenticationFinished(this LoginGoogleSDKComponent self, Task<GoogleSignInUser> task)
        {
            try{
                if (task.IsFaulted)
                {
                    using (IEnumerator<System.Exception> enumerator =
                            task.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            GoogleSignIn.SignInException error =
                                (GoogleSignIn.SignInException)enumerator.Current;
                            Log.Info("Got Error Status: " + error.Status);
                            Log.Info("Got Error Message: " + error.Message);
                        }
                        else
                        {
                            Log.Info("Got Unexpected Exception?!?" + task.Exception);
                        }
                    }
                    self.failCallBack?.Invoke();
                }
                else if (task.IsCanceled)
                {
                    Log.Info("Canceled");
                    self.failCallBack?.Invoke();
                }
                else
                {
                    Log.Info("Welcome: " + task.Result.DisplayName + "!");
                    self.Email = task.Result.Email;
                    self.Token = task.Result.IdToken;
                    self.UpdateAuthentication(task.Result.UserId, task.Result.DisplayName);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
#endif

        public static async ETTask SDKLoginOut(this LoginGoogleSDKComponent self, bool needReLogin)
        {
#if UNITY_ANDROID
            GoogleSignIn.DefaultInstance.SignOut();
#endif
        }

        public static void UpdateAuthentication(this LoginGoogleSDKComponent self, string accountId, string accountName)
        {
            self.SetClientRecordAccountId($"GooglePlayer_{accountId}");
            self.SetClientRecordAccountName(accountName);

            self.finishCallBack?.Invoke();
        }
    }
}