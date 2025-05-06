using Unity.Mathematics;
namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_GetNavMeshFromPositionHandler: AMActorLocationRpcHandler<Unit, C2M_GetNavMeshFromPosition, M2C_GetNavMeshFromPosition>
    {
        protected override async ETTask Run(Unit unit, C2M_GetNavMeshFromPosition request, M2C_GetNavMeshFromPosition response)
        {
            float3 homePos = request.Position;
            NavmeshManagerComponent.NavMeshData navMesh = RecastHelper.GetNavMesh(unit.DomainScene(), homePos);
            response.Indices = navMesh.Indices;
            response.Vertices = navMesh.Vertices;
            response.PolygonRefs = navMesh.PolygonRefs;
            await ETTask.CompletedTask;
        }
    }
}
