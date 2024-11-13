using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public string resName = "";
        public float resScale = 1f;
        public GameObject gameObject { get; set; }
        public EntityRef<GameObjectComponent> refGameObjectComponent;

        public GameObject cubeGameObject;
        public GameObject sphereGameObject;
        public GameObject cylinderGameObject;
    }
}