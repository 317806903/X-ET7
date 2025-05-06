using System;
using System.Collections.Generic;
using System.Xml.Schema;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (UnitResetNavObstacleComponent))]
    public static class UnitResetNavObstacleComponentSystem
    {
        [ObjectSystem]
        public class UnitResetNavObstacleComponentAwakeSystem: AwakeSystem<UnitResetNavObstacleComponent, float, float>
        {
            protected override void Awake(UnitResetNavObstacleComponent self, float radius, float height)
            {

                Unit unit = self.GetUnit();
                float navObstacleRadius = ET.Ability.UnitHelper.GetNavObstacleRadius(unit.DomainScene(), unit.CfgId);
                float navObstacleHeight = ET.Ability.UnitHelper.GetNavObstacleHeight(unit.DomainScene(), unit.CfgId);

                self.resetNavObstacleRadius = math.min(radius, navObstacleRadius);
                self.resetNavObstacleHeight = math.min(height, navObstacleHeight);
            }
        }

        [ObjectSystem]
        public class UnitResetNavObstacleComponentDestroySystem: DestroySystem<UnitResetNavObstacleComponent>
        {
            protected override void Destroy(UnitResetNavObstacleComponent self)
            {
            }
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this UnitResetNavObstacleComponent self)
        {
            return self.GetParent<Unit>();
        }
    }
}