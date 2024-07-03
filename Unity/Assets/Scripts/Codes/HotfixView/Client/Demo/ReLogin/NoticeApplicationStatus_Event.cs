using System;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeApplicationStatus_Event: AEvent<Scene, EventType.NoticeApplicationStatus>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeApplicationStatus args)
        {
            await ET.Client.ReLoginComponent.Instance.ApplicationStatusChg(args.isPause);
            await ETTask.CompletedTask;
        }
    }
}