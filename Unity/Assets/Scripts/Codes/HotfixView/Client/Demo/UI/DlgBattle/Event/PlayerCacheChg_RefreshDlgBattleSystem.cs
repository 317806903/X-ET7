using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class PlayerCacheChg_RefreshDlgBattleSystem: AEvent<Scene, EventType.NoticePlayerCacheChg>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticePlayerCacheChg args)
        {
            if (args.playerModelType != PlayerModelType.Skills)
            {
                return;
            }
            DlgBattle _DlgBattle = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattle>(true);
            if (_DlgBattle != null)
            {
                _DlgBattle.RefreshSkill().Coroutine();
            }
            await ETTask.CompletedTask;
        }
    }
}