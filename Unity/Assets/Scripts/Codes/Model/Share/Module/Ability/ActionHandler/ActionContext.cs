using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    public struct ActionContext
    {
        public long unitId;
        public string skillCfgId;
        public int skillLevel;
        public string timelineCfgId;
        public long timelineId;
        public long buffUnitId;
        public string buffCfgId;
        public long buffId;
        public long attackerUnitId;
        public long defenderUnitId;
        public bool isBreakSoftBati;
        public bool isBreakStrongBati;
    }
}