using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUIShowCommonLoading_UIManagerHelper: AEvent<Scene, ClientEventType.NoticeUIShowCommonLoading>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeUIShowCommonLoading args)
        {
            bool bForceShow = args.bForce;
            ET.Client.UIManagerHelper.ShowCommonLoading(scene, bForceShow);
        }
    }
}