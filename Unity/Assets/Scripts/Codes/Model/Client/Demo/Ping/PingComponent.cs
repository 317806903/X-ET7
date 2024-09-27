namespace ET.Client
{
    [ComponentOf(typeof(Session))]
    public class PingComponent: Entity, IAwake, IDestroy
    {
        [StaticField]
        public static PingComponent Instance;

        public int fps;
        public long Ping; //延迟值
    }

    public struct GetFPS
    {
    }
}