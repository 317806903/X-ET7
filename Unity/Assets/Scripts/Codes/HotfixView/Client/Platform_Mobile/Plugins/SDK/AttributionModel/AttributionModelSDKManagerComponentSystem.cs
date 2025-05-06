using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET.Client
{
    [FriendOf(typeof(AttributionModelSDKManagerComponent))]
    public static class AttributionModelSDKManagerComponentSystem
    {
        [ObjectSystem]
        public class AttributionModelSDKManagerComponentAwakeSystem : AwakeSystem<AttributionModelSDKManagerComponent>
        {
            protected override void Awake(AttributionModelSDKManagerComponent self)
            {
                AttributionModelSDKManagerComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class AttributionModelSDKManagerComponentDestroySystem : DestroySystem<AttributionModelSDKManagerComponent>
        {
            protected override void Destroy(AttributionModelSDKManagerComponent self)
            {
                self.Destroy().Coroutine();
                if (AttributionModelSDKManagerComponent.Instance == self)
                {
                    AttributionModelSDKManagerComponent.Instance = null;
                }
            }
        }

        public static async ETTask Awake(this AttributionModelSDKManagerComponent self)
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

        public static async ETTask Destroy(this AttributionModelSDKManagerComponent self)
        {
            try
            {
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETTask Init(this AttributionModelSDKManagerComponent self)
        {
            self.isAvailable = true;
            self.AddComponent<AppsflyerSDKComponent>();
            await ETTask.CompletedTask;
        }

        public static void SetIsAvailable(this AttributionModelSDKManagerComponent self, bool IsAvailable)
        {
            self.isAvailable = IsAvailable;
        }

        public static void SendEvent_TutorialCompleted(this AttributionModelSDKManagerComponent self, bool isTutorialCompleted, string tutorialId, string tutorialName)
        {
            if (self.isAvailable == false)
            {
                return;
            }
            AppsflyerSDKComponent appsflyerSDKComponent = self.GetComponent<AppsflyerSDKComponent>();
            if (appsflyerSDKComponent == null)
            {
                return;
            }

            appsflyerSDKComponent.SendEvent_TutorialCompleted(isTutorialCompleted, tutorialId, tutorialName);
        }

    }
}