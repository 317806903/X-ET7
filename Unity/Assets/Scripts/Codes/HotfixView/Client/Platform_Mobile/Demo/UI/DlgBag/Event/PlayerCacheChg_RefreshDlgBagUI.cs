using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgBagUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType != PlayerModelType.BackPack)
            {
                return;
            }
            DlgBag _DlgBag = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBag>(true);
            if (_DlgBag != null)
            {
                _DlgBag.Refresh().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}