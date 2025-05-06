using System.Collections.Generic;

namespace ET
{
    [ChildOf(typeof(AOIManagerComponent))]
    public class Cell: Entity, IAwake, IDestroy
    {
        // 处在这个cell的单位
        public Dictionary<long, EntityRef<AOIEntity>> AOIUnits = new ();

        // 订阅了这个Cell的进入事件
        public Dictionary<long, EntityRef<AOIEntity>> SubscribeEnterEntities = new ();

        // 订阅了这个Cell的退出事件
        public Dictionary<long, EntityRef<AOIEntity>> SubscribeLeaveEntities = new ();
    }
}