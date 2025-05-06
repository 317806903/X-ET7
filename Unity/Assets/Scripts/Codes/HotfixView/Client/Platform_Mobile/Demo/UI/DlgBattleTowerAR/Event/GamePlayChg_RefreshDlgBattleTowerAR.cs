using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayChg_RefreshDlgBattleTowerAR: AEvent<Scene, ClientEventType.GamePlayChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.GamePlayChg args)
        {
            DlgBattleTowerAR _DlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>(true);
            if (_DlgBattleTowerAR != null)
            {
                _DlgBattleTowerAR.RefreshUI().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}