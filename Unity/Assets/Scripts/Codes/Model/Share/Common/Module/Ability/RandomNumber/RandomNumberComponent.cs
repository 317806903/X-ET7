using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class RandomNumberComponent: Entity, IAwake, IDestroy
    {
        public long clientFrameTime;
        public int randomNumber;
    }
}