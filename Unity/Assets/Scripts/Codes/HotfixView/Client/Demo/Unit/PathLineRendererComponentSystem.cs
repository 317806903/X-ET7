using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class PathLineRendererComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<PathLineRendererComponent>
        {
            protected override void Awake(PathLineRendererComponent self)
            {
                self.lineRendererTrans = new();
                self.lineRenderers = new();
                PathLineRendererComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<PathLineRendererComponent>
        {
            protected override void Destroy(PathLineRendererComponent self)
            {
                if (self.lineRendererRoot != null)
                {
                    foreach (var lineRendererTran in self.lineRendererTrans)
                    {
                        GameObject.Destroy(lineRendererTran.Value.gameObject);
                    }
                    GameObjectPoolHelper.ReturnTransformToPool(self.lineRendererRoot);
                    self.lineRendererRoot = null;
                }
                self.lineRendererTrans.Clear();
                self.lineRenderers.Clear();
                PathLineRendererComponent.Instance = null;
            }
        }

        public static bool ChkIsShowPath(this PathLineRendererComponent self, long playerId, float3 pos)
        {
            if (self.lineRenderers.ContainsKey(playerId))
            {
                LineRenderer lineRenderer = self.lineRenderers[playerId];
                float3 startPos = lineRenderer.GetPosition(0);
                if (math.abs(startPos.x - pos.x) < 0.1f && math.abs(startPos.z - pos.z) < 0.1f)
                {
                    return true;
                }
            }

            return false;
        }

        public static async ETTask ShowPath(this PathLineRendererComponent self, long playerId, bool canArrive, List<float3> points)
        {
            Transform lineRendererTran;
            LineRenderer lineRenderer;
            if (self.lineRenderers.ContainsKey(playerId))
            {
                lineRendererTran = self.lineRendererTrans[playerId];
                lineRenderer = self.lineRenderers[playerId];
            }
            else
            {
                ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_PathLineRenderer_1");
                GameObject PathLineRendererGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                self.lineRendererRoot = PathLineRendererGo.transform;
                PathLineRendererGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                PathLineRendererGo.transform.localPosition = Vector3.zero;
                PathLineRendererGo.transform.localScale = Vector3.one;

                self.lineRendererItem = PathLineRendererGo.transform.Find("LineRendererItem");
                self.lineRendererItem.gameObject.SetActive(false);

                GameObject lineRendererNew = GameObject.Instantiate(self.lineRendererItem.gameObject);
                lineRendererNew.SetActive(true);
                lineRendererNew.transform.SetParent(self.lineRendererRoot);
                lineRendererNew.transform.localPosition = Vector3.zero;
                lineRendererNew.transform.localScale = Vector3.one;

                lineRendererTran = lineRendererNew.transform;

                lineRenderer = lineRendererNew.gameObject.GetComponentInChildren<LineRenderer>();
                self.lineRendererTrans.Add(playerId, lineRendererNew.transform);
                self.lineRenderers.Add(playerId, lineRenderer);
            }

            if (points != null && points.Count > 0)
            {
                lineRenderer.enabled = true;
                float alpha = lineRenderer.material.color.a;
                if (canArrive)
                {
                    lineRenderer.material.color = new Color(1, 1, 1, alpha);
                }
                else
                {
                    lineRenderer.material.color = new Color(1, 0, 0, alpha);
                }
                lineRenderer.positionCount = points.Count;
                Vector3[] linePoints = new Vector3[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    linePoints[i] = new Vector3(0, 0.01f, 0) + (Vector3)points[i];
                }
                lineRenderer.SetPositions(linePoints);


                MeshCollider meshCollider = lineRendererTran.gameObject.GetComponent<MeshCollider>();
                Mesh lineMesh;
                if (meshCollider == null)
                {
                    meshCollider = lineRendererTran.gameObject.AddComponent<MeshCollider>();
                    //lineRendererTran.gameObject.AddComponent<MeshRenderer>();
                    lineRendererTran.gameObject.AddComponent<MeshFilter>();
                    lineMesh = new Mesh();
                }
                else
                {
                    lineMesh = meshCollider.sharedMesh;
                    //lineMesh = new Mesh();
                }
                MeshFilter meshFilter = lineRendererTran.gameObject.GetComponent<MeshFilter>();
                meshFilter.sharedMesh = lineMesh;
                try
                {
                    while (true)
                    {
                        lineRenderer.BakeMesh(lineMesh, CameraHelper.GetMainCamera(self.DomainScene()), true);
                        if (lineMesh.vertices.Length > 3)
                        {
                            break;
                        }
                        await TimerComponent.Instance.WaitFrameAsync();
                    }
                }
                catch (Exception e)
                {
                    Log.Debug(e.Message);
                }
                meshCollider.sharedMesh = lineMesh;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }

        public static Unit GetUnit(this PathLineRendererComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static bool ChkIsHitPath(this PathLineRendererComponent self, GameObject hitGo)
        {
            foreach (var lineRendererTran in self.lineRendererTrans)
            {
                if (lineRendererTran.Value.gameObject == hitGo)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ChkIsHitPath(this PathLineRendererComponent self, float3 pos, float radius)
        {
            int hitCount = Physics.SphereCastNonAlloc(pos + new float3(0, 5, 0), radius + 0.5f, Vector3.down, self.results);
            for (int i = 0; i < hitCount; i++)
            {
                RaycastHit hit = self.results[i];
                GameObject hitGo = hit.collider.gameObject;
                if (self.ChkIsHitPath(hitGo))
                {
                    return true;
                }
            }
            return false;
        }
    }
}