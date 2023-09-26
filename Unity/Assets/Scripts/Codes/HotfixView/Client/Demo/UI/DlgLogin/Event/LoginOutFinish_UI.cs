namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginOutFinish_UI: AEvent<Scene, EventType.LoginOutFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginOutFinish args)
        {
        }
    }
}