using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgCameraPlayerSkill : AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, NoticePlayerCacheChg args)
        {
            if (args.playerModelType != PlayerModelType.Skills)
            {
                return;
            }
            DlgCameraPlayerSkill dlgCameraPlayerSkill=UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgCameraPlayerSkill>();
            if(dlgCameraPlayerSkill != null)
            {
                dlgCameraPlayerSkill.RefreshLoopList().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}
