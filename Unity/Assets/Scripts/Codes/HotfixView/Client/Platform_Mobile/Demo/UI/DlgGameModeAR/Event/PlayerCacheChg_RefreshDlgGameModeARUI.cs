using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgGameModeARUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
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
                else if (args.playerModelType == PlayerModelType.OtherInfo)
                {
                    _DlgGameModeAR.RefreshWhenOtherInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}