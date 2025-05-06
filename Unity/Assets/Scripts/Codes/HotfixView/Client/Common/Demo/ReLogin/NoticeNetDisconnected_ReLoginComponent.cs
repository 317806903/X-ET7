using System;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeNetDisconnected_ReLoginComponent: AEvent<Scene, ClientEventType.NoticeNetDisconnected>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeNetDisconnected args)
        {
            Scene clientScene = scene.ClientScene();
            bool bReLogin = args.bReLogin;
            if (bReLogin)
            {
                await ET.Client.ReLoginComponent.Instance?.DoReLogin(true);
            }
            else
            {
                EventSystem.Instance.Publish(clientScene, new ClientEventType.NoticeUIReconnect());
            }
            await ETTask.CompletedTask;
        }
    }
}