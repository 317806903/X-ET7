using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Client
{
    public class DynamicAtlasManagerComponent: Entity, IAwake, IDestroy
    {
        public GameObject root;
    }
}