using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleDragItem_DlgBattleTowerNotice: AEvent<Scene, ClientEventType.ShowBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleDragItem args)
        {
            DlgBattleTowerNotice _DlgBattleTowerNotice = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerNotice>(true);
            if (_DlgBattleTowerNotice != null)
            {
                await _DlgBattleTowerNotice.ShowOrHide(false);
            }
            await ETTask.CompletedTask;
        }
    }
}