using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UIFollowPosComponent))]
    public static class UIFollowPosComponentSystem
    {
        [ObjectSystem]
        public class UIFollowPosComponentAwakeSystem: AwakeSystem<UIFollowPosComponent>
        {
            protected override void Awake(UIFollowPosComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFollowPosComponentDestroySystem: DestroySystem<UIFollowPosComponent>
        {
            protected override void Destroy(UIFollowPosComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFollowPosComponentUpdateSystem: UpdateSystem<UIFollowPosComponent>
        {
            protected override void Update(UIFollowPosComponent self)
            {
                self.Update();
            }
        }

        public static void Init(this UIFollowPosComponent self, Transform transUIRoot, Camera mainCamera, float3 pos, float3 offset, float rotationXLimit)
        {
            self.transUIRoot = transUIRoot;
            self.MainCamera = mainCamera;
            self.pos = pos;
            self.offset = offset;
            self.rotationXLimit = rotationXLimit;

            self.UpdatePos(true);
            self.UpdateRotation(true);
        }

        public static void Update(this UIFollowPosComponent self)
        {
            if (self.transUIRoot == null)
            {
                return;
            }

            if (self.MainCamera == null)
            {
                return;
            }

            self.UpdatePos(false);
            self.UpdateRotation(false);
        }

        public static void UpdatePos(this UIFollowPosComponent self, bool isQuick)
        {
            float3 pos = self.pos;
            Vector3 targetPos = Vector3.zero;
            if (self.offset.Equals(float3.zero))
            {
                targetPos = (Vector3)pos;
            }
            else
            {
                Quaternion rotation = self.MainCamera.transform.rotation;
                targetPos = (Vector3)pos + Quaternion.Euler(0, rotation.eulerAngles.y, 0) * (Vector3)self.offset;
            }

            if (self.transUIRoot.position.Equals(targetPos))
            {
                return;
            }
            if (isQuick)
            {
                self.transUIRoot.position = targetPos;
                return;
            }
            float disSqr = (targetPos - self.transUIRoot.position).sqrMagnitude;
            if (disSqr < 0.2f)
            {
                self.transUIRoot.position = targetPos;
                return;
            }
            float adjustedSmoothing = 0.008f;
            self.transUIRoot.position = Vector3.Lerp(self.transUIRoot.position, targetPos, adjustedSmoothing);
        }

        public static void UpdateRotation(this UIFollowPosComponent self, bool isQuick)
        {
            Quaternion targetRotation = Quaternion.LookRotation(self.transUIRoot.position - self.MainCamera.transform.position);
            if (self.rotationXLimit == 0)
            {
            }
            else
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