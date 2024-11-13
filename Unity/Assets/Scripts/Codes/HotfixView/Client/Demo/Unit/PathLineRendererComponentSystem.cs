using ET.Ability;
using ET.AbilityConfig;
using System;
using System.Collections.Generic;
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
                self.lineRendererMidPos = new();
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
                self.lineRendererMidPos.Clear();

                if (PathLineRendererComponent.Instance == self)
                {
                    PathLineRendererComponent.Instance = null;
                }
            }
        }

        public static void Clear(this PathLineRendererComponent self)
        {
            if (self.lineRendererRoot != null)
            {
                foreach (var lineRendererTran in self.lineRendererTrans.Values)
                {
                    if (lineRendererTran != null)
                    {
                        GameObject.Destroy(lineRendererTran.gameObject);
                    }
                }
                GameObjectPoolHelper.ReturnTransformToPool(self.lineRendererRoot);
                self.lineRendererRoot = null;
            }
            self.lineRendererTrans.Clear();
            self.lineRenderers.Clear();
        }

        public static bool ChkIsShowPath(this PathLineRendererComponent self, TeamFlagType homeTeamFlagType, long monsterCallUnitId, float3 pos)
        {
            string key = $"{homeTeamFlagType}_{monsterCallUnitId}";
            if (self.lineRenderers.ContainsKey(key))
            {
                LineRenderer lineRenderer = self.lineRenderers[key];
                if (lineRenderer == null)
                {
                    self.lineRendererTrans.Remove(key);
                    self.lineRenderers.Remove(key);
                    return false;
                }
                return true;
            }

            return false;
        }

        public static async ETTask ShowPath(this PathLineRendererComponent self, TeamFlagType homeTeamFlagType, long monsterCallUnitId, bool canArrive, List<float3> points)
        {
            Transform lineRendererTran;
            LineRenderer lineRenderer;
            string key = $"{homeTeamFlagType}_{monsterCallUnitId}";
            if (self.lineRenderers.ContainsKey(key))
            {
                lineRendererTran = self.lineRendererTrans[key];
                lineRenderer = self.lineRenderers[key];
            }
            else
            {
                if (self.lineRendererRoot == null)
                {
                    ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get("ResEffect_PathLineRenderer_1");
                    GameObject PathLineRendererGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);
                    self.lineRendererRoot = PathLineRendererGo.transform;
                    PathLineRendererGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
                    PathLineRendererGo.transform.localPosition = Vector3.zero;
                    PathLineRendererGo.transform.localScale = Vector3.one;

                    self.lineRendererItem = PathLineRendererGo.transform.Find("LineRendererItem");
                    self.lineRendererItem.gameObject.SetActive(false);
                }

                GameObject lineRendererNew = GameObject.Instantiate(self.lineRendererItem.gameObject);
                lineRendererNew.SetActive(true);
                lineRendererNew.transform.SetParent(self.lineRendererRoot);
                lineRendererNew.transform.localPosition = Vector3.zero;
                lineRendererNew.transform.localScale = Vector3.one;

                lineRendererTran = lineRendererNew.transform;

                lineRenderer = lineRendererNew.gameObject.GetComponentInChildren<LineRenderer>();
                self.lineRendererTrans.Add(key, lineRendererNew.transform);
                self.lineRenderers.Add(key, lineRenderer);
            }

            if (points != null && points.Count > 0)
            {
                self.lineRendererMidPos[key] = RecastHelper.GetMidPosWhen2Pos(points);
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
                float lineRendererHeight = lineRenderer.transform.localPosition.y;
                if (lineRendererHeight <= 0)
                {
                    lineRendererHeight = 0.02f;
                }
                Vector3[] linePoints = new Vector3[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    linePoints[i] = new Vector3(0, lineRendererHeight, 0) + (Vector3)points[i];
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
                    int retryNum = 10;
                    while (true)
                    {
                        lineRenderer.BakeMesh(lineMesh, CameraHelper.GetMainCamera(self.DomainScene()), true);
                        if (lineMesh.vertices.Length > 3)
                        {
                            break;
                        }

                        if (retryNum -- <= 0)
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

        public static float GetPathLength(this PathLineRendererComponent self, TeamFlagType homeTeamFlagType, long monsterCallUnitId)
        {
            LineRenderer lineRenderer;
            string key = $"{homeTeamFlagType}_{monsterCallUnitId}";
            if (self.lineRenderers.ContainsKey(key))
            {
                lineRenderer = self.lineRenderers[key];
            }
            else
            {
                return 0;
            }

            if (lineRenderer.enabled == false)
            {
                return 0;
            }
            int count = lineRenderer.positionCount;
            Vector3[] linePoints = new Vector3[count];
            lineRenderer.GetPositions(linePoints);
            float length = 0;
            for (int i = 1; i < linePoints.Length; i++)
            {
                length += (linePoints[i] - linePoints[i - 1]).magnitude;
            }

            return length;
        }

        public static float3 GetPathMidPos(this PathLineRendererComponent self, TeamFlagType homeTeamFlagType, long monsterCallUnitId)
        {
            string key = $"{homeTeamFlagType}_{monsterCallUnitId}";
            if (self.lineRendererMidPos.ContainsKey(key))
            {
                return self.lineRendererMidPos[key];
            }
            else
            {
                return float3.zero;
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
                    if (pos.y > hit.point.y + 1.5f)
                    {
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        public static async ETTask<bool> ShowPathIfCanArrive(this PathLineRendererComponent self, TeamFlagType homeTeamFlagType,
        long monsterCallUnitId, float3 homePos, List<float3> points)
        {
            bool canArrive = false;
            if (points != null && points.Count > 0)
            {
                float3 lastPoint = points[points.Count - 1];
                if (math.abs(homePos.x - lastPoint.x) < 0.3f
                    && math.abs(homePos.y - lastPoint.y) < 0.3f
                    && math.abs(homePos.z - lastPoint.z) < 0.3f)
                {
                    canArrive = true;
                }
            }

            try
            {
                await self.ShowPath(homeTeamFlagType, monsterCallUnitId, canArrive, points);
            }
            catch (Exception e)
            {
                Log.Error($" PathLineRendererComponent.Instance.ShowPath {e}");
            }
            return canArrive;
        }
    }
}