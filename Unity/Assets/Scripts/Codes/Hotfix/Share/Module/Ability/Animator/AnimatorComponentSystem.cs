using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (AnimatorComponent))]
    public static class AnimatorComponentSystem
    {
        [ObjectSystem]
        public class AnimatorComponentAwakeSystem: AwakeSystem<AnimatorComponent>
        {
            protected override void Awake(AnimatorComponent self)
            {
                self.name = AnimatorMotionName.None;
                self.isStoppingAnimator = false;
                self.controlStateName = AnimatorMotionName.None;
            }
        }

        [ObjectSystem]
        public class AnimatorComponentDestroySystem: DestroySystem<AnimatorComponent>
        {
            protected override void Destroy(AnimatorComponent self)
            {
            }
        }

        [ObjectSystem]
        public class AnimatorComponentFixedUpdateSystem: FixedUpdateSystem<AnimatorComponent>
        {
            protected override void FixedUpdate(AnimatorComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static Unit GetUnit(this AnimatorComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void SetAnimatorMotion(this AnimatorComponent self, AnimatorMotionName animatorMotionName, bool isOnlySelfShow)
        {
            self.isOnlySelfShow = isOnlySelfShow;
            if (self.name != animatorMotionName)
            {
                self.isNeedNoticeClient = true;
            }
            else
            {
                if (self.name == AnimatorMotionName.None || ET.Ability.AnimatorHelper.ChkIsLoopAnimatorMotion(self.name))
                {
                }
                else
                {
                    self.isNeedNoticeClient = true;
                }
            }
            self.name = animatorMotionName;
            self.animatorTickTime = TimeHelper.ServerNow();
        }

        public static void ResetControlStateAnimatorMotion(this AnimatorComponent self)
        {
            (bool isStoppingAnimator, AnimatorMotionName animatorMotionName) = ET.Ability.BuffHelper.GetControlStateAnimatorMotion(self.GetUnit());
            if (self.isStoppingAnimator != isStoppingAnimator || self.controlStateName != animatorMotionName)
            {
                self.isNeedNoticeClient = true;
            }
            self.isStoppingAnimator = isStoppingAnimator;
            self.controlStateName = animatorMotionName;
            self.controlAnimatorTickTime = TimeHelper.ServerNow();
        }

        public static void NoticeClient(this AnimatorComponent self)
        {
            Ability.UnitHelper.AddSyncData_UnitComponent(self.GetUnit(), self.GetType());
        }

        public static void FixedUpdate(this AnimatorComponent self, float fixedDeltaTime)
        {
            if (self.isNeedNoticeClient == false)
            {
                return;
            }

            self.NoticeClient();

            self.isNeedNoticeClient = false;
        }
    }
}