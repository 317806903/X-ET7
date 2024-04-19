using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgBattleDeckUI: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType != PlayerModelType.BattleCard)
            {
                return;
            }
            DlgBattleDeck _DlgBattleDeck = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleDeck>(true);
            if (_DlgBattleDeck != null)
            {
                _DlgBattleDeck.Refresh().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}