using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class GamePlayCoinChg_RefreshUI: AEvent<Scene, EventType.GamePlayCoinChg>
    {
        protected override async ETTask Run(Scene scene, EventType.GamePlayCoinChg args)
        {
            DlgBattleTower _DlgBattleTower = scene.GetComponent<UIComponent>().GetDlgLogic<DlgBattleTower>(true);
            if (_DlgBattleTower != null)
            {
                _DlgBattleTower.RefreshCoin();
            }
            await ETTask.CompletedTask;
        }
    }
}