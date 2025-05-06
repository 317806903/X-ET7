namespace ET.Client
{
    [Event(SceneType.Client)]
    public class AfterCreateClientScene_AddComponent: AEvent<Scene, ClientEventType.AfterCreateClientScene>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.AfterCreateClientScene args)
        {
            scene.AddComponent<UIEventComponent>();
            scene.AddComponent<UIPathComponent>();
            scene.AddComponent<UITextLocalizeComponent>();
            scene.AddComponent<UIImageLocalizeComponent>();
            scene.AddComponent<UIComponent>();
            scene.AddComponent<UIAudioManagerComponent>();
            scene.AddComponent<AuthorizedPermissionManagerComponent>();

            // scene.AddComponent<FUIEventComponent>();
            // scene.AddComponent<FUIAssetComponent>();
            // scene.AddComponent<FUIComponent>();
            await ETTask.CompletedTask;
        }
    }
}