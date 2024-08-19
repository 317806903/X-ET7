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
                self.isInit = false;
                self.isNeedNoticeClient = false;
                self.name = AnimatorMotionName.None;
                self.isStoppingAnimator = false;
                self.controlStateName = AnimatorMotionName.None;
                self.isControlAnimatorLoop = false;
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

        public static void SetAnimatorMotion(this AnimatorComponent self, AnimatorMotionName animatorMotionName, bool isLoop, bool isOnlySelfShow)
        {
            self.isOnlySelfShow = isOnlySelfShow;
            if (self.isInit == false)
            {
                self.isNeedNoticeClient = true;
                self.isInit = true;
            }
            else
            {
                if (animatorMotionName == AnimatorMotionName.None)
                {
                    return;
                }
                if (self.name != animatorMotionName)
                {
                    self.isNeedNoticeClient = true;
                }
                else
                {
                    if (self.name == AnimatorMotionName.None ||
                        ET.Ability.AnimatorHelper.ChkIsIdleOrMove(self.name))
                    {
                    }
                    else
                    {
                        self.isNeedNoticeClient = true;
                    }
                }
            }
            self.name = animatorMotionName;
            self.animatorTickTime = TimeHelper.ServerNow();
            self.isAnimatorLoop = isLoop;
        }

        public static void ResetControlStateAnimatorMotion(this AnimatorComponent self)
        {
            (bool isStoppingAnimator, AnimatorMotionName animatorMotionName, bool isLoop) = ET.Ability.BuffHelper.GetControlStateAnimatorMotion(self.GetUnit());
            if (self.isStoppingAnimator != isStoppingAnimator ||
                self.controlStateName != animatorMotionName ||
                self.isControlAnimatorLoop != isLoop)
            {
                self.isNeedNoticeClient = true;
            }
            self.isStoppingAnimator = isStoppingAnimator;
            self.controlStateName = animatorMotionName;
            self.controlAnimatorTickTime = TimeHelper.ServerNow();
            self.isControlAnimatorLoop = isLoop;
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