using Unity.Mathematics;

namespace ET.Server
{
    [Event(SceneType.Map)]
    public class ChangeRotation_SyncUnit: AEvent<Scene, ET.EventType.ChangeRotation>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.ChangeRotation args)
        {
            Unit unit = args.Unit;
            
            Ability.UnitHelper.AddSyncPosUnit(unit);
            await ETTask.CompletedTask;
        }
    }
}