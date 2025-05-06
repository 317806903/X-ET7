namespace ET.Client
{
    [Event(SceneType.Client)]
    public class TutorialCompleted_AttributionModelSDKManagerComponent: AEvent<Scene, ClientEventType.TutorialCompleted>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.TutorialCompleted args)
        {
            AttributionModelSDKManagerComponent.Instance.SendEvent_TutorialCompleted(args.isTutorialCompleted, args.tutorialId, args.tutorialName);
            await ETTask.CompletedTask;
        }
    }
}
