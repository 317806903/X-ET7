using System;
using ET.AbilityConfig;

namespace ET
{
    public class AIHandlerAttribute: BaseAttribute
    {
    }

    [AIHandler]
    public abstract class AAIHandler
    {
        // 检查是否满足条件
        public abstract ET.AIChkResult Check(AIComponent aiComponent, AICfg aiConfig, bool isFirst);

        // 协程编写必须可以取消
        public abstract ETTask Execute(AIComponent aiComponent, AICfg aiConfig, ETCancellationToken cancellationToken);
    }
}