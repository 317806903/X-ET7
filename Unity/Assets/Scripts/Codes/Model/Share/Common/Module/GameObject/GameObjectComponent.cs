
using ET.AbilityConfig;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent: Entity, IAwake, IDestroy, ITransferClient
    {
        public string orgUnitCfgId;
        public string curUnitCfgId;
        public string resName = "";
        public float resScale = 1f;

        //隐藏
        public bool isHiding;
        //闪烁结束时刻
        public long flickerEndTime;
        //每秒闪多少下
        public float flickerFrequency;
        public ColorBean flickerStartColor;
        public ColorBean flickerEndColor;
        //隐身(半透)
        public bool isTransparent;
        public bool isFly;
    }
}