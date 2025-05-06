using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgTowerDetailsUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType != PlayerModelType.BackPack)
            {
                return;
            }
            DlgTowerDetails _DlgTowerDetails = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgTowerDetails>(true);
            if (_DlgTowerDetails != null)
            {
                _DlgTowerDetails.Refresh().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}