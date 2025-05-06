using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUISeasonRemainChg_RefreshDlgChallengeMode: AEvent<Scene, ClientEventType.NoticeUISeasonRemainChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeUISeasonRemainChg args)
        {
            DlgChallengeMode _DlgChallengeMode = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgChallengeMode>(true);
            if (_DlgChallengeMode != null)
            {
                _DlgChallengeMode.RefreshWhenSeasonRemainChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}