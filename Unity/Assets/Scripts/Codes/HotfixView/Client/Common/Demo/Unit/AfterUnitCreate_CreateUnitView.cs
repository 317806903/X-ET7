using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_CreateUnitView: AEvent<Scene, ClientEventType.AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.AfterUnitCreate args)
        {
            Unit unit = args.Unit;
            if (unit == null || unit.IsDisposed)
            {
                return;
            }

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent != null)
            {
                unit.RemoveComponent<GameObjectShowComponent>();
                GameObjectShowComponent gameObjectShowComponent = unit.AddComponent<GameObjectShowComponent>();
                await gameObjectShowComponent.Init();
                if (unit.IsDisposed)
                {
                    return;
                }
            }

            if (Ability.UnitHelper.ChkIsPlayer(unit)
                || Ability.UnitHelper.ChkIsActor(unit))
            {
                unit.RemoveComponent<HealthBarComponent>();
                HealthBarComponent healthBarComponent = unit.AddComponent<HealthBarComponent>();
                await healthBarComponent.Init();
                if (unit.IsDisposed)
                {
                    return;
                }
            }

            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent != null)
            {
                unit.RemoveComponent<TowerShowComponent>();
                TowerShowComponent towerShowComponent = unit.AddComponent<TowerShowComponent>();
                towerShowComponent.Init(towerComponent);

                QualityRank qualityRank = ET.ItemHelper.GetTowerItemQualityRank(towerComponent.towerCfgId);
                if (qualityRank == QualityRank.None)
                {
                }
                else
                {
                    unit.RemoveComponent<TowerStarBarComponent>();
                    TowerStarBarComponent towerStarBarComponent = unit.AddComponent<TowerStarBarComponent>();
                    towerStarBarComponent.Init(towerComponent);
                }
            }

            HomeComponent homeComponent = unit.GetComponent<HomeComponent>();
            if (homeComponent != null)
            {
                unit.RemoveComponent<HomeShowComponent>();
                HomeShowComponent homeShowComponent = unit.AddComponent<HomeShowComponent>();
                homeShowComponent.Init(homeComponent);
            }

            MonsterCallComponent monsterCallComponent = unit.GetComponent<MonsterCallComponent>();
            if (monsterCallComponent != null)
            {
                unit.RemoveComponent<MonsterCallShowComponent>();
                MonsterCallShowComponent monsterCallShowComponent = unit.AddComponent<MonsterCallShowComponent>();
                monsterCallShowComponent.Init(monsterCallComponent);
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

            if (unit.GetComponent<ET.Ability.Client.FloatingTextComponent>() == null)
            {
                unit.AddComponent<ET.Ability.Client.FloatingTextComponent>();
            }

            await ETTask.CompletedTask;
        }
    }
}