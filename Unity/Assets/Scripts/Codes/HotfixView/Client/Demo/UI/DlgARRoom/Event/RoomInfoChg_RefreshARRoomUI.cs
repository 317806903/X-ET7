using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshARRoomUI: AEvent<Scene, EventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, EventType.RoomInfoChg args)
        {
            DlgARRoom _DlgARRoom = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoom>(true);
            if (_DlgARRoom != null)
            {
                _DlgARRoom.RefreshWhenRoomInfoChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}