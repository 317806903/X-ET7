using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgGameModeARUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgGameModeAR _DlgGameModeAR = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgGameModeAR>(true);
            if (_DlgGameModeAR != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgGameModeAR.RefreshWhenBaseInfoChg().Coroutine();
                }
                else if (args.playerModelType == PlayerModelType.FunctionMenu)
                {
                    _DlgGameModeAR.RefreshWhenFunctionMenuChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}