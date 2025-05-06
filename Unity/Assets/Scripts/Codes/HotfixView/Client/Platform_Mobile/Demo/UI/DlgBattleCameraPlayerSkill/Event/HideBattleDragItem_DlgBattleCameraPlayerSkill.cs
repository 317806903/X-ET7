using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class HideBattleDragItem_DlgBattleCameraPlayerSkill: AEvent<Scene, ClientEventType.HideBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.HideBattleDragItem args)
        {
            DlgBattleCameraPlayerSkill _DlgBattleCameraPlayerSkill = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleCameraPlayerSkill>(true);
            if (_DlgBattleCameraPlayerSkill != null)
            {
                _DlgBattleCameraPlayerSkill.ShowOrHide(true, false, false);
            }
            await ETTask.CompletedTask;
        }
    }
}