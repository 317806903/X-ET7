using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowObjectComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transUIRoot;
        public GameObject followObj;
        public float3 offset;
        public float rotationXLimit;
        public Camera MainCamera { get; set; }

        public Vector3 orgUIScale;
    }
}