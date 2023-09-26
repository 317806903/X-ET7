using Unity.Mathematics;

namespace ET.Server
{
    [Event(SceneType.Map)]
    public class PointList_NotifyClient: AEvent<Scene, ET.EventType.MovePointList>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.MovePointList args)
        {
            MoveHelper.NoticePointListToClient(args.unit, args.pointList);
            await ETTask.CompletedTask;
        }
    }
}