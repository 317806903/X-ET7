using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public string resName = "";
        public float resScale = 1f;
        public GameObject gameObject { get; set; }
        public GameObject unitResGameObject { get; set; }
        public GameObject unitResRoot { get; set; }
        public GameObject effectResRoot { get; set; }
        public GameObject effectResScaleRoot { get; set; }

        public GameObject effectResNoRotateRoot { get; set; }
        public GameObject effectResScaleNoRotateRoot { get; set; }

        public float chkChildCountTime;
        public int effectResNoRotateChildCount;
        public int effectResScaleNoRotateChildCount;

        public quaternion firstRotation;


        public EntityRef<GameObjectComponent> refGameObjectComponent;

        public KeepTilted keepTilted;
        public KeepForward keepForward;

        public GameObject cubeGameObject;
        public GameObject sphereGameObject;
        public GameObject cylinderGameObject;
    }
}