using System.Collections.Generic;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class EffectShowComponent: Entity, IAwake, IDestroy, IUpdate
    {
        /// <summary>
        /// 当前存在的列表
        /// </summary>
        public Dictionary<long, EffectShowObj> curExistEffectList;
        /// <summary>
        /// 准备移除的列表
        /// </summary>
        public HashSet<long> waitRemoveEffectList;

        public int curFrame = 0;
        public int waitFrame = 30;

        public int curFrameOnly = 0;
        public int waitFrameOnly = 1;
    }
}