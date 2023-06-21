using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class RoomInfoChg_RefreshUI: AEvent<Scene, EventType.RoomInfoChg>
    {
        protected override async ETTask Run(Scene scene, EventType.RoomInfoChg args)
        {
            DlgRoom _DlgRoom = scene.GetComponent<UIComponent>().GetDlgLogic<DlgRoom>();
            if (_DlgRoom != null)
            {
                _DlgRoom.RefreshUI();
            }
            await ETTask.CompletedTask;
        }
    }
}