using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class HallSceneEnterStart_UI: AEvent<Scene, EventType.HallSceneEnterStart>
    {
        protected override async ETTask Run(Scene scene, EventType.HallSceneEnterStart args)
        {
            Scene clientScene = scene;

            UIManagerHelper.GetUIComponent(scene).HideAllShownWindow();
            //zpb UIManagerHelper.GetUIComponent(scene).CloseAllWindow();

            ET.Ability.Client.UIAudioManagerHelper.PlayMusicMain(scene);

            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLoading>();
            DlgLoading _DlgLoading = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgLoading>(true);
            await ResComponent.Instance.LoadSceneAsync("Hall", _DlgLoading.UpdateProcess);

            PlayerComponent playerComponent = ET.Client.PlayerHelper.GetMyPlayerComponent(scene);
            PlayerGameMode playerGameMode = playerComponent.PlayerGameMode;
            PlayerStatus playerStatus = playerComponent.PlayerStatus;
            bool isDebugMode = playerComponent.IsDebugMode;

            bool isFromLogin = args.isFromLogin;
            Log.Debug($"------ HallSceneEnterStart_UI playerGameMode[{playerGameMode.ToString()}] playerStatus[{playerStatus.ToString()}]");

            if (isDebugMode)
            {
                await DealWhenIsDebugMode(clientScene, playerComponent, isFromLogin);
            }
            else
            {
                await DealWhenNotDebugMode(clientScene, playerComponent, isFromLogin);
            }

        }

        protected async ETTask DealWhenIsDebugMode(Scene scene, PlayerComponent playerComponent, bool isFromLogin)
        {
            PlayerGameMode playerGameMode = playerComponent.PlayerGameMode;
            PlayerStatus playerStatus = playerComponent.PlayerStatus;

            if (playerGameMode == PlayerGameMode.None)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameMode>();
            }
            else if (playerGameMode == PlayerGameMode.SingleMap)
            {
                //进入全局场景，所有人都进同个Map
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgLobby>();
            }
            else if (playerGameMode == PlayerGameMode.Room)
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
            else if (playerGameMode == PlayerGameMode.ARRoom)
            {
                if (playerStatus == PlayerStatus.Hall)
                {
                    //AR战斗自行退出 会进到这里
                    await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>();
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
                            _ARRoomType = ARRoomType.Normal,
                            arRoomId = playerComponent.RoomId,
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
                                UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>().Coroutine();
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
                        _ARRoomType = ARRoomType.Normal,
                        arRoomId = playerComponent.RoomId,
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
                            UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>().Coroutine();
                        });
                    }
                    else
                    {
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARHall>(_DlgARHall_ShowWindowData);
                    }
                }
            }

        }

        protected async ETTask DealWhenNotDebugMode(Scene scene, PlayerComponent playerComponent, bool isFromLogin)
        {
            PlayerGameMode playerGameMode = playerComponent.PlayerGameMode;
            PlayerStatus playerStatus = playerComponent.PlayerStatus;
            ARRoomType _ARRoomType = playerComponent.ARRoomType;
            Log.Debug($"--DealWhenNotDebugMode ARRoomType={_ARRoomType.ToString()}");

            if (playerGameMode == PlayerGameMode.None)
            {
                await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgGameModeAR>();
            }
            else if (playerGameMode == PlayerGameMode.SingleMap)
            {
                Log.Error($"不应该进入这里");
            }
            else if (playerGameMode == PlayerGameMode.Room)
            {
                Log.Error($"不应该进入这里");
            }
            else if (playerGameMode == PlayerGameMode.ARRoom)
            {
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
                        await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgARRoom>();
                    }
                    else
                    {
                        DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                        {
                            playerStatus = PlayerStatus.Room,
                            _ARRoomType = _ARRoomType,
                            arRoomId = playerComponent.RoomId,
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
                else if (playerStatus == PlayerStatus.Battle)
                {
                    DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = new()
                    {
                        playerStatus = PlayerStatus.Battle,
                        _ARRoomType = _ARRoomType,
                        arRoomId = playerComponent.RoomId,
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
}