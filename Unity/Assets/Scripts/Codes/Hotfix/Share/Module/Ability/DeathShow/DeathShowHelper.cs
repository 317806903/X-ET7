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
            unit.RemoveComponent<AIComponent>();
            float deathShowDuration = 0;
            ActionCfg_DeathShow actionCfg_DeathShow;
            if (UnitHelper.ChkIsBullet(unit))
            {
                actionCfg_DeathShow = unit.GetComponent<BulletObj>().model.DeathShow_Ref;
            }
            else
            {
                actionCfg_DeathShow = unit.model.DeathShow_Ref;
            }

            deathShowDuration = actionCfg_DeathShow.Duration;

            if (deathShowDuration == 0)
            {
                unit.Destroy();
            }
            else
            {
                DeathShowComponent deathShowComponent = unit.AddComponent<DeathShowComponent>();
                deathShowComponent.Init(actionCfg_DeathShow);
            }
        }
    }
}