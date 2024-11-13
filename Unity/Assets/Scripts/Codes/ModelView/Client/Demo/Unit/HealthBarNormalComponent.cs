using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarNormalComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public float targetNormalizedHealth;
        public float curDelayHpPer;

        public bool isShow;
    }
}