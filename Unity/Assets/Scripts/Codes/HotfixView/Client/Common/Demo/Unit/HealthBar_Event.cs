using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class HealthBar_Event: AEvent<Scene, EventType.SyncHealthBar>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncHealthBar args)
        {
            List<Unit> list = args.list;
            if (list == null)
            {
                return;
            }
            foreach (Unit unit in list)
            {
                if (Ability.UnitHelper.ChkIsBullet(unit))
                {
                    continue;
                }
                unit.GetComponent<HealthBarComponent>()?.UpdateHealth();
            }
        }
    }
}