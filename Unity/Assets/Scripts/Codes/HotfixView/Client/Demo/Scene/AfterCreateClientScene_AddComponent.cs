namespace ET.Client
{
    [Event(SceneType.Client)]
    public class AfterCreateClientScene_AddComponent: AEvent<Scene, EventType.AfterCreateClientScene>
    {
        protected override async ETTask Run(Scene scene, EventType.AfterCreateClientScene args)
        {
            scene.AddComponent<UIEventComponent>();
            scene.AddComponent<UIPathComponent>();
            scene.AddComponent<UIComponent>();
            
            // scene.AddComponent<FUIEventComponent>();
            // scene.AddComponent<FUIAssetComponent>();
            // scene.AddComponent<FUIComponent>();
            scene.AddComponent<LocalizeComponent>();
            await ETTask.CompletedTask;
        }
    }
}