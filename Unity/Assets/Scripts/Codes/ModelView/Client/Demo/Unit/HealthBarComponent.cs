using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public bool isHome;
    }
}