using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_TowerDefense_Escape: AAIHandler
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

            if (aiComponent.GetTargetUnit(aiConfig, 0, false) == null)
            {
                return ET.AIChkResult.False;
            }

            return ET.AIChkResult.True;
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
                if (ET.Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                aiComponent.ResetRepeatedTimerByDis(targetUnit);

                float3 nextTarget = targetUnit.Position;
                //Log.Debug($"开始靠近 22 {nextTarget}");

                if (aiComponent.ChkCanAttack(unit, targetUnit, 0f, false, false))
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
                    gamePlayTowerDefenseComponent.DealEscape(unit, targetUnit);

                    aiComponent.Cancel();
                    return;
                }

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