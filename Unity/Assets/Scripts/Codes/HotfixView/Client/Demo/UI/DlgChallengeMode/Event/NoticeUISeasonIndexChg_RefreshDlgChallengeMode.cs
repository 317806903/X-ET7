using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUISeasonIndexChg_RefreshDlgChallengeMode: AEvent<Scene, EventType.NoticeUISeasonIndexChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUISeasonIndexChg args)
        {
            DlgChallengeMode _DlgChallengeMode = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgChallengeMode>(true);
            if (_DlgChallengeMode != null)
            {
                UIManagerHelper.GetUIComponent(scene).HideWindow<DlgChallengeMode>();
                await UIManagerHelper.EnterGameModeUI(scene);
            }
            await ETTask.CompletedTask;
        }
    }
}