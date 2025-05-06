using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class HideBattleDragItem_DlgBattleTower: AEvent<Scene, ClientEventType.HideBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.HideBattleDragItem args)
        {
            DlgBattleTower _DlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>(true);
            if (_DlgBattleTower != null)
            {
                _DlgBattleTower.ShowOrHide(true);
            }
            await ETTask.CompletedTask;
        }
    }
}