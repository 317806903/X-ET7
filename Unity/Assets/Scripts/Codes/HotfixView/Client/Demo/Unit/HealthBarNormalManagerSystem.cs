using System;
using ET.AbilityConfig;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarNormalManagerSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarNormalManager>
        {
            protected override void Awake(HealthBarNormalManager self)
            {
                HealthBarNormalManager.Instance = self;
                self.Init().Coroutine();
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarNormalManager>
        {
            protected override void Destroy(HealthBarNormalManager self)
            {
                if (self.go != null)
                {
                    //UnityEngine.Object.Destroy(self.go);
                    GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
                    self.go = null;
                }
                if (HealthBarNormalManager.Instance == self)
                {
                    HealthBarNormalManager.Instance = null;
                }
            }
        }

        [ObjectSystem]
        public class UpdateSystem: UpdateSystem<HealthBarNormalManager>
        {
            protected override void Update(HealthBarNormalManager self)
            {
                self.Update();
            }
        }

        public static async ETTask Init(this HealthBarNormalManager self)
        {
            string resName = "ResEffect_HealthBar_1";

            ResEffectCfg resEffectCfg = ResEffectCfgCategory.Instance.Get(resName);
            GameObject HealthBarGo = GameObjectPoolHelper.GetObjectFromPool(resEffectCfg.ResName,true,1);

            HealthBarGo.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            HealthBarGo.transform.localPosition = Vector3.zero;
            HealthBarGo.transform.localScale = Vector3.one;

            self.go = HealthBarGo;
            self.healthBar = self.go.transform.Find("Bar/ShaderShow");
            self.healthBarRenderer = self.healthBar.gameObject.GetComponent<Renderer>();
            self.healthBarMesh = self.healthBar.gameObject.GetComponent<MeshFilter>().mesh;
            self.healthBarMaterial = self.healthBar.gameObject.GetComponent<MeshRenderer>().sharedMaterial;

            self.meshLocalPosition = self.healthBar.localPosition;
            self.meshSize = self.healthBar.localScale;
        }

        public static void ResetRendererData(this HealthBarNormalManager self)
        {
            int totalCount = self.refDic.Count;
            self.batches.Clear();
            if (totalCount == 0)
            {
                return;
            }

            float[] curDelayHpPers = self.curDelayHpPers;
            float[] curHpPers = self.curHpPers;

            int i = 0;
            foreach (var item in self.refDic)
            {
                if (i > HealthBarNormalManager.MAX_CONT)
                {
                    self._propertyBlock.SetFloatArray("_CurHpPers", curHpPers);
                    self._propertyBlock.SetFloatArray("_CurDelayHpPers", curDelayHpPers);

                    Graphics.DrawMeshInstanced(self.healthBarMesh, 0, self.healthBarMaterial, self.batches, self._propertyBlock);

                    i = 0;
                    self.batches.Clear();
                    continue;
                }

                HealthBarNormalComponent healthBarNormalComponent = item.Value;
                if (healthBarNormalComponent == null || healthBarNormalComponent.ChkIsShow() == false)
                {
                    continue;
                }
                Vector3 pos = healthBarNormalComponent.GetPos() + self.meshLocalPosition;
                Quaternion rot = self.GetForward();
                Vector3 size = self.meshSize;

                Matrix4x4 matrices = new();
                matrices.SetTRS(pos, rot, size);
                self.batches.Add(matrices);

                curHpPers[i] = healthBarNormalComponent.GetCurHpPer();
                curDelayHpPers[i] = healthBarNormalComponent.GetCurDelayHpPer();
                i++;
            }

            if (self.batches.Count > 0)
            {
                self._propertyBlock.SetFloatArray("_CurHpPers", curHpPers);
                self._propertyBlock.SetFloatArray("_CurDelayHpPers", curDelayHpPers);

                Graphics.DrawMeshInstanced(self.healthBarMesh, 0, self.healthBarMaterial, self.batches, self._propertyBlock);
            }
        }

        public static void DealData(this HealthBarNormalManager self)
        {
            self.removeList.Clear();
            foreach (var item in self.refDic)
            {
                HealthBarNormalComponent healthBarNormalComponent = item.Value;
                if (healthBarNormalComponent == null)
                {
                    self.removeList.Add(item.Key);
                }
                else
                {

                }
            }

            foreach (long id in self.removeList)
            {
                self.refDic.Remove(id);
            }
            self.removeList.Clear();
        }

        public static Camera GetMainCamera(this HealthBarNormalManager self)
        {
            if (self.mainCamera == null)
            {
                Camera mainCamera = CameraHelper.GetMainCamera(self.DomainScene());
                self.mainCamera = mainCamera;
            }
            return self.mainCamera;
        }

        public static void Update(this HealthBarNormalManager self)
        {
            if (self.go == null)
            {
                return;
            }

            if (++self.curFrame >= self.waitFrame)
            {
                self.curFrame = 0;

                self.DealData();
                self.ResetRendererData();
            }
        }

        public static Quaternion GetForward(this HealthBarNormalManager self)
        {
            Camera mainCamera = self.GetMainCamera();
            if (mainCamera == null)
            {
                return Quaternion.identity;
            }
            Quaternion rotation = mainCamera.transform.rotation;
            return rotation;
        }

        public static void AddRefList(this HealthBarNormalManager self, HealthBarNormalComponent healthBarNormalComponent)
        {
            self.refDic.Add(healthBarNormalComponent.Id, healthBarNormalComponent);
        }

        public static void RemoveRefList(this HealthBarNormalManager self, long id)
        {
            self.refDic.Remove(id);
        }

    }
}