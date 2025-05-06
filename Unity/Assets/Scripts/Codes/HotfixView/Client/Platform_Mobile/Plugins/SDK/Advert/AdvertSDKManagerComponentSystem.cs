using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET.Client
{
    [FriendOf(typeof(AdvertSDKManagerComponent))]
    public static class AdvertSDKManagerComponentSystem
    {
        [ObjectSystem]
        public class AdvertSDKManagerComponentAwakeSystem : AwakeSystem<AdvertSDKManagerComponent>
        {
            protected override void Awake(AdvertSDKManagerComponent self)
            {
                AdvertSDKManagerComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class AdvertSDKManagerComponentDestroySystem : DestroySystem<AdvertSDKManagerComponent>
        {
            protected override void Destroy(AdvertSDKManagerComponent self)
            {
                self.Destroy().Coroutine();
                if (AdvertSDKManagerComponent.Instance == self)
                {
                    AdvertSDKManagerComponent.Instance = null;
                }
            }
        }

        public static async ETTask Awake(this AdvertSDKManagerComponent self)
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

        public static async ETTask Destroy(this AdvertSDKManagerComponent self)
        {
            try
            {
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask Init(this AdvertSDKManagerComponent self)
        {
            self.isAvailable = true;
            self.AddComponent<AdmobSDKComponent>();
            await ETTask.CompletedTask;
        }

        public static void SetIsAvailable(this AdvertSDKManagerComponent self, bool IsAvailable)
        {
            self.isAvailable = IsAvailable;
        }

        public static async ETTask ShowRewardedAd(this AdvertSDKManagerComponent self, Action rewardCallback, Action failCallback = null)
        {
            if (self.isAvailable == false)
            {
                return;
            }
            AdmobSDKComponent admobSDKComponent = self.GetComponent<AdmobSDKComponent>();
            if (admobSDKComponent == null)
            {
                return;
            }
            await admobSDKComponent.ShowRewardedAd(rewardCallback, failCallback);
        }

    }
}