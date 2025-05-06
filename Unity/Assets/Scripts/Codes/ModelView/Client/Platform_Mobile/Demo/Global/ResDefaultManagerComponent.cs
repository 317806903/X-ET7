using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(GlobalComponent))]
    public class ResDefaultManagerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static ResDefaultManagerComponent Instance;

    }
}