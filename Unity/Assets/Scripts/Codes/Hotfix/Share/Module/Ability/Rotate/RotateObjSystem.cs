using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (RotateObj))]
    public static class RotateObjSystem
    {
        [ObjectSystem]
        public class RotateObjAwakeSystem: AwakeSystem<RotateObj>
        {
            protected override void Awake(RotateObj self)
            {
            }
        }

        [ObjectSystem]
        public class RotateObjDestroySystem: DestroySystem<RotateObj>
        {
            protected override void Destroy(RotateObj self)
            {
            }
        }

        public static void Init(this RotateObj self, int buffCfgId)
        {
        }

        public static float GetIncrementRotateInTime(this RotateObj self)
        {
            return self.incrementRotate;
        }
    }
}