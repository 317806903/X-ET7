using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(MoveTweenObj))]
    public static class MoveTweenHelper
    {
        public static void CreateMoveTween(Unit unit, MoveTweenType moveTweenType, SelectHandle selectHandle)
        {
            MoveTweenObj moveTweenObj = unit.AddComponent<MoveTweenObj>();
            moveTweenObj.Init(unit.Id, moveTweenType, selectHandle);
        }

        public static void MoveTweenChgTarget(Unit unit, ActionCfg_MoveTweenChgTarget actionCfgMoveTweenChgTarget, ref ActionContext actionContext)
        {
            MoveTweenObj moveTweenObj = unit.GetComponent<MoveTweenObj>();
            if (moveTweenObj == null)
            {
                return;
            }

            SelectHandle selectHandle = SelectHandleHelper.CreateSelectHandle(unit, null, actionCfgMoveTweenChgTarget.ActionCallAutoUnitArea_Ref, ref actionContext, false);

            moveTweenObj.ChgSelectHandle(selectHandle);
        }

    }
}