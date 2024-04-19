using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Ability.Client
{
    [FriendOf(typeof (EffectShowObj))]
    public static class EffectShowObjSystem
    {
        [ObjectSystem]
        public class EffectShowObjAwakeSystem: AwakeSystem<EffectShowObj>
        {
            protected override void Awake(EffectShowObj self)
            {
            }
        }

        [ObjectSystem]
        public class EffectShowObjDestroySystem: DestroySystem<EffectShowObj>
        {
            protected override void Destroy(EffectShowObj self)
            {
                self.RefEffectObj = null;
                if (self.go != null)
                {
                    //UnityEngine.Object.Destroy(self.go);
                    GameObjectPoolHelper.ReturnTransformToPool(self.go.transform);
                    self.go = null;
                }

                if (self.RefAudioPlayObj != null)
                {
                    self.RefAudioPlayObj.Dispose();
                }

                self.lineRenderers = null;
                self.lightningBoltScripts = null;
                self.lightningBoltScriptManagers = null;
            }
        }

        public static async ETTask Init(this EffectShowObj self, EffectObj effectObj)
        {
            self.RefEffectObj = effectObj;

            string resName = effectObj.model.ResName;
            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName,false,1);
            if (go == null)
            {
                Log.Error($"EffectShowObjSystem.Init go == null when resName={resName}");
            }
            self.go = go;
            Unit unit = effectObj.GetUnit();

            if (unit == null)
            {
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = effectObj.offSet;
                go.transform.localEulerAngles = effectObj.rotation;
            }
            else
            {
                GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
                if (gameObjectComponent != null && gameObjectComponent.GetGo() != null)
                {
                    // 通过 effectObj.hangPointName 找到节点
                    Transform tran = gameObjectComponent.GetGo().transform;
                    go.transform.SetParent(tran);
                    go.transform.localPosition = effectObj.offSet;
                    go.transform.localEulerAngles = effectObj.rotation;
                    if (effectObj.isScaleByUnit)
                    {
                        go.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        float scaleX = tran.localScale.x;
                        go.transform.localScale = Vector3.one / scaleX;
                    }
                }
            }
            self.go.SetActive(true);
            ET.Client.GameObjectPoolHelper.TrigFromPool(go);

            self.UpdateEffect(true, 0);
        }

        public static void Refresh(this EffectShowObj self, EffectObj effectObj)
        {
            if (self.RefEffectObj == effectObj)
            {
                return;
            }
            self.RefEffectObj = effectObj;

            self.UpdateEffect(false, 0);
        }

        public static void UpdateEffect(this EffectShowObj self, bool isFirst, float fixedDeltaTime)
        {
            if (self.RefEffectObj == null)
            {
                return;
            }
            self.UpdateLineEffect(isFirst);
            self.UpdatePointLightningTrailEffect();
        }

        public static void UpdateLineEffect(this EffectShowObj self, bool isFirst)
        {
            if (!(self.RefEffectObj.effectShowType == EffectShowType.Send2Receives || self.RefEffectObj.effectShowType == EffectShowType.Send2Receive2Receive))
            {
                return;
            }

            if (isFirst)
            {
                if (self.lightningBoltScripts == null)
                {
                    self._UpdateLightningBoltEffect();
                }
                if (self.lightningBoltScripts.Length == 0)
                {
                    if (self.lineRenderers == null)
                    {
                        self._UpdateSimpleLineEffect();
                    }
                    if (self.lineRenderers.Length == 0)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (self.lightningBoltScripts != null && self.lightningBoltScripts.Length > 0)
                {
                    self._UpdateLightningBoltEffect();
                }
                else if (self.lineRenderers != null && self.lineRenderers.Length > 0)
                {
                    self._UpdateSimpleLineEffect();
                }
            }
        }

        public static void _UpdateLightningBoltEffect(this EffectShowObj self)
        {
            if (self.lightningBoltScripts == null)
            {
                self.lightningBoltScripts = self.go.GetComponentsInChildren<DigitalRuby.LightningBolt.LightningBoltScript>();
            }
            if (self.lightningBoltScripts.Length == 0)
            {
                return;
            }

            foreach (DigitalRuby.LightningBolt.LightningBoltScript lightningBoltScript in self.lightningBoltScripts)
            {
                lightningBoltScript.StartObject = null;
                lightningBoltScript.EndObject = null;
                Unit casterUnit = UnitHelper.GetUnit(self.DomainScene(), self.RefEffectObj.casterUnitId);
                float3 attackPos = new float3(casterUnit.Position.x, UnitHelper.GetAttackPointHeight(casterUnit), casterUnit.Position.z);
                lightningBoltScript.StartPosition = attackPos;
                float3 effectUnitPos = self.RefEffectObj.GetUnit().Position;
                float3 endPos = new float3(effectUnitPos.x, UnitHelper.GetBodyHeight(self.RefEffectObj.GetUnit())*0.5f, effectUnitPos.z);
                lightningBoltScript.EndPosition = endPos;
            }
        }

        public static void _UpdateSimpleLineEffect(this EffectShowObj self)
        {
            if (self.lineRenderers == null)
            {
                self.lineRenderers = self.go.GetComponentsInChildren<LineRenderer>();
            }
            if (self.lineRenderers.Length == 0)
            {
                return;
            }

            foreach (LineRenderer lineRenderer in self.lineRenderers)
            {
                lineRenderer.useWorldSpace = true;
                lineRenderer.positionCount = 2;
                Unit casterUnit = UnitHelper.GetUnit(self.DomainScene(), self.RefEffectObj.casterUnitId);
                float3 beginPos = casterUnit.Position;
                lineRenderer.SetPosition(0, beginPos);
                float3 endPos = self.RefEffectObj.GetUnit().Position;
                lineRenderer.SetPosition(1, endPos);
            }
        }

        public static void UpdatePointLightningTrailEffect(this EffectShowObj self)
        {
            if (self.RefEffectObj.effectShowType != EffectShowType.PointLightningTrail)
            {
                return;
            }

            if (self.lightningBoltScriptManagers == null)
            {
                self.lightningBoltScriptManagers = self.go.GetComponentsInChildren<DigitalRuby.LightningBolt.LightningBoltScriptManager>();
            }
            if (self.lightningBoltScriptManagers.Length == 0)
            {
                return;
            }

            List<long> list = self.RefEffectObj.pointLightningTrailList;
            if (list == null || list.Count == 0)
            {
                return;
            }
            foreach (var lightningBoltScriptManager in self.lightningBoltScriptManagers)
            {
                int midIndex = lightningBoltScriptManager.GetMidIndex();

                List<(long key, Vector3 pos)> dic = new();
                for (int i = 0; i < list.Count; i++)
                {
                    long key = list[i];
                    Vector3 pos = Vector3.zero;
                    if (midIndex <= i)
                    {
                        Unit unitTmp = UnitHelper.GetUnit(self.DomainScene(), key);
                        if (unitTmp != null)
                        {
                            pos = unitTmp.Position;
                        }
                    }
                    dic.Add((key, pos));
                }
                lightningBoltScriptManager.ResetDicKeyPos(dic);
            }
        }
    }
}