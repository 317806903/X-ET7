using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class DamageShow_ShowGameObjectFlicker: AEvent<Scene, EventType.SyncDamageShow>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncDamageShow args)
        {
            List<(Unit unit, int damageValue, bool isCrt)> list = args.list;
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, int damageValue, bool isCrt) in list)
            {
                if (Ability.UnitHelper.ChkIsBullet(unit))
                {
                    continue;
                }

                if (damageValue >= 0)
                {
                    continue;
                }
                GameObjectShowComponent gameObjectShowComponent = unit.GetComponent<GameObjectShowComponent>();
                if (gameObjectShowComponent == null)
                {
                    continue;
                }
                gameObjectShowComponent.FlickerWhenBeHit().Coroutine();
            }
        }
    }
}