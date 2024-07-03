using System;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeNetDisconnected_Event: AEvent<Scene, EventType.NoticeNetDisconnected>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeNetDisconnected args)
        {
            Scene clientScene = scene.ClientScene();
            bool bReLogin = args.bReLogin;
            if (bReLogin)
            {
                await ET.Client.ReLoginComponent.Instance?.DoReLogin(true);
            }
            else
            {
                EventSystem.Instance.Publish(clientScene, new EventType.NoticeUIReconnect());
            }
            await ETTask.CompletedTask;
        }
    }
}