using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgARRoomPVESeasonSeasonUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgARRoomPVESeason _DlgARRoomPVESeason = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoomPVESeason>(true);
            if (_DlgARRoomPVESeason != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgARRoomPVESeason.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}