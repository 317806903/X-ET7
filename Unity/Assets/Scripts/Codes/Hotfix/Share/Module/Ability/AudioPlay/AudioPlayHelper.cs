using System.Collections.Generic;
using ET.Ability.AbilityEventType;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class AudioPlayHelper
    {
        public static void DoAudioPlay(Unit unit, string playAudioActionId, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (string.IsNullOrEmpty(playAudioActionId))
            {
                return;
            }

            ListComponent<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, ref actionContext, true);
            if (list == null)
            {
                return;
            }

            ActionCfg_PlayAudio actionCfg_PlayAudio = ActionCfg_PlayAudioCategory.Instance.Get(playAudioActionId);
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                Ability.UnitHelper.AddSyncData_UnitPlayAudio(targetUnit, playAudioActionId, actionCfg_PlayAudio.IsOnlySelfShow);
            }
            list.Dispose();
        }

    }
}