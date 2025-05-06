using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowLeftHandComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transUIRoot;
        public Transform leftHandTrans;
        public float3 offset;
        public float rotationXLimit;
        public Camera MainCamera { get; set; }
    }
}