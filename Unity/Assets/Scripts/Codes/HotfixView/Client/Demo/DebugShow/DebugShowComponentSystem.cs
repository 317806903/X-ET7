using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DebugShowComponent))]
    public static class DebugShowComponentSystem
    {
        [ObjectSystem]
        public class DebugShowComponentAwakeSystem: AwakeSystem<DebugShowComponent>
        {
            protected override void Awake(DebugShowComponent self)
            {
                DebugShowComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DebugShowComponentDestroySystem: DestroySystem<DebugShowComponent>
        {
            protected override void Destroy(DebugShowComponent self)
            {
                DebugShowComponent.Instance = null;

            }
        }

        [ObjectSystem]
        public class DebugShowComponentUpdateSystem: UpdateSystem<DebugShowComponent>
        {
            protected override void Update(DebugShowComponent self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this DebugShowComponent self, Transform DebugRoot)
        {
            self.Root = DebugRoot;

            Transform transShowFPS = self.Root.Find("ShowFPS");
            self.showFPS = transShowFPS.gameObject.GetComponent<ShowFPS>();

            await ETTask.CompletedTask;
        }

        public static void SetPing(this DebugShowComponent self, PingComponent pingComponent)
        {
            self.pingComponent = pingComponent;
        }

        public static void Update(this DebugShowComponent self)
        {
            if (++self.curFrameUpdate >= self.waitFrameUpdate)
            {
                self.curFrameUpdate = 0;

                self.UpdatePing();
            }
        }

        public static void UpdatePing(this DebugShowComponent self)
        {
            if (self.showFPS == null || self.pingComponent == null)
            {
                return;
            }

            if (self.showFPS.gameObject.activeInHierarchy)
            {
                self.showFPS.ExShow = $"{self.pingComponent.Ping}ms";
            }
        }
    }
}