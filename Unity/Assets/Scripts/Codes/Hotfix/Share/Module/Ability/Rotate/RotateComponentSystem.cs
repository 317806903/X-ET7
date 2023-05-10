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

        public static RotateObj AddRotate(this RotateComponent self, int rotateCfgId)
        {
            RotateObj rotateObj = self.AddChild<RotateObj>();
            rotateObj.Init(rotateCfgId);
            
            return rotateObj;
        }

        public static void SetRotateInput(this RotateComponent self, float rotateDirectionInput)
        {
            self.rotateDirectionInput = rotateDirectionInput;
        }

        public static void RunRotate(this RotateComponent self, float fixedDeltaTime)
        {
            foreach (var rotateObjs in self.Children)
            {
                RotateObj rotateObj = rotateObjs.Value as RotateObj;
                self.rotateDirectionInput += rotateObj.GetIncrementRotateInTime();
            }

            Unit unit = self.GetParent<Unit>();
            //float targetRotation = unit.Rotation + self.rotateDirectionInput;
            
        }

        public static void FixedUpdate(this RotateComponent self, float fixedDeltaTime)
        {
            self.RunRotate(fixedDeltaTime);
            
            if (self.Children.Count <= 0)
            {
                return;
            }

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