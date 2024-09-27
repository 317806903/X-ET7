using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgBattleSkill : AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, NoticePlayerCacheChg a)
        {
            if (a.playerModelType != PlayerModelType.Skills)
                return;
            DlgBattleSkill dlgBattleSkill=UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleSkill>();
            if(dlgBattleSkill != null)
            {
                dlgBattleSkill.RefreshLoopList().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}
