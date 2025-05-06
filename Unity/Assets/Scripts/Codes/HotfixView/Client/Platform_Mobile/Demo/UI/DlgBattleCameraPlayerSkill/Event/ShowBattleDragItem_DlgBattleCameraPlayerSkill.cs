using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleDragItem_DlgBattleCameraPlayerSkill: AEvent<Scene, ClientEventType.ShowBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleDragItem args)
        {
            DlgBattleCameraPlayerSkill _DlgBattleCameraPlayerSkill = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleCameraPlayerSkill>(true);
            if (_DlgBattleCameraPlayerSkill != null)
            {
                _DlgBattleCameraPlayerSkill.ShowOrHide(false, false, false);
            }
            await ETTask.CompletedTask;
        }
    }
}