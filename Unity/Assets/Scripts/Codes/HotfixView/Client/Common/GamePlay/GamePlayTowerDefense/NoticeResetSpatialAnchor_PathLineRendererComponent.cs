using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeResetSpatialAnchor_PathLineRendererComponent: AEvent<Scene, ClientEventType.NoticeResetSpatialAnchor>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeResetSpatialAnchor args)
        {
            await ETTask.CompletedTask;
            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(scene);
            if (gamePlayComponent == null)
            {
                return;
            }

            ET.Client.PathLineRendererComponent.Instance?.Clear();
            await TimerComponent.Instance.WaitAsync(2000);
            ET.Client.PathLineRendererComponent.Instance?.Clear();
        }
    }
}