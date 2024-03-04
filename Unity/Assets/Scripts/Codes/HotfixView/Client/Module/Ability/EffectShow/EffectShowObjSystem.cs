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
            }
        }

        public static async ETTask Init(this EffectShowObj self, EffectObj effectObj)
        {
            string resName = effectObj.model.ResName;
            GameObject go = GameObjectPoolHelper.GetObjectFromPool(resName,true,1);
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
            ET.Client.GameObjectPoolHelper.TrigFromPool(go);

        }

    }
}