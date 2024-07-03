using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    public struct ActionGameContext
    {
        public TeamFlagType teamFlagType;
        public long playerId;
        public long unitId;
        public string towerCfgId;
        public TowerType towerType;
        public long attackerUnitId;
        public long defenderUnitId;
    }
}