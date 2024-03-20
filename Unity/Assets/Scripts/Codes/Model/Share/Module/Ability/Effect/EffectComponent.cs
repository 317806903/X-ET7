using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class EffectComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<EffectObj> removeList;
        public MultiMapSimple<string, long> recordEffectList;
        public MultiMapSimple<EffectShowType, long> effectShowType2EffectObjId;
    }
}