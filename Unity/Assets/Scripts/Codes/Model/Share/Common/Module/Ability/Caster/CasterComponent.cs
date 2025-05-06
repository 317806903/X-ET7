using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ComponentOf(typeof (Unit))]
    public class CasterComponent: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///要发射子弹的这个人
        ///当然可以是null发射的，但是写效果逻辑的时候得小心caster是null的情况
        ///</summary>
        public long casterUnitId;
    }
}