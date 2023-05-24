using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    public enum UnitType: byte
    {
        Player = 1,
        Monster = 2,
        NPC = 3,
        SceneObj = 4,
        Bullet = 5,
        Aoe = 6,
    }

    public struct SyncUnits
    {
        public List<Unit> units;
    }
    public struct SyncUnitEffects
    {
        public Unit unit;
        public bool isSceneEffect;
        public bool isAddEffect;
        public long effectObjId;
        public ET.Ability.EffectObj effectObj;
    }
    public struct CreateEffect
    {
        public bool isSceneEffect;
        public string key;
        public long componentId;
        public long unitId;
        public string nodeName;
        public float3 offsetPosition;
        public float3 relateForward;
    }
}