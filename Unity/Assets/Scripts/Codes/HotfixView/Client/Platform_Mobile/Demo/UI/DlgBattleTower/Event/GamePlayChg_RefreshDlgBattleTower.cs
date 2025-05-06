using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayChg_RefreshDlgBattleTower: AEvent<Scene, ClientEventType.GamePlayChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.GamePlayChg args)
        {
            DlgBattleTower _DlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>(true);
            if (_DlgBattleTower != null)
            {
                _DlgBattleTower.RefreshUI().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}