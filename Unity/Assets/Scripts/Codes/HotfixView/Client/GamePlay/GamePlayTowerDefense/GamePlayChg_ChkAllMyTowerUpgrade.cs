using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayChg_ChkAllMyTowerUpgrade: AEvent<Scene, EventType.GamePlayChg>
    {
        protected override async ETTask Run(Scene scene, EventType.GamePlayChg args)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent != null)
            {
                gamePlayTowerDefenseComponent.ChkAllMyTowerUpgrade();
            }

            await ETTask.CompletedTask;
        }
    }
}