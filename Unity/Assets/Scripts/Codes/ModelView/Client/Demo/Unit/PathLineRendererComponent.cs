using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class PathLineRendererComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static PathLineRendererComponent Instance;

        public Transform lineRendererRoot;
        public Transform lineRendererItem;
        public Dictionary<long, Transform> lineRendererTrans;
        public Dictionary<long, LineRenderer> lineRenderers;

        public RaycastHit[] results = new RaycastHit[5];
    }
}