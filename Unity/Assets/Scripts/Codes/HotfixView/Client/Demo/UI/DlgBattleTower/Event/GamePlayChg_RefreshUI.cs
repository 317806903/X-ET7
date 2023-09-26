using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayChg_RefreshUI: AEvent<Scene, EventType.GamePlayChg>
    {
        protected override async ETTask Run(Scene scene, EventType.GamePlayChg args)
        {
            if (ET.Client.GamePlayHelper.GetGamePlay(scene).IsAR())
            {
                DlgBattleTowerAR _DlgBattleTowerAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerAR>(true);
                if (_DlgBattleTowerAR != null)
                {
                    _DlgBattleTowerAR.RefreshUI().Coroutine();
                }
            }
            else
            {
                DlgBattleTower _DlgBattleTower = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTower>(true);
                if (_DlgBattleTower != null)
                {
                    _DlgBattleTower.RefreshUI().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}