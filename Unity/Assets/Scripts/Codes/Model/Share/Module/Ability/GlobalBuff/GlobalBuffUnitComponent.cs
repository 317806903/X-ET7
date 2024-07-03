using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf(typeof(GlobalBuffManagerComponent))]
	public class GlobalBuffUnitComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<GlobalBuffUnitObj> removeList;

        /// <summary>
        /// 记录配置有触发事件对应的GlobalBuffObj
        /// </summary>
        public MultiMapSimple<ET.AbilityConfig.GlobalBuffTriggerEvent, EntityRef<GlobalBuffUnitObj>> monitorTriggerList;

        /// <summary>
        /// tagGroup对应buff列表
        /// </summary>
        public MultiMapSimple<BuffTagGroupType, EntityRef<GlobalBuffUnitObj>> buffTagGroupTypeList;

    }
}