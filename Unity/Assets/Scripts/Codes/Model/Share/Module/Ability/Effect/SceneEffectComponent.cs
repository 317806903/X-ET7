using System.Collections.Generic;

namespace ET.Ability
{
    [ComponentOf(typeof(Scene))]
	public class SceneEffectComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<long> removeList;
        public List<(string key, EntityRef<EffectObj> effectObjRef)> removeKeyList;
        /// <summary>
        /// unitId -> effectKey -> effectObjId
        /// </summary>
        public Dictionary<long, MultiMapSimple<string, EntityRef<EffectObj>>> recordUnitId2EffectList;
        /// <summary>
        /// effectKey -> effectObjId
        /// </summary>
        public MultiMapSimple<string, EntityRef<EffectObj>> recordEffectList;

        public int waitFrameChk = 5;
        public int curFrameChk = 0;
    }
}