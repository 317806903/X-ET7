using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class EnterMapFinish_UI: AEvent<Scene, EventType.EnterMapFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.EnterMapFinish args)
        {
            Log.Debug("EnterMapFinish 11");
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(scene);

            bool isAR = false;
            if (playerStatusComponent.RoomType == RoomType.Normal)
            {
                if (playerStatusComponent.SubRoomType == SubRoomType.NormalARCreate || playerStatusComponent.SubRoomType == SubRoomType.NormalARScanCode)
                {
                    isAR = true;
                }
            }
            else if (playerStatusComponent.RoomType == RoomType.AR)
            {
                isAR = true;
            }

            if (isAR == false)
            {
                if (playerStatusComponent.SubRoomType == SubRoomType.NormalSingleMap)
                {
                    Scene currentScene = scene.CurrentScene();
                    GamePlayPKComponent gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                    while (gamePlayPKComponent == null || gamePlayPKComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                    }

                    Log.Debug("EnterMapFinish 22");
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
                    Log.Debug("EnterMapFinish 33");
                }
                else if (playerStatusComponent.SubRoomType == SubRoomType.NormalRoom)
                {
                    Scene currentScene = scene.CurrentScene();
                    while (currentScene == null || currentScene.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        currentScene = scene.CurrentScene();
                    }
                    GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
                    while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
                    }

                    if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
                    {
                        GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                        while (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
                        {
                            await TimerComponent.Instance.WaitFrameAsync();
                            gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                        }

                        if (gamePlayComponent.IsAR())
                        {
                            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTowerAR>();
                        }
                        else
                        {
                            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTower>();
                        }
                    }
                    else if (gamePlayComponent.gamePlayMode == GamePlayMode.PK)
                    {
                        GamePlayPKComponent gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                        while (gamePlayPKComponent == null || gamePlayPKComponent.IsDisposed)
                        {
                            await TimerComponent.Instance.WaitFrameAsync();
                            gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                        }

                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
                    }
                }
            }
            else
            {
                Scene currentScene = scene.CurrentScene();
                while (currentScene == null || currentScene.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    currentScene = scene.CurrentScene();
                }
                GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
                while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
                }

                float fARScale = gamePlayComponent.GetARScale();
                ET.Client.ARSessionHelper.SetScaleARCamera(currentScene, fARScale);

                if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                    while (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                    }

                    if (gamePlayComponent.IsAR())
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTowerAR>();
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTower>();
                    }
                }
                else if (gamePlayComponent.gamePlayMode == GamePlayMode.PK)
                {
                    GamePlayPKComponent gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                    while (gamePlayPKComponent == null || gamePlayPKComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                    }

                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
                }
            }


            await ETTask.CompletedTask;
        }
    }
}