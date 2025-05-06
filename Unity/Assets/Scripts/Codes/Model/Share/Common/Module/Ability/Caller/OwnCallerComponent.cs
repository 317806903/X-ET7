using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class OwnCallerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<EntityRef<Unit>> clearList = new();
        public HashSet<EntityRef<Unit>> ownCallActor;
        public HashSet<EntityRef<Unit>> ownCallBullet;
        public HashSet<EntityRef<Unit>> ownCallAoe;
        public HashSet<EntityRef<Unit>> ownCallSkillCaster;

        public int waitFrameChk = 100;
        public int curFrameChk = 0;
    }
}