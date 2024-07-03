using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUITip_Event: AEvent<Scene, EventType.NoticeUITip>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUITip args)
        {
            string tipMsg = args.tipMsg;
            UIManagerHelper.ShowTip(scene, tipMsg);
        }
    }
}