using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowUnitComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transUIRoot;
        public long unitId;
        public float3 offset;
        public float rotationXLimit;
        public Camera MainCamera { get; set; }

        public Vector3 orgUIScale;
    }
}