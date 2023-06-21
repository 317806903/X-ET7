namespace ET.Client
{
    [Event(SceneType.Client)]
    public class LoginFinish_UI: AEvent<Scene, EventType.LoginFinish>
    {
        protected override async ETTask Run(Scene scene, EventType.LoginFinish args)
        {
            await SceneHelper.EnterHall(scene);
        }
    }
}