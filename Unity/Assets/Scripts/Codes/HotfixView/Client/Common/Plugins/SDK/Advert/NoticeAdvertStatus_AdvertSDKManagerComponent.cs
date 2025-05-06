using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeAdvertStatus_AdvertSDKManagerComponent: AEvent<Scene, ClientEventType.NoticeAdvertStatusChg>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeAdvertStatusChg args)
        {
            bool IsAdmobAvailable = args.IsAvailable;
            ET.Client.AdvertSDKManagerComponent.Instance.SetIsAvailable(IsAdmobAvailable);
        }
    }
}