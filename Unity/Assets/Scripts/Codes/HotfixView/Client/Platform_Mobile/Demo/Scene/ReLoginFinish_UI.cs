namespace ET.Client
{
    [Event(SceneType.Client)]
    public class ReLoginFinish_UI: AEvent<Scene, ClientEventType.ReLoginFinish>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ReLoginFinish args)
        {
            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);

            if (sessionComponent.Session.GetComponent<ReLoginComponent>() == null)
            {
                sessionComponent.Session.AddComponent<ReLoginComponent>();
            }
        }
    }
}