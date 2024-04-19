using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Events;
using Application = UnityEngine.Device.Application;
using AppsFlyerSDK;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace ET.Client
{
    [FriendOf(typeof(AppsflyerSDKComponent))]
    public static class AppsflyerSDKComponentSystem
    {
        [ObjectSystem]
        public class AppsflyerSDKComponentAwakeSystem : AwakeSystem<AppsflyerSDKComponent>, IAppsFlyerConversionData
        {
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

            protected override void Awake(AppsflyerSDKComponent self)
            {
                AppsflyerSDKComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class AppsflyerSDKComponentDestroySystem : DestroySystem<AppsflyerSDKComponent>
        {
            protected override void Destroy(AppsflyerSDKComponent self)
            {
                self.Destroy().Coroutine();
                AppsflyerSDKComponent.Instance = null;
            }
        }

        public static async ETTask Awake(this AppsflyerSDKComponent self)
        {
            if (ResConfig.Instance.Channel == "10001")
			{
				AppsFlyer.initSDK("Mv3t3nzveDt2q4mJCd5rLD", "com.dm.realityguard");
                AppsFlyer.startSDK();
			}
			else if (ResConfig.Instance.Channel == "10002")
			{
				AppsFlyer.initSDK("Mv3t3nzveDt2q4mJCd5rLD", "id6474414179");
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
            await ETTask.CompletedTask;
        }

        public static async ETTask Destroy(this AppsflyerSDKComponent self)
        {
            await ETTask.CompletedTask;
        }
    }
}