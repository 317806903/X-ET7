using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowPosComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transUIRoot;
        public float3 pos;
        public float3 offset;
        public float rotationXLimit;
        public Camera MainCamera { get; set; }
    }
}