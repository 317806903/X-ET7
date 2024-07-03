using System;
using ET.Ability.Client;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class NoticeAdmobSDKStatus_Event: AEvent<Scene, EventType.NoticeAdmobSDKStatus>
    {
        protected override async ETTask Run(Scene scene, EventType.NoticeAdmobSDKStatus args)
        {
            bool IsAdmobAvailable = args.IsAdmobAvailable;
            ET.Client.AdmobSDKComponent.Instance.SetIsAdmobAvailable(IsAdmobAvailable);
        }
    }
}