using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshARRoomPVEUI: AEvent<Scene, EventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, EventType.RoomInfoChg args)
        {
            DlgARRoomPVE _DlgARRoomPVE = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoomPVE>(true);
            if (_DlgARRoomPVE != null)
            {
                _DlgARRoomPVE.RefreshWhenRoomInfoChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}