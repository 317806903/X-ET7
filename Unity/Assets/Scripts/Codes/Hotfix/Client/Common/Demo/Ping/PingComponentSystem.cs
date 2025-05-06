using System;

namespace ET.Client
{
    [ObjectSystem]
    public class PingComponentAwakeSystem: AwakeSystem<PingComponent>
    {
        protected override void Awake(PingComponent self)
        {
            PingComponent.Instance = self;
            PingAsync(self).Coroutine();
        }

        private static async ETTask PingAsync(PingComponent self)
        {
            Session session = self.GetParent<Session>();
            long instanceId = self.InstanceId;

            while (true)
            {
                if (self.InstanceId != instanceId)
                {
                    return;
                }

                long time1 = TimeHelper.ClientNow();
                try
                {
                    int fps = EventSystem.Instance.Invoke<ET.Client.GetFPS, int>(new ET.Client.GetFPS());
                    self.fps = fps;
                    G2C_Ping response = await session.Call(new C2G_Ping()
                    {
                        Fps = self.fps,
                        PingTime = self.Ping,
                    }, false) as G2C_Ping;

                    if (self.InstanceId != instanceId)
                    {
                        return;
                    }

                    long time2 = TimeHelper.ClientNow();
                    self.Ping = time2 - time1;

                    TimeInfo.Instance.ServerMinusClientTime = response.Time + (time2 - time1) / 2 - time2;

                    await TimerComponent.Instance.WaitAsync(ET.ConstValue.PingTime);
                }
                catch (RpcException e)
                {
                    // session断开导致ping rpc报错，记录一下即可，不需要打成error
                    Log.Info($"ping error: {self.Id} {e.Error}");
                    return;
                }
                catch (Exception e)
                {
                    Log.Error($"ping error: \n{e}");
                }
            }
        }
    }

    [ObjectSystem]
    public class PingComponentDestroySystem: DestroySystem<PingComponent>
    {
        protected override void Destroy(PingComponent self)
        {
            PingComponent.Instance = null;
            self.Ping = default;
        }
    }
}