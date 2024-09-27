using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AudioPlay_Event: AEvent<Scene, EventType.SyncPlayAudio>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncPlayAudio args)
        {
            List<(Unit unit, string playAudioActionId, bool isOnlySelfShow)> list = args.list;
            if (list == null)
            {
                return;
            }
            foreach ((Unit unit, string playAudioActionId, bool isOnlySelfShow) in list)
            {
                ET.Ability.Client.AudioPlayHelper.PlayAudio(unit, playAudioActionId);
            }
        }
    }
}