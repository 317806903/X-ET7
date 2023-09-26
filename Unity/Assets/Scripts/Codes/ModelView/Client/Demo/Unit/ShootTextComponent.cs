using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class ShootTextComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static ShootTextComponent Instance;

        public Transform shootTextRoot;
        public ShootTextProManager shootTextProManager;
    }
}