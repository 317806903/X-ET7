namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterCreateCurrentScene_AddComponent: AEvent<Scene, ClientEventType.AfterCreateCurrentScene>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.AfterCreateCurrentScene args)
        {
            //scene.AddComponent<UIComponent>();
            //scene.AddComponent<FUIComponent>();
            scene.AddComponent<PathLineRendererComponent>();
            scene.AddComponent<HealthBarNormalManager>();
            scene.AddComponent<NavMeshRendererComponent>();
            scene.AddComponent<ShootTextComponent>();
            scene.AddComponent<ShowGetGoldTextComponent>();
            scene.AddComponent<ModelClickManagerComponent>();
            await ETTask.CompletedTask;
        }
    }
}