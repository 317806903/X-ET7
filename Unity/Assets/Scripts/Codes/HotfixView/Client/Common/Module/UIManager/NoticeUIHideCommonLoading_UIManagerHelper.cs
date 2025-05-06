using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUIHideCommonLoading_UIManagerHelper: AEvent<Scene, ClientEventType.NoticeUIHideCommonLoading>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeUIHideCommonLoading args)
        {
            bool bForceHide = args.bForce;
            ET.Client.UIManagerHelper.HideCommonLoading(scene, bForceHide);
        }
    }
}