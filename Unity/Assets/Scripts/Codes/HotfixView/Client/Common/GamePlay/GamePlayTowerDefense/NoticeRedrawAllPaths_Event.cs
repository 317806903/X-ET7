namespace ET.Client
{
    [Event(SceneType.Current)]
    public class NoticeRedrawAllPaths_Event: AEvent<Scene, ClientEventType.NoticeRedrawAllPaths>
    {
        protected override async ETTask Run(Scene scene, ClientEventType.NoticeRedrawAllPaths args)
        {
            await GamePlayHelper.GetGamePlayTowerDefense(scene).DrawPaths(args.pathToDraw.Path);
            await ETTask.CompletedTask;
        }
    }
}
