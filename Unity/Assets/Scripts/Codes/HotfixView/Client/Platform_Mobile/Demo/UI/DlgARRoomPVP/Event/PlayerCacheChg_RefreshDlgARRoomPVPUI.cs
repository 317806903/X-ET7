using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgARRoomPVPUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            DlgARRoomPVP _DlgARRoomPVP = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoomPVP>(true);
            if (_DlgARRoomPVP != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgARRoomPVP.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}