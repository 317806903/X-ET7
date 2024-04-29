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
            Log.Debug("EnterMapFinish begins.");
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            bool isAR = false;
            if (playerStatusComponent.RoomType == RoomType.Normal)
            {
                if (playerStatusComponent.SubRoomType == SubRoomType.NormalARCreate
                    || playerStatusComponent.SubRoomType == SubRoomType.NormalARScanCode)
                {
                    isAR = true;
                }
            }
            else if (playerStatusComponent.RoomType == RoomType.AR)
            {
                isAR = true;
            }

            Log.Debug($"EnterMapFinish isAR={isAR} RoomType={playerStatusComponent.RoomType} SubRoomType={playerStatusComponent.SubRoomType}");

            EventSystem.Instance.Publish(scene, new EventType.NoticeUIShowCommonLoading());

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


            //await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFixedMenu>();
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFixedMenuHighest>();

            EventSystem.Instance.Publish(scene, new EventType.NoticeUIHideCommonLoading());

            if (isAR == false)
            {
                await EnterMap_WhenNoAR(scene);
            }
            else
            {
                await EnterMap_WhenAR(scene);
            }
            Log.Debug($"EnterMapFinish ends.");
            await ETTask.CompletedTask;
        }

        public async ETTask EnterMap_WhenAR(Scene scene)
        {
            Scene currentScene = scene.CurrentScene();

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);

            float fARScale = gamePlayComponent.GetARScale();
            ET.Client.ARSessionHelper.SetScaleARCamera(currentScene, fARScale);

            Log.Debug($"EnterMapFinish AR GamePlay ready. Game mode: {gamePlayComponent.gamePlayMode} AR scale: {fARScale}.");
            if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                while (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                }

                //if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
                {
                    EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
                    {
                        eventName = "LevelStarted",
                        properties = new()
                        {
                            {"player_num", gamePlayComponent.GetPlayerList().Count},
                        }
                    });

                    EventSystem.Instance.Publish(scene, new EventType.NoticeEventLoggingStart()
                    {
                        eventName = "BasePlaced",
                    });
                    EventSystem.Instance.Publish(scene, new EventType.NoticeEventLoggingStart()
                    {
                        eventName = "LevelEnded",
                    });
                }

                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTowerAR>();
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
            await ETTask.CompletedTask;
        }

        public async ETTask EnterMap_WhenNoAR(Scene scene)
        {
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            if (playerStatusComponent.SubRoomType == SubRoomType.NormalSingleMap)
            {
                Scene currentScene = scene.CurrentScene();
                GamePlayPKComponent gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                while (gamePlayPKComponent == null || gamePlayPKComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    gamePlayPKComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                }

                Log.Debug($"EnterMapFinish nonAR NormalSingleMap ready.");
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
            }
            else if (playerStatusComponent.SubRoomType == SubRoomType.NormalRoom
                     || playerStatusComponent.SubRoomType == SubRoomType.NormalPVE
                     || playerStatusComponent.SubRoomType == SubRoomType.NormalPVP
                     || playerStatusComponent.SubRoomType == SubRoomType.NormalEndlessChallenge
                     || playerStatusComponent.SubRoomType == SubRoomType.NormalScanMesh
                     )
            {
                Scene currentScene = scene.CurrentScene();
                GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);

                Log.Debug($"EnterMapFinish nonAR NormalRoom GamePlay ready. Game mode: {gamePlayComponent.gamePlayMode}");

                if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                    while (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                    }
                    //if (gamePlayTowerDefenseComponent.IsEndlessChallengeMode())
                    {
                        EventSystem.Instance.Publish(scene, new EventType.NoticeEventLogging()
                        {
                            eventName = "LevelStarted",
                            properties = new()
                            {
                                {"player_num", gamePlayComponent.GetPlayerList().Count},
                            }
                        });

                        EventSystem.Instance.Publish(scene, new EventType.NoticeEventLoggingStart()
                        {
                            eventName = "BasePlaced",
                        });
                        EventSystem.Instance.Publish(scene, new EventType.NoticeEventLoggingStart()
                        {
                            eventName = "LevelEnded",
                        });
                    }

                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTower>();
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