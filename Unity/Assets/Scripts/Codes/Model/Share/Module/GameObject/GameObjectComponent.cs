
namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public string resName = "";
        public float resScale = 1f;

        //隐藏
        public bool isHiding;
        //闪烁结束时刻
        public long flickerEndTime;
        //隐身(半透)
        public bool isTransparent;
    }
}