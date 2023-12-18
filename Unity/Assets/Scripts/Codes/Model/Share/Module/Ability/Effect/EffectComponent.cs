using System.Collections.Generic;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class EffectComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<EffectObj> removeList;
        public MultiMapSimple<string, long> recordEffectList;
    }
}