using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerPowerUPCacheChg_RefreshDlgRankPowerupSeason : AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            DlgRankPowerupSeason _dgRankPowerupSeason = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgRankPowerupSeason>(true);
            if (_dgRankPowerupSeason != null)
            {
                if (args.playerModelType == PlayerModelType.BackPack)
                {
                    await _dgRankPowerupSeason.RefreshWhenDiamondChg();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}