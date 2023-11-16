using System;
using System.Collections.Generic;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLogging_Event: AEvent<Scene, EventType.NoticeEventLogging>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeEventLogging args)
        {
            string eventName = args.eventName;
            Dictionary<string, object> properties = args.properties;
            string timerKey = args.timerKey;
            if (string.IsNullOrEmpty(timerKey))
            {
                EventLoggingSDKComponent.Instance.Track(eventName, properties);
            }
            else
            {
                EventLoggingSDKComponent.Instance.Track(eventName, properties, timerKey);
            }

            await ETTask.CompletedTask;
        }
    }
}