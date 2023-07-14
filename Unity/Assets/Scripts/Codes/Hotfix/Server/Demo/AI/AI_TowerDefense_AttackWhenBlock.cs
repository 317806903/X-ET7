using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using SkillSlotType = ET.Ability.SkillSlotType;

namespace ET.Server
{
    public class AI_TowerDefense_AttackWhenBlock: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return 1;
            }
            if (ChkUnitCanAttack(unit) == false)
            {
                return 1;
            }
            if (GetHeadQuarter(unit) == null)
            {
                return 1;
            }
            if (ChkIsBlockWhenToHeadQuarter(unit) == false)
            {
                return 1;
            }
            if (GetHostileForce(unit, 10f) == null)
            {
                return 1;
            }
            return 0;
        }

        public static bool ChkUnitCanAttack(Unit unit)
        {
            UnitCfg unitCfg = unit.model;
            int count = unitCfg.SkillList.Count;
            return count > 0;
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
            List<float3> list = ListComponent<float3>.Create();
            Unit headQuarterUnit = GetHeadQuarter(unit);
            unit.GetComponent<PathfindingComponent>().Find(unit.Position, headQuarterUnit.Position, list);
            if (list.Count == 0)
            {
                return true;
            }
            else if (list.Count == 1)
            {
                if (list[0].Equals(unit.Position))
                {
                    if (Ability.UnitHelper.ChkIsNear(unit, headQuarterUnit))
                    {
                        return false;
                    }
                    return true;
                }
            }

            return false;
        }
        
        public static Unit GetHostileForce(Unit unit, float radius)
        {
            ListComponent<Unit> hostileForces = Ability.UnitHelper.GetHostileForces(unit, false);
            Unit unitHostileForce = null;
            foreach (Unit hostileForce in hostileForces)
            {
                if (Ability.UnitHelper.ChkCanAttack(unit, hostileForce, radius))
                {
                    unitHostileForce = hostileForce;
                    break;
                }
            }

            return unitHostileForce;
        }

        public override async ETTask Execute(AIComponent aiComponent, AICfg aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                aiComponent.Cancel();
                return;
            }

            Unit unitHostileForce = GetHostileForce(unit, 10f);
            if (unitHostileForce == null || Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
            {
                aiComponent.Cancel();
                return;
            }

            // 停在当前位置
            unit.Stop(WaitTypeError.Cancel);
            
            Log.Debug("开始攻击");
            
            while (true)
            {
                unitHostileForce = GetHostileForce(unit, 10f);
                if (unitHostileForce == null || Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
                {
                    aiComponent.Cancel();
                    return;
                }
                
                UnitCfg unitCfg = unit.model;
                int count = unitCfg.SkillList.Count;
                if (count == 0)
                {
                    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
                    if (cancellationToken.IsCancel())
                    {
                        return;
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        string skillId = unitCfg.SkillList[i];
                        (bool ret, string msg) = SkillHelper.ChkCanUseSkill(unit, skillId);
                        //Log.Debug($"开始攻击 {skillId} {ret} {msg}");
                        if (ret == false)
                        {
                            Log.Debug($"SkillHelper.ChkCanUseSkill {skillId} {ret} {msg}");
                            continue;
                        }

                        Log.Debug($"=============开始攻击 {skillId} ");
                        SkillHelper.CastSkill(unit, skillId);
                    }
                
                    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                    if (cancellationToken.IsCancel())
                    {
                        return;
                    }
                }
            }

        }
    }
}