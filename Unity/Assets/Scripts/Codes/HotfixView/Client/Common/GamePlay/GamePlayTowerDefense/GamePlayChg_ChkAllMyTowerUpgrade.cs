using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayChg_ChkAllMyTowerUpgrade: AEvent<Scene, ClientEventType.GamePlayChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.GamePlayChg args)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(scene);
            if (gamePlayTowerDefenseComponent != null)
            {
                await gamePlayTowerDefenseComponent.ChkAllMyTowerUpgrade();
            }

            await ETTask.CompletedTask;
        }
    }
}