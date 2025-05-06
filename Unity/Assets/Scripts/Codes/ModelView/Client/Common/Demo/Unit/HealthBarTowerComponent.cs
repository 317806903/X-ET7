using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HealthBarTowerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject go { get; set; }
        public Camera mainCamera { get; set; }
        public Transform healthBar { get; set; }
        public Transform backgroundBar { get; set; }
        public Transform HpValueShowTrans { get; set; }
        public Material mat;

        public int curFrame = 0;
        public int waitFrame = 0;
        public bool isActivity;
        public bool isShowWhenFull;
    }
}