using ET.EventType;
namespace ET.Client
{
    [Event(SceneType.Current)]
    public class NoticeRedrawAllPaths_Event: AEvent<Scene, NoticeRedrawAllPaths>
    {
        protected override async ETTask Run(Scene scene, NoticeRedrawAllPaths args)
        {
            GamePlayHelper.GetGamePlayTowerDefense(scene).DrawPaths(args.pathToDraw.Path);
            await ETTask.CompletedTask;
        }
    }
}
