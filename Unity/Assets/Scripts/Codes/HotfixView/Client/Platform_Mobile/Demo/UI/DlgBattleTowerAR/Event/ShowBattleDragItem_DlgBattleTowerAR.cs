using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleDragItem_DlgBattleTowerAR: AEvent<Scene, ClientEventType.ShowBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleDragItem args)
        {
            DlgBattleTowerAR _DlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>(true);
            if (_DlgBattleTowerAR != null)
            {
                _DlgBattleTowerAR.ShowOrHide(false);
            }
            await ETTask.CompletedTask;
        }
    }
}