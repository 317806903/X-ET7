using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgArcadeCoinUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            DlgArcadeCoin _DlgArcadeCoin = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgArcadeCoin>(true);
            if (_DlgArcadeCoin != null)
            {
                if (args.playerModelType == PlayerModelType.TokenArcadeCoinAdd
                    || args.playerModelType == PlayerModelType.TokenArcadeCoinReduce)
                {
                    _DlgArcadeCoin.RefreshWhenBaseInfoChg().Coroutine();
                }
            }
            await ETTask.CompletedTask;
        }
    }
}