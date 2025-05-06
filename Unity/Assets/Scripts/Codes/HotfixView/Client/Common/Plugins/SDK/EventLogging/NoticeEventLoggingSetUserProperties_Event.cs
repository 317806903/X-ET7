using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLoggingSetUserProperties_Event: AEvent<Scene, ClientEventType.NoticeEventLoggingSetUserProperties>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeEventLoggingSetUserProperties args)
        {
            Dictionary<string, object> properties = args.properties;
            EventLoggingSDKComponent.Instance.SetUserProperties(properties);
            await ETTask.CompletedTask;
        }
    }
}