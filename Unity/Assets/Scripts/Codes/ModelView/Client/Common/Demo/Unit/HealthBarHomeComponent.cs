using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarHomeComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject go { get; set; }
        public Camera mainCamera { get; set; }
        public Transform healthBar { get; set; }
        public Transform backgroundBar { get; set; }
        public Transform hpValueShowTrans { get; set; }

        public RectTransform canvas;
        public RectTransform rectTrans;

        public int curFrame = 0;
        public int waitFrame = 0;
    }
}