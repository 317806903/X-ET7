using ET.AbilityConfig;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class HallSceneEnterStart_UI: AEvent<Scene, ClientEventType.EnterHallSceneStart>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.EnterHallSceneStart args)
        {
            Scene clientScene = scene;

            //UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            ET.Client.GameObjectPoolHelper.ResetPoolDictCount(10);
            System.GC.Collect();

            UIAudioManagerHelper.PlayMusic(scene, MusicType.Main);

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync(WindowID.WindowID_Platform_Mobile_DlgLoading);
            await ResComponent.Instance.LoadSceneAsync("Hall", (progress) =>
            {
                EventSystem.Instance.Publish(scene, new ClientEventType.LoadingProgress() { curProgress = progress });
            });

            await ET.Client.SeasonHelper.Init(scene);
            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(scene);
            RoomType roomType = playerStatusComponent.RoomTypeInfo.roomType;
            SubRoomType subRoomType = playerStatusComponent.RoomTypeInfo.subRoomType;

            bool isFromLogin = args.isFromLogin;
            bool isRelogin = args.isRelogin;

            bool isDebugMode;
            if (isFromLogin)
            {
                isDebugMode = DebugConnectComponent.Instance.IsDebugMode;
            }
            else
            {
                if (roomType == RoomType.AR)
                {
                    isDebugMode = false;
                }
                else if (roomType == RoomType.Normal && subRoomType == SubRoomType.NormalPVE)
                {
                    isDebugMode = false;
                }
                else if (roomType == RoomType.Normal && subRoomType == SubRoomType.NormalPVP)
                {
                    isDebugMode = false;
                }
                else if (roomType == RoomType.Normal && subRoomType == SubRoomType.NormalEndlessChallenge)
                {
                    isDebugMode = false;
                }
                else if (roomType == RoomType.Normal && subRoomType == SubRoomType.NormalScanMesh)
                {
                    isDebugMode = false;
                }
                else if (DebugConnectComponent.Instance.IsDebugMode == false && roomType == RoomType.Normal && subRoomType == SubRoomType.None)
                {
                    isDebugMode = false;
                }
                else
                {
                    isDebugMode = true;
                }
            }

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFixedMenu>();
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgFixedMenuHighest>();

            Log.Debug($"--ET.Client.HallSceneEnterStart_UI.Run isFromLogin[{isFromLogin}] isRelogin[{isRelogin}] isDebugMode[{isDebugMode}]");
            if (isDebugMode)
            {
                await DealWhenIsDebugMode(clientScene, playerStatusComponent, isFromLogin, isRelogin);
            }
            else
            {
                await DealWhenNotDebugMode(clientScene, playerStatusComponent, isFromLogin, isRelogin);
            }

        }

        protected async ETTask DealWhenIsDebugMode(Scene scene, PlayerStatusComponent playerStatusComponent, bool isFromLogin, bool isRelogin)
        {

            PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;
            RoomTypeInfo roomTypeInfo = playerStatusComponent.RoomTypeInfo;
            RoomType roomType = roomTypeInfo.roomType;
            SubRoomType subRoomType = roomTypeInfo.subRoomType;

            Log.Debug($"--DealWhenIsDebugMode playerStatusComponent.PlayerStatus[{playerStatusComponent.PlayerStatus.ToString()}] playerStatusComponent.RoomTypeInfo[{playerStatusComponent.RoomTypeInfo.ToString()}] playerStatusComponent.RoomId[{playerStatusComponent.RoomId}] playerStatusComponent.LastBattleCfgId[{playerStatusComponent.LastBattleCfgId}] playerStatusComponent.LastBattleResult[{playerStatusComponent.LastBattleResult.ToString()}]");

            ET.Client.ARSessionHelper.ResetMainCamera(scene, false, false);

            if (roomType == RoomType.AR)
            {
                if (playerStatus == PlayerStatus.Room)
                {
                    await RoomHelper.QuitRoomAsync(scene);
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    await RoomHelper.MemberQuitBattleAsync(scene);
                }
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
                return;
            }

            if (subRoomType == SubRoomType.None)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            }
            else if (subRoomType == SubRoomType.NormalSingleMap)
            {
                //进入全局场景，所有人都进同个Map
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLobby>();
            }
            else if (subRoomType == SubRoomType.NormalRoom
                     || subRoomType == SubRoomType.NormalPVE
                     || subRoomType == SubRoomType.NormalPVP
                     || subRoomType == SubRoomType.NormalEndlessChallenge
                     || subRoomType == SubRoomType.NormalScanMesh
                     )
            {
                //进入动态场景，按房间都进同个Map
                if (playerStatus == PlayerStatus.Hall)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgHall>();
                }
                else if (playerStatus == PlayerStatus.Room)
                {
                    //在房间的时候杀掉进程后重进 会进到这里
                    if (isFromLogin)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnRoom");
                        ET.Client.UIManagerHelper.ShowConfirm(scene, msg, () =>
                        {
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgRoom>().Coroutine();
                        }, () =>
                        {
                            RoomHelper.QuitRoomAsync(scene).Coroutine();
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgHall>().Coroutine();
                        });
                    }
                    else if (isRelogin)
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgRoom>();
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgRoom>();
                    }
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    //在战斗的时候杀掉进程后重进 会进到这里
                    if (isFromLogin)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnBattle");
                        ET.Client.UIManagerHelper.ShowConfirm(scene, msg, () =>
                        {
                            RoomHelper.ReturnBackBattle(scene).Coroutine();
                        }, () =>
                        {
                            RoomHelper.MemberQuitBattleAsync(scene).Coroutine();
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgHall>().Coroutine();
                        });
                    }
                    else if (isRelogin)
                    {
                        await RoomHelper.ReturnBackBattle(scene);
                    }
                    else
                    {
                        await RoomHelper.ReturnBackBattle(scene);
                    }
                }
            }
            else if (subRoomType == SubRoomType.NormalARCreate || subRoomType == SubRoomType.NormalARScanCode)
            {
                ET.Client.ARSessionHelper.ResetMainCamera(scene, true);

                if (playerStatus == PlayerStatus.Hall)
                {
                    //AR战斗自行退出 会进到这里
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
                }
                else if (playerStatus == PlayerStatus.Room)
                {
                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        ARHallType = ARHallType.JoinTheRoom,
                        roomTypeInfo = roomTypeInfo,
                        roomId = playerStatusComponent.RoomId,
                    };
                    //在AR房间的时候杀掉进程后重进 会进到这里
                    if (isFromLogin)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnRoom");
                        ET.Client.UIManagerHelper.ShowConfirm(scene, msg, () =>
                        {
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();
                        }, () =>
                        {
                            RoomHelper.QuitRoomAsync(scene).Coroutine();
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>().Coroutine();
                        });
                    }
                    else if (isRelogin)
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    }
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        ARHallType = ARHallType.JoinTheRoom,
                        roomTypeInfo = roomTypeInfo,
                        roomId = playerStatusComponent.RoomId,
                    };
                    //在AR战斗的时候杀掉进程后重进 会进到这里
                    if (isFromLogin)
                    {
                        string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnBattle");
                        ET.Client.UIManagerHelper.ShowConfirm(scene, msg, () =>
                        {
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();
                        }, () =>
                        {
                            RoomHelper.MemberQuitBattleAsync(scene).Coroutine();
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>().Coroutine();
                        });
                    }
                    else if (isRelogin)
                    {
                        //await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                        ET.Client.ARSessionHelper.ResetMainCamera(scene, true);
                        await RoomHelper.ReturnBackBattle(scene);
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    }
                }
            }

        }

        protected async ETTask DealWhenNotDebugMode(Scene scene, PlayerStatusComponent playerStatusComponent, bool isFromLogin, bool isRelogin)
        {
            PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;
            RoomTypeInfo roomTypeInfo = playerStatusComponent.RoomTypeInfo;
            RoomType roomType = roomTypeInfo.roomType;
            SubRoomType subRoomType = roomTypeInfo.subRoomType;

            Log.Debug($"--DealWhenNotDebugMode playerStatusComponent.PlayerStatus[{playerStatusComponent.PlayerStatus.ToString()}] playerStatusComponent.RoomTypeInfo[{playerStatusComponent.RoomTypeInfo.ToString()}] playerStatusComponent.RoomId[{playerStatusComponent.RoomId}] playerStatusComponent.LastBattleCfgId[{playerStatusComponent.LastBattleCfgId}] playerStatusComponent.LastBattleResult[{playerStatusComponent.LastBattleResult.ToString()}]");

            ET.Client.ARSessionHelper.ResetMainCamera(scene, false, false);

            if (roomType == RoomType.Normal)
            {
                if (playerStatus == PlayerStatus.Room)
                {
                    if (subRoomType == SubRoomType.NormalPVE)
                    {
                        if (playerStatusComponent.LastBattleResult == BattleResult.Successed)
                        {
                            string nextBattleCfgId = "";
                            int seasonCfgId = roomTypeInfo.seasonCfgId;
                            if (seasonCfgId > 0)
                            {
                                nextBattleCfgId = SeasonChallengeLevelCfgCategory.Instance.GetNextChallengeGamePlayBattleLevelCfgId(roomTypeInfo);
                            }
                            else
                            {
                                nextBattleCfgId = TowerDefense_ChallengeLevelCfgCategory.Instance.GetNextChallengeGamePlayBattleLevelCfgId(roomTypeInfo);
                            }
                            if(string.IsNullOrEmpty(nextBattleCfgId) == false)
                            {
                                roomTypeInfo.gamePlayBattleLevelCfgId = nextBattleCfgId;
                                roomTypeInfo.pveIndex = roomTypeInfo.pveIndex + 1;
                                await RoomHelper.ChgRoomBattleLevelCfgAsync(scene, roomTypeInfo);
                                await ET.Client.UIManagerHelper.EnterRoomUI(scene);
                                return;
                            }
                            else
                            {
                                await RoomHelper.QuitRoomAsync(scene);
                                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgChallengeMode>();
                                return;
                            }
                        }
                        else
                        {
                            await ET.Client.UIManagerHelper.EnterRoomUI(scene);
                            return;
                        }
                    }
                    else if (subRoomType == SubRoomType.NormalPVP)
                    {
                        await ET.Client.UIManagerHelper.EnterRoomUI(scene);
                        return;
                    }
                    else if (subRoomType == SubRoomType.NormalEndlessChallenge)
                    {
                        await ET.Client.UIManagerHelper.EnterRoomUI(scene);
                        return;
                    }
                    else if (subRoomType == SubRoomType.NormalScanMesh)
                    {
                        await RoomHelper.QuitRoomAsync(scene);
                    }
                    else
                    {
                        await RoomHelper.QuitRoomAsync(scene);
                    }
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    if (subRoomType == SubRoomType.NormalPVE ||
                        subRoomType == SubRoomType.NormalPVP ||
                        subRoomType == SubRoomType.NormalEndlessChallenge)
                    {
                        if (isFromLogin)
                        {
                            string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnBattle");
                            ET.Client.UIManagerHelper.ShowConfirm(scene, msg, async () =>
                            {
                                await RoomHelper.ReturnBackBattle(scene);
                            }, () =>
                            {
                                RoomHelper.MemberQuitBattleAsync(scene).Coroutine();
                                UIManagerHelper.EnterGameModeUI(scene).Coroutine();
                            });
                            return;
                        }
                        else if (isRelogin)
                        {
                            await RoomHelper.ReturnBackBattle(scene);
                            return;
                        }
                    }
                    await RoomHelper.MemberQuitBattleAsync(scene);
                }
                await UIManagerHelper.EnterGameModeUI(scene);
                return;
            }

            if (subRoomType == SubRoomType.None)
            {
                await UIManagerHelper.EnterGameModeUI(scene);
                return;
            }
            else if (subRoomType == SubRoomType.ArcadeScanMesh)
            {
                if (playerStatus == PlayerStatus.Room)
                {
                    await RoomHelper.QuitRoomAsync(scene);
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    await RoomHelper.MemberQuitBattleAsync(scene);
                }
                await UIManagerHelper.EnterGameModeUI(scene);
                return;
            }
            else if (subRoomType == SubRoomType.ARTutorialFirst)
            {
                if (isRelogin == false)
                {
                    if (playerStatus == PlayerStatus.Room)
                    {
                        await RoomHelper.QuitRoomAsync(scene);
                    }
                    else if (playerStatus == PlayerStatus.Battle)
                    {
                        await RoomHelper.MemberQuitBattleAsync(scene);
                    }
                    await UIManagerHelper.EnterGameModeUI(scene);
                    return;
                }
            }

            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            if (playerBaseInfoComponent.isFinishTutorialFirst == false)
            {
                if (isRelogin == false)
                {
                    if (playerStatus == PlayerStatus.Room)
                    {
                        await RoomHelper.QuitRoomAsync(scene);
                    }
                    else if (playerStatus == PlayerStatus.Battle)
                    {
                        await RoomHelper.MemberQuitBattleAsync(scene);
                    }
                    await UIManagerHelper.EnterGameModeUI(scene);
                    return;
                }
            }

            if (playerStatus == PlayerStatus.Hall)
            {
                //AR战斗自行退出 会进到这里
                await UIManagerHelper.EnterGameModeUI(scene);
            }
            else if (playerStatus == PlayerStatus.Room)
            {
                if (subRoomType == SubRoomType.ARPVE)
                {
                    if (playerStatusComponent.LastBattleResult == BattleResult.Successed)
                    {
                        string nextBattleCfgId = "";
                        int seasonCfgId = roomTypeInfo.seasonCfgId;
                        if (seasonCfgId > 0)
                        {
                            nextBattleCfgId = SeasonChallengeLevelCfgCategory.Instance.GetNextChallengeGamePlayBattleLevelCfgId(roomTypeInfo);
                        }
                        else
                        {
                            nextBattleCfgId = TowerDefense_ChallengeLevelCfgCategory.Instance.GetNextChallengeGamePlayBattleLevelCfgId(roomTypeInfo);
                        }
                        if(string.IsNullOrEmpty(nextBattleCfgId) == false)
                        {
                            roomTypeInfo.gamePlayBattleLevelCfgId = nextBattleCfgId;
                            roomTypeInfo.pveIndex = roomTypeInfo.pveIndex + 1;
                            await RoomHelper.ChgRoomBattleLevelCfgAsync(scene, roomTypeInfo);
                        }
                    }
                    // await RoomHelper.QuitRoomAsync(scene);
                    // await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgChallengeMode>();
                    // return;
                }

                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    ARHallType = ARHallType.JoinTheRoom,
                    roomId = playerStatusComponent.RoomId,
                    roomTypeInfo = roomTypeInfo,
                };
                //在AR房间的时候杀掉进程后重进 会进到这里
                if (isFromLogin)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnRoom");
                    ET.Client.UIManagerHelper.ShowConfirm(scene, msg, () =>
                    {
                        UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();
                    }, () =>
                    {
                        RoomHelper.QuitRoomAsync(scene).Coroutine();
                        UIManagerHelper.EnterGameModeUI(scene).Coroutine();
                    });
                }
                else if (isRelogin)
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                }
            }
            else if (playerStatus == PlayerStatus.Battle)
            {
                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    ARHallType = ARHallType.JoinTheRoom,
                    roomId = playerStatusComponent.RoomId,
                    roomTypeInfo = roomTypeInfo,
                };
                //在AR战斗的时候杀掉进程后重进 会进到这里
                if (isFromLogin)
                {
                    string msg = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Login_IsReturnBattle");
                    ET.Client.UIManagerHelper.ShowConfirm(scene, msg, () =>
                    {
                        UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();
                    }, () =>
                    {
                        RoomHelper.MemberQuitBattleAsync(scene).Coroutine();
                        UIManagerHelper.EnterGameModeUI(scene).Coroutine();
                    });
                }
                else if (isRelogin)
                {
                    //await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    ET.Client.ARSessionHelper.ResetMainCamera(scene, true);
                    await RoomHelper.ReturnBackBattle(scene);
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                }
            }
        }
    }
}
