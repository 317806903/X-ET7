using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, EventType.AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, EventType.AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            unit.RemoveComponent<GameObjectComponent>();
            unit.AddComponent<GameObjectComponent>();

            if (Ability.UnitHelper.ChkIsPlayer(unit) || Ability.UnitHelper.ChkIsActor(unit))
            {
                unit.RemoveComponent<HealthBarComponent>();
                unit.AddComponent<HealthBarComponent>();
            }
            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent != null)
            {
                unit.RemoveComponent<TowerShowComponent>();
                TowerShowComponent towerShowComponent = unit.AddComponent<TowerShowComponent>();
                towerShowComponent.Init(towerComponent);

                unit.RemoveComponent<TowerStarBarComponent>();
                TowerStarBarComponent towerStarBarComponent = unit.AddComponent<TowerStarBarComponent>();
                towerStarBarComponent.Init(towerComponent);
            }

            if (unit.GetComponent<ET.Ability.Client.AnimatorShowComponent>() == null)
            {
                unit.AddComponent<ET.Ability.Client.AnimatorShowComponent>();
            }

            if (unit.GetComponent<ET.Ability.Client.EffectShowComponent>() == null)
            {
                unit.AddComponent<ET.Ability.Client.EffectShowComponent>();
            }

            if (unit.GetComponent<ET.Ability.Client.AudioPlayComponent>() == null)
            {
                unit.AddComponent<ET.Ability.Client.AudioPlayComponent>();
            }
            await ETTask.CompletedTask;
        }
    }
}