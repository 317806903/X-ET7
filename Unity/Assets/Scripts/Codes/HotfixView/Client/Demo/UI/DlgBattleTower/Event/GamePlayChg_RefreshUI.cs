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
            DlgBattleTower _DlgBattleTower = scene.GetComponent<UIComponent>().GetDlgLogic<DlgBattleTower>(true);
            if (_DlgBattleTower != null)
            {
                _DlgBattleTower.RefreshUI().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}