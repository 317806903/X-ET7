using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerMailCacheChg_RefreshDlgMailUI : AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            DlgMail _dlgMail = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgMail>(true);
            if (_dlgMail != null)
            {
                if (args.playerModelType == PlayerModelType.Mails)
                {
                    _dlgMail.RefreshDlgMail().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}