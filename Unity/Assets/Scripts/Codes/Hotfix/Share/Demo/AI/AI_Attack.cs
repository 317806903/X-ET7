using ET.Server;

namespace ET.Client
{
    public class AI_Attack: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            long sec = TimeHelper.ClientFrameTime() / 1000 % 15;
            if (sec >= 10)
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
            return 1;
        }

        public static Unit GetHostileForce(Unit unit, float radius)
        {
            ListComponent<Unit> hostileForces = Ability.UnitHelper.GetHostileForces(unit);
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
                return;
            }

            Unit unitHostileForce = GetHostileForce(unit, 5f);
            if (Ability.UnitHelper.ChkUnitAlive(unitHostileForce) == false)
            {
                return;
            }

            // 停在当前位置
            unit.Stop(WaitTypeError.Cancel);
            
            Log.Debug("开始攻击");
            

            // 因为协程可能被中断，任何协程都要传入cancellationToken，判断如果是中断则要返回
            await TimerComponent.Instance.WaitAsync(1000, cancellationToken);
            if (cancellationToken.IsCancel())
            {
                return;
            }
        }
    }
}