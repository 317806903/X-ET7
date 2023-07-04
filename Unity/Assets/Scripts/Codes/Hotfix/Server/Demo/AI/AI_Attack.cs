using ET.Ability;
using ET.AbilityConfig;
using SkillSlotType = ET.Ability.SkillSlotType;

namespace ET.Server
{
    public class AI_Attack: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return 1;
            }
            if (GetHostileForce(unit, 10f) == null)
            {
                return 1;
            }
            return 0;
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
                for (int i = 0; i < count; i++)
                {
                    string skillId = unitCfg.SkillList[i];
                    (bool ret, string msg) = SkillHelper.ChkCanUseSkill(unit, skillId);
                    //Log.Debug($"开始攻击 {skillId} {ret} {msg}");
                    if (ret == false)
                    {
                        //Log.Debug($"SkillHelper.ChkCanUseSkill {skillId} {ret} {msg}");
                        continue;
                    }

                    //Log.Debug($"=============开始攻击 {skillId} ");
                    SkillHelper.CastSkill(unit, skillId);
                    if (cancellationToken.IsCancel())
                    {
                        return;
                    }
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