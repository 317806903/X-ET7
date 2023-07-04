using System;
using System.Collections.Generic;
using System.Linq;

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
                self.dynamicUsedIndex = new();
                
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
            for (int i = 10000; i < 20000; i++)
            {
                if (self.dynamicUsedIndex.Contains(i) == false)
                {
                    return i;
                }
            }
            return -1;
        }

        public static async ETTask<Scene> CreateDynamicMap(this DynamicMapManagerComponent self, RoomComponent roomComponent, List<RoomMember> roomMemberList)
        {
            Scene dynamicMapBase = self.GetParent<Scene>();
            var processScenes = StartSceneConfigCategory.Instance.GetByProcess(Options.Instance.Process);
            StartSceneConfig dynamicMapBaseConfig = processScenes[(int)dynamicMapBase.Id];
            int dynamicMapBaseId = self.GetDynamicMapIndex();
            
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(dynamicMapBaseConfig.Process, (uint) dynamicMapBaseId);
            long dynamicMapBaseInstanceId = instanceIdStruct.ToLong();
            
            //Scene dynamicMapNew = await SceneFactory.CreateServerScene(ServerSceneManagerComponent.Instance, dynamicMapBaseId, dynamicMapBaseInstanceId, dynamicMapBaseConfig.Zone, sceneMapName, dynamicMapBaseConfig.Type);
            Scene dynamicMapNew = await SceneFactory.CreateServerScene(self, dynamicMapBaseId, dynamicMapBaseInstanceId, dynamicMapBaseConfig.Zone, 
                roomComponent.sceneName, dynamicMapBaseConfig.Type);

            dynamicMapNew.AddComponent(roomComponent);
            for (int i = 0; i < roomMemberList.Count; i++)
            {
                roomComponent.AddChild(roomMemberList[i]);
            }

            dynamicMapNew.AddComponent<GamePlayComponent>();
            
            self.dynamicMapList.Add(dynamicMapNew.InstanceId, dynamicMapNew.Id);
            self.dynamicUsedIndex.Add(dynamicMapBaseId);
            
            return dynamicMapNew;
        }
        
        public static async ETTask DestroyDynamicMap(this DynamicMapManagerComponent self, long dynamicMapInstanceId)
        {
            if (self.dynamicMapList.ContainsKey(dynamicMapInstanceId) == false)
            {
                return;
            }

            long dynamicMapId = self.dynamicMapList[dynamicMapInstanceId];
            self.dynamicMapList.Remove(dynamicMapInstanceId);
            self.dynamicUsedIndex.Remove((int)dynamicMapId);

            self.RemoveChild(dynamicMapId);

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
        
        //轮询一段时间后，判断player是否断线一段时间，是的话踢出 Map，踢出房间，
        //      当没有玩家的时候，退出 DynamicMap，并关闭房间
    }
}