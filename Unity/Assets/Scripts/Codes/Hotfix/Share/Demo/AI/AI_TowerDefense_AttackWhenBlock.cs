using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using SkillSlotType = ET.AbilityConfig.SkillSlotType;

namespace ET
{
    public class AI_TowerDefense_AttackWhenBlock: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig, bool isFirst)
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
            if (aiComponent.GetTargetUnit(aiConfig, 0, false) == null)
            {
                return 1;
            }
            if (ChkIsBlockWhenToHeadQuarter(aiComponent, aiConfig, unit) == false)
            {
                return 1;
            }
            (float skillAttackDis, SkillObj skillObj) = ET.Ability.SkillHelper.GetSkillAttackDis(unit);
            if (aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true) == null)
            {
                return 1;
            }
            return 0;
        }

        public bool ChkUnitCanAttack(Unit unit)
        {
            UnitCfg unitCfg = unit.model;
            int count = unitCfg.SkillList.Count;
            return count > 0;
        }

        public bool ChkIsBlockWhenToHeadQuarter(AIComponent aiComponent, AICfg aiConfig, Unit unit)
        {
            Unit targetUnit = aiComponent.GetTargetUnit(aiConfig, 0, false);
            if (targetUnit == null)
            {
                return true;
            }

            if (Ability.UnitHelper.ChkCanAttack(unit, targetUnit, 0f))
            {
                return false;
            }

            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            if (pathfindingComponent == null)
            {
                return true;
            }
            bool canArrive = pathfindingComponent.ChkCanArrive(unit.Position, targetUnit.Position);
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

            (float skillAttackDis, SkillObj skillObj) = ET.Ability.SkillHelper.GetSkillAttackDis(unit);
            // if(skillObj == null)
            // {
            //     aiComponent.Cancel();
            //     return;
            // }
            Unit unitHostileForce = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true);
            if (unitHostileForce == null || Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
            {
                aiComponent.Cancel();
                return;
            }

            // 停在当前位置
            unit.Stop(WaitTypeError.Cancel);

            aiComponent.ResetRepeatedTimerByAttack();

            //Log.Debug("开始攻击");
            bool ret;
            string msg;
            while (true)
            {
                (skillAttackDis, skillObj) = ET.Ability.SkillHelper.GetSkillAttackDis(unit);
                // if(skillObj == null)
                // {
                //     aiComponent.Cancel();
                //     return;
                // }
                unitHostileForce = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true);
                if (unitHostileForce == null || Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
                {
                    aiComponent.Cancel();
                    return;
                }

                if (skillObj == null)
                {
                    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                    if (cancellationToken.IsCancel())
                    {
                        return;
                    }
                }
                else
                {
                    (ret, msg) = SkillHelper.ChkCanUseSkill(unit, skillObj.skillCfgId);
                    //Log.Debug($"开始攻击 {skillId} {ret} {msg}");
                    if (ret == false)
                    {
                        await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                        if (cancellationToken.IsCancel())
                        {
                            return;
                        }
                        continue;
                    }

                    await skillObj.CastSkill();

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