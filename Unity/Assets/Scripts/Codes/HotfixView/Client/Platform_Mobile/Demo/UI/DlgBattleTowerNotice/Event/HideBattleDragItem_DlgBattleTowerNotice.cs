using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class HideBattleDragItem_DlgBattleTowerNotice: AEvent<Scene, ClientEventType.HideBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.HideBattleDragItem args)
        {
            DlgBattleTowerNotice _DlgBattleTowerNotice = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerNotice>(true);
            if (_DlgBattleTowerNotice != null)
            {
                await _DlgBattleTowerNotice.ShowOrHide(true);
            }
            await ETTask.CompletedTask;
        }
    }
}