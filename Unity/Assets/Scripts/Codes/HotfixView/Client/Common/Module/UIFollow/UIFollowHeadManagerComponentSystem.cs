using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (UIFollowHeadManagerComponent))]
    public static class UIFollowHeadManagerComponentSystem
    {
        [ObjectSystem]
        public class UIFollowHeadManagerComponentAwakeSystem: AwakeSystem<UIFollowHeadManagerComponent>
        {
            protected override void Awake(UIFollowHeadManagerComponent self)
            {
                UIFollowHeadManagerComponent.Instance = self;
                self.list = new();
            }
        }

        [ObjectSystem]
        public class UIFollowHeadManagerComponentDestroySystem: DestroySystem<UIFollowHeadManagerComponent>
        {
            protected override void Destroy(UIFollowHeadManagerComponent self)
            {
                UIFollowHeadManagerComponent.Instance = null;
            }
        }

        public static void AddList(this UIFollowHeadManagerComponent self, UIFollowHeadComponent uiFollowHeadComponent)
        {
            self.list.Add(uiFollowHeadComponent);
        }

        public static async ETTask SetDefault(this UIFollowHeadManagerComponent self, Camera camera)
        {
            if (camera != null)
            {
                self.camera = camera;
            }
            while (self.isSetDefault == false)
            {
                while (self.camera.transform.position.Equals(Vector3.zero))
                {
                    await TimerComponent.Instance.WaitFrameAsync();
                    if (self.IsDisposed)
                    {
                        return;
                    }
                }

                // Vector3 lastCameraPos = self.camera.transform.position;
                // Log.Error($"---zpb self.camera.transform.position 22[{self.camera.transform.position}]");
                // while (MathHelper.IsEqualFloat3(self.camera.transform.position, lastCameraPos, 0.1f))
                // {
                //     await TimerComponent.Instance.WaitFrameAsync();
                //     if (self.IsDisposed)
                //     {
                //         return;
                //     }
                // }
                // Log.Error($"---zpb self.camera.transform.position 33[{self.camera.transform.position}]");
                // if (self.IsDisposed)
                // {
                //     return;
                // }
                Log.Error($"---zpb self.camera.transform.position 444[{self.camera.transform.position}]");
                self.isSetDefault = true;
            }
            self.defaultPosition = self.camera.transform.position;
            self.defaultRotation = self.camera.transform.rotation;
        }

        public static Vector3 GetDefaultPosition(this UIFollowHeadManagerComponent self)
        {
            return self.defaultPosition;
        }

        public static Quaternion GetDefaultRotation(this UIFollowHeadManagerComponent self)
        {
            return self.defaultRotation;
        }

        public static async ETTask ForceReset(this UIFollowHeadManagerComponent self)
        {
            Log.Error($"===ET.Client.UIFollowHeadManagerComponentSystem.ForceReset [{self.IsDisposed}] [{self.camera}]");
            await self.SetDefault(null);

            self.removeList.Clear();
            for (int i = 0; i < self.list.Count; i++)
            {
                UIFollowHeadComponent uiFollowHeadComponent = self.list[i];
                if (uiFollowHeadComponent == null)
                {
                    self.removeList.Add(i);
                }
                else
                {
                    uiFollowHeadComponent.isForceReset = true;
                }
            }

            for (int i = self.removeList.Count - 1; i >= 0; i--)
            {
                self.list.RemoveAt(self.removeList[i]);
            }
        }
    }
}