using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Server
{
    public class AI_Escape: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
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
            GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlayer(unit.DomainScene());
            if (gamePlayComponent == null)
            {
                return null;
            }
            Unit unitHeadQuarter = ET.GamePlayHelper.GetHomeUnit(gamePlayComponent);
            return unitHeadQuarter;
        }
        
        public static bool ChkIsBlockWhenToHeadQuarter(Unit unit)
        {
            List<float3> list = ListComponent<float3>.Create();
            Unit headQuarterUnit = GetHeadQuarter(unit);
            unit.GetComponent<PathfindingComponent>().Find(unit.Position, headQuarterUnit.Position, list);
            if (list.Count == 0)
            {
                return true;
            }

            return false;
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
                
                if (Ability.UnitHelper.ChkCanAttack(unit, headQuarterUnit, 0.3f))
                {
                    Log.Debug($"-------------开始靠近 3333 {nextTarget}");
                    
                    aiComponent.Cancel();
                    unit.Dispose();
                    return;
                }
                
                await unit.FindPathMoveToAsync(nextTarget, null);
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