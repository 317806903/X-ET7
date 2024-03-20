using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET.Client
{
    [FriendOf(typeof(LoginSDKManagerComponent))]
    public static class LoginSDKManagerComponentSystem
    {
        [ObjectSystem]
        public class LoginSDKManagerComponentAwakeSystem : AwakeSystem<LoginSDKManagerComponent>
        {
            protected override void Awake(LoginSDKManagerComponent self)
            {
                LoginSDKManagerComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class LoginSDKManagerComponentDestroySystem : DestroySystem<LoginSDKManagerComponent>
        {
            protected override void Destroy(LoginSDKManagerComponent self)
            {
                self.Destroy().Coroutine();
                if (LoginSDKManagerComponent.Instance == self)
                {
                    LoginSDKManagerComponent.Instance = null;
                }
            }
        }

        public static async ETTask Awake(this LoginSDKManagerComponent self)
        {
            try
            {
                await self.Init();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static LoginType GetLoginType(this LoginSDKManagerComponent self)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            return LoginType.UnitySDK;
#elif UNITY_ANDROID
            return LoginType.GoogleSDK;
#elif UNITY_IOS
            return LoginType.AppleSDK;
#else
            return LoginType.UnitySDK;
#endif
        }

        public static async ETTask Init(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {            
                self.AddComponent<LoginUnitySDKComponent>();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                self.AddComponent<LoginGoogleSDKComponent>();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                self.AddComponent<LoginAppleSDKComponent>();
            }
            await ETTask.CompletedTask;
        }

        public static void SetClientRecordAccountId(this LoginSDKManagerComponent self, string accountId)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                self.GetComponent<LoginUnitySDKComponent>()?.SetClientRecordAccountId(accountId);
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                self.GetComponent<LoginGoogleSDKComponent>()?.SetClientRecordAccountId(accountId);
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                self.GetComponent<LoginAppleSDKComponent>()?.SetClientRecordAccountId(accountId);
            }
        }

        public static string GetClientRecordAccountId(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                return self.GetComponent<LoginUnitySDKComponent>()?.GetClientRecordAccountId();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                return self.GetComponent<LoginGoogleSDKComponent>()?.GetClientRecordAccountId();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                return self.GetComponent<LoginAppleSDKComponent>()?.GetClientRecordAccountId();
            }
            return "";
        }

        public static void SetClientRecordAccountLoginTime(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                self.GetComponent<LoginUnitySDKComponent>()?.SetClientRecordAccountLoginTime();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                self.GetComponent<LoginGoogleSDKComponent>()?.SetClientRecordAccountLoginTime();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                self.GetComponent<LoginAppleSDKComponent>()?.SetClientRecordAccountLoginTime();
            }
        }

        public static void SetClientRecordAccountLoginTimeNone(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                self.GetComponent<LoginUnitySDKComponent>()?.SetClientRecordAccountLoginTimeNone();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                self.GetComponent<LoginGoogleSDKComponent>()?.SetClientRecordAccountLoginTimeNone();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                self.GetComponent<LoginAppleSDKComponent>()?.SetClientRecordAccountLoginTimeNone();
            }
        }

        public static long GetClientRecordAccountLoginTime(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                return self.GetComponent<LoginUnitySDKComponent>().GetClientRecordAccountLoginTime();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                return self.GetComponent<LoginGoogleSDKComponent>().GetClientRecordAccountLoginTime();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                return self.GetComponent<LoginAppleSDKComponent>().GetClientRecordAccountLoginTime();
            }
            return 0;
        }

        public static string GetClientRecordAccountName(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                return self.GetComponent<LoginUnitySDKComponent>().GetClientRecordAccountName();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                return self.GetComponent<LoginGoogleSDKComponent>().GetClientRecordAccountName();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                return self.GetComponent<LoginAppleSDKComponent>().GetClientRecordAccountName();
            }
            return "";
        }

        public static async ETTask Destroy(this LoginSDKManagerComponent self)
        {
            await ETTask.CompletedTask;
        }

        public static async ETTask<bool> ChkSDKLoginDone(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                return await self.GetComponent<LoginUnitySDKComponent>().ChkSDKLoginDone();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                return await self.GetComponent<LoginGoogleSDKComponent>().ChkSDKLoginDone();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                return await self.GetComponent<LoginAppleSDKComponent>().ChkSDKLoginDone();
            }
            return false;
        }

        public static string GetSDKToken(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                return self.GetComponent<LoginUnitySDKComponent>().GetSDKToken();
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                return self.GetComponent<LoginGoogleSDKComponent>().GetSDKToken();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                return self.GetComponent<LoginAppleSDKComponent>().GetSDKToken();
            }
            return "";
        }

        public static string GetSDKEmail(this LoginSDKManagerComponent self)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                return "";
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                return self.GetComponent<LoginGoogleSDKComponent>().GetSDKEmail();
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                return self.GetComponent<LoginAppleSDKComponent>().GetSDKEmail();
            }
            return "";
        }

        public static async ETTask SDKLoginIn(this LoginSDKManagerComponent self, Action finishCallBack, Action failCallBack = null)
        {
            self.finishCallBack = finishCallBack;
            self.failCallBack = failCallBack;
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                await self.GetComponent<LoginUnitySDKComponent>().SDKLoginIn(self.finishCallBack);
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                await self.GetComponent<LoginGoogleSDKComponent>().SDKLoginIn(self.finishCallBack, self.failCallBack);
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                await self.GetComponent<LoginAppleSDKComponent>().SDKLoginIn(self.finishCallBack, self.failCallBack);
            }else{
            }
        }

        public static async ETTask SDKLoginOut(this LoginSDKManagerComponent self, bool needReLogin)
        {
            if (self.GetLoginType() == LoginType.UnitySDK)
            {
                await self.GetComponent<LoginUnitySDKComponent>().SDKLoginOut(needReLogin);
            }
            else if (self.GetLoginType() == LoginType.GoogleSDK)
            {
                await self.GetComponent<LoginGoogleSDKComponent>().SDKLoginOut(needReLogin);
            }
            else if (self.GetLoginType() == LoginType.AppleSDK)
            {
                await self.GetComponent<LoginAppleSDKComponent>().SDKLoginOut(needReLogin);
            }
        }

    }
}