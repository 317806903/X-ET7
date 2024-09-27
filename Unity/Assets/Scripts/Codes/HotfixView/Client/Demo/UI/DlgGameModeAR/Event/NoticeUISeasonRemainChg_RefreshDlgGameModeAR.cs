using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUISeasonRemainChg_RefreshDlgGameModeAR: AEvent<Scene, EventType.NoticeUISeasonRemainChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUISeasonRemainChg args)
        {
            DlgGameModeAR _DlgGameModeAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgGameModeAR>(true);
            if (_DlgGameModeAR != null)
            {
                _DlgGameModeAR.RefreshWhenSeasonRemainChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}