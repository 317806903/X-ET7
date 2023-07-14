using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (EffectObj))]
    public static class EffectObjSystem
    {
        [ObjectSystem]
        public class EffectObjAwakeSystem: AwakeSystem<EffectObj>
        {
            protected override void Awake(EffectObj self)
            {
            }
        }

        [ObjectSystem]
        public class EffectObjDestroySystem: DestroySystem<EffectObj>
        {
            protected override void Destroy(EffectObj self)
            {
            }
        }

        public static void Init(this EffectObj self, long unitId, string key, string effectCfgId, float duration, OffSetInfo offSetInfo)
        {
            string nodeName = offSetInfo.NodeName;
            float3 offSetPosition = new float3(offSetInfo.OffSetPosition.X, offSetInfo.OffSetPosition.Y, offSetInfo.OffSetPosition.Z);
            float3 relateForward = new float3(offSetInfo.RelateForward.X, offSetInfo.RelateForward.Y, offSetInfo.RelateForward.Z);
            
            self.isSceneEffect = unitId == 0? true : false;
            self.unitId = unitId;
            self.CfgId = effectCfgId;
            self.timeElapsed = 0;
            self.permanent = duration == -1? true : false;
            self.duration = duration == -1? 1 : duration;
            self.hangPointName = nodeName;
            self.offSet = offSetPosition;
            self.rotation = relateForward;
            self.key = key;
        }
        
        public static Unit GetUnit(this EffectObj self)
        {
            EffectComponent effectComponent = self.GetParent<EffectComponent>();
            return effectComponent.GetParent<Unit>();
        }

        public static void FixedUpdate(this EffectObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            self.timeElapsed += timePassed;
        }

        public static bool ChkNeedRemove(this EffectObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0)
            {
                return true;
            }

            return false;
        }
    }
}