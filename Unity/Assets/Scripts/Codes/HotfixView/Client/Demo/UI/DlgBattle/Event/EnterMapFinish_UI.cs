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
            if (playerStatusComponent.RoomTypeInfo.roomType == RoomType.Normal)
            {
                if (playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalARCreate
                    || playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalARScanCode)
                {
                    isAR = true;
                }
            }
            else if (playerStatusComponent.RoomTypeInfo.roomType == RoomType.AR)
            {
                isAR = true;
            }

            Log.Debug($"EnterMapFinish isAR={isAR} RoomType={playerStatusComponent.RoomTypeInfo.roomType} SubRoomType={playerStatusComponent.RoomTypeInfo.subRoomType}");

            EventSystem.Instance.Publish(scene, new EventType.NoticeUIShowCommonLoading());

            Scene currentScene = scene.CurrentScene();
            while (currentScene == null || currentScene.IsDisposed)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (scene.IsDisposed)
                {
                    return;
                }
                currentScene = scene.CurrentScene();
            }

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
            while (gamePlayComponent == null || gamePlayComponent.IsDisposed)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (currentScene.IsDisposed)
                {
                    return;
                }
                gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
            }


            //await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFixedMenu>();
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFixedMenuHighest>();
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTowerNotice>();

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
            gamePlayComponent.PlayBattleStartMusic();

            float fARScale = gamePlayComponent.GetARScale();
            ET.Client.ARSessionHelper.SetScaleARCamera(currentScene, fARScale);


            Log.Debug($"EnterMapFinish AR GamePlay ready. Game mode: {gamePlayComponent.gamePlayMode} AR scale: {fARScale}.");
            if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
            {
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                while (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (currentScene.IsDisposed)
                    {
                        return;
                    }
                    gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                }
                gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);

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
                GamePlayPkComponent gamePlayPkComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                while (gamePlayPkComponent == null || gamePlayPkComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (currentScene.IsDisposed)
                    {
                        return;
                    }
                    gamePlayPkComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                }

                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
            }
            await ETTask.CompletedTask;
        }

        public async ETTask EnterMap_WhenNoAR(Scene scene)
        {
            Scene currentScene = scene.CurrentScene();

            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
            gamePlayComponent.PlayBattleStartMusic();

            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            if (playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalSingleMap)
            {
                GamePlayPkComponent gamePlayPkComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                while (gamePlayPkComponent == null || gamePlayPkComponent.IsDisposed)
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (currentScene.IsDisposed)
                    {
                        return;
                    }
                    gamePlayPkComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                }

                Log.Debug($"EnterMapFinish nonAR NormalSingleMap ready.");
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
            }
            else if (playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalRoom
                     || playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalPVE
                     || playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalPVP
                     || playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalEndlessChallenge
                     || playerStatusComponent.RoomTypeInfo.subRoomType == SubRoomType.NormalScanMesh
                     )
            {
                Log.Debug($"EnterMapFinish nonAR NormalRoom GamePlay ready. Game mode: {gamePlayComponent.gamePlayMode}");

                if (gamePlayComponent.gamePlayMode == GamePlayMode.TowerDefense)
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                    while (gamePlayTowerDefenseComponent == null || gamePlayTowerDefenseComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        if (currentScene.IsDisposed)
                        {
                            return;
                        }
                        gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(currentScene);
                    }
                    gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(currentScene);
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
                    GamePlayPkComponent gamePlayPkComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                    while (gamePlayPkComponent == null || gamePlayPkComponent.IsDisposed)
                    {
                        await TimerComponent.Instance.WaitFrameAsync();
                        if (currentScene.IsDisposed)
                        {
                            return;
                        }
                        gamePlayPkComponent = GamePlayHelper.GetGamePlayPK(currentScene);
                    }
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattle>();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}