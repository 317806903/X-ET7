using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client | SceneType.Current)]
    public class NoticeShowBattleNotice_RefreshDlgBattleTowerNotice: AEvent<Scene, EventType.NoticeShowBattleNotice>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeShowBattleNotice args)
        {
            DlgBattleTowerNotice _DlgBattleTowerNotice = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerNotice>(true);
            if (_DlgBattleTowerNotice != null)
            {
                string tutorialCfgId = args.tutorialCfgId;
                _DlgBattleTowerNotice.RefreshWhenNoticeShowBattleNotice(tutorialCfgId).Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}