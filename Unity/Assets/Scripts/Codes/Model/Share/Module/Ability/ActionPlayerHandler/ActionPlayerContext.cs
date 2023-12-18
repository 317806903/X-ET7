using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    public struct ActionPlayerContext
    {
        public long unitId;
        public string skillCfgId;
        public SkillSlotType skillSlotType;
        public int skillLevel;
        public float skillDis;
        public string timelineCfgId;
        public long timelineId;
        public long buffUnitId;
        public string buffCfgId;
        public long buffId;
        public long aoeId;
        public long attackerUnitId;
        public long defenderUnitId;
        public int selectUnitNum;
        public bool isBreakSoftBati;
        public bool isBreakStrongBati;
        public long motionUnitId;
        public float3 motionDirection;
        public float3 motionPosition;
        public float3 hitPosition;
    }
}