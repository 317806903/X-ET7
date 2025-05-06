using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleTowerHUD_DlgBattleTowerHUD: AEvent<Scene, ClientEventType.ShowBattleTowerHUD>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleTowerHUD args)
        {
            DlgBattleTowerHUD_ShowWindowData showWindowData = new()
            {
                playerId = args.playerId,
                towerUnitId = args.towerUnitId,
                towerCfgId = args.towerCfgId,
            };
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleTowerHUD>(showWindowData);
            await ETTask.CompletedTask;
        }
    }
}