using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET
{
    public class AI_XunLuo: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AICfg aiConfig, bool isFirst)
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

            long sec = TimeHelper.ClientFrameTime() / 1000 % 15;
            if (sec < 10)
            {
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AICfg aiConfig, ETCancellationToken cancellationToken)
        {
            Unit unit = aiComponent.GetUnit();
            if (unit == null)
            {
                return;
            }

            Log.Debug("开始巡逻");

            while (true)
            {
                XunLuoPathComponent xunLuoPathComponent = unit.GetComponent<XunLuoPathComponent>();
                float3 nextTarget = xunLuoPathComponent.GetCurrent();
                //await unit.FindPathMoveToAsync(nextTarget, cancellationToken);
                await ET.Ability.MoveOrIdleHelper.DoMoveTargetPosition(unit, nextTarget);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
                xunLuoPathComponent.MoveNext();
            }
        }
    }
}