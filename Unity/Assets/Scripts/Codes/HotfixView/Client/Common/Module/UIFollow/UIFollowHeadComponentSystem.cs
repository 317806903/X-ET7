using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof (UIFollowHeadComponent))]
    public static class UIFollowHeadComponentSystem
    {
        [ObjectSystem]
        public class UIFollowHeadComponentAwakeSystem: AwakeSystem<UIFollowHeadComponent>
        {
            protected override void Awake(UIFollowHeadComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFollowHeadComponentDestroySystem: DestroySystem<UIFollowHeadComponent>
        {
            protected override void Destroy(UIFollowHeadComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UIFollowHeadComponentUpdateSystem: UpdateSystem<UIFollowHeadComponent>
        {
            protected override void Update(UIFollowHeadComponent self)
            {
                self.Update();
            }
        }

        public static void Init(this UIFollowHeadComponent self, Transform transUIRoot, Camera mainCamera, UIFollowHeadCfg uiFollowHeadCfg)
        {
            self.transUIRoot = transUIRoot;
            self.MainCamera = mainCamera;
            self.uiFollowHeadCfg = uiFollowHeadCfg;

            float3 offsetValue = new float3(0, -0.2f, uiFollowHeadCfg.OffsetDis);
            offsetValue += new float3(0, 0, - 0.01f * EUIRootHelper.GetCanvasSortingOrder(transUIRoot));

            self.offset = offsetValue;

            self.UpdatePos(true);
            self.UpdateRotation(true);
        }

        public static void Update(this UIFollowHeadComponent self)
        {
            if (self.transUIRoot == null)
            {
                return;
            }

            if (self.MainCamera == null)
            {
                return;
            }

            if (self.isForceReset)
            {
                self.isForceReset = false;
                self.UpdatePos(true);
                self.UpdateRotation(true);
                return;
            }
            if (self.lastRecordPosition.Equals(Vector3.zero))
            {
                self.UpdatePos(true);
                self.UpdateRotation(true);
                return;
            }
            else
            {
                self.UpdatePosToTarget();

                self.UpdateRotation(false);

                if (self.isNeedMove == false)
                {
                    self.ChkIsNeedMove();
                    if (self.isNeedMove)
                    {
                        self.startTime = Time.time;
                    }
                }
                self.ChkIsStaying();

                // self.ChkIsNeedMove();
                // if (self.isStaying == true)
                // {
                //     self.ChkIsStaying();
                //     if (self.isStaying == false)
                //     {
                //         self.isFirstMove = true;
                //     }
                // }
                // else
                // {
                //     self.ChkIsStaying();
                // }
            }
            if (self.isNeedMove == false)
            {
                return;
            }
            if (self.isStaying == false)
            {
                return;
            }

            self.UpdatePos(false);
            self.UpdateRotation(false);
        }

        public static void ChkIsNeedMove(this UIFollowHeadComponent self)
        {
            if (self.uiFollowHeadCfg.MoveSpeed == 0)
            {
                return;
            }

            if (self.isNeedMove)
            {
                return;
            }

            float posDis = self.uiFollowHeadCfg.HoldDis;
            Vector3 pos = self.MainCamera.transform.position;
            if (MathHelper.IsEqualFloat3(self.lastRecordPosition, pos, posDis) == false)
            {
                self.isNeedMove = true;
                return;
            }

            float rotationDis = self.uiFollowHeadCfg.HoldRotation;
            if (UIManagerHelper.ChkIsDebug() && rotationDis < 90)
            {
                rotationDis = 90f;
            }
            Quaternion rotation = self.MainCamera.transform.rotation;
            float angle = Quaternion.Angle(self.lastRecordRotation, rotation);
            if (angle > rotationDis)
            {
                self.isNeedMove = true;
                return;
            }
        }

        public static void ChkIsStaying(this UIFollowHeadComponent self)
        {
            self.isStaying = true;
            return;

            if (self.uiFollowHeadCfg.MoveSpeed == 0)
            {
                return;
            }

            if (self.isNeedMove && self.isStaying)
            {
                return;
            }

            if (self.lastPosition.Equals(Vector3.zero))
            {
                self.isStaying = false;
                self.chkStayIndex = 0;
                self.lastPosition = self.MainCamera.transform.position;
                self.lastRotation = self.MainCamera.transform.rotation;
                return;
            }

            float posDis = 0.1f;
            Vector3 pos = self.MainCamera.transform.position;
            if (MathHelper.IsEqualFloat3(self.lastPosition, pos, posDis) == false)
            {
                self.isStaying = false;
                self.lastPosition = self.MainCamera.transform.position;
                self.lastRotation = self.MainCamera.transform.rotation;
                return;
            }

            float rotationDis = 0.1f;
            Quaternion rotation = self.MainCamera.transform.rotation;
            float angle = Quaternion.Angle(self.lastRotation, rotation);
            if (angle >= rotationDis)
            {
                self.isStaying = false;
                self.lastPosition = self.MainCamera.transform.position;
                self.lastRotation = self.MainCamera.transform.rotation;
                return;
            }

            if (self.chkStayIndex++ > self.chkStayCount)
            {
                self.isStaying = true;
                self.chkStayIndex = 0;
            }
        }

        public static void UpdatePos(this UIFollowHeadComponent self, bool isQuick)
        {
            Vector3 pos = self.MainCamera.transform.position;

            if (self.uiFollowHeadCfg.IsCreateAtDefault)
            {
                pos = UIFollowHeadManagerComponent.Instance.GetDefaultPosition();
            }

            Vector3 targetPos = Vector3.zero;
            if (self.offset.Equals(float3.zero))
            {
                targetPos = pos;
            }
            else
            {
                if (self.uiFollowHeadCfg.IsKeepHeight)
                {
                    Vector3 eulerAngles = self.MainCamera.transform.eulerAngles;
                    if (self.uiFollowHeadCfg.IsCreateAtDefault)
                    {
                        Quaternion rotationTmp = UIFollowHeadManagerComponent.Instance.GetDefaultRotation();
                        eulerAngles = rotationTmp.eulerAngles;
                    }

                    Quaternion rotation = Quaternion.Euler(0, eulerAngles.y, 0);
                    targetPos = (Vector3)pos + rotation * self.offset;
                }
                else
                {
                    Quaternion rotation = self.MainCamera.transform.rotation;

                    if (self.uiFollowHeadCfg.IsCreateAtDefault)
                    {
                        rotation = UIFollowHeadManagerComponent.Instance.GetDefaultRotation();
                    }

                    targetPos = (Vector3)pos + rotation * self.offset;
                }
            }

            if (isQuick)
            {
                self.targetPosition = Vector3.zero;

                self.transUIRoot.position = targetPos;

                self.isNeedMove = false;
                self.lastRecordPosition = self.MainCamera.transform.position;
                self.lastRecordRotation = self.MainCamera.transform.rotation;

                // self.isStaying = false;
                // self.lastPosition = self.MainCamera.transform.position;
                // self.lastRotation = self.MainCamera.transform.rotation;
                return;
            }

            self.targetPosition = targetPos;

            if (MathHelper.IsEqualFloat3(targetPos, self.transUIRoot.position, 0.2f))
            {
                self.isNeedMove = false;
                self.lastRecordPosition = self.MainCamera.transform.position;
                self.lastRecordRotation = self.MainCamera.transform.rotation;

                // self.isStaying = false;
                // self.lastPosition = self.MainCamera.transform.position;
                // self.lastRotation = self.MainCamera.transform.rotation;
                return;
            }
        }

        public static void UpdatePosToTarget(this UIFollowHeadComponent self)
        {
            if (self.uiFollowHeadCfg.MoveSpeed == 0)
            {
                return;
            }

            if (self.targetPosition.Equals(Vector3.zero))
            {
                return;
            }

            if (MathHelper.IsEqualFloat3(self.targetPosition, self.transUIRoot.position, 0.001f))
            {
                self.transUIRoot.position = self.targetPosition;
                self.targetPosition = Vector3.zero;
                self.UpdateRotation(true);
                return;
            }

            float time = self.uiFollowHeadCfg.MoveSpeed;
            float timeFirst = 0f;
            time = timeFirst + (time - timeFirst) * Mathf.Clamp01((Time.time - self.startTime) / self.uiFollowHeadCfg.MoveSpeedWhenToTop);

            self.transUIRoot.position = Vector3.Lerp(self.transUIRoot.position, self.targetPosition, time);

        }

        public static void UpdateRotation(this UIFollowHeadComponent self, bool isQuick)
        {
            if (self.uiFollowHeadCfg.RotationSpeed == 0 && isQuick == false)
            {
                return;
            }

            Vector3 pos = self.MainCamera.transform.position;

            if (self.uiFollowHeadCfg.IsCreateAtDefault)
            {
                pos = UIFollowHeadManagerComponent.Instance.GetDefaultPosition();
            }

            Quaternion targetRotation = Quaternion.LookRotation(self.transUIRoot.position - pos);
            // if (self.rotationXLimit == 0)
            // {
            // }
            // else
            {
                Vector3 rot = targetRotation.eulerAngles;
                if (rot.x >= 0 && rot.x < 180)
                {
                    targetRotation = Quaternion.Euler(new Vector3(Mathf.Min(rot.x, self.uiFollowHeadCfg.RotationXLimit), rot.y, Mathf.Min(rot.z, self.uiFollowHeadCfg.RotationXLimit)));
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

            // float angle = Quaternion.Angle(self.transUIRoot.rotation, targetRotation);
            // if (angle <= 2)
            // {
            //     self.transUIRoot.rotation = targetRotation;
            // }
            // else
            {
                float time = self.uiFollowHeadCfg.RotationSpeed;
                self.transUIRoot.rotation = Quaternion.Slerp(self.transUIRoot.rotation, targetRotation, time);
            }
        }
    }
}