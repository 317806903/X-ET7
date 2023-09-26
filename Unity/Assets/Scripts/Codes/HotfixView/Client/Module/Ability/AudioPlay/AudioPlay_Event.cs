using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class AudioPlay_Event: AEvent<Scene, EventType.SyncPlayAudio>
    {
        protected override async ETTask Run(Scene scene, EventType.SyncPlayAudio args)
        {
            Unit unit = args.unit;
            string playAudioActionId = args.playAudioActionId;
            AudioPlayHelper.PlayAudio(unit, playAudioActionId);
        }
    }
}