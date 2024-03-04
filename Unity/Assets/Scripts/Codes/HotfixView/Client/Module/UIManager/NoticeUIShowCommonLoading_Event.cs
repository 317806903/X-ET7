using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class NoticeUIShowCommonLoading_Event: AEvent<Scene, EventType.NoticeUIShowCommonLoading>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUIShowCommonLoading args)
        {
            ET.Client.UIManagerHelper.ShowCommonLoading(scene);
        }
    }
}