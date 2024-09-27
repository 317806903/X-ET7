using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_TowerDefense_Escape: AAIHandler
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

            if (aiComponent.GetTargetUnit(aiConfig, 0, false) == null)
            {
                return 1;
            }

            return 0;
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

                if (aiComponent.ChkCanAttack(unit, targetUnit, 0f, false))
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