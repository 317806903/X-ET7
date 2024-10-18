using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeGamePlayPKStatusWhenClient_DlgBattleCameraPlayerSkill: AEvent<Scene, EventType.NoticeGamePlayPKStatusWhenClient>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeGamePlayPKStatusWhenClient args)
        {
            DlgBattleCameraPlayerSkill _DlgBattleCameraPlayerSkill = UIManagerHelper.GetUIComponent(scene).GetDlgLogic<DlgBattleCameraPlayerSkill>(true);
            if (_DlgBattleCameraPlayerSkill != null)
            {
                bool isShow = true;
                bool isHigh = false;
                _DlgBattleCameraPlayerSkill.ShowOrHide(isShow, isHigh);
            }
            await ETTask.CompletedTask;
        }
    }
}