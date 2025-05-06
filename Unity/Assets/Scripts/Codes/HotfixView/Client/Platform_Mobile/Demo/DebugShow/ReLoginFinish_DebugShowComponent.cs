namespace ET.Client
{
    [Event(SceneType.Client)]
    public class ReLoginFinish_DebugShowComponent: AEvent<Scene, ClientEventType.ReLoginFinish>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.ReLoginFinish args)
        {
            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);
            PingComponent pingComponent = sessionComponent.Session.GetComponent<PingComponent>();
            DebugShowComponent.Instance.SetPing(pingComponent);
        }
    }
}