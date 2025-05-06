using Unity.Mathematics;

namespace ET.Server
{
    [Event(SceneType.Map)]
    public class StopMove_NotifyClient: AEvent<Scene, ET.EventType.StopMove>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.StopMove args)
        {
            MoveHelper.NoticeStopToClient(args.unit, args.error);
            await ETTask.CompletedTask;
        }
    }
}