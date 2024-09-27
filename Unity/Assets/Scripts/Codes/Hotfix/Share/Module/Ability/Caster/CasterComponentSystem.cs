using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (CasterComponent))]
    public static class CasterComponentSystem
    {
        [ObjectSystem]
        public class CasterComponentAwakeSystem: AwakeSystem<CasterComponent>
        {
            protected override void Awake(CasterComponent self)
            {
            }
        }

        [ObjectSystem]
        public class CasterComponentDestroySystem: DestroySystem<CasterComponent>
        {
            protected override void Destroy(CasterComponent self)
            {
            }
        }

        public static void AddCaster(this CasterComponent self, long casterUnitId)
        {
            self.casterUnitId = casterUnitId;
        }

        public static Unit GetCaster(this CasterComponent self)
        {
            Unit casterUnit = UnitHelper.GetUnit(self.DomainScene(), self.casterUnitId);
            return casterUnit;
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this CasterComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}