using System;
using System.Net;

namespace ET.Client
{
    [ObjectSystem]
    public class RouterCheckComponentAwakeSystem: AwakeSystem<RouterCheckComponent>
    {
        protected override void Awake(RouterCheckComponent self)
        {
            CheckAsync(self).Coroutine();
        }

        private static async ETTask CheckAsync(RouterCheckComponent self)
        {
            Session session = self.GetParent<Session>();
            Scene scene = self.DomainScene();
            Scene clientScene = self.ClientScene();
            long instanceId = self.InstanceId;

            while (true)
            {
                await TimerComponent.Instance.WaitAsync(1000);

                if (self.IsDisposed)
                {
                    return;
                }

                if (session == null || session.IsDisposed)
                {
                    return;
                }
                if (scene == null || scene.IsDisposed)
                {
                    return;
                }

                if (self.InstanceId != instanceId)
                {
                    return;
                }

                if (self.InstanceId != instanceId)
                {
                    return;
                }

                long time = TimeHelper.ClientFrameTime();

                if (time - session.LastRecvTime < 7 * 1000)
                {
                    self.retryIndex = 0;
                    continue;
                }

                try
                {
                    if (self.ClientScene() == null)
                    {
                        EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                        return;
                    }
                    long sessionId = session.Id;

                    (uint localConn, uint remoteConn) = await NetServices.Instance.GetChannelConn(session.ServiceId, sessionId);

                    if (self.ClientScene() == null)
                    {
                        EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                        return;
                    }
                    IPEndPoint realAddress = session.RemoteAddress;
                    Log.Info($"get recvLocalConn start: {self.ClientScene().Id} {realAddress} {localConn} {remoteConn} self.retryIndex={self.retryIndex}");

                    self.retryIndex++;
                    (uint recvLocalConn, IPEndPoint routerAddress) = await RouterHelper.GetRouterAddress(self.ClientScene(), realAddress, localConn, remoteConn);
                    if (self.ClientScene() == null)
                    {
                        EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                        return;
                    }
                    if (recvLocalConn == 0)
                    {
                        Log.Error($"get recvLocalConn fail: {self.ClientScene().Id} {routerAddress} {realAddress} {localConn} {remoteConn} self.retryIndex={self.retryIndex}");
                        if (self.retryIndex >= self.retryNum)
                        {
                            self.retryIndex = 0;
                            EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                            return;
                        }
                        continue;
                    }

                    Log.Info($"get recvLocalConn ok: {self.ClientScene().Id} {routerAddress} {realAddress} {recvLocalConn} {localConn} {remoteConn} self.retryIndex={self.retryIndex}");

                    if (self.retryIndex >= self.retryNum)
                    {
                        self.retryIndex = 0;
                        EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected()
                        {
                            bReLogin = true,
                        });
                        return;
                    }

                    if (self.IsDisposed)
                    {
                        return;
                    }

                    if (session == null || session.IsDisposed)
                    {
                        return;
                    }
                    if (scene == null || scene.IsDisposed)
                    {
                        return;
                    }

                    if (self.InstanceId != instanceId)
                    {
                        return;
                    }

                    //session.LastRecvTime = TimeHelper.ClientNow();

                    NetServices.Instance.ChangeAddress(session.ServiceId, sessionId, routerAddress);
                }
                catch (Exception e)
                {
                    Log.Error($"Exception: {e}");

                    EventSystem.Instance.Publish(scene, new EventType.NoticeNetDisconnected());
                    return;
                }
            }
        }
    }
}