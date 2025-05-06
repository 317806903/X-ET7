using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginOutFinish_UI: AEvent<Scene, ClientEventType.LoginOutFinish>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.LoginOutFinish args)
        {
            bool isNeedLoginOutAccount = args.isNeedLoginOutAccount;
            ET.Client.UIGuideHelper.ClearUIGuide();

            Player player = PlayerStatusHelper.GetMyPlayer(scene);
            Log.Debug($"--zpb player[{player}]");
            if (player == null)
            {
                return;
            }
            if (player.LoginType == LoginType.UnitySDK ||
                player.LoginType == LoginType.GoogleSDK ||
                player.LoginType == LoginType.AppleSDK)
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