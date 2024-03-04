using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AdmobSDKComponent : Entity,IAwake,IDestroy
    {
        [StaticField]
        public static AdmobSDKComponent Instance;

        public bool isAdmobAvailable = false;

        //激励视频
        public int retryCount;
        public string rewardedAdUnitId;
        public RewardedAd rewardedAd;
        public bool isShowRewardedAding;
    }
}