namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginOutFinish_UI: AEvent<Scene, EventType.LoginOutFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginOutFinish args)
        {
            Player player = PlayerHelper.GetMyPlayer(scene);
            Log.Debug($"--zpb player[{player}]");
            if (player.LoginType == LoginType.UnitySDK)
            {
                ET.Client.LoginSDKComponent.Instance.SetClientRecordAccountLoginTimeNone();
                ET.Client.LoginSDKComponent.Instance.SDKLoginOut(false).Coroutine();
            }
        }
    }
}