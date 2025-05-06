using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class ShowGetGoldTextComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static ShowGetGoldTextComponent Instance;

        public Transform shootTextRoot;
        public ShootTextProManager shootTextProManager;
    }
}