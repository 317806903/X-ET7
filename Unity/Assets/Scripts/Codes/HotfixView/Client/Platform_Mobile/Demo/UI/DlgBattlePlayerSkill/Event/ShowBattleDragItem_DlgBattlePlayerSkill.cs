using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class ShowBattleDragItem_DlgBattlePlayerSkill: AEvent<Scene, ClientEventType.ShowBattleDragItem>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowBattleDragItem args)
        {
            DlgBattlePlayerSkill _DlgBattlePlayerSkill = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattlePlayerSkill>(true);
            if (_DlgBattlePlayerSkill != null)
            {
                _DlgBattlePlayerSkill.ShowOrHide(false);
            }
            await ETTask.CompletedTask;
        }
    }
}