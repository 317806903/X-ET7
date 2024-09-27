using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUISeasonIndexChg_RefreshDlgRankPowerupSeason: AEvent<Scene, EventType.NoticeUISeasonIndexChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUISeasonIndexChg args)
        {
            DlgRankPowerupSeason _DlgRankPowerupSeason = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgRankPowerupSeason>(true);
            if (_DlgRankPowerupSeason != null)
            {
                UIManagerHelper.GetUIComponent(scene).HideWindow<DlgRankPowerupSeason>();
                await UIManagerHelper.EnterGameModeUI(scene);
            }
            await ETTask.CompletedTask;
        }
    }
}