using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_Attack: AAIHandler
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

            (float skillAttackDis, SkillObj skillObj, int skillCount) = ET.Ability.SkillHelper.GetSkillAttackDis(unit, null, true);
            if (aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true) == null)
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

        public override async ETTask Execute(AIComponent aiComponent, AICfg aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                aiComponent.Cancel();
                return;
            }

            using HashSetComponent<string> excludeSkillCfgIds = HashSetComponent<string>.Create();
            float skillAttackDis = 0;
            SkillObj skillObj = null;
            Unit targetUnit = null;
            int skillCount;
            while (true)
            {
                (skillAttackDis, skillObj, skillCount) = ET.Ability.SkillHelper.GetSkillAttackDis(unit, excludeSkillCfgIds, false);
                if (skillAttackDis == 0)
                {
                    aiComponent.Cancel();
                    return;
                }
                targetUnit = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true);
                if (targetUnit == null || Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    excludeSkillCfgIds.Add(skillObj.skillCfgId);
                    continue;
                }
                else
                {
                    break;
                }
            }

            // 停在当前位置
            unit.Stop(WaitTypeError.Cancel);

            aiComponent.ResetRepeatedTimerByAttack();

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

                (skillAttackDis, skillObj, skillCount) = ET.Ability.SkillHelper.GetSkillAttackDis(unit, excludeSkillCfgIds, false);
                if (skillCount == 0)
                {
                    aiComponent.Cancel();
                    return;
                }
                if (skillAttackDis == 0 && skillObj == null)
                {
                    aiComponent.Cancel();
                    return;
                    // if (excludeSkillCfgIds.Count > 0)
                    // {
                    //     excludeSkillCfgIds.Clear();
                    //     // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    //     await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                    //     continue;
                    // }
                    // else
                    // {
                    //     aiComponent.Cancel();
                    //     return;
                    // }
                }
                if (skillObj == null)
                {
                    excludeSkillCfgIds.Clear();
                    // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                    await TimerComponent.Instance.WaitAsync(100, cancellationToken);
                    continue;
                }

                targetUnit = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true);

                if (targetUnit == null || Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    if (excludeSkillCfgIds.Count < skillCount -1 )
                    {
                        excludeSkillCfgIds.Add(skillObj.skillCfgId);
                        continue;
                    }
                    else if (excludeSkillCfgIds.Count == skillCount -1 )
                    {
                        await ET.Ability.MoveOrIdleHelper.DoIdle(unit);
                        aiComponent.Cancel();
                        return;
                    }
                }

                (ret, msg) = SkillHelper.ChkCanUseSkill(unit, skillObj.skillCfgId);
                //Log.Debug($"开始攻击 {skillId} {ret} {msg}");
                if (ret == false)
                {
                    excludeSkillCfgIds.Add(skillObj.skillCfgId);
                    continue;
                }

                //Log.Debug($"=============开始攻击 {skillId} ");
                (TimelineObj timelineObj, ActionContext actionContext) = await skillObj.CastSkill();
                if (timelineObj == null)
                {
                    excludeSkillCfgIds.Add(skillObj.skillCfgId);
                    continue;
                }

                excludeSkillCfgIds.Clear();
                // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
                await TimerComponent.Instance.WaitAsync(100, cancellationToken);
            }

        }
    }
}
