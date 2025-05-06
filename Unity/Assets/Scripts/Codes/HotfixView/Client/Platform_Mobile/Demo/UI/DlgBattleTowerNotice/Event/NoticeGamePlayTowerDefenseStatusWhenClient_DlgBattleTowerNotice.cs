using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeGamePlayTowerDefenseStatusWhenClient_DlgBattleTowerNotice: AEvent<Scene, ClientEventType.NoticeGamePlayTowerDefenseStatusWhenClient>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeGamePlayTowerDefenseStatusWhenClient args)
        {
            DlgBattleTowerNotice _DlgBattleTowerNotice = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleTowerNotice>(true);
            if (_DlgBattleTowerNotice != null)
            {
                bool isShow = false;
                if (args.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
                {
                    isShow = true;
                }
                else if (args.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
                {
                    isShow = true;
                }
                else if (args.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattleEnd)
                {
                    isShow = true;
                }
                await _DlgBattleTowerNotice.ShowOrHide(isShow);
            }
            await ETTask.CompletedTask;
        }
    }
}