using System;
using UnityEngine.Device;

namespace ET.Client
{
    [FriendOf(typeof (ReLoginComponent))]
    public static class ReLoginComponentSystem
    {

        [ObjectSystem]
        public class ReLoginComponentAwakeSystem: AwakeSystem<ReLoginComponent>
        {
            protected override void Awake(ReLoginComponent self)
            {
                ReLoginComponent.Instance = self;
                self.isReLogining = false;
                self.isReCreateSessioning = false;

                ET.Client.ApplicationStatusComponent.Instance.AddApplicationPauseListern(self.RegApplicationStatusChg);
            }
        }

        [ObjectSystem]
        public class ReLoginComponentDestroySystem: DestroySystem<ReLoginComponent>
        {
            protected override void Destroy(ReLoginComponent self)
            {
                ReLoginComponent.Instance = null;
                ET.Client.ApplicationStatusComponent.Instance.RemoveApplicationPauseListern(self.RegApplicationStatusChg);
            }
        }

        public static void RegApplicationStatusChg(this ReLoginComponent self, bool isPause)
        {
            self.ApplicationStatusChg(isPause).Coroutine();
        }

        public static async ETTask ApplicationStatusChg(this ReLoginComponent self, bool isPause)
        {
            if (self.IsDisposed)
            {
                return;
            }

            Log.Debug($"ET.Client.ReLoginComponentSystem.ApplicationStatusChg isPause[{isPause}]");
            if (isPause)
            {
                if (self.lastPauseTime == 0)
                {
                    self.lastPauseTime = TimeHelper.ClientNow();
                }
                return;
            }
            else
            {
                bool bRet = await self.ChkNeedReLogin();
                Log.Debug($"ChkNeedReLogin bRet[{bRet}] self.isReLogining[{self.isReLogining}] self.isReCreateSession[{self.isNeedReCreateSession}]");
                if (bRet)
                {
                    if (self.isReLogining)
                    {
                        return;
                    }
                    self.isReLogining = true;
                    try
                    {
                        await self.DoReLogin(self.isNeedReCreateSession);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"ET.Client.ReLoginComponentSystem.ApplicationStatusChg {e}");
                    }
                    finally
                    {
                        self.isNeedReCreateSession = false;
                        self.isReLogining = false;
                    }
                }
            }
        }

        public static async ETTask<bool> ChkNeedReLogin(this ReLoginComponent self)
        {
            if (self.IsDisposed)
            {
                return false;
            }

            Scene clientScene = self.DomainScene();
            Session session = ET.Client.SessionHelper.GetSession(clientScene);
            if (session == null)
            {
                return false;
            }

            long timeNow = TimeHelper.ClientNow();
            if (timeNow - self.lastPauseTime > ConstValue.ReCreateSessionTime)
            {
                self.isNeedReCreateSession = true;
            }
            if (Application.isMobilePlatform == false)
            {
                if (timeNow - self.lastPauseTime > ConstValue.ReLoginChkTimeoutTime)
                {
                    self.lastPauseTime = 0;
                    return true;
                }
            }

            if (timeNow - session.LastRecvTime < ConstValue.ReLoginChkTimeoutTime)
            {
                return false;
            }
            return true;
        }

        public static async ETTask DoReLogin(this ReLoginComponent self, bool forceReCreateSession)
        {
            Log.Debug($"==ET.Client.ReLoginComponentSystem.DoReLogin forceReCreateSession[{forceReCreateSession}]");
            if (self.IsDisposed)
            {
                return;
            }

            Scene clientScene = self.DomainScene();
            Session session = ET.Client.SessionHelper.GetSession(clientScene);
            if (session == null)
            {
                return;
            }

            if (forceReCreateSession)
            {
                self.isReCreateSessioning = true;
                (bool bRet, string msg) = await ET.Client.LoginHelper.ReLogin(clientScene);
                if (bRet == false)
                {
                    self.isReCreateSessioning = false;
                    Log.Error($"LoginHelper.ReLogin Err: {msg}");
                    EventSystem.Instance.Publish(clientScene, new EventType.NoticeNetDisconnected());
                    return;
                }
                ReLoginComponent.Instance.isReCreateSessioning = false;
                await ReLoginComponent.Instance.DoAfterReLogin();
            }
            else
            {
                await self.DoAfterReLogin();
            }
        }

        public static async ETTask DoAfterReLogin(this ReLoginComponent self)
        {

            Scene clientScene = self.DomainScene();
            await ET.Client.PlayerHelper.SendGetPlayerStatus(clientScene);

            PlayerStatusComponent playerStatusComponent = ET.Client.PlayerHelper.GetMyPlayerStatusComponent(clientScene);
            Log.Debug($"--ReLoginComponentSystem.ApplicationStatusChg playerStatusComponent.PlayerStatus[{playerStatusComponent.PlayerStatus.ToString()}] playerStatusComponent.RoomType[{playerStatusComponent.RoomType.ToString()}] playerStatusComponent.SubRoomType[{playerStatusComponent.SubRoomType.ToString()}] playerStatusComponent.RoomId[{playerStatusComponent.RoomId}]");
            if (playerStatusComponent.PlayerStatus == PlayerStatus.Hall)
            {
                if (self.IsInRoomUI())
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
                else if (self.IsInBattleUI())
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
                else
                {

                }
            }
            else if (playerStatusComponent.PlayerStatus == PlayerStatus.Room)
            {
                if (self.IsInRoomUI())
                {
                }
                else if (self.IsInBattleUI())
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
                else
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
            }
            else if (playerStatusComponent.PlayerStatus == PlayerStatus.Battle)
            {
                if (self.IsInRoomUI())
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
                else if (self.IsInBattleUI())
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
                else
                {
                    await SceneHelper.EnterHall(self.ClientScene(), false, true);
                }
            }
        }

        public static bool IsInRoomUI(this ReLoginComponent self)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            var _DlgRoom = _UIComponent.GetDlgLogic<DlgRoom>(true);
            if (_DlgRoom != null)
            {
                Log.Debug($"IsInRoomUI true DlgRoom");
                return true;
            }

            var _DlgARRoom = _UIComponent.GetDlgLogic<DlgARRoom>(true);
            if (_DlgARRoom != null)
            {
                Log.Debug($"IsInRoomUI true DlgARRoom");
                return true;
            }

            var _DlgARRoomPVP = _UIComponent.GetDlgLogic<DlgARRoomPVP>(true);
            if (_DlgARRoomPVP != null)
            {
                Log.Debug($"IsInRoomUI true DlgARRoomPVP");
                return true;
            }

            Log.Debug($"IsInRoomUI false");
            return false;
        }

        public static bool IsInBattleUI(this ReLoginComponent self)
        {
            UIComponent _UIComponent = UIManagerHelper.GetUIComponent(self.DomainScene());
            var _DlgBattle = _UIComponent.GetDlgLogic<DlgBattle>(true);
            if (_DlgBattle != null)
            {
                Log.Debug($"IsInBattleUI true DlgBattle");
                return true;
            }

            var _DlgBattleTower = _UIComponent.GetDlgLogic<DlgBattleTower>(true);
            if (_DlgBattleTower != null)
            {
                Log.Debug($"IsInBattleUI true DlgBattleTower");
                return true;
            }

            var _DlgBattleTowerAR = _UIComponent.GetDlgLogic<DlgBattleTowerAR>(true);
            if (_DlgBattleTowerAR != null)
            {
                Log.Debug($"IsInBattleUI true DlgBattleTowerAR");
                return true;
            }

            Log.Debug($"IsInBattleUI false");
            return false;
        }
    }

}