using System.Collections.Generic;

namespace ET.Ability
{
    [ComponentOf]
	public class EffectShowChgComponent: Entity, IAwake, IDestroy
    {
        public HashSet<long> chgEffectList = new();
    }
}