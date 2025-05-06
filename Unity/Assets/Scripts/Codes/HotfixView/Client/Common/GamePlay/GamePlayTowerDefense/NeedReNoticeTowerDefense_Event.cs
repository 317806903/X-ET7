using System;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class NeedReNoticeTowerDefense_Event: AEvent<Scene, EventType.NeedReNoticeTowerDefense>
    {
        protected override async ETTask Run(Scene scene, EventType.NeedReNoticeTowerDefense args)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = ET.Client.GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
            {
                return;
            }
            gamePlayTowerDefenseComponent.isNeedReNoticeTowerDefense = true;
            await ETTask.CompletedTask;
        }
    }
}