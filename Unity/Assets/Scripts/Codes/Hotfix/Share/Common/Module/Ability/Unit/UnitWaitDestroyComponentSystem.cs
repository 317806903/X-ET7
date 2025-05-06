using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (UnitWaitDestroyComponent))]
    public static class UnitWaitDestroyComponentSystem
    {
        [ObjectSystem]
        public class UnitWaitDestroyComponentAwakeSystem: AwakeSystem<UnitWaitDestroyComponent, float>
        {
            protected override void Awake(UnitWaitDestroyComponent self, float duration)
            {
                self.duration = duration;
                self.timeElapsed = 0;
            }
        }

        [ObjectSystem]
        public class UnitWaitDestroyComponentDestroySystem: DestroySystem<UnitWaitDestroyComponent>
        {
            protected override void Destroy(UnitWaitDestroyComponent self)
            {
            }
        }
        
        [ObjectSystem]
        public class UnitWaitDestroyComponentFixedUpdateSystem: FixedUpdateSystem<UnitWaitDestroyComponent>
        {
            protected override void FixedUpdate(UnitWaitDestroyComponent self)
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
        public static Unit GetUnit(this UnitWaitDestroyComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this UnitWaitDestroyComponent self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            self.duration -= timePassed;
            self.timeElapsed += timePassed;
            if (self.duration <= 0)
            {
                self.GetUnit().DestroyWithDeathShow();
            }
        }
    }
}