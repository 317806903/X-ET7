using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_AttackWhenBlock: AAIHandler
    {
        public override ET.AIChkResult Check(AIComponent aiComponent, AICfg aiConfig, bool isFirst)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return ET.AIChkResult.False;
            }
            if (ChkUnitCanAttack(unit) == false)
            {
                return ET.AIChkResult.False;
            }
            if (aiComponent.GetTargetUnit(aiConfig, 0, false, false, 0) == null)
            {
                return ET.AIChkResult.False;
            }
            if (ChkIsBlockWhenToHeadQuarter(aiComponent, aiConfig, unit) == false)
            {
                return ET.AIChkResult.False;;
            }
            (float skillAttackDis, SkillObj skillObj) = ET.Ability.SkillHelper.GetSkillAttackDisWhenBlock(unit, null);
            if (aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true, true, 1) == null)
            {
                return ET.AIChkResult.False;
            }
            return ET.AIChkResult.True;
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

            if (aiComponent.ChkCanAttack(unit, targetUnit, 0f, true))
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

            (float skillAttackDis, SkillObj skillObj) = ET.Ability.SkillHelper.GetSkillAttackDisWhenBlock(unit, null);
            // if(skillObj == null)
            // {
            //     aiComponent.Cancel();
            //     return;
            // }
            Unit unitHostileForce = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true, true, 1);
            if (unitHostileForce == null || Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
            {
                aiComponent.Cancel();
                return;
            }

            // 停在当前位置
            unit.Stop(WaitTypeError.Cancel);

            aiComponent.ResetRepeatedTimerByAttack();

            using HashSetComponent<string> excludeSkillCfgIds = HashSetComponent<string>.Create();
            //Log.Debug("开始攻击");
            bool ret;
            string msg;
            while (true)
            {
                if (cancellationToken.IsCancel())
                {
                    return;
                }

                if (aiComponent.IsDisposed)
                {
                    return;
                }

                (skillAttackDis, skillObj) = ET.Ability.SkillHelper.GetSkillAttackDisWhenBlock(unit, excludeSkillCfgIds);
                if (skillAttackDis == 0 && skillObj == null)
                {
                    if (excludeSkillCfgIds.Count > 0)
                    {
                        excludeSkillCfgIds.Clear();
                        // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                        await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                        continue;
                    }
                    else
                    {
                        aiComponent.Cancel();
                        return;
                    }
                }
                unitHostileForce = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true, true, 1);
                if (unitHostileForce == null || Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
                {
                    await ET.Ability.MoveOrIdleHelper.DoIdle(unit);
                    aiComponent.Cancel();
                    return;
                }

                if (skillObj == null)
                {
                    excludeSkillCfgIds.Clear();
                    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                    continue;
                }
                else
                {
                    (ret, msg) = SkillHelper.ChkCanUseSkill(unit, skillObj.skillCfgId);
                    //Log.Debug($"开始攻击 {skillId} {ret} {msg}");
                    if (ret == false)
                    {
                        excludeSkillCfgIds.Add(skillObj.skillCfgId);
                        continue;
                    }

                    (TimelineObj timelineObj, ActionContext actionContext) = await skillObj.CastSkill();
                    if (timelineObj == null)
                    {
                        excludeSkillCfgIds.Add(skillObj.skillCfgId);
                        continue;
                    }
                    else
                    {
                        excludeSkillCfgIds.Clear();
                    }

                    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                }
            }

        }
    }
}