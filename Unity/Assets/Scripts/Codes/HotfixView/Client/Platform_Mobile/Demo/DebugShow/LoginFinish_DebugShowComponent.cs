using ET.AbilityConfig;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginFinish_DebugShowComponent: AEvent<Scene, ClientEventType.LoginFinish>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.LoginFinish args)
        {
            SessionComponent sessionComponent = ET.Client.SessionHelper.GetSessionCompent(scene);
            PingComponent pingComponent = sessionComponent.Session.GetComponent<PingComponent>();
            DebugShowComponent.Instance.SetPing(pingComponent);
        }
    }
}