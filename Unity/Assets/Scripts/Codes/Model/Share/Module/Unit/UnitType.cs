using System.Collections.Generic;
using Unity.Mathematics;

namespace ET
{
    public enum UnitType: byte
    {
        ObserverUnit = 0,
        PlayerUnit = 1,
        CameraPlayerUnit = 2,
        ActorUnit = 3,
        NPC = 4,
        SceneObj = 5,
        Bullet = 6,
        Aoe = 7,
        SceneEffect = 8,
    }

}