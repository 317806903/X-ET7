using ET.Ability;
using ET.AbilityConfig;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPathHandler: AMActorLocationRpcHandler<Unit,
        C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath, M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath>
    {

        protected override async ETTask Run(Unit observerUnit, C2M_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath message,
        M2C_TryMoveUnitAndGetAllMonsterCall2HeadQuarterPath response)
        {
            Scene scene = observerUnit.DomainScene();
            if (scene == null)
            {
                response.Error = ErrorCode.ERR_LogicError;
                return;
            }
            var previousObstaclePosition = ET.RecastHelper.RemoveObstacleFromUnit(scene, message.UnitId);
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(message.UnitCfgId);
            long refId = 0;
            if (unitCfg.NavObstacleRadius > 0)
            {
                refId = RecastHelper.AddObstacle(scene, message.Position, unitCfg.NavObstacleRadius);
            }
            RecastHelper.UpdateDynamicMesh(scene);

            response.Path = RecastHelper.GetAllPathsFromMonsterCallsToHeadQuarter(observerUnit);

            // Remove the obstacles.
            if (refId != 0)
            {
                RecastHelper.RemoveObstacle(scene, refId);
            }

            // Restore the unit.
            if (previousObstaclePosition.HasValue)
            {
                ET.RecastHelper.AddObstacleFromUnit(scene, message.UnitId, previousObstaclePosition.Value);
            }
            RecastHelper.UpdateDynamicMesh(scene);

            await ETTask.CompletedTask;
        }
    }
}
