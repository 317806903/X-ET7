using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (UnitWaitResetPosComponent))]
    public static class UnitWaitResetPosComponentSystem
    {
        [ObjectSystem]
        public class UnitWaitResetPosComponentAwakeSystem: AwakeSystem<UnitWaitResetPosComponent, float3>
        {
            protected override void Awake(UnitWaitResetPosComponent self, float3 pos)
            {
                self.startPos = self.GetUnit().Position;
                self.resetPos = pos;
                self.timeElapsed = 0;
            }
        }

        [ObjectSystem]
        public class UnitWaitResetPosComponentDestroySystem: DestroySystem<UnitWaitResetPosComponent>
        {
            protected override void Destroy(UnitWaitResetPosComponent self)
            {
            }
        }

        [ObjectSystem]
        public class UnitWaitResetPosComponentFixedUpdateSystem: FixedUpdateSystem<UnitWaitResetPosComponent>
        {
            protected override void FixedUpdate(UnitWaitResetPosComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this UnitWaitResetPosComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this UnitWaitResetPosComponent self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.chkTime -= timePassed;
            self.timeElapsed += timePassed;
            if (self.chkTime <= 0)
            {
                if (self.GetUnit().Position.Equals(self.startPos))
                {
                    ET.Ability.UnitHelper.ResetPos(self.GetUnit(), self.resetPos, float3.zero);
                }
                self.Dispose();
            }
        }
    }
}