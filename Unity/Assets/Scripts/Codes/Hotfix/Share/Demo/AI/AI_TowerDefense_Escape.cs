using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_TowerDefense_Escape: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig)
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

            if (GetHeadQuarter(unit) == null)
            {
                return 1;
            }
            if (ChkIsBlockWhenToHeadQuarter(unit))
            {
                return 1;
            }


            return 0;
        }

        public static Unit GetHeadQuarter(Unit unit)
        {
            GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
            if (gamePlayTowerDefenseComponent == null)
            {
                return null;
            }
            Unit unitHeadQuarter = gamePlayTowerDefenseComponent.GetHomeUnit();
            return unitHeadQuarter;
        }

        public static bool ChkIsBlockWhenToHeadQuarter(Unit unit)
        {
            Unit headQuarterUnit = GetHeadQuarter(unit);
            if (headQuarterUnit == null)
            {
                return true;
            }
            if (Ability.UnitHelper.ChkCanAttack(unit, headQuarterUnit, 0f))
            {
                return false;
            }

            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return true;
            }
            bool canArrive = pathfindingComponent.ChkCanArrive(unit.Position, headQuarterUnit.Position);
            return canArrive == false;
        }

        public override async ETTask Execute(AIComponent aiComponent, AICfg aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                aiComponent.Cancel();
                return;
            }

            Unit headQuarterUnit = GetHeadQuarter(unit);

            //Log.Debug("开始靠近 11");

            while (true)
            {
                if (ET.Ability.UnitHelper.ChkUnitAlive(unit) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                if (ET.Ability.UnitHelper.ChkUnitAlive(headQuarterUnit) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                if (ChkIsBlockWhenToHeadQuarter(unit))
                {
                    aiComponent.Cancel();
                    return;
                }

                float3 nextTarget = headQuarterUnit.Position;
                //Log.Debug($"开始靠近 22 {nextTarget}");

                if (Ability.UnitHelper.ChkCanAttack(unit, headQuarterUnit, 0f, true))
                {
                    GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(unit.DomainScene());
                    gamePlayTowerDefenseComponent.DealEscape(unit);

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