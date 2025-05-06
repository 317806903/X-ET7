using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeResetSpatialAnchor_GamePlayTowerDefenseComponent: AEvent<Scene, ClientEventType.NoticeResetSpatialAnchor>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeResetSpatialAnchor args)
        {
            await ETTask.CompletedTask;
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent == null)
            {
                return;
            }

            gamePlayTowerDefenseComponent.isNavmeshFromHomeInitialized = false;
        }
    }
}