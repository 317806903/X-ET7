using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeEventLoggingLoginIn_Event: AEvent<Scene, EventType.NoticeEventLoggingLoginIn>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeEventLoggingLoginIn args)
        {
            await EventLoggingSDKComponent.Instance.SDKLoginIn(args.playerId);
            await ETTask.CompletedTask;
        }
    }
}