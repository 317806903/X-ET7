using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLoggingSetCommonProperties_Event: AEvent<Scene, ClientEventType.NoticeEventLoggingSetCommonProperties>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeEventLoggingSetCommonProperties args)
        {
            Dictionary<string, object> properties = args.properties;
            EventLoggingSDKComponent.Instance.SetCommonProperties(properties);
            await ETTask.CompletedTask;
        }
    }
}