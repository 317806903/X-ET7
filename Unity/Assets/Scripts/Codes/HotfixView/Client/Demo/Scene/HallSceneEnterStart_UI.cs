using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class HallSceneEnterStart_UI: AEvent<Scene, EventType.EnterHallSceneStart>
    {
        protected override async ETTask Run(Scene scene, EventType.EnterHallSceneStart args)
        {
            Scene clientScene = scene;

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            //zpb UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            UIAudioManagerHelper.PlayMusicMain(scene);

            ET.Client.ARSessionHelper.ResetMainCamera(scene, false);

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();
            DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>(true);
            await ResComponent.Instance.LoadSceneAsync("Hall", _DlgLoading.UpdateProcess);

            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(scene);
            RoomType roomType = playerStatusComponent.RoomType;

            bool isFromLogin = args.isFromLogin;
            bool isDebugMode;
            if (isFromLogin)
            {
                isDebugMode = DebugConnectComponent.Instance.IsDebugMode;
            }
            else
            {
                isDebugMode = roomType == RoomType.Normal;
            }

            if (isDebugMode)
            {
                await DealWhenIsDebugMode(clientScene, playerStatusComponent, isFromLogin);
            }
            else
            {
                await DealWhenNotDebugMode(clientScene, playerStatusComponent, isFromLogin);
            }

        }

        protected async ETTask DealWhenIsDebugMode(Scene scene, PlayerStatusComponent playerStatusComponent, bool isFromLogin)
        {
            RoomType roomType = playerStatusComponent.RoomType;
            SubRoomType subRoomType = playerStatusComponent.SubRoomType;
            PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;
            Log.Debug($"--DealWhenIsDebugMode playerStatusComponent={playerStatusComponent}");

            if (subRoomType == SubRoomType.None)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            }
            else if (subRoomType == SubRoomType.NormalSingleMap)
            {
                //进入全局场景，所有人都进同个Map
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLobby>();
            }
            else if (subRoomType == SubRoomType.NormalRoom)
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
                    bool _ARSceneStatusCompleted = ET.Client.ARSessionHelper.ChkARSceneStatusCompleted(scene);
                    Log.Debug($"_ARSceneStatusCompleted[{_ARSceneStatusCompleted}]");
                    if (_ARSceneStatusCompleted)
                    {
                        //AR战斗后返回 会进到这里
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
                    }
                    else
                    {
                        DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                        {
                            playerStatus = PlayerStatus.Room,
                            RoomType = roomType,
                            SubRoomType = subRoomType,
                            arRoomId = playerStatusComponent.RoomId,
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
                        else
                        {
                            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                        }
                    }
                }
                else if (playerStatus == PlayerStatus.Battle)
                {
                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        playerStatus = PlayerStatus.Battle,
                        RoomType = roomType,
                        SubRoomType = subRoomType,
                        arRoomId = playerStatusComponent.RoomId,
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
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    }
                }
            }

        }

        protected async ETTask DealWhenNotDebugMode(Scene scene, PlayerStatusComponent playerStatusComponent, bool isFromLogin)
        {
            RoomType roomType = playerStatusComponent.RoomType;
            SubRoomType subRoomType = playerStatusComponent.SubRoomType;
            PlayerStatus playerStatus = playerStatusComponent.PlayerStatus;
            Log.Debug($"--DealWhenNotDebugMode playerStatusComponent={playerStatusComponent}");

            if (subRoomType == SubRoomType.None)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
                return;
            }
            else if (subRoomType == SubRoomType.ARTutorialFirst)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
                return;
            }
            ET.Client.ARSessionHelper.ResetMainCamera(scene, true);

            if (playerStatus == PlayerStatus.Hall)
            {
                //AR战斗自行退出 会进到这里
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
            }
            else if (playerStatus == PlayerStatus.Room)
            {
                bool _ARSceneStatusCompleted = ET.Client.ARSessionHelper.ChkARSceneStatusCompleted(scene);
                Log.Debug($"_ARSceneStatusCompleted[{_ARSceneStatusCompleted}]");
                if (_ARSceneStatusCompleted)
                {
                    //AR战斗后返回 会进到这里
                    //await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
                    await UIManagerHelper.EnterRoom(scene);

                }
                else
                {
                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        playerStatus = PlayerStatus.Room,
                        RoomType = roomType,
                        SubRoomType = subRoomType,
                        arRoomId = playerStatusComponent.RoomId,
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
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>().Coroutine();
                        });
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    }
                }
            }
            else if (playerStatus == PlayerStatus.Battle)
            {
                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                {
                    playerStatus = PlayerStatus.Battle,
                    RoomType = roomType,
                    SubRoomType = subRoomType,
                    arRoomId = playerStatusComponent.RoomId,
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
                        _DlgARHall_ShowWindowData.playerStatus = PlayerStatus.Hall;
                        _DlgARHall_ShowWindowData.arRoomId = 0;
                        UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData).Coroutine();
                    });
                }
                else
                {
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                }
            }
        }
    }
}
