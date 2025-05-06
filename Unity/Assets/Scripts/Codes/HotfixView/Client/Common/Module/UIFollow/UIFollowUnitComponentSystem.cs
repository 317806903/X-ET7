using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UIFollowUnitComponent))]
    public static class UIFollowUnitComponentSystem
    {
        [ObjectSystem]
        public class UIFollowUnitComponentAwakeSystem: AwakeSystem<UIFollowUnitComponent>
        {
            protected override void Awake(UIFollowUnitComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFollowUnitComponentDestroySystem: DestroySystem<UIFollowUnitComponent>
        {
            protected override void Destroy(UIFollowUnitComponent self)
            {
                if (ET.Client.UIManagerHelper.ChkIsOVRCamera() == false)
                {
                    self.transUIRoot.localScale = self.orgUIScale;
                }
            }
        }

        [ObjectSystem]
        public class UIFollowUnitComponentUpdateSystem: UpdateSystem<UIFollowUnitComponent>
        {
            protected override void Update(UIFollowUnitComponent self)
            {
                self.Update();
            }
        }

        public static void Init(this UIFollowUnitComponent self, Transform transUIRoot, Camera mainCamera, long unitId, float3 offset, float rotationXLimit)
        {
            self.orgUIScale = transUIRoot.localScale;
            if (ET.Client.UIManagerHelper.ChkIsOVRCamera() == false)
            {
                float clientResScale = ET.Ability.UnitHelper.GetClientGameResScale(self.DomainScene());
                transUIRoot.localScale *= clientResScale;
            }

            self.transUIRoot = transUIRoot;
            self.MainCamera = mainCamera;
            self.unitId = unitId;
            self.offset = offset;
            self.rotationXLimit = rotationXLimit;

            self.UpdatePos(true);
            self.UpdateRotation(true);
        }

        public static Unit GetUnit(this UIFollowUnitComponent self)
        {
            Scene currentScene = SceneHelper.GetCurrentScene(self.DomainScene());
            Unit unit = ET.Client.UnitHelper.GetUnit(currentScene, self.unitId);
            return unit;
        }

        public static void Update(this UIFollowUnitComponent self)
        {
            if (self.transUIRoot == null)
            {
                return;
            }

            if (self.unitId == 0)
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

        public static void UpdatePos(this UIFollowUnitComponent self, bool isQuick)
        {
            Unit unit = self.GetUnit();
            if (unit == null)
            {
                return;
            }
            float3 pos = unit.GetUnitClientPos();
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
            if (disSqr < 0.01f)
            {
                self.transUIRoot.position = targetPos;
                return;
            }
            float adjustedSmoothing = 0.3f;
            self.transUIRoot.position = Vector3.Lerp(self.transUIRoot.position, targetPos, adjustedSmoothing);
        }

        public static void UpdateRotation(this UIFollowUnitComponent self, bool isQuick)
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
                float rotationSmoothing = 0.1f;
                self.transUIRoot.rotation = Quaternion.Slerp(self.transUIRoot.rotation, targetRotation, rotationSmoothing);
            }
        }
    }
}