using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUITip_UIManagerHelper: AEvent<Scene, ClientEventType.NoticeUITip>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeUITip args)
        {
            string tipMsg = args.tipMsg;
            UIManagerHelper.ShowTip(scene, tipMsg);
        }
    }
}