using System;
using System.Collections.Generic;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLoggingSetUserProperties_Event: AEvent<Scene, EventType.NoticeEventLoggingSetUserProperties>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeEventLoggingSetUserProperties args)
        {
            Dictionary<string, object> properties = args.properties;
            EventLoggingSDKComponent.Instance.SetUserProperties(properties);
            await ETTask.CompletedTask;
        }
    }
}