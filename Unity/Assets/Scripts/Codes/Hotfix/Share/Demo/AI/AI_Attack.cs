using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_Attack: AAIHandler
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
            Unit targetUnit = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true);
            if (targetUnit == null || Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
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
                targetUnit = aiComponent.GetTargetUnit(aiConfig, skillAttackDis, true);
                if (targetUnit == null || Ability.UnitHelper.ChkUnitAlive(targetUnit) == false)
                {
                    await ET.Ability.MoveOrIdleHelper.DoIdle(unit);
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
                        //Log.Debug($"SkillHelper.ChkCanUseSkill {skillId} {ret} {msg}");
                        continue;
                    }

                    //Log.Debug($"=============开始攻击 {skillId} ");
                    await skillObj.CastSkill();
                    if (cancellationToken.IsCancel())
                    {
                        return;
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