using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUISeasonIndexChg_RefreshDlgGameModeAR: AEvent<Scene, EventType.NoticeUISeasonIndexChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUISeasonIndexChg args)
        {
            DlgGameModeAR _DlgGameModeAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgGameModeAR>(true);
            if (_DlgGameModeAR != null)
            {
                _DlgGameModeAR.RefreshWhenSeasonIndexChg().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}