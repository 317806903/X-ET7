using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeResetSpatialAnchor_NavMeshRendererComponent: AEvent<Scene, ClientEventType.NoticeResetSpatialAnchor>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeResetSpatialAnchor args)
        {
            await ETTask.CompletedTask;
            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(scene);
            if (gamePlayComponent == null)
            {
                return;
            }

            await TimerComponent.Instance.WaitFrameAsync();
            ET.Client.NavMeshRendererComponent.Instance?.ResetPos();
            ET.Client.NavMeshRendererComponent.Instance?.Clear();
        }
    }
}