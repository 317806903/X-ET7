using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_KaoJin: AAIHandler
    {
        public override ET.AIChkResult Check(AIComponent aiComponent, AICfg aiConfig, bool isFirst)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return ET.AIChkResult.False;
            }
            if (ET.Ability.UnitHelper.ChkCanMove(unit) == false)
            {
                return ET.AIChkResult.False;
            }

            if (isFirst)
            {
                return ET.AIChkResult.True;
            }
            else
            {
                long sec = TimeHelper.ClientFrameTime() / 1000 % 15;
                if (true || sec < 10)
                {
                    if (aiComponent.GetTargetUnit(aiConfig, 0, false) == null)
                    {
                        return ET.AIChkResult.False;
                    }
                    return ET.AIChkResult.True;
                }
            }

            return ET.AIChkResult.False;
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
                if (aiComponent.IsDisposed)
                {
                    return;
                }

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