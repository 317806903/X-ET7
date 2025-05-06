namespace ET.Server
{
    [Event(SceneType.Map)]
    public class SendDrawPathsToClients_Handler: AEvent<Scene, ET.EventType.SendDrawPathsToClients>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.SendDrawPathsToClients args)
        {
            UnitComponent unitComponent = ET.Ability.UnitHelper.GetUnitComponent(scene);
            foreach (var observerUnit in unitComponent.GetRecordList(UnitType.ObserverUnit))
            {
                MessageHelper.SendToClient(observerUnit, args.pathToDraw);
            }
            await ETTask.CompletedTask;
        }
    } 
}
