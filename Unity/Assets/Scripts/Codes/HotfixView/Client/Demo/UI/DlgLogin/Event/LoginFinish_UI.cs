using NUnit.Framework;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginFinish_UI: AEvent<Scene, EventType.LoginFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginFinish args)
        {
            if (DebugConnectComponent.Instance.IsDebugMode == false)
            {
                PlayerBaseInfoComponent playerBaseInfoComponent =
                    await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
                //Log.Debug($"--LoginFinish_UI playerBaseInfoComponent[{playerBaseInfoComponent}]");
                if (playerBaseInfoComponent.isFinishTutorialFirst == false)
                {
                    ET.Client.UIGuideHelper.DoUIGuide(scene, "TutorialFirst", () =>
                    {
                        FinishedCallBack(scene).Coroutine();

                    }).Coroutine();
                }
            }

            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);
            PingComponent pingComponent = sessionComponent.Session.GetComponent<PingComponent>();
            DebugShowComponent.Instance.SetPing(pingComponent);

            await SceneHelper.EnterHall(scene, true);
        }

        protected async ETTask FinishedCallBack(Scene scene)
        {
            if (scene.IsDisposed)
            {
                return;
            }
            PlayerBaseInfoComponent playerBaseInfoComponent = await ET.Client.PlayerCacheHelper.GetMyPlayerBaseInfo(scene);
            playerBaseInfoComponent.isFinishTutorialFirst = true;
            await ET.Client.PlayerCacheHelper.SaveMyPlayerModel(scene, PlayerModelType.BaseInfo, new (){"isFinishTutorialFirst"});
        }
    }
}