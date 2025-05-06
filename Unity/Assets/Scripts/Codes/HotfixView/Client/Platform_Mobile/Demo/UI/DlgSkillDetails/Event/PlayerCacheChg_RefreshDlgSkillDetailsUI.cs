using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgSkillDetailsUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType != PlayerModelType.BackPack)
            {
                return;
            }
            DlgSkillDetails _DlgSkillDetails = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgSkillDetails>(true);
            if (_DlgSkillDetails != null)
            {
                _DlgSkillDetails.Refresh().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}