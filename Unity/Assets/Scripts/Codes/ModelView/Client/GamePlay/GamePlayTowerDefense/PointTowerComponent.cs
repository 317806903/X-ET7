using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class PointTowerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transRoot { get; set; }
    }
}