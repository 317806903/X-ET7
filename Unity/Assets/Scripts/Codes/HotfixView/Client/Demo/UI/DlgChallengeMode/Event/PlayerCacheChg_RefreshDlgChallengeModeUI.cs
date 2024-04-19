using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgChallengeModeUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgChallengeMode _DlgChallengeMode = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgChallengeMode>(true);
            if (_DlgChallengeMode != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgChallengeMode.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}