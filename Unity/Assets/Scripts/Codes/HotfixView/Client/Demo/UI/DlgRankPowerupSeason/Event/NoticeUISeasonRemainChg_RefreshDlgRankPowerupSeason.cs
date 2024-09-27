using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUISeasonRemainChg_RefreshDlgRankPowerupSeason: AEvent<Scene, EventType.NoticeUISeasonRemainChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUISeasonRemainChg args)
        {
            DlgRankPowerupSeason _DlgRankPowerupSeason = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgRankPowerupSeason>(true);
            if (_DlgRankPowerupSeason != null)
            {
                _DlgRankPowerupSeason.RefreshWhenSeasonRemainChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}