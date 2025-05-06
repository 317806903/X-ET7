using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgARRoomUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            DlgARRoom _DlgARRoom = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgARRoom>(true);
            if (_DlgARRoom != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo || args.playerModelType == PlayerModelType.BackPack)
                {
                    _DlgARRoom.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}