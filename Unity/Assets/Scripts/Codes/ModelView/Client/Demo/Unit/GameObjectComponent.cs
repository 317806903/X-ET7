using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject GameObject { get; set; }
    }
}