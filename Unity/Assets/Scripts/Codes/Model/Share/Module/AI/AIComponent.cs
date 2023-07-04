namespace ET
{
    // 客户端挂在ClientScene上，服务端挂在Unit上
    //[ComponentOf(typeof(Scene))]
    [ComponentOf(typeof(Unit))]
    public class AIComponent: Entity, IAwake<string>, IDestroy
    {
        public string AICfgId;
        
        public ETCancellationToken CancellationToken;

        public long Timer;

        public string Current;
    }
}