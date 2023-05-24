using System.Collections.Generic;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class EffectShowComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public Dictionary<long, EffectShowObj> recordEffectList;
        public HashSet<long> recordExistEffectList;
    }
}