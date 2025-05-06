namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_GetNearestNavMeshPointHandler : AMActorLocationRpcHandler<Unit, C2M_GetNearestNavMeshPoint, M2C_GetNearestNavMeshPoint>
    {
        protected override async ETTask Run(Unit unit, C2M_GetNearestNavMeshPoint request, M2C_GetNearestNavMeshPoint response)
        {
            long polygonRef;
            response.NavMeshPosition = RecastHelper.GetNearNavmeshPos(unit, request.Position, out polygonRef);
            response.PolygonRef = polygonRef;
            await ETTask.CompletedTask; 
        }
    }
}

