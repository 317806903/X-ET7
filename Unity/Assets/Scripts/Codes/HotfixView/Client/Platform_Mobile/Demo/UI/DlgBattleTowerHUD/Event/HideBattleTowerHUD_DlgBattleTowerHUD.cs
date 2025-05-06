using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class HideBattleTowerHUD_DlgBattleTowerHUD: AEvent<Scene, ClientEventType.HideBattleTowerHUD>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.HideBattleTowerHUD args)
        {
            UIManagerHelper.GetUIComponent(scene).HideWindow<DlgBattleTowerHUD>();
            await ETTask.CompletedTask;
        }
    }
}