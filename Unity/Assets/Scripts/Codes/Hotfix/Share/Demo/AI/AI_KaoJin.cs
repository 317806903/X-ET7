using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_KaoJin: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig, bool isFirst)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return 1;
            }
            if (ET.Ability.UnitHelper.ChkCanMove(unit) == false)
            {
                return 1;
            }

            if (isFirst)
            {
                return 0;
            }
            else
            {
                long sec = TimeHelper.ClientFrameTime() / 1000 % 15;
                if (true || sec < 10)
                {
                    return 0;
                }
            }

            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AICfg aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                aiComponent.Cancel();
                return;
            }

            List<Unit> hostileForces = Ability.UnitHelper.GetHostileForces(unit, false);
            Unit unitPlayer = null;
            float nearPlayerDisSq = 0;
            foreach (Unit hostileForce in hostileForces)
            {
                if (Ability.UnitHelper.ChkIsPlayer(hostileForce))
                {
                    float disSq = math.lengthsq(unit.Position - hostileForce.Position);
                    if (unitPlayer == null)
                    {
                        unitPlayer = hostileForce;
                        nearPlayerDisSq = disSq;
                    }
                    else
                    {
                        if (nearPlayerDisSq > disSq)
                        {
                            unitPlayer = hostileForce;
                            nearPlayerDisSq = disSq;
                        }
                    }
                }
            }

            if (unitPlayer == null && hostileForces.Count > 0)
            {
                unitPlayer = hostileForces[0];
            }

            if (ET.Ability.UnitHelper.ChkUnitAlive(unitPlayer) == false)
            {
                aiComponent.Cancel();
                return;
            }

            aiComponent.ResetRepeatedTimerByDis(unitPlayer);

            //Log.Debug("开始靠近 11");

            while (true)
            {
                if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                if (ET.Ability.UnitHelper.ChkUnitAlive(unitPlayer) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                aiComponent.ResetRepeatedTimerByDis(unitPlayer);

                if (ET.Ability.UnitHelper.ChkIsNear(unit, unitPlayer, 0, false))
                {
                    await ET.Ability.MoveOrIdleHelper.DoIdle(unit);
                    await TimerComponent.Instance.WaitAsync(500, cancellationToken);
                    if (cancellationToken.IsCancel())
                    {
                        return;
                    }
                    continue;
                }

                float3 nextTarget = unitPlayer.Position;
                //Log.Debug($"开始靠近 22 {nextTarget}");
                //await unit.FindPathMoveToAsync(nextTarget, null);
                await ET.Ability.MoveOrIdleHelper.DoMoveTargetPosition(unit, nextTarget);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
                await TimerComponent.Instance.WaitAsync(500, cancellationToken);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
            }
        }
    }
}