using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgGameModeArcadecadeUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgGameModeArcade _DlgGameModeArcade = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgGameModeArcade>(true);
            if (_DlgGameModeArcade != null)
            {
                if (args.playerModelType == PlayerModelType.BaseInfo)
                {
                    _DlgGameModeArcade.RefreshWhenBaseInfoChg().Coroutine();
                }
                else if (args.playerModelType == PlayerModelType.FunctionMenu)
                {
                    _DlgGameModeArcade.RefreshWhenFunctionMenuChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}