using System;
using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.Current|SceneType.Client)]
    public class ShowAdvert_AdvertSDKManagerComponent: AEvent<Scene, ClientEventType.ShowAdvert>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ShowAdvert args)
        {
            await ET.Client.AdvertSDKManagerComponent.Instance.ShowRewardedAd(args.rewardCallback, args.failCallback);
        }
    }
}