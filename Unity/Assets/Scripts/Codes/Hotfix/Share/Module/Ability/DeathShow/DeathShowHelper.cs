using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(DeathShowComponent))]
    public static class DeathShowHelper
    {
        public static void DeathShow(Unit unit)
        {
            if (unit.GetComponent<DeathShowComponent>() != null)
            {
                return;
            }
            unit.RemoveComponent<AIComponent>();
            unit.Stop(WaitTypeError.Cancel);
            float deathShowDuration = 0;
            ActionCfg_DeathShow actionCfg_DeathShow = UnitHelper.GetDeathShow(unit);
            deathShowDuration = actionCfg_DeathShow.Duration;

            if (deathShowDuration == 0)
            {
                unit._Destroy();
            }
            else
            {
                DeathShowComponent deathShowComponent = unit.AddComponent<DeathShowComponent>();
                deathShowComponent.Init(actionCfg_DeathShow);
            }
        }
    }
}