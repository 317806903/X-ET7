using ET.Ability;
using Unity.Mathematics;
namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_GetNavMeshFromHeadQuarterHandler: AMActorLocationRpcHandler<Unit, C2M_GetNavMeshFromHeadQuarter, M2C_GetNavMeshFromHeadQuarter>
    {
        protected override async ETTask Run(Unit unit, C2M_GetNavMeshFromHeadQuarter request, M2C_GetNavMeshFromHeadQuarter response)
        {
            TeamFlagType homeTeamFlagType = (TeamFlagType)request.HomeTeamFlagType;
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
            PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
            Unit homeUnit = putHomeComponent.GetHomeUnit(homeTeamFlagType);
            if (homeUnit == null)
            {
                response.Vertices = null;
                response.Indices = null;
                return;
            }
            float3 homePos = homeUnit.Position;
            NavmeshManagerComponent.NavMeshData navmeshManagerComponent = RecastHelper.GetNavMesh(unit.DomainScene(), homePos);
            response.Indices = navmeshManagerComponent.Indices;
            response.Vertices = navmeshManagerComponent.Vertices;
            await ETTask.CompletedTask;
        }
    }
}
