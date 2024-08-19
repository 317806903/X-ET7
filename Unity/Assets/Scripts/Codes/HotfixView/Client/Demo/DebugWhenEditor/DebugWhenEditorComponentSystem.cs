using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DebugWhenEditorComponent))]
    public static class DebugWhenEditorComponentSystem
    {
        [ObjectSystem]
        public class DebugWhenEditorComponentAwakeSystem: AwakeSystem<DebugWhenEditorComponent>
        {
            protected override void Awake(DebugWhenEditorComponent self)
            {
                DebugWhenEditorComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DebugWhenEditorComponentDestroySystem: DestroySystem<DebugWhenEditorComponent>
        {
            protected override void Destroy(DebugWhenEditorComponent self)
            {
                DebugWhenEditorComponent.Instance = null;

            }
        }

        [ObjectSystem]
        public class DebugWhenEditorComponentUpdateSystem: UpdateSystem<DebugWhenEditorComponent>
        {
            protected override void Update(DebugWhenEditorComponent self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this DebugWhenEditorComponent self, Transform DebugRoot)
        {
            self.Root = DebugRoot;
            self.IsShowShootDamageNum = false;
            self.IsStopActorMove = false;

            self.AddIngameDebugConsoleCommand();

            await ETTask.CompletedTask;
        }

        public static void Update(this DebugWhenEditorComponent self)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                self.IsStopActorMove = !self.IsStopActorMove;
                self.SetStopActorMoveWhenDebug();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                self.IsShowShootDamageNum = !self.IsShowShootDamageNum;
            }
#endif
        }

        public static Scene GetClientScene(this DebugWhenEditorComponent self)
        {
            Scene clientScene = null;
            var childs = ClientSceneManagerComponent.Instance.Children;
            foreach (var child in childs.Values)
            {
                Scene scene = (Scene)child;
                if (scene.SceneType == SceneType.Client)
                {
                    clientScene = scene;
                    break;
                }
            }

            if (clientScene == null)
            {
                return null;
            }
            return clientScene;
        }

        public static void AddIngameDebugConsoleCommand(this DebugWhenEditorComponent self)
        {
            IngameDebugConsole.DebugLogConsole.AddCommand("SeeDebugConnectList", "SeeDebugConnectList desc",
                () => ET.Client.DebugConnectHelper.SeeDebugConnectList());
            IngameDebugConsole.DebugLogConsole.AddCommand("SeeCurDebugConnect", "SeeCurDebugConnect desc",
                () => ET.Client.DebugConnectHelper.SeeCurDebugConnect());
            IngameDebugConsole.DebugLogConsole.AddCommand("SetDebugConnectNull", "SetDebugConnectNull desc",
                () => ET.Client.DebugConnectHelper.SetDebugConnectNull());
            IngameDebugConsole.DebugLogConsole.AddCommand<string>("SetDebugConnect", "SetDebugConnect desc",
                (str) => ET.Client.DebugConnectHelper.SetDebugConnect(str));

            IngameDebugConsole.DebugLogConsole.AddCommand("SetStopActorMoveWhenDebug", "SetStopActorMoveWhenDebug", () =>
            {
                self.SetStopActorMoveWhenDebug();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand("ForceGameEndWhenDebug", "ForceGameEndWhenDebug", () =>
            {
                self.SendForceGameEndWhenDebug();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<string, int, int>("SetMyRankScoreWhenDebug", "SetMyRankScoreWhenDebug", (rankType, score, killNum) =>
            {
                RankType rankType2 = (RankType)Enum.Parse(typeof(RankType), rankType);
                self.SetMyRankScoreWhenDebug(rankType2, score, killNum).Coroutine();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<string>("ClearRankWhenDebug", "ClearRankWhenDebug", (rankType) =>
            {
                RankType rankType2 = (RankType)Enum.Parse(typeof(RankType), rankType);
                self.ClearRankWhenDebug(rankType2).Coroutine();
            });
            IngameDebugConsole.DebugLogConsole.AddCommand<string, int>("ClearPlayerRankWhenDebug", "ClearPlayerRankWhenDebug", (rankType, playerId) =>
            {
                RankType rankType2 = (RankType)Enum.Parse(typeof(RankType), rankType);
                self.ClearPlayerRankWhenDebug(rankType2, playerId).Coroutine();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<int, string>("ResetMyFunctionMenuStatusWhenDebug", "ResetMyFunctionMenuStatusWhenDebug", (operateType, functionMenuCfgIds) =>
            {
                self.ResetMyFunctionMenuStatusWhenDebug(operateType, functionMenuCfgIds).Coroutine();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<int, int, string>("ResetPlayerFunctionMenuStatusWhenDebug", "ResetPlayerFunctionMenuStatusWhenDebug", (playerId, operateType, functionMenuCfgIds) =>
            {
                self.ResetPlayerFunctionMenuStatusWhenDebug(playerId, operateType, functionMenuCfgIds).Coroutine();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<int>("ShowPoolDictCount", "ShowPoolDictCount", (showCount) =>
            {
                ET.Client.GameObjectPoolHelper.ShowPoolDictCount(showCount);
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<int>("OpenSeasonRoomWhenDebug", "OpenSeasonRoomWhenDebug", (index) =>
            {
                self.OpenSeasonRoomWhenDebug(index).Coroutine();
            });

            IngameDebugConsole.DebugLogConsole.AddCommand<string, bool>("ShowRedDotNode", "ShowRedDotNode", (target, isShow) =>
            {
                self.ShowRedDotNodeWhenDebug(target, isShow).Coroutine();
            });
        }

        public static void SetStopActorMoveWhenDebug(this DebugWhenEditorComponent self)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }
            ET.Client.GamePlayHelper.SendSetStopActorMoveWhenDebug(clientScene, self.IsStopActorMove).Coroutine();
        }

        public static void SendForceGameEndWhenDebug(this DebugWhenEditorComponent self)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }
            ET.Client.GamePlayHelper.SendForceGameEndWhenDebug(clientScene).Coroutine();
        }

        public static async ETTask SetMyRankScoreWhenDebug(this DebugWhenEditorComponent self, RankType rankType, int score, int killNum)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            if (ET.Client.SessionHelper.ChkSessionExist(clientScene) == false)
            {
                return;
            }

            C2G_SetMyRankScoreWhenDebug _C2G_SetMyRankScoreWhenDebug = new ();
            _C2G_SetMyRankScoreWhenDebug.RankType = (int)rankType;
            _C2G_SetMyRankScoreWhenDebug.Score = score;
            _C2G_SetMyRankScoreWhenDebug.KillNum = killNum;
            ET.Client.SessionHelper.GetSession(clientScene).Send(_C2G_SetMyRankScoreWhenDebug);
            await ETTask.CompletedTask;
        }

        public static async ETTask ClearRankWhenDebug(this DebugWhenEditorComponent self, RankType rankType)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            if (ET.Client.SessionHelper.ChkSessionExist(clientScene) == false)
            {
                return;
            }

            C2G_ClearRankWhenDebug _C2G_ClearRankWhenDebug = new ();
            _C2G_ClearRankWhenDebug.RankType = (int)rankType;
            ET.Client.SessionHelper.GetSession(clientScene).Send(_C2G_ClearRankWhenDebug);
            await ETTask.CompletedTask;
        }

        public static async ETTask ClearPlayerRankWhenDebug(this DebugWhenEditorComponent self, RankType rankType, long playerId)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            if (ET.Client.SessionHelper.ChkSessionExist(clientScene) == false)
            {
                return;
            }

            C2G_ClearPlayerRankWhenDebug _C2G_ClearPlayerRankWhenDebug = new ();
            _C2G_ClearPlayerRankWhenDebug.RankType = (int)rankType;
            _C2G_ClearPlayerRankWhenDebug.PlayerId = playerId;
            ET.Client.SessionHelper.GetSession(clientScene).Send(_C2G_ClearPlayerRankWhenDebug);
            await ETTask.CompletedTask;
        }

        public static async ETTask ResetMyFunctionMenuStatusWhenDebug(this DebugWhenEditorComponent self, int operateType, string functionMenuCfgIds)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            if (ET.Client.SessionHelper.ChkSessionExist(clientScene) == false)
            {
                return;
            }

            await self.ResetPlayerFunctionMenuStatusWhenDebug(-1, operateType, functionMenuCfgIds);
            await ETTask.CompletedTask;
        }

        public static async ETTask ResetPlayerFunctionMenuStatusWhenDebug(this DebugWhenEditorComponent self, long playerId, int operateType, string functionMenuCfgIds)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            if (ET.Client.SessionHelper.ChkSessionExist(clientScene) == false)
            {
                return;
            }

            C2G_ResetPlayerFunctionMenuStatusWhenDebug _C2G_ResetPlayerFunctionMenuStatusWhenDebug = new ();
            _C2G_ResetPlayerFunctionMenuStatusWhenDebug.PlayerId = playerId;
            // 1 强制打开
            // 2 强制关闭
            // 3 按照条件调整
            _C2G_ResetPlayerFunctionMenuStatusWhenDebug.OperateType = operateType;
            // All 全部
            // AllLock 全部被锁的
            // AllOpenned 全部已打开的
            // 具体 menu1;menu2;...
            _C2G_ResetPlayerFunctionMenuStatusWhenDebug.FunctionMenuCfgIds = functionMenuCfgIds;
            ET.Client.SessionHelper.GetSession(clientScene).Send(_C2G_ResetPlayerFunctionMenuStatusWhenDebug);
            await ETTask.CompletedTask;
        }

        public static async ETTask OpenSeasonRoomWhenDebug(this DebugWhenEditorComponent self, int index)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            RoomType roomType = RoomType.Normal;
            SubRoomType subRoomType = SubRoomType.NormalPVE;

            RoomTypeInfo roomTypeInfo = ET.GamePlayHelper.GetRoomTypeInfo(roomType, subRoomType, 1, index + 1, "");

            await RoomHelper.QuitRoomAsync(clientScene);
            (bool result, long roomId) = await RoomHelper.CreateRoomAsync(clientScene, roomTypeInfo);
            if (result)
            {
                await ET.Client.UIManagerHelper.EnterRoomUI(clientScene);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask ShowRedDotNodeWhenDebug(this DebugWhenEditorComponent self, string target, bool isShow)
        {
            Scene clientScene = self.GetClientScene();
            if (clientScene == null)
            {
                return;
            }

            if (isShow)
            {
                ET.Client.UIRedDotHelper.ShowRedDotNode(clientScene, target);
            }
            else
            {
                ET.Client.UIRedDotHelper.HideRedDotNode(clientScene, target);
            }

            await ETTask.CompletedTask;
        }

    }
}