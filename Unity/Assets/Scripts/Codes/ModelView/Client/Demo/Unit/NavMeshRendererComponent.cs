using UnityEngine;
namespace ET.Client
{ 
    [ComponentOf(typeof(Unit))]
    public class NavMeshRendererComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static NavMeshRendererComponent Instance;

        public Transform navMeshRendererRoot;
        public Transform navMeshRendererItem;
        public Transform wireframeRendererItem;
        public MeshFilter wireframeMeshFilter; 
        public MeshFilter MeshFilter;
    }
}
