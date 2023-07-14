using System.Collections.Generic;

namespace ET.Ability
{
    //[ComponentOf(typeof(Unit))]
    [ComponentOf]
	public class EffectComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<EffectObj> removeList;
        public MultiMap<string, long> recordEffectList;
    }
}