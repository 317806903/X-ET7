using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (RotateComponent))]
    [FriendOf(typeof (RotateObj))]
    public static class RotateComponentSystem
    {
        [ObjectSystem]
        public class RotateComponentAwakeSystem: AwakeSystem<RotateComponent>
        {
            protected override void Awake(RotateComponent self)
            {
                self.removeList = new();
            }
        }

        [ObjectSystem]
        public class RotateComponentDestroySystem: DestroySystem<RotateComponent>
        {
            protected override void Destroy(RotateComponent self)
            {
                self.removeList.Clear();
            }
        }

        [ObjectSystem]
        public class RotateComponentFixedUpdateSystem: FixedUpdateSystem<RotateComponent>
        {
            protected override void FixedUpdate(RotateComponent self)
            {
                if (self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static RotateObj AddRotate(this RotateComponent self, float incrementRotate)
        {
            RotateObj rotateObj = self.AddChild<RotateObj>();
            rotateObj.Init(incrementRotate);

            return rotateObj;
        }

        public static Unit GetUnit(this RotateComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void ForceSetRotate(this RotateComponent self, float forceRotateAngle)
        {
            self.rotateDirectionInput = forceRotateAngle;
        }

        public static void ForceSetRotateImmediately(this RotateComponent self, float forceRotateAngle)
        {
            self.rotateDirectionInput = 0;
            self._ForceSetRotate(forceRotateAngle);
        }

        public static void ForceSetForward(this RotateComponent self, float3 forwardDirection)
        {
            self.rotateDirectionInput = 0;
            self._ForceSetForward(forwardDirection);
        }

        public static void _ForceSetRotate(this RotateComponent self, float rotateAngle)
        {
            Unit unit = self.GetUnit();
            unit.Rotation = math.mul(unit.Rotation, quaternion.RotateY(rotateAngle));
        }

        public static void _ForceSetForward(this RotateComponent self, float3 forwardDirection)
        {
            Unit unit = self.GetUnit();
            unit.Forward = forwardDirection;
        }

        public static (bool, float) GetFaceOneFixdDeltaTime(this RotateComponent self, float fixedDeltaTime, float rotateDirectionInput)
        {
            if (rotateDirectionInput > math.PI)
            {
                rotateDirectionInput -= math.PI * 2;
            }
            else if (rotateDirectionInput < -math.PI)
            {
                rotateDirectionInput += math.PI * 2;
            }

            float rotationSpeed = ET.Ability.UnitHelper.GetRotationSpeed(self.GetUnit());
            float faceOneFixdDeltaTime = rotationSpeed * fixedDeltaTime * math.PI / 180;
            if (math.abs(rotateDirectionInput) <= faceOneFixdDeltaTime)
            {
                return (true, rotateDirectionInput);
            }

            if (rotateDirectionInput < 0)
            {
                faceOneFixdDeltaTime = -faceOneFixdDeltaTime;
            }
            return (false, faceOneFixdDeltaTime);
        }

        public static void RunRotate(this RotateComponent self, float fixedDeltaTime)
        {
            foreach (var rotateObjs in self.Children)
            {
                RotateObj rotateObj = rotateObjs.Value as RotateObj;
                self.rotateDirectionInput += rotateObj.GetIncrementRotateInTime();
            }

            (bool bRet, float rotateFixdDeltaTime) = self.GetFaceOneFixdDeltaTime(fixedDeltaTime, self.rotateDirectionInput);
            if (bRet)
            {
                self.rotateDirectionInput = 0;
            }
            else
            {
                self.rotateDirectionInput = self.rotateDirectionInput - rotateFixdDeltaTime;
            }
            self._ForceSetRotate(rotateFixdDeltaTime);
        }

        public static void FixedUpdate(this RotateComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0 && self.rotateDirectionInput == 0)
            {
                return;
            }

            self.RunRotate(fixedDeltaTime);


            self.removeList.Clear();
            foreach (var rotateObjs in self.Children)
            {
                RotateObj rotateObj = rotateObjs.Value as RotateObj;
                self.removeList.Add(rotateObj);
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.removeList[i].Dispose();
            }

            self.removeList.Clear();
        }
    }
}