using Unity.Mathematics;

namespace ET.Client
{
    public class AI_KaoJin: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            long sec = TimeHelper.ClientFrameTime() / 1000 % 15;
            if (sec < 10)
            {
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return;
            }

            ListComponent<Unit> hostileForces = Ability.UnitHelper.GetHostileForces(unit);
            Unit unitPlayer = null;
            foreach (Unit hostileForce in hostileForces)
            {
                if (Ability.UnitHelper.ChkIsPlayer(hostileForce))
                {
                    unitPlayer = hostileForce;
                    break;
                }
            }

            if (unitPlayer == null)
            {
                return;
            }

            Log.Debug("开始靠近");

            while (true)
            {
                float3 nextTarget = unitPlayer.Position;
                await unit.MoveToAsync(nextTarget, cancellationToken);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
            }
        }
    }
}