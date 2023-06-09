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
                UnityEngine.Object.Destroy(self.go);
            }
        }

        public static async ETTask Init(this EffectShowObj self, EffectObj effectObj)
        {
            string resName = effectObj.model.ResName;
            GameObject prefab = await ResComponent.Instance.LoadAssetAsync<GameObject>(resName);
            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);

            ParticleSystem particleSystem = go.GetComponentInChildren<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
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
                GameObject gameObject = unit.GetComponent<GameObjectComponent>().GameObject;
                // 通过 effectObj.hangPointName 找到节点
                Transform tran = gameObject.transform;
                go.transform.SetParent(tran);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = effectObj.offSet;
                go.transform.localEulerAngles = effectObj.rotation;
            }
        }

    }
}