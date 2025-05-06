using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectHideComponent: Entity, IAwake, IDestroy
    {
    }
}