using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Server
{
    [Invoke(TimerInvokeType.DynamicMapChecker)]
    public class DynamicMapManagerComponentChecker: ATimer<DynamicMapManagerComponent>
    {
        protected override void Run(DynamicMapManagerComponent self)
        {
            try
            {
                self.Check();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }

    [FriendOf(typeof(DynamicMapManagerComponent))]
    public static class DynamicMapManagerComponentSystem
    {
        public const int CheckInteral = 2000;
        public class DynamicMapManagerComponentAwakeSystem: AwakeSystem<DynamicMapManagerComponent>
        {
            protected override void Awake(DynamicMapManagerComponent self)
            {
                self.dynamicMapList = new();
                self.dynamicUsedIndexList = new();

                self.RepeatedTimer = TimerComponent.Instance.NewRepeatedTimer(DynamicMapManagerComponentSystem.CheckInteral, TimerInvokeType.DynamicMapChecker, self);
            }
        }

        public class DynamicMapManagerComponentDestroySystem: DestroySystem<DynamicMapManagerComponent>
        {
            protected override void Destroy(DynamicMapManagerComponent self)
            {
                TimerComponent.Instance?.Remove(ref self.RepeatedTimer);
            }
        }

        public static int GetDynamicMapIndex(this DynamicMapManagerComponent self)
        {
            for (int i = 10000; i < 30000; i++)
            {
                if (self.dynamicUsedIndexList.Contains(i) == false)
                {
                    return i;
                }
            }
            return -1;
        }

        public static async ETTask<Scene> CreateDynamicMap(this DynamicMapManagerComponent self, RoomComponent roomComponent, List<RoomMember> roomMemberList, ARMeshType _ARMeshType, string _ARSceneId, string _ARSceneMeshId, string _ARMeshDownLoadUrl, byte[] _ARMeshBytes)
        {
            Scene dynamicMapBase = self.GetParent<Scene>();
            var processScenes = StartSceneConfigCategory.Instance.GetByProcess(Options.Instance.Process);
            StartSceneConfig dynamicMapBaseConfig = processScenes[(int)dynamicMapBase.Id];
            int dynamicMapBaseId = self.GetDynamicMapIndex();

            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(dynamicMapBaseConfig.Process, (uint) dynamicMapBaseId);
            long dynamicMapBaseInstanceId = instanceIdStruct.ToLong();

            string gamePlayBattleLevelCfgId = roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId;
            GamePlayBattleLevelCfg gamePlayBattleLevelCfg = GamePlayBattleLevelCfgCategory.Instance.Get(gamePlayBattleLevelCfgId);
            Scene dynamicMapNew = await SceneHelper.CreateServerScene(self, dynamicMapBaseId, dynamicMapBaseInstanceId, dynamicMapBaseConfig.Zone,
                gamePlayBattleLevelCfg.SceneMap, dynamicMapBaseConfig.Type);

            GamePlayComponent gamePlayComponent = dynamicMapNew.AddComponent<GamePlayComponent>();
            await gamePlayComponent.InitWhenRoom(dynamicMapNew.InstanceId, roomComponent.roomTypeInfo.gamePlayBattleLevelCfgId, roomComponent, roomMemberList, _ARMeshType, _ARSceneId, _ARSceneMeshId, _ARMeshDownLoadUrl, _ARMeshBytes);

            self.dynamicMapList.Add(dynamicMapNew.InstanceId, dynamicMapNew.Id);
            self.dynamicUsedIndexList.Add(dynamicMapBaseId);

            roomComponent.Dispose();
            for (int i = 0; i < roomMemberList.Count; i++)
            {
                roomMemberList[i].Dispose();
            }

            return dynamicMapNew;
        }

        public static async ETTask DestroyDynamicMap(this DynamicMapManagerComponent self, long dynamicMapInstanceId)
        {
            if (self.dynamicMapList.ContainsKey(dynamicMapInstanceId) == false)
            {
                return;
            }

            long dynamicMapBaseId = self.dynamicMapList[dynamicMapInstanceId];
            Scene scene = self.GetChild<Scene>(dynamicMapBaseId);
            Log.Debug($"DestroyDynamicMap [{scene.SceneType.ToString()}] [{scene.Id}] [{scene.InstanceId}]");

            self.dynamicMapList.Remove(dynamicMapInstanceId);
            self.dynamicUsedIndexList.Remove((int)dynamicMapBaseId);

            self.RemoveChild(dynamicMapBaseId);

            await ETTask.CompletedTask;
        }

        public static void Check(this DynamicMapManagerComponent self)
        {
            // Session session = self.GetParent<Session>();
            // long timeNow = TimeHelper.ClientNow();
            //
            // if (timeNow - session.LastRecvTime < ConstValue.SessionTimeoutTime && timeNow - session.LastSendTime < ConstValue.SessionTimeoutTime)
            // {
            //     return;
            // }
            //
            // Log.Info($"session timeout: {session.Id} {timeNow} {session.LastRecvTime} {session.LastSendTime} {timeNow - session.LastRecvTime} {timeNow - session.LastSendTime}");
            // session.Error = ErrorCore.ERR_SessionSendOrRecvTimeout;
            //
            // session.Dispose();
        }
    }
}