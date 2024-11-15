﻿using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ObjectSystem]
    public class LocationProxyComponentAwakeSystem: AwakeSystem<LocationProxyComponent>
    {
        protected override void Awake(LocationProxyComponent self)
        {
            LocationProxyComponent.Instance = self;
        }
    }

    [ObjectSystem]
    public class LocationProxyComponentDestroySystem: DestroySystem<LocationProxyComponent>
    {
        protected override void Destroy(LocationProxyComponent self)
        {
            LocationProxyComponent.Instance = null;
        }
    }

    public static class LocationProxyComponentSystem
    {
        private static long GetLocationSceneId()
        {
            return StartSceneConfigCategory.Instance.LocationConfig.InstanceId;
        }

        public static async ETTask Add(this LocationProxyComponent self, int type, long key, long instanceId)
        {
            Log.Info($"location proxy add {key}, {instanceId} {TimeHelper.ServerNow()}");
            await ActorMessageSenderComponent.Instance.Call(GetLocationSceneId(),
                new ObjectAddRequest() { Type = type, Key = key, InstanceId = instanceId });
        }

        public static async ETTask Lock(this LocationProxyComponent self, int type, long key, long instanceId, int time = 60000)
        {
            Log.Info($"location proxy lock {key}, {instanceId} {TimeHelper.ServerNow()}");
            await ActorMessageSenderComponent.Instance.Call(GetLocationSceneId(),
                new ObjectLockRequest() { Type = type, Key = key, InstanceId = instanceId, Time = time });
        }

        public static async ETTask UnLock(this LocationProxyComponent self, int type, long key, long oldInstanceId, long instanceId)
        {
            Log.Info($"location proxy unlock {key}, {instanceId} {TimeHelper.ServerNow()}");
            await ActorMessageSenderComponent.Instance.Call(GetLocationSceneId(),
                new ObjectUnLockRequest() { Type = type, Key = key, OldInstanceId = oldInstanceId, InstanceId = instanceId });
        }

        public static async ETTask Remove(this LocationProxyComponent self, int type, long key)
        {
            Log.Info($"location proxy add {key}, {TimeHelper.ServerNow()}");
            await ActorMessageSenderComponent.Instance.Call(GetLocationSceneId(),
                new ObjectRemoveRequest() { Type = type, Key = key });
        }

        public static async ETTask<long> Get(this LocationProxyComponent self, int type, long key, long sceneInstanceId)
        {
            if (key == 0)
            {
                throw new Exception($"get location key 0");
            }

            // location server配置到共享区，一个大战区可以配置N多个location server,这里暂时为1
            ObjectGetResponse response =
                    (ObjectGetResponse) await ActorMessageSenderComponent.Instance.Call(GetLocationSceneId(),
                        new ObjectGetRequest() { Type = type, Key = key, SceneInstanceId = sceneInstanceId });
            return response.InstanceId;
        }

        public static async ETTask<List<long>> ChkObjectListExist(this LocationProxyComponent self, int type, List<long> keyList, long sceneInstanceId)
        {
            // location server配置到共享区，一个大战区可以配置N多个location server,这里暂时为1
            ChkObjectListExistResponse response =
                    (ChkObjectListExistResponse) await ActorMessageSenderComponent.Instance.Call(GetLocationSceneId(),
                        new ChkObjectListExistRequest()
                        {
                            Type = type,
                            KeyList = keyList,
                            SceneInstanceId = sceneInstanceId
                        });
            return response.NotExistKeyList;
        }

        public static async ETTask AddLocation(this Entity self, int type)
        {
            await LocationProxyComponent.Instance.Add(type, self.Id, self.InstanceId);
        }

        public static async ETTask RemoveLocation(this Entity self, int type)
        {
            await LocationProxyComponent.Instance.RemoveLocation(self.Id, type);
        }

        public static async ETTask RemoveLocation(this LocationProxyComponent self, long id, int type)
        {
            await LocationProxyComponent.Instance.Remove(type, id);
            await TimerComponent.Instance.WaitFrameAsync();
            ActorLocationSenderComponent.Instance?.Get(type).Remove(id);
        }
    }
}