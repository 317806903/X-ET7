using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshARRoomPVPUI: AEvent<Scene, EventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, EventType.RoomInfoChg args)
        {
            DlgARRoomPVP _DlgARRoomPVP = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoomPVP>(true);
            if (_DlgARRoomPVP != null)
            {
                _DlgARRoomPVP.RefreshUI().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}