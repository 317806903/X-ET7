namespace ET.Client
{
    [Event(SceneType.Client)]
    public class Appsflyer_Event: AEvent<Scene, EventType.AppsFlyerTutorialCompleted>
    {
        protected override async ETTask Run(Scene scene, EventType.AppsFlyerTutorialCompleted args)
        {
            AppsflyerSDKComponent.Instance.SendEvent_TutorialCompleted(args.isTutorialCompleted, args.tutorialId, args.tutorialName);
            await ETTask.CompletedTask;
        }
    }
}
