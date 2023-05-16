using Unity.Mathematics;

namespace ET.Server
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
                cancellationToken.Cancel();
                return;
            }

            ListComponent<Unit> hostileForces = Ability.UnitHelper.GetHostileForces(unit, false);
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
                cancellationToken.Cancel();
                return;
            }

            Log.Debug("开始靠近");

            while (true)
            {
                float3 nextTarget = unitPlayer.Position;
                Log.Debug($"开始靠近 {nextTarget}");
                await unit.FindPathMoveToAsync(nextTarget, null);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
                await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
            }
        }
    }
}