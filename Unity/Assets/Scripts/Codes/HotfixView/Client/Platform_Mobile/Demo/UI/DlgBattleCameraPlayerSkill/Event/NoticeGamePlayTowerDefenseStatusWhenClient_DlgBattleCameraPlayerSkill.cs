using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeGamePlayTowerDefenseStatusWhenClient_DlgBattleCameraPlayerSkill: AEvent<Scene, ClientEventType.NoticeGamePlayTowerDefenseStatusWhenClient>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeGamePlayTowerDefenseStatusWhenClient args)
        {
            DlgBattleCameraPlayerSkill _DlgBattleCameraPlayerSkill = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleCameraPlayerSkill>(true);
            if (_DlgBattleCameraPlayerSkill != null)
            {
                bool isShow = false;
                bool isHigh = false;
                if (args.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.RestTime)
                {
                    isShow = true;
                    isHigh = true;
                }
                else if (args.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattle)
                {
                    isShow = true;
                }
                else if (args.gamePlayTowerDefenseStatus == GamePlayTowerDefenseStatus.InTheBattleEnd)
                {
                    isShow = true;
                }
                _DlgBattleCameraPlayerSkill.ShowOrHide(isShow, true, isHigh);
            }
            await ETTask.CompletedTask;
        }
    }
}