using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class AnimatorHelper
    {
        public static AnimatorComponent _GetAnimatorComponent(Unit unit)
        {
            AnimatorComponent animatorComponent = unit.GetComponent<AnimatorComponent>();
            if (animatorComponent == null)
            {
                animatorComponent = unit.AddComponent<AnimatorComponent>();
            }
            return animatorComponent;
        }

        public static void PlayAnimator(Unit unit, ActionCfg_PlayAnimator actionCfg_PlayAnimator, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                AnimatorComponent animatorComponent = _GetAnimatorComponent(targetUnit);
                animatorComponent.SetAnimatorMotion(actionCfg_PlayAnimator.AnimatorName, actionCfg_PlayAnimator.IsOnlySelfShow);
            }
            list.Dispose();
        }

        public static void ResetControlStateAnimatorMotion(Unit unit)
        {
            AnimatorComponent animatorComponent = _GetAnimatorComponent(unit);
            animatorComponent.ResetControlStateAnimatorMotion();
        }

        public static bool ChkIsLoopAnimatorMotion(AnimatorMotionName animatorMotionName)
        {
            if (animatorMotionName == AnimatorMotionName.Idle || animatorMotionName == AnimatorMotionName.Move)
            {
                return true;
            }

            return false;
        }
    }
}