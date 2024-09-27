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

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent != null)
            {
                unit.RemoveComponent<GameObjectShowComponent>();
                GameObjectShowComponent gameObjectShowComponent = unit.AddComponent<GameObjectShowComponent>();
                await gameObjectShowComponent.Init();
            }

            if (Ability.UnitHelper.ChkIsPlayer(unit)
                || Ability.UnitHelper.ChkIsActor(unit))
            {
                unit.RemoveComponent<HealthBarComponent>();
                HealthBarComponent healthBarComponent = unit.AddComponent<HealthBarComponent>();
                await healthBarComponent.Init();
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
            PlayerUnitComponent playerUnitComponent = unit.GetComponent<PlayerUnitComponent>();
            if (playerUnitComponent != null)
            {
                unit.RemoveComponent<PlayerUnitShowComponent>();
                PlayerUnitShowComponent playerUnitShowComponent = unit.AddComponent<PlayerUnitShowComponent>();
                playerUnitShowComponent.Init(playerUnitComponent);
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