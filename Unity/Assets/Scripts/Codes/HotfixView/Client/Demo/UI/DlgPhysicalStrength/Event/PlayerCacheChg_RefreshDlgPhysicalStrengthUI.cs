using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgPhysicalStrengthUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgPhysicalStrength _DlgPhysicalStrength = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgPhysicalStrength>(true);
            if (_DlgPhysicalStrength != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgPhysicalStrength.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}