using Unity.Mathematics;

namespace ET.Server
{
    public class AI_XunLuo: AAIHandler
    {
        public override int Check(AIComponent aiComponent, AIConfig aiConfig)
        {
            long sec = TimeHelper.ClientFrameTime() / 1000 % 15;
            if (sec < 10)
            {
                return 0;
            }
            return 1;
        }

        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
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
                await unit.FindPathMoveToAsync(nextTarget, cancellationToken);
                if (cancellationToken.IsCancel())
                {
                    return;
                }
                xunLuoPathComponent.MoveNext();
            }
        }
    }
}