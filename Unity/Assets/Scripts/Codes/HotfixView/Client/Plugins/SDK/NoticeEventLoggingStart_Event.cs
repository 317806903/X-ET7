using System;
using System.Collections.Generic;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLoggingStart_Event: AEvent<Scene, EventType.NoticeEventLoggingStart>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeEventLoggingStart args)
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

            await ETTask.CompletedTask;
        }
    }
}