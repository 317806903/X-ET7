using ET.Ability;

namespace ET.Server
{
    public class AI_Attack: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return 1;
            }
            if (GetHostileForce(unit, 5f) == null)
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
        
        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                cancellationToken.Cancel();
                return;
            }

            Unit unitHostileForce = GetHostileForce(unit, 5f);
            if (Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
            {
                cancellationToken.Cancel();
                return;
            }

            // 停在当前位置
            unit.Stop(WaitTypeError.Cancel);
            
            Log.Debug("开始攻击");
            
            while (true)
            {
                string skillId = "Skill_1";
                (bool ret, string msg) = SkillHelper.ChkCanUseSkill(unit, skillId);
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

                Log.Debug($"=============开始攻击 {skillId} ");
                SkillHelper.CastSkill(unit, skillId);

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