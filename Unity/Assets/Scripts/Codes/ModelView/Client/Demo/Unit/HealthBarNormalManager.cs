using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarNormalManager: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static HealthBarNormalManager Instance;

        public static int MAX_CONT = 1023;

        public GameObject go { get; set; }
        public Camera mainCamera { get; set; }
        public Transform healthBar { get; set; }
        public Renderer healthBarRenderer { get; set; }
        public MaterialPropertyBlock _propertyBlock = new();
        public Mesh healthBarMesh { get; set; }
        public Material healthBarMaterial { get; set; }
        public Vector3 meshLocalPosition;
        public Vector3 meshSize;

        public int curFrame = 0;
        public int waitFrame = 0;

        public List<Matrix4x4> batches = new();

        public float[] curDelayHpPers = new float[MAX_CONT];
        public float[] curHpPers = new float[MAX_CONT];
        public Dictionary<long, EntityRef<HealthBarNormalComponent>> refDic = new();
        public List<long> removeList = new();
    }
}