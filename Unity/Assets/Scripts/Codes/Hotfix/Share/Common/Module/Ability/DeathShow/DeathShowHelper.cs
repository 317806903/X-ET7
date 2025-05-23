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
        public static bool ChkIsInDeath(Unit unit)
        {
            if (unit.GetComponent<DeathShowComponent>() != null)
            {
                return true;
            }
            return false;
        }

        public static void DeathShow(Unit unit, ActionCfg_DeathShow actionCfg_DeathShow = null)
        {
            if (ChkIsInDeath(unit))
            {
                return;
            }
            unit.GetComponent<AIComponent>()?.PauseAI();
            unit.Stop(WaitTypeError.Cancel);
            float deathShowDuration = 0;
            if (actionCfg_DeathShow == null)
            {
                actionCfg_DeathShow = UnitHelper.GetDeathShow(unit);
            }
            deathShowDuration = actionCfg_DeathShow.Duration;

            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            if (numericComponent != null)
            {
                numericComponent.SetAsInt(NumericType.Hp, 0);
            }

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