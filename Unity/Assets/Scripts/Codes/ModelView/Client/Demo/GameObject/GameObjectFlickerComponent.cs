using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectFlickerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        //闪烁结束时刻
        public long flickerEndTime;
        public bool isFlickering;
        public Dictionary<Renderer, Material[]> MaterialRecordDic = new();
    }
}