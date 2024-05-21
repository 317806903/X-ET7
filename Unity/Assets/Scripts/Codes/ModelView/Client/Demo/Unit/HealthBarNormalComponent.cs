using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarNormalComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject go { get; set; }
        public Camera mainCamera { get; set; }
        public Transform healthBar { get; set; }
        public Transform delayHealthBar { get; set; }
        public Transform backgroundBar { get; set; }
        public float targetNormalizedHealth;
        public float curNormalizedHealth;
    }
}