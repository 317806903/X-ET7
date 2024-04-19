using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgFixedMenuUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgFixedMenu _DlgFixedMenu = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgFixedMenu>(true);
            if (_DlgFixedMenu != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgFixedMenu.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}