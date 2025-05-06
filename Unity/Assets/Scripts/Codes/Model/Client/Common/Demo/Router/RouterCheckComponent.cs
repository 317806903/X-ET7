namespace ET.Client
{
    [ComponentOf(typeof(Session))]
    public class RouterCheckComponent: Entity, IAwake
    {
        public int retryNum = 5;
        public int retryIndex = 0;
    }
}