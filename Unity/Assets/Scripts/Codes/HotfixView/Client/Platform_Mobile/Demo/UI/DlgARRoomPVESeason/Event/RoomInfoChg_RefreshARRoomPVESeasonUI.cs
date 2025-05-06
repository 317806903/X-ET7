using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshARRoomPVESeasonUI: AEvent<Scene, ClientEventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.RoomInfoChg args)
        {
            DlgARRoomPVESeason _DlgARRoomPVESeason = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoomPVESeason>(true);
            if (_DlgARRoomPVESeason != null)
            {
                _DlgARRoomPVESeason.RefreshWhenRoomInfoChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}