using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [ComponentOf(typeof(Unit))]
	public class BuffComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<BuffObj> removeList;
        public bool isForeaching;

        /// <summary>
        /// 记录配置有触发事件对应的BuffObj
        /// </summary>
        public MultiMapSimple<AbilityConfig.BuffTriggerEvent, BuffObj> monitorTriggerList;

        /// <summary>
        /// tag标志对应buff列表
        /// </summary>
        public MultiMapSimple<BuffTagType, BuffObj> buffTagTypeList;

        /// <summary>
        /// 免疫哪个类型的buff 对应的buff列表
        /// </summary>
        public MultiMapSimple<BuffTagType, BuffObj> buffImmuneTagTypeList;

        /// <summary>
        /// tagGroup对应buff列表
        /// </summary>
        public MultiMapSimple<BuffTagGroupType, BuffObj> buffTagGroupTypeList;

        /// <summary>
        /// 免疫TagGroup的buff 对应的buff列表
        /// </summary>
        public MultiMapSimple<BuffTagGroupType, BuffObj> buffImmuneTagGroupTypeList;

        public MultiMapSimple<BuffType, BuffObj> buffTypeList;

        /// <summary>
        /// 存在 Motion的BuffObj
        /// </summary>
        public HashSet<BuffObj> buffMotionList;
        /// <summary>
        /// 存在 PlayAnimator的BuffObj
        /// </summary>
        public HashSet<BuffObj> buffPlayAnimatorList;
    }
}