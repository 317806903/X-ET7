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
                if (EventLoggingSDKComponent.Instance == self)
                {
                    EventLoggingSDKComponent.Instance = null;
                }
            }
        }

        public static async ETTask Awake(this EventLoggingSDKComponent self)
        {
            self.IsOpenEventLogging = ResConfig.Instance.IsNeedSendEventLog;

            self.serverURL = ChannelSettingComponent.Instance.GetEventLogURL();
            self.appID = ChannelSettingComponent.Instance.GetEventLogKey();

            self.enableLog = false;
            self.enableAutoTrack = true;

            if (self.IsOpenEventLogging)
            {
                EventLoggingAPI.StartEventLogging(self.serverURL, self.appID);
                EventLoggingAPI.EnableLog(self.enableLog, self.appID);
                EventLoggingAPI.EnableAutoTrack(self.enableAutoTrack);
            }
            await ETTask.CompletedTask;
        }

        public static async ETTask Destroy(this EventLoggingSDKComponent self)
        {

            await ETTask.CompletedTask;
        }

        public static async ETTask SDKLoginIn(this EventLoggingSDKComponent self, long playerId)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
            Dm.EventLogging.EventLoggingAPI.Login(playerId.ToString(), self.appID);
        }

        public static async ETTask SDKLoginOut(this EventLoggingSDKComponent self)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
            Dm.EventLogging.EventLoggingAPI.Logout(self.appID);
        }

        public static void StartEvent(this EventLoggingSDKComponent self, string eventName)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
            try
            {
                try
                {
                    EventLoggingAPI.StartEvent(eventName, self.appID);
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

        public static void StartEvent(this EventLoggingSDKComponent self, string eventName, string timerKey)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
            try
            {
                try
                {
                    EventLoggingAPI.StartEvent(eventName, timerKey, self.appID);
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

        public static void Track(this EventLoggingSDKComponent self, string eventName, Dictionary<string, object> properties)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
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
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
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

        public static void SetCommonProperties(this EventLoggingSDKComponent self, Dictionary<string, object> properties)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
            try
            {
                try
                {
                    EventLoggingAPI.SetCommonProperties(properties, self.appID);
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

        public static void SetUserProperties(this EventLoggingSDKComponent self, Dictionary<string, object> properties)
        {
            if (self.IsOpenEventLogging == false)
            {
                return;
            }
            try
            {
                try
                {
                    EventLoggingAPI.SetUserProperties(properties, self.appID);
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