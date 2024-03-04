using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginOutFinish_UI: AEvent<Scene, EventType.LoginOutFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginOutFinish args)
        {
            UIGuideComponent.Instance?.Dispose();

            Player player = PlayerHelper.GetMyPlayer(scene);
            Log.Debug($"--zpb player[{player}]");
            if (player.LoginType == LoginType.UnitySDK || player.LoginType == LoginType.GoogleSDK || player.LoginType == LoginType.AppleSDK)
            {
                ET.Client.LoginSDKManagerComponent.Instance.SetClientRecordAccountLoginTimeNone();
                ET.Client.LoginSDKManagerComponent.Instance.SDKLoginOut(false).Coroutine();
            }else{
                string key = $"GuestPlayerLoginTime_{LoginType.Editor.ToString()}";
                PlayerPrefs.SetString(key, "0");
            }
        }
    }
}