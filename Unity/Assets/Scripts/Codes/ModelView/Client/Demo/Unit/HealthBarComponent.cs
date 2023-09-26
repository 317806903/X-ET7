using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject go { get; set; }
        public Transform healthBar { get; set; }
        public Transform backgroundBar { get; set; }
    }
}