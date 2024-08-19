using System.Collections.Generic;
using ET.Ability;
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
                    if (aiComponent.GetTargetUnit(aiConfig, 0, false) == null)
                    {
                        return 1;
                    }
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

            Unit targetUnit = aiComponent.GetTargetUnit(aiConfig, 0, false);

            if (ET.Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
            {
                aiComponent.Cancel();
                return;
            }

            aiComponent.ResetRepeatedTimerByDis(targetUnit);

            //Log.Debug("开始靠近 11");

            while (true)
            {
                if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                targetUnit = aiComponent.GetTargetUnit(aiConfig, 0, false);
                if (ET.Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                aiComponent.ResetRepeatedTimerByDis(targetUnit);

                if (ET.Ability.UnitHelper.ChkIsNear(unit, targetUnit, 0, false))
                {
                    await ET.Ability.MoveOrIdleHelper.DoIdle(unit);
                    await TimerComponent.Instance.WaitAsync(500, cancellationToken);
                    if (cancellationToken.IsCancel())
                    {
                        return;
                    }
                    continue;
                }

                float3 nextTarget = targetUnit.Position;
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