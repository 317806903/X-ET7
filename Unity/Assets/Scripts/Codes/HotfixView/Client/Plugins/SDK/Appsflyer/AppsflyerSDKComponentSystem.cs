using AppsFlyerSDK;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace ET.Client
{
    [FriendOf(typeof (AppsflyerSDKComponent))]
    public static class AppsflyerSDKComponentSystem
    {
        [ObjectSystem]
        public class AppsflyerSDKComponentAwakeSystem: AwakeSystem<AppsflyerSDKComponent>
        {

            protected override void Awake(AppsflyerSDKComponent self)
            {
                AppsflyerSDKComponent.Instance = self;
                if (self.AppsflyerSDKMono != null)
                {
                    return;
                }
                bool isNeedAppsflyer = ChannelSettingComponent.Instance.ChkIsNeedAppsflyer();
                if (isNeedAppsflyer == false)
                {
                    return;
                }
                AppsflyerSDKMono appsflyerSDKMono = new GameObject("AppsflyerSDKGO").AddComponent<AppsflyerSDKMono>();
                appsflyerSDKMono.gameObject.transform.SetParent(GlobalComponent.Instance.Global);
                self.AppsflyerSDKMono = appsflyerSDKMono;
            }
        }

        public class AppsflyerSDKMono: MonoBehaviour, IAppsFlyerConversionData
        {

            public void Start()
            {
                string appsflyerKey = ChannelSettingComponent.Instance.GetAppsflyerKey();
                string appsflyerAppID = ChannelSettingComponent.Instance.GetAppsflyerAppID();

                AppsFlyer.initSDK(appsflyerKey, appsflyerAppID, this);
#if UNITY_IOS
            AppsFlyer.waitForATTUserAuthorizationWithTimeoutInterval(60);
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus()
                == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
#endif
                AppsFlyer.startSDK();
            }

            public void onConversionDataSuccess(string conversionData)
            {
                AppsFlyer.AFLog("onConversionDataSuccess", conversionData);
                Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
            }

            public void onConversionDataFail(string error)
            {
                AppsFlyer.AFLog("onConversionDataFail", error);
            }

            public void onAppOpenAttribution(string attributionData)
            {
                AppsFlyer.AFLog("onAppOpenAttribution: This method was replaced by UDL. This is a fake call.", attributionData);
            }

            public void onAppOpenAttributionFailure(string error)
            {
                AppsFlyer.AFLog("onAppOpenAttributionFailure: This method was replaced by UDL. This is a fake call.", error);
            }

        }

        [ObjectSystem]
        public class AppsflyerSDKComponentDestroySystem: DestroySystem<AppsflyerSDKComponent>
        {
            protected override void Destroy(AppsflyerSDKComponent self)
            {
                self.Destroy().Coroutine();
                AppsflyerSDKComponent.Instance = null;
            }
        }

        private const string TutorialId = "new_user_tutorial";
        private const string TutorialName = "new_user_tutorial";

        public static void SendEvent_TutorialCompleted(this AppsflyerSDKComponent self, bool isTutorialCompleted, string tutorialId = TutorialId,
            string tutorialName = TutorialName)
        {
            Log.Info($"Send AppsFlyer event tutorial completed: {isTutorialCompleted}");
            Dictionary<string, string> eventParameters4 = new Dictionary<string, string>();
            eventParameters4.Add(AFInAppEvents.SUCCESS, isTutorialCompleted.ToString());
            eventParameters4.Add("af_tutorial_id", tutorialId);
            eventParameters4.Add("af_content", tutorialName);
            // Send af_tutorial_completion event.
            // Trigger: User completes the tutorial, or user starts the tutorial but quits without completing it
            AppsFlyer.sendEvent(AFInAppEvents.TUTORIAL_COMPLETION, eventParameters4);
        }

        public static async ETTask Destroy(this AppsflyerSDKComponent self)
        {
            await ETTask.CompletedTask;
        }
    }
}
