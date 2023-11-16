using UnityEngine;
using System;
using System.Collections.Generic;
using Dm.EventLogging;
using Unity.Services.PlayerAccounts;
using Unity.Services.Authentication;
using Unity.Services.Core;

namespace ET.Client
{
    [FriendOf(typeof(EventLoggingSDKComponent))]
    public static class EventLoggingSDKComponentSystem
    {
        [ObjectSystem]
        public class EventLoggingSDKComponentAwakeSystem : AwakeSystem<EventLoggingSDKComponent>
        {
            protected override void Awake(EventLoggingSDKComponent self)
            {
                EventLoggingSDKComponent.Instance = self;
                self.Awake().Coroutine();
            }
        }

        [ObjectSystem]
        public class EventLoggingSDKComponentDestroySystem : DestroySystem<EventLoggingSDKComponent>
        {
            protected override void Destroy(EventLoggingSDKComponent self)
            {
                self.Destroy().Coroutine();
                EventLoggingSDKComponent.Instance = null;
            }
        }

        public static async ETTask Awake(this EventLoggingSDKComponent self)
        {
            self.serverURL = "https://dev-event.deepmirror.com.cn";
            self.appID = "e6a386f285574d80a1402d7c1bd4e42e";
            self.enableLog = false;
            self.enableAutoTrack = false;

            EventLoggingAPI.StartEventLogging(self.serverURL, self.appID);
            EventLoggingAPI.EnableLog(self.enableLog, self.appID);
            EventLoggingAPI.EnableAutoTrack(self.enableAutoTrack);
            await ETTask.CompletedTask;
        }

        public static async ETTask Destroy(this EventLoggingSDKComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask SDKLoginIn(this EventLoggingSDKComponent self)
        {
        }

        public static async ETTask SDKLoginOut(this EventLoggingSDKComponent self)
        {
        }

        public static void Track(this EventLoggingSDKComponent self, string eventName, Dictionary<string, object> properties)
        {
            try
            {
                try
                {
                    EventLoggingAPI.Track(eventName, properties, self.appID);
                }
                finally
                {

                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Track(this EventLoggingSDKComponent self, string eventName, Dictionary<string, object> properties, string timerKey)
        {
            try
            {
                try
                {
                    EventLoggingAPI.Track(eventName, properties, timerKey, self.appID);
                }
                finally
                {

                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

    }
}