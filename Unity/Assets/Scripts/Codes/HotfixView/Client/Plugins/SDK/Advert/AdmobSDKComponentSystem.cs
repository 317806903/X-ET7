using UnityEngine;
using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using System.Threading.Tasks;
using UnityEngine.Events;
using Application = UnityEngine.Device.Application;
using GoogleMobileAds.Mediation.UnityAds.Api;

namespace ET.Client
{
    [FriendOf(typeof(AdmobSDKComponent))]
    public static class AdmobSDKComponentSystem
    {
        [ObjectSystem]
        public class AdmobSDKComponentAwakeSystem : AwakeSystem<AdmobSDKComponent>
        {
            protected override void Awake(AdmobSDKComponent self)
            {
                AdmobSDKComponent.Instance = self;

                self.ForceSetAdmobAvailable();
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class AdmobSDKComponentDestroySystem : DestroySystem<AdmobSDKComponent>
        {
            protected override void Destroy(AdmobSDKComponent self)
            {
                self.Destroy().Coroutine();
                AdmobSDKComponent.Instance = null;
            }
        }

        public static void SetIsAdmobAvailable(this AdmobSDKComponent self, bool isAdmobAvailable)
        {
            self.isAdmobAvailable = isAdmobAvailable;
        }

        public static void ForceSetAdmobAvailable(this AdmobSDKComponent self)
        {
            self.isAdmobAvailable = true;
            self.isShowRewardedAding = false;
        }

        public static async ETTask Awake(this AdmobSDKComponent self)
        {
            if (self.isAdmobAvailable == false)
            {
                return;
            }

#if UNITY_ANDROID
            self.rewardedAdUnitId = "ca-app-pub-6514784270434577/2970547189";
#elif UNITY_IPHONE
            self.rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            self.rewardedAdUnitId = "unused";
#endif
            UnityAds.SetConsentMetaData("privacy.consent", true);
            self.retryCount = 3;
            MobileAds.Initialize(initStatus => {self.LoadRewardedAd(); });
            await ETTask.CompletedTask;
        }

        public static async ETTask Destroy(this AdmobSDKComponent self)
        {
            self.DestroyRewardedAd();
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// Loads the ad.
        /// </summary>
        private static void LoadRewardedAd(this AdmobSDKComponent self)
        {
            // Clean up the old ad before loading a new one.
            if (self.rewardedAd != null)
            {
                self.DestroyRewardedAd();
            }

            Log.Info("Loading rewarded ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest();

            // Send the request to load the ad.
            RewardedAd.Load(self.rewardedAdUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                // If the operation failed with a reason.
                if (error != null)
                {
                    Log.Error("Rewarded ad failed to load an ad with error : " + error);
                    if(self.retryCount > 0){
                        self.retryCount--;
                        self.LoadRewardedAd();
                    }
                    return;
                }
                // If the operation failed for unknown reasons.
                // This is an unexpected error, please report this bug if it happens.
                if (ad == null)
                {
                    Log.Error("Unexpected error: Rewarded load event fired with null ad and null error.");
                    if(self.retryCount > 0){
                        self.retryCount--;
                        self.LoadRewardedAd();
                    }
                    return;
                }

                // The operation completed successfully.
                Log.Info("Rewarded ad loaded with response : " + ad.GetResponseInfo());
                self.retryCount = 0;
                self.rewardedAd = ad;

                // Register to ad events to extend functionality.
                self.RegisterEventHandlers(ad);
            });
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public static async ETTask ShowRewardedAd(this AdmobSDKComponent self, Action rewardCallback, Action failCallback = null)
        {
            if (self.isAdmobAvailable == false)
            {
                return;
            }

            if (self.isShowRewardedAding)
            {
                return;
            }

            self.isShowRewardedAding = true;

            if (Application.isMobilePlatform == false)
            {
                UIManagerHelper.ShowTip(self.DomainScene(), "Editor rewarded ad.");
                rewardCallback();

                await TimerComponent.Instance.WaitAsync(500);
                self.isShowRewardedAding = false;

                return;
            }

            if (self.rewardedAd != null && self.rewardedAd.CanShowAd())
            {

            }
            else
            {
                self.LoadRewardedAd();

                int waitNum = 90;
                while (true)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    waitNum--;
                    if (waitNum <= 0)
                    {
                        break;
                    }
                    if (self.rewardedAd != null && self.rewardedAd.CanShowAd())
                    {
                        break;
                    }
                }
            }

            if (self.rewardedAd != null && self.rewardedAd.CanShowAd())
            {
                Log.Info("Showing rewarded ad.");
                // await UIManagerHelper.GetUIComponent(self.DomainScene()).ShowWindowAsync<DlgCommonLoading>();
                // await TimerComponent.Instance.WaitAsync(1000);

                self.rewardedAd.Show((Reward reward) =>
                {
                    self.FinishRewardedAd(rewardCallback).Coroutine();
                });
            }
            else
            {
                await self.FailRewardedAd(failCallback);
            }
        }

        public static async ETTask FinishRewardedAd(this AdmobSDKComponent self, Action rewardCallback)
        {
            Log.Info(String.Format("Rewarded ad granted a reward"));
            await TimerComponent.Instance.WaitAsync(1000);
            if(rewardCallback != null){
                rewardCallback();
            }
            await TimerComponent.Instance.WaitAsync(5000);
            self.isShowRewardedAding = false;
            await ETTask.CompletedTask;
        }

        public static async ETTask FailRewardedAd(this AdmobSDKComponent self, Action failCallback)
        {
            Log.Info(String.Format("Rewarded ad granted a reward without watching"));
            await TimerComponent.Instance.WaitAsync(1000);
            if(failCallback != null){
                failCallback();
            }
            await TimerComponent.Instance.WaitAsync(5000);
            self.isShowRewardedAding = false;
            self.retryCount = 3;
            self.LoadRewardedAd();
            await ETTask.CompletedTask;
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public static void DestroyRewardedAd(this AdmobSDKComponent self)
        {
            if (self.rewardedAd != null)
            {
                Log.Info("Destroying rewarded ad.");
                self.rewardedAd.Destroy();
                self.rewardedAd = null;
            }
        }

        /// <summary>
        /// Logs the ResponseInfo.
        /// </summary>
        public static void LogResponseInfo(this AdmobSDKComponent self)
        {
            if (self.rewardedAd != null)
            {
                var responseInfo = self.rewardedAd.GetResponseInfo();
                Log.Info($"LogResponseInfo: {responseInfo}");
            }
        }

        private static void RegisterEventHandlers(this AdmobSDKComponent self, RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Log.Info(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Log.Info("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Log.Info("Rewarded ad was clicked.");
            };
            // Raised when the ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Log.Info("Rewarded ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Log.Info("Rewarded ad full screen content closed.");
                self.retryCount = 3;
                self.LoadRewardedAd();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Log.Error("Rewarded ad failed to open full screen content with error : "
                    + error);
                self.retryCount = 3;
                self.LoadRewardedAd();
            };
        }
    }
}