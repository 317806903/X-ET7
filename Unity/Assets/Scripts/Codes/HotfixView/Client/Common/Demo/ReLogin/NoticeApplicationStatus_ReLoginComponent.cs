using System;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client|SceneType.Process)]
    public class NoticeApplicationStatus_ReLoginComponent: AEvent<Scene, ClientEventType.NoticeApplicationStatus>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeApplicationStatus args)
        {
            ET.Client.ReLoginComponent.Instance?.ApplicationStatusChg(args.isPause).Coroutine();
            await ETTask.CompletedTask;
        }
    }
}