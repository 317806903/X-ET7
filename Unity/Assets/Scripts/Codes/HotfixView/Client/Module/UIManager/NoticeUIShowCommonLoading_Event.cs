using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUIShowCommonLoading_Event: AEvent<Scene, EventType.NoticeUIShowCommonLoading>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUIShowCommonLoading args)
        {
            bool bForceShow = args.bForce;
            ET.Client.UIManagerHelper.ShowCommonLoading(scene, bForceShow);
        }
    }
}