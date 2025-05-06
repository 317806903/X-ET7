using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFrontOfHeadComponent: Entity, IAwake, IDestroy, ILateUpdate
    {
        public Transform transUIRoot;
        public float3 offset;
        public float rotationXLimit;
        public Camera MainCamera { get; set; }

    }
}