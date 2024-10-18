using ET.AbilityConfig;
using System.Collections.Generic;
using UnityEngine;
namespace ET.Client
{

    public static class NavMeshRendererComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<NavMeshRendererComponent>
        {
            protected override void Awake(NavMeshRendererComponent self)
            {
                NavMeshRendererComponent.Instance = self;
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_NavMeshRenderer");
                GameObject navMeshRenderer = ResComponent.Instance.LoadAsset<GameObject>(resEffectCfg.ResName);
                navMeshRenderer = GameObject.Instantiate(navMeshRenderer);
                self.navMeshRendererRoot = navMeshRenderer.transform;
                self.navMeshRendererItem = navMeshRenderer.transform.Find("NavMeshRendererItem");
                navMeshRenderer.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                self.meshRenderer = self.navMeshRendererItem.GetComponent<MeshRenderer>();
                self.MeshFilter = self.navMeshRendererItem.GetComponent<MeshFilter>();
                self.meshRenderer.enabled = false;
                self.navMeshRendererRoot.gameObject.SetActive(false);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<NavMeshRendererComponent>
        {
            protected override void Destroy(NavMeshRendererComponent self)
            {
                if (self.navMeshRendererRoot != null)
                {
                    GameObject.Destroy(self.navMeshRendererRoot.gameObject);
                }
                NavMeshRendererComponent.Instance = null;
            }
        }

        public static void ShowMesh(this NavMeshRendererComponent self, bool isShow)
        {
            if (self.navMeshRendererRoot != null)
            {
                self.navMeshRendererRoot.gameObject.SetActive(isShow);
                self.meshRenderer.enabled = isShow;
            }
        }

        public static void SetNavMesh(this NavMeshRendererComponent self, Scene scene, NavmeshManagerComponent.NavMeshData navMeshData)
        {
            if (self.navMeshRendererRoot == null)
            {
                return;
            }
            GamePlayComponent gamePlayComponent = ET.Client.GamePlayHelper.GetGamePlay(scene);
            if (gamePlayComponent == null)
            {
                return;
            }

            Mesh mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            var vertices = new Vector3[navMeshData.Vertices.Count];
            for (int i = 0; i < navMeshData.Vertices.Count; i++)
            {
                vertices[i] = navMeshData.Vertices[i];
            }
            mesh.vertices = vertices;

            List<int> triangles = new List<int>();
            for (int i = 0; i < navMeshData.Indices.Count; i++)
            {
                int count = navMeshData.Indices[i];
                for (int j = i + 2; j < i + count; j++)
                {
                    triangles.Add(navMeshData.Indices[i + 1]);
                    triangles.Add(navMeshData.Indices[j + 1]);
                    triangles.Add(navMeshData.Indices[j]);
                }
                i += count;
            }
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateNormals();
            mesh.RecalculateUVDistributionMetric(0);
            mesh.RecalculateBounds();
            mesh.Optimize();
            mesh.UploadMeshData(false);
            
            self.MeshFilter.sharedMesh = mesh;

            // TODO(jiabei): Draw bounds.
        }
    }
}
