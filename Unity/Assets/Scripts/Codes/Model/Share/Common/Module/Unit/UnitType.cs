using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    public enum UnitType: byte
    {
        ObserverUnit = 0,
        PlayerUnit = 1,
        CameraPlayerUnit = 2,
        SkillCasterUnit = 3,
        ActorUnit = 4,
        NPC = 5,
        SceneObj = 6,
        Bullet = 7,
        Aoe = 8,
        SceneEffect = 9,
    }

}