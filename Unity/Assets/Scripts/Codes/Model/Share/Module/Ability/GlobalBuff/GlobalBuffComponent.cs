using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    public enum GlobalBuffType
    {
        Unit,
        Player,
        Game,
    }

    [ComponentOf(typeof(Scene))]
	public class GlobalBuffComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public List<long> removeList;

        public Dictionary<long, GlobalBuffType> childId2GlobalBuffType;

        /// <summary>
        /// 记录配置有触发事件对应的GlobalBuffObj
        /// </summary>
        public MultiMapSimple<AbilityGameMonitorTriggerEvent, long> monitorTriggerList;
    }
}