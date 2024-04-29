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

                self.UpdateShow();
            }
        }

        public static void UpdateShow(this DebugShowComponent self)
        {
            if (self.showFPS == null || self.pingComponent == null)
            {
                return;
            }

            if (self.Root.gameObject.activeSelf && self.showFPS.gameObject.activeInHierarchy)
            {
                self.showFPS.ExShow = $"{self.pingComponent.Ping}ms";
                if (self.arCameraPosition.Equals(Vector3.zero) && self.arCameraEulerAngles.Equals(Vector3.zero) )
                {
                    self.showFPS.ExShow2 = "";
                }
                else
                {
                    self.showFPS.ExShow2 = $"{self.arCameraPosition} {self.arCameraEulerAngles}";
                }
            }
        }

        public static void SetDebugPose(this DebugShowComponent self, Pose? pose)
        {
            if (pose.HasValue)
            {
                self.arCameraPosition = pose.Value.position;
                self.arCameraEulerAngles = pose.Value.rotation.eulerAngles;
            }
            else
            {
                self.arCameraPosition = Vector3.zero;
                self.arCameraEulerAngles = Vector3.zero;
            }
        }
    }
}