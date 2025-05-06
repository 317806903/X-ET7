using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowHeadManagerComponent: Entity, IAwake, IDestroy
    {
        [StaticField]
        public static UIFollowHeadManagerComponent Instance;

        public Camera camera;
        public List<EntityRef<UIFollowHeadComponent>> list;
        public List<int> removeList = new();

        public bool isSetDefault;
        public Vector3 defaultPosition;
        public Quaternion defaultRotation;
    }
}