using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshARRoomUI: AEvent<Scene, ClientEventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.RoomInfoChg args)
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