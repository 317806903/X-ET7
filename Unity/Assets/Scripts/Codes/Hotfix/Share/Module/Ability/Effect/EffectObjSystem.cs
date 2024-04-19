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
                self.pointLightningTrailList?.Clear();
            }
        }

        public static void Init(this EffectObj self, long casterUnitId, ActionCfg_EffectCreate actionCfgCreateEffect, bool isScaleByUnit)
        {
            self.casterUnitId = casterUnitId;

            string key = actionCfgCreateEffect.Key;
            int maxKeyNum = actionCfgCreateEffect.MaxKeyNum;
            string effectCfgId = actionCfgCreateEffect.ResEffectId;
            string playAudioActionId = actionCfgCreateEffect.PlayAudioActionId;
            float duration = actionCfgCreateEffect.Duration;
            OffSetInfo offSetInfo = actionCfgCreateEffect.OffSetInfo;
            EffectShowType effectShowType = actionCfgCreateEffect.EffectShowType;

            EffectNodeName nodeName = offSetInfo.NodeName;
            float3 offSetPosition = new float3(offSetInfo.OffSetPosition.X, offSetInfo.OffSetPosition.Y, offSetInfo.OffSetPosition.Z);
            float3 relateForward = new float3(offSetInfo.RelateForward.X, offSetInfo.RelateForward.Y, offSetInfo.RelateForward.Z);

            self.CfgId = effectCfgId;
            self.PlayAudioActionId = playAudioActionId;
            self.timeElapsed = 0;
            self.permanent = duration == -1? true : false;
            self.duration = duration == -1? 1 : duration;
            self.key = key;
            self.isScaleByUnit = isScaleByUnit;
            self.effectShowType = effectShowType;

            self.createTime = TimeHelper.ServerNow();
            self.hangPointName = nodeName;
            if (self.hangPointName == EffectNodeName.Self)
            {
                self.offSet = offSetPosition;
                self.rotation = relateForward;
                self.createPos = self.GetUnit().Position + self.offSet;
            }
            else
            {
                Unit casterUnit = UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);

                self.offSet = casterUnit.Position - self.GetUnit().Position + offSetPosition;
                self.rotation = relateForward;
                self.createPos = self.GetUnit().Position + self.offSet;
            }
        }

        public static void ResetTime(this EffectObj self, ActionCfg_EffectCreate actionCfgCreateEffect)
        {
            float duration = actionCfgCreateEffect.Duration;
            self.timeElapsed = 0;
            self.permanent = duration == -1? true : false;
            self.duration = duration == -1? 1 : duration;
        }

        public static void AddPointLightningTrail(this EffectObj self, long unitId)
        {
            if (self.pointLightningTrailList == null)
            {
                self.pointLightningTrailList = new();
            }
            self.pointLightningTrailList.Add(unitId);
        }

        public static EffectComponent GetEffectComponent(this EffectObj self)
        {
            EffectComponent effectComponent = self.GetParent<EffectComponent>();
            return effectComponent;
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

        public static void WillDestroy(this EffectObj self)
        {
            self.duration = 0;
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