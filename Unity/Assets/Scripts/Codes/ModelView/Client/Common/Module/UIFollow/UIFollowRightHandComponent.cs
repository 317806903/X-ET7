using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowRightHandComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transUIRoot;
        public Transform rightHandTrans;
        public float3 offset;
        public float rotationXLimit;
        public Camera MainCamera { get; set; }
    }
}