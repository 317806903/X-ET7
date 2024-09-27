using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using ET.AbilityConfig;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DlgARHall))]
    public static class DlgARHallSystem
    {
        public static void RegisterUIEvent(this DlgARHall self)
        {
        }

        public static async ETTask ShowWindow(this DlgARHall self, ShowWindowData contextData = null)
        {
            if (contextData == null)
            {
                Log.Error("contextData == null");
                return;
            }
            else
            {
                DlgARHall_ShowWindowData _DlgARHall_ShowWindowData = contextData as DlgARHall_ShowWindowData;
                self.roomTypeInfo = _DlgARHall_ShowWindowData.roomTypeInfo;
                self.roomId = _DlgARHall_ShowWindowData.roomId;
                self.arSceneId = _DlgARHall_ShowWindowData.arSceneId;

                self.isCreateRooming = false;

                self._ARHallType = _DlgARHall_ShowWindowData.ARHallType;
                if (self._ARHallType == ARHallType.JoinTheRoom)
                {
                    if (self.roomId <= 0)
                    {
                        Log.Error($"self._ARHallType == ARHallType.JoinTheRoom self.roomId <= 0");
                        return;
                    }
                    if (string.IsNullOrEmpty(self.arSceneId) == false)
                    {
                        Log.Error($"self._ARHallType == ARHallType.JoinTheRoom string.IsNullOrEmpty(self.arSceneId) == false");
                        return;
                    }
                }
                else if (self._ARHallType == ARHallType.KeepRoomAndRescan)
                {
                    if (self.roomId <= 0)
                    {
                        Log.Error($"self._ARHallType == ARHallType.KeepRoomAndRescan self.roomId <= 0");
                        return;
                    }
                    if (string.IsNullOrEmpty(self.arSceneId) == false)
                    {
                        Log.Error($"self._ARHallType == ARHallType.KeepRoomAndRescan string.IsNullOrEmpty(self.arSceneId) == false");
                        return;
                    }
                }
                else if (self._ARHallType == ARHallType.CreateRoomWithARSceneId)
                {
                    if (self.roomId > 0)
                    {
                        Log.Error($"self._ARHallType == ARHallType.CreateRoomWithARSceneId self.roomId > 0");
                        return;
                    }
                    if (string.IsNullOrEmpty(self.arSceneId))
                    {
                        Log.Error($"self._ARHallType == ARHallType.CreateRoomWithARSceneId string.IsNullOrEmpty(self.arSceneId)");
                        return;
                    }
                }
                else if (self._ARHallType == ARHallType.CreateRoomWithOutARSceneId)
                {
                    if (self.roomId > 0)
                    {
                        Log.Error($"self._ARHallType == ARHallType.CreateRoomWithOutARSceneId self.roomId > 0");
                        return;
                    }
                    if (string.IsNullOrEmpty(self.arSceneId) == false)
                    {
                        Log.Error($"self._ARHallType == ARHallType.CreateRoomWithOutARSceneId string.IsNullOrEmpty(self.arSceneId) == false");
                        return;
                    }
                }
                else if (self._ARHallType == ARHallType.ScanQRCode)
                {
                    if (self.roomId > 0)
                    {
                        Log.Error($"self._ARHallType == ARHallType.ScanQRCode self.roomId > 0");
                        return;
                    }
                    if (string.IsNullOrEmpty(self.arSceneId) == false)
                    {
                        Log.Error($"self._ARHallType == ARHallType.ScanQRCode string.IsNullOrEmpty(self.arSceneId) == false");
                        return;
                    }
                }

            }

            self.InitArSession().Coroutine();
        }

        public static async ETTask InitArSession(this DlgARHall self)
        {
            await self.TriggerJoinScene();

            ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
            await arSessionComponent.Init(() =>
                {
                    self.OnQuitRoomCallBack().Coroutine();
                    self.OnClose().Coroutine();
                },
                () =>
                {
                    self.OnFinishedCallBack().Coroutine();
                },
                (isNeedReCreateRoom) =>
                {
                    self.OnCreateRoomCallBack(isNeedReCreateRoom).Coroutine();
                },
                () =>
                {

                },
                (isNeedReCreateRoom) =>
                {
                    self.OnCreateRoomCallBack(isNeedReCreateRoom).Coroutine();
                },
                (sQRCodeInfo) =>
                {
                    self.OnJoinByQRCodeCallBack(sQRCodeInfo).Coroutine();
                },
                () =>
                {
                    return self.GetQRCodeInfo();
                },
                self.arSceneId, self._ARHallType);

            // 此时进入AR场景，开始扫图或扫码，或加载上次扫图
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.ARStart);
            Log.Debug($"AR Prepare Start. game mode is {self.roomTypeInfo.subRoomType}.");
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                // 准备阶段开始
                eventName = "PrepareStarted", properties = new() { { "game_mode", self.roomTypeInfo.subRoomType.ToString() }, }
            });

            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLoggingStart()
            {
                // 准备阶段结束计时开始
                eventName = "PrepareEnded",
            });
        }

        public static async ETTask ReStart(this DlgARHall self)
        {
            ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
            await arSessionComponent.ReStart();
        }

        public static async ETTask HideMenu(this DlgARHall self)
        {
            ARSessionComponent arSessionComponent = ET.Client.ARSessionHelper.GetARSession(self.DomainScene());
            arSessionComponent.HideMenu();
        }

        public static async ETTask TriggerJoinScene(this DlgARHall self)
        {
            if (self.roomId == 0)
            {
                if (string.IsNullOrEmpty(self.roomTypeInfo.gamePlayBattleLevelCfgId))
                {
                    self.roomTypeInfo.gamePlayBattleLevelCfgId = ET.GamePlayHelper.GetBattleCfgId(self.roomTypeInfo);
                }

                Log.Debug($"self.roomId==0 {self.roomTypeInfo.gamePlayBattleLevelCfgId}");
                return;
            }

            bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
            if (roomExist == false)
            {
                if (string.IsNullOrEmpty(self.roomTypeInfo.gamePlayBattleLevelCfgId))
                {
                    self.roomTypeInfo.gamePlayBattleLevelCfgId = ET.GamePlayHelper.GetBattleCfgId(self.roomTypeInfo);
                }

                Log.Debug($"roomExist==false {self.roomTypeInfo.gamePlayBattleLevelCfgId}");
                return;
            }

            RoomManagerComponent roomManagerComponent = ET.Client.RoomHelper.GetRoomManager(self.DomainScene());
            RoomComponent roomComponent = roomManagerComponent.GetRoom(self.roomId);
            self.arSceneId = roomComponent.arSceneId;
            self.roomTypeInfo = roomComponent.roomTypeInfo;
            await ETTask.CompletedTask;
        }

        public static async ETTask OnClose(this DlgARHall self)
        {
            Log.Debug($"ET.Client.DlgARHallSystem.OnClose ");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            // 此时AR准备阶段被用户取消，AR界面关闭，回到主界面。
            string entranceType = ARSessionHelper.GetAREntranceType(self.DomainScene());
            Log.Debug($"AR Prepare End with cancellation. EntranceType={entranceType}");
            EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
            {
                // 准备阶段结束计时结束
                eventName = "PrepareEnded",
                properties = new()
                {
                    { "success", false }, { "session_id", "" }, { "game_mode", self.roomTypeInfo.subRoomType.ToString() }, { "mesh_source", entranceType }
                }
            });
            // 重置entrance type给下一个session
            ARSessionHelper.ResetAREntranceType(self.DomainScene());

            // 播放主界面音乐
            UIAudioManagerHelper.PlayMusic(self.DomainScene(), MusicType.Main);

            await UIManagerHelper.ExitRoomUI(self.DomainScene());
        }

        public static async ETTask OnCreateRoomCallBack(this DlgARHall self, bool isNeedReCreateRoom)
        {
            Log.Debug("ET.Client.DlgARHallSystem.OnCreateRoomCallBack begin");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);
            //UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Scan, true);

            if (isNeedReCreateRoom)
            {
                self.roomId = 0;
                await self.CreateRoom();
                PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
                long roomId = playerStatusComponent.RoomId;
                self.isHost = true;
                self.roomId = roomId;
            }
            else
            {
                if (self.roomId == 0)
                {
                    PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
                    long roomId = playerStatusComponent.RoomId;
                    if (roomId != 0)
                    {
                        self.roomId = roomId;
                    }
                    else
                    {
                        await self.CreateRoom();
                        playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
                        roomId = playerStatusComponent.RoomId;
                        self.roomId = roomId;
                    }
                }
                self.isHost = true;
            }
            Log.Debug($"ET.Client.DlgARHallSystem.OnCreateRoomCallBack end {self.roomId}");
        }

        public static async ETTask OnQuitRoomCallBack(this DlgARHall self)
        {
            try
            {
                Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack begin self=[{self}]");
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

                self.QuitRoom().Coroutine();
                self.isHost = false;
                self.roomId = 0;
                Log.Debug($"ET.Client.DlgARHallSystem.OnQuitRoomCallBack end {self.roomId}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static (bool, string) GetQRCodeInfo(this DlgARHall self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            if (self.roomId == 0)
            {
                Log.Debug($"ET.Client.DlgARHallSystem.GetQRCodeInfo self.roomId == 0");
                return (false, "");
            }

            Log.Debug($"ET.Client.DlgARHallSystem.GetQRCodeInfo end {self.roomId}");
            return (true, self.roomId.ToString());
        }

        public static async ETTask OnJoinByQRCodeCallBack(this DlgARHall self, string sQRCodeInfo)
        {
            Log.Debug($"ET.Client.DlgARHallSystem.OnJoinByQRCodeCallBack end {sQRCodeInfo}");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            self.isHost = false;
            self.roomId = long.Parse(sQRCodeInfo);

            bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
            if (roomExist == false)
            {
                self.HideMenu().Coroutine();
                self.roomId = 0;

                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
                {
                    //self.ReStart().Coroutine();
                    self.OnClose().Coroutine();
                });
                return;
            }
            else
            {
                bool result = await RoomHelper.JoinRoomAsync(self.ClientScene(), self.roomId);
                if (result == false)
                {
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_JoinRoomError");
                    UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
                    {
                        //self.ReStart().Coroutine();
                        self.OnClose().Coroutine();
                    });
                }
            }
        }

        public static async ETTask OnFinishedCallBack(this DlgARHall self)
        {
            while (self.isCreateRooming)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                if (self.IsDisposed)
                {
                    return;
                }
            }
            Log.Debug($"ET.Client.DlgARHallSystem.OnFinishedCallBack ");
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Confirm);

            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerStatusHelper.GetMyPlayerStatusComponent(self.DomainScene());
            Log.Debug(
                $"-OnFinishedCallBack self._ARHallType[{self._ARHallType.ToString()}] playerComponent.PlayerStatus[{playerStatusComponent.PlayerStatus.ToString()}]");

            if (playerStatusComponent.PlayerStatus == PlayerStatus.Battle)
            {
                if (playerStatusComponent.RoomId != self.roomId)
                {

                }
                //这里应该是已经进入战斗后杀掉进程重启后
                await RoomHelper.ReturnBackBattle(self.ClientScene());

                return;
            }
            else if (playerStatusComponent.PlayerStatus == PlayerStatus.Room)
            {
                if (playerStatusComponent.RoomId != self.roomId)
                {

                }
                bool roomExist = await RoomHelper.GetRoomInfoAsync(self.DomainScene(), self.roomId);
                if (roomExist == false)
                {
                    self.HideMenu().Coroutine();
                    string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
                    UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
                    {
                        //self.ReStart().Coroutine();
                        self.OnClose().Coroutine();
                    });
                    return;
                }

                if (self.isHost)
                {
                    await self.SetARRoomInfoAsync();

                    await self.EnterARRoomUI();
                }
                else
                {
                    //从机 会走这里
                    await self.JoinRoom(self.roomId);
                }

                string arSceneId = ARSessionHelper.GetARSceneId(self.DomainScene());

                // 此时AR准备阶段完全结束，玩家已经加入房间等待
                string entranceType = ARSessionHelper.GetAREntranceType(self.DomainScene());
                Log.Debug($"AR Prepare End. EntranceType={entranceType} sessionID={arSceneId}");
                EventSystem.Instance.Publish(self.DomainScene(), new EventType.NoticeEventLogging()
                {
                    // 准备阶段结束计时结束
                    eventName = "PrepareEnded",
                    properties = new()
                    {
                        { "success", true },
                        { "session_id", arSceneId },
                        { "game_mode", self.roomTypeInfo.subRoomType.ToString() },
                        { "mesh_source", entranceType }
                    }
                });
                // 重置entrance type给下一个session
                ARSessionHelper.ResetAREntranceType(self.DomainScene());
                return;
            }
            else if (playerStatusComponent.PlayerStatus == PlayerStatus.Hall)
            {
                //这里应该是下载完地图后发现已经被踢出房间

                await self.CreateRoom();

                await self.SetARRoomInfoAsync();

                await self.EnterARRoomUI();

                return;
            }

        }

        public static async ETTask CreateRoom(this DlgARHall self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            if (GamePlayBattleLevelCfgCategory.Instance.Contain(self.roomTypeInfo.gamePlayBattleLevelCfgId) == false)
            {
                Log.Error($"GamePlayBattleLevelCfgCategory.Instance.Contain({self.roomTypeInfo.gamePlayBattleLevelCfgId}) == false");
                return;
            }

            self.isCreateRooming = true;
            (bool result, long roomId) = await RoomHelper.CreateRoomAsync(self.ClientScene(), self.roomTypeInfo);
            self.isCreateRooming = false;
            if (result)
            {
            }
            else
            {
                self.HideMenu().Coroutine();
                string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_CreateRoomError");
                UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
                {
                    //self.ReStart().Coroutine();
                    self.OnClose().Coroutine();
                });
            }
        }

        public static async ETTask QuitRoom(this DlgARHall self)
        {
            UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Click);

            await RoomHelper.QuitRoomAsync(self.ClientScene());
        }

        public static async ETTask EnterARRoomUI(this DlgARHall self)
        {
            await ET.Client.UIManagerHelper.EnterRoomUI(self.DomainScene());
        }

        public static async ETTask JoinRoom(this DlgARHall self, long roomId)
        {
            await self.EnterARRoomUI();
            // bool result = await RoomHelper.JoinRoomAsync(self.ClientScene(), roomId);
            // if (result)
            // {
            // 	await self.EnterARRoomUI();
            // }
            // else
            // {
            // 	self.HideMenu().Coroutine();
            // 	string txt = LocalizeComponent.Instance.GetTextValue("TextCode_Key_Hall_RoomNotExist");
            // 	UIManagerHelper.ShowOnlyConfirm(self.DomainScene(), txt, () =>
            // 	{
            // 		self.ReStart().Coroutine();
            // 	});
            // }
        }

        public static async ETTask SetARRoomInfoAsync(this DlgARHall self)
        {
            await ET.Client.ARSessionHelper.SetARRoomInfoAsync(self.DomainScene());
        }
    }
}