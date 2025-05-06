using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgBattleDeckUI: AEvent<Scene, ClientEventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticePlayerCacheChg args)
        {
            DlgBattleDeck _DlgBattleDeck = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleDeck>(true);
            if (_DlgBattleDeck != null)
            {
                _DlgBattleDeck.RefreshWhenPlayerModelChg(args.playerModelType).Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}