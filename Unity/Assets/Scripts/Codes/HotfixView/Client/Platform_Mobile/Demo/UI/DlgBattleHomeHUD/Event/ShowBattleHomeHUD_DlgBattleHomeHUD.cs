using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleHomeHUD_DlgBattleHomeHUD: AEvent<Scene, ClientEventType.ShowBattleHomeHUD>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleHomeHUD args)
        {
            DlgBattleHomeHUD_ShowWindowData showWindowData = new()
            {
                homeUnitId = args.homeUnitId,
                homeCfgId = args.homeCfgId,
            };
            await UIManagerHelper.GetUIComponent(scene).ShowWindowAsync<DlgBattleHomeHUD>(showWindowData);
            await ETTask.CompletedTask;
        }
    }
}