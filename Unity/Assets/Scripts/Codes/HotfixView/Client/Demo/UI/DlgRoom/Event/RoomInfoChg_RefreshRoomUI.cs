using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshRoomUI: AEvent<Scene, EventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, EventType.RoomInfoChg args)
        {
            DlgRoom _DlgRoom = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgRoom>(true);
            if (_DlgRoom != null)
            {
                _DlgRoom.RefreshWhenRoomInfoChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}