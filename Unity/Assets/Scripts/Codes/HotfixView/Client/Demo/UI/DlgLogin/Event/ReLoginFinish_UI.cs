namespace ET.Client
{
    [Event(SceneType.Client)]
    public class ReLoginFinish_UI: AEvent<Scene, EventType.ReLoginFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.ReLoginFinish args)
        {
            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);
            PingComponent pingComponent = sessionComponent.Session.GetComponent<PingComponent>();
            DebugShowComponent.Instance.SetPing(pingComponent);

            sessionComponent.Session.AddComponent<ReLoginComponent>();
        }
    }
}