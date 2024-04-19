using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgARRoomPVEUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgARRoomPVE _DlgARRoomPVE = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoomPVE>(true);
            if (_DlgARRoomPVE != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgARRoomPVE.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}