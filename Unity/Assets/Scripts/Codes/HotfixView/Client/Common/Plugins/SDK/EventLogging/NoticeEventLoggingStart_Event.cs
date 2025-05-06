using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLoggingStart_Event: AEvent<Scene, ClientEventType.NoticeEventLoggingStart>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeEventLoggingStart args)
        {
            string eventName = args.eventName;
            string timerKey = args.timerKey;
            if (string.IsNullOrEmpty(timerKey))
            {
                EventLoggingSDKComponent.Instance.StartEvent(eventName);
            }
            else
            {
                EventLoggingSDKComponent.Instance.StartEvent(eventName, timerKey);
            }

            Log.Debug($"EventLogging started {eventName} {timerKey}");
            await ETTask.CompletedTask;
        }
    }
}