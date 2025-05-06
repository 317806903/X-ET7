using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (UIFrontOfHeadComponent))]
    public static class UIFrontOfHeadComponentSystem
    {
        [ObjectSystem]
        public class UIFrontOfHeadComponentAwakeSystem: AwakeSystem<UIFrontOfHeadComponent>
        {
            protected override void Awake(UIFrontOfHeadComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFrontOfHeadComponentDestroySystem: DestroySystem<UIFrontOfHeadComponent>
        {
            protected override void Destroy(UIFrontOfHeadComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFrontOfHeadComponentLateUpdateSystem: LateUpdateSystem<UIFrontOfHeadComponent>
        {
            protected override void LateUpdate(UIFrontOfHeadComponent self)
            {
                self.Update();
            }
        }

        public static void Init(this UIFrontOfHeadComponent self, Transform transUIRoot, Camera mainCamera, float3 offset, float rotationXLimit)
        {
            self.transUIRoot = transUIRoot;
            self.MainCamera = mainCamera;
            self.offset = offset;
            self.rotationXLimit = rotationXLimit;
            self.UpdatePos(true);
            self.UpdateRotation(true);
        }

        public static void Update(this UIFrontOfHeadComponent self)
        {
            if (self.transUIRoot == null)
            {
                return;
            }

            if (self.MainCamera == null)
            {
                return;
            }

            self.UpdateRotation(false);
        }

        public static void UpdatePos(this UIFrontOfHeadComponent self, bool isQuick)
        {
            float3 pos = self.MainCamera.transform.position;

            Vector3 targetPos = Vector3.zero;
            if (self.offset.Equals(float3.zero))
            {
                targetPos = pos;
            }
            else
            {
                Vector3 eulerAngles = self.MainCamera.transform.eulerAngles;
                Quaternion rotation = Quaternion.Euler(0, eulerAngles.y, 0);
                targetPos = (Vector3)pos + rotation * self.offset;
            }

            self.transUIRoot.position = targetPos;
        }

        public static void UpdateRotation(this UIFrontOfHeadComponent self, bool isQuick)
        {
            Quaternion targetRotation = Quaternion.LookRotation(self.transUIRoot.position - self.MainCamera.transform.position);
            // if (self.rotationXLimit == 0)
            // {
            // }
            // else
            {
                Vector3 rot = targetRotation.eulerAngles;
                if (rot.x >= 0 && rot.x < 180)
                {
                    targetRotation = Quaternion.Euler(new Vector3(Mathf.Min(rot.x, self.rotationXLimit), rot.y, rot.z));
                }
            }

            if (self.transUIRoot.rotation.Equals(targetRotation))
            {
                return;
            }

            if (isQuick)
            {
                self.transUIRoot.rotation = targetRotation;
                return;
            }
            float angle = Quaternion.Angle(self.transUIRoot.rotation, targetRotation);
            if (angle <= 2)
            {
                self.transUIRoot.rotation = targetRotation;
            }
            else
            {
                float rotationSmoothing = 0.01f;
                self.transUIRoot.rotation = Quaternion.Slerp(self.transUIRoot.rotation, targetRotation, rotationSmoothing);
            }
        }
    }
}