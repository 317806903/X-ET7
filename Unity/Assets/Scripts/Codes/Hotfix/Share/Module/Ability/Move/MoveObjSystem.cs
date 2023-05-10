using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (MoveObj))]
    public static class MoveObjSystem
    {
        [ObjectSystem]
        public class MoveObjAwakeSystem: AwakeSystem<MoveObj>
        {
            protected override void Awake(MoveObj self)
            {
            }
        }

        [ObjectSystem]
        public class MoveObjDestroySystem: DestroySystem<MoveObj>
        {
            protected override void Destroy(MoveObj self)
            {
            }
        }

        public static void Init(this MoveObj self, int buffCfgId)
        {
        }

        public static float3 GetVeloInTime(this MoveObj self)
        {
            return self.inTime <= 0 ? self.velocity : (self.velocity / self.inTime);
        }

        public static void FixedUpdate(this MoveObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            self.timeElapsed += timePassed;
        }

        public static bool ChkNeedRemove(this MoveObj self)
        {
            if (self.duration <= 0)
            {
                return true;
            }

            return false;
        }
    }
}