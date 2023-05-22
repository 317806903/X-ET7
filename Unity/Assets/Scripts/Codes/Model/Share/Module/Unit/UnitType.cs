using System.Collections.Generic;

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
}