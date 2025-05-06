using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectFlickerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public long flickerStartTime;
        //闪烁结束时刻
        public long flickerEndTime;
        //每秒闪多少下
        public float flickerFrequency;
        public Color startColor;
        public Color endColor;

        public bool isFlickering;
        public List<Material> MaterialRecordList = new();
    }
}