using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Ability.Client
{
    [ChildOf(typeof (EffectShowComponent))]
    [FriendOf(typeof(Unit))]
    public class EffectShowObj: Entity, IAwake, IDestroy
    {
        public GameObject go;
    }
}