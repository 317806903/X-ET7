using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgRoomUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            DlgRoom _DlgRoom = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgRoom>(true);
            if (_DlgRoom != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgRoom.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}