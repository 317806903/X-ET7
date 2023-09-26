using System.Collections.Generic;
using ET.Ability.AbilityEventType;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    public static class AudioPlayHelper
    {
        public static void DoAudioPlay(Unit unit, string playAudioActionId, SelectHandle selectHandle, ActionContext actionContext)
        {
            if (string.IsNullOrEmpty(playAudioActionId))
            {
                return;
            }

            List<Unit> list = ET.Ability.SelectHandleHelper.GetSelectUnitList(unit, selectHandle, actionContext, true);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Unit targetUnit = list[i];
                EventType.SyncPlayAudio _SyncPlayAudio = new ()
                {
                    unit = targetUnit,
                    playAudioActionId = playAudioActionId,
                };
                EventSystem.Instance.Publish(unit.DomainScene(), _SyncPlayAudio);
            }
        }

    }
}