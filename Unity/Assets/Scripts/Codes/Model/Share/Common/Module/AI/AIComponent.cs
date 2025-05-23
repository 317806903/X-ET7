using System.Collections.Generic;
using ET.Ability;

namespace ET
{
    public enum AIChkResult
    {
        True,
        False,
    }

    // 客户端挂在ClientScene上，服务端挂在Unit上
    //[ComponentOf(typeof(Scene))]
    [ComponentOf(typeof(Unit))]
    public class AIComponent: Entity, IAwake<string>, IDestroy
    {
        public bool isEnable;
        public string AICfgId;
        public Dictionary<string, EntityRef<Unit>> TargetUnitDic;

        public Dictionary<int, int> curFrameIndex;
        public Dictionary<int, long> ResetTargetTimeDic;
        public ActionContext actionContext;

        public ETCancellationToken CancellationToken;

        public long Timer;
        public int NewRepeatedTimerNear = 500;
        public int NewRepeatedTimerFast = 1500;
        public float nearDis = 20;
        public int curNewRepeatedTimer;
        public bool isNear;
        public long lastChkDisTime;
        public int chkDisTimeInterval = 1;

        public string Current;
    }
}