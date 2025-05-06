using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ChildOf(typeof(GlobalBuffPlayerManagerComponent))]
	public class GlobalBuffPlayerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<GlobalBuffPlayerObj> removeList;

        /// <summary>
        /// 记录配置有触发事件对应的GlobalBuffObj
        /// </summary>
        public MultiMapSimple<ET.AbilityConfig.GlobalBuffTriggerEvent, EntityRef<GlobalBuffPlayerObj>> monitorTriggerList;

        /// <summary>
        /// tagGroup对应buff列表
        /// </summary>
        public MultiMapSimple<BuffTagGroupType, EntityRef<GlobalBuffPlayerObj>> buffTagGroupTypeList;

    }
}