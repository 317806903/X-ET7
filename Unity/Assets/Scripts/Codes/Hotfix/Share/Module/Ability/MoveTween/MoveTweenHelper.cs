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
        
    }
}