using System.Collections.Generic;

namespace ET.Ability
{
    [ComponentOf(typeof(Scene))]
	public class SceneEffectComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<long> removeList;
        /// <summary>
        /// unitId -> effectKey -> effectObjId
        /// </summary>
        public Dictionary<long, MultiMap<string, EntityRef<EffectObj>>> recordEffectList;
    }
}