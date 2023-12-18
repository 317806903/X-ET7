using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class HomeHealthBarComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public GameObject go { get; set; }
        public Transform healthBar { get; set; }
        public Transform backgroundBar { get; set; }
        public Transform hpValueShowTrans { get; set; }
        
        public Camera camera;
        public RectTransform canvas;
        public RectTransform rectTrans;
    }
}