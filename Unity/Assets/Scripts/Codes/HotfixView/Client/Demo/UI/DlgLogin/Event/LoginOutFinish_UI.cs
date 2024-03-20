using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginOutFinish_UI: AEvent<Scene, EventType.LoginOutFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginOutFinish args)
        {
            bool isNeedLoginOutAccount = args.isNeedLoginOutAccount;
            UIGuideComponent.Instance?.Dispose();

            Player player = PlayerHelper.GetMyPlayer(scene);
            Log.Debug($"--zpb player[{player}]");
            if (player.LoginType == LoginType.UnitySDK || player.LoginType == LoginType.GoogleSDK || player.LoginType == LoginType.AppleSDK)
            {
                if (isNeedLoginOutAccount)
                {
                    ET.Client.LoginSDKManagerComponent.Instance.SetClientRecordAccountLoginTimeNone();
                    ET.Client.LoginSDKManagerComponent.Instance.SDKLoginOut(false).Coroutine();
                }
            }
            else
            {
                if (isNeedLoginOutAccount)
                {
                    string key = $"GuestPlayerLoginTime_{LoginType.Editor.ToString()}";
                    PlayerPrefs.SetString(key, "0");
                }
            }
        }
    }
}