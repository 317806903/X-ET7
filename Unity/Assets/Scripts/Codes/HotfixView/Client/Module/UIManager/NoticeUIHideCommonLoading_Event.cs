using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeUIHideCommonLoading_Event: AEvent<Scene, EventType.NoticeUIHideCommonLoading>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeUIHideCommonLoading args)
        {
            bool bForceHide = args.bForce;
            ET.Client.UIManagerHelper.HideCommonLoading(scene, bForceHide);
        }
    }
}