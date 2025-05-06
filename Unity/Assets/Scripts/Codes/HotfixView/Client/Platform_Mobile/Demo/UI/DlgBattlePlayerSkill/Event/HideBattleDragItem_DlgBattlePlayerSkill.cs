using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class HideBattleDragItem_DlgBattlePlayerSkill: AEvent<Scene, ClientEventType.HideBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.HideBattleDragItem args)
        {
            DlgBattlePlayerSkill _DlgBattlePlayerSkill = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattlePlayerSkill>(true);
            if (_DlgBattlePlayerSkill != null)
            {
                _DlgBattlePlayerSkill.ShowOrHide(true);
            }
            await ETTask.CompletedTask;
        }
    }
}