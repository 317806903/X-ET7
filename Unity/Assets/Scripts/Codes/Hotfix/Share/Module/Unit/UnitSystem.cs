using System.Collections.Generic;
using ET.Ability;
using Unity.Mathematics;

namespace ET
{
    public static class UnitSystem
    {
        [ObjectSystem]
        public class UnitAwakeSystem: AwakeSystem<Unit, string>
        {
            protected override void Awake(Unit self, string unitCfgId)
            {
                self.CfgId = unitCfgId;
            }
        }

        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, EntityRef<AOIEntity>> GetBeSeePlayers(this Unit self)
        {
            AOIEntity aoiEntity = self.GetComponent<AOIEntity>();
            if (aoiEntity == null)
            {
                return null;
            }
            return aoiEntity.GetBeSeePlayers();
        }

        public static bool ChkIsInDeath(this Unit self)
        {
            return ET.Ability.DeathShowHelper.ChkIsInDeath(self);
        }

        public static void DestroyWithDeathShow(this Unit self)
        {
            ET.Ability.DeathShowHelper.DeathShow(self);
        }

        public static void DestroyNotDeathShow(this Unit self)
        {
            self._Destroy();
        }

        public static void _Destroy(this Unit self)
        {
            EventSystem.Instance.Publish(self.DomainScene(), new ET.Ability.AbilityTriggerEventType.UnitOnRemoved() { unit = self });
            //self.Dispose();
            UnitHelper.AddWaitRemove(self);
        }

        public static float3 GetUnitClientPos(this Unit self)
        {
            UnitClientPosComponent unitClientPosComponent = self.GetComponent<UnitClientPosComponent>();
            if (unitClientPosComponent != null)
            {
                if (unitClientPosComponent.clientPosition.Equals(float3.zero))
                {
                    return self.Position;
                }
                return unitClientPosComponent.clientPosition;
            }
            return self.Position;
        }

        public static void AddOwnCaller(this Unit self, Unit caller)
        {
            ET.Ability.OwnCallerHelper.AddOwnCaller(self, caller);
        }

        public static HashSet<long> GetOwnCaller(this Unit self, bool ownCallActor, bool ownBullet, bool ownAoe)
        {
            HashSet<long> unitList = ET.Ability.OwnCallerHelper.GetOwnCaller(self, ownCallActor, ownBullet, ownAoe);
            return unitList;
        }

        public static void AddCaster(this Unit self, Unit caster)
        {
            ET.Ability.CasterHelper.AddCaster(self, caster);
        }

        /// <summary>
        /// 往上找到的第一个的Unit，例如 A1 召唤了 A2， A2 发射了子弹 B1， B1某个时刻再发射的子弹B2
        ///     则 子弹B2通过这个接口可以找到B1
        ///     则 子弹B1通过这个接口可以找到A2
        ///     则 A2通过这个接口可以找到A1
        /// </summary>
        /// <returns></returns>
        public static Unit GetCaster(this Unit self)
        {
            return ET.Ability.CasterHelper.GetCaster(self);
        }

        /// <summary>
        /// 往上找到的第一个的Actor，例如 A1 召唤了 A2， A2 发射了子弹 B1， B1某个时刻再发射的子弹B2
        ///     则 子弹B2通过这个接口可以找到A2
        ///     则 子弹B1通过这个接口可以找到A2
        ///     则 A2通过这个接口可以找到A1
        /// </summary>
        /// <returns></returns>
        public static Unit GetCasterFirstActor(this Unit self, bool isContainSelf = true)
        {
            Unit unit = self;
            while (true)
            {
                if (UnitHelper.ChkIsPlayer(unit)
                    || UnitHelper.ChkIsCameraPlayer(unit)
                    || UnitHelper.ChkIsActor(unit))
                {
                    break;
                }
                Unit casterUnit = unit.GetCaster();
                if (casterUnit == null)
                {
                    break;
                }

                unit = casterUnit;
            }

            if (isContainSelf == false)
            {
                if (unit == self)
                {
                    return null;
                }
            }

            return unit;
        }

        /// <summary>
        /// 往上找到的最后的Actor，例如 A1 召唤了 A2， A2 发射了子弹 B1， B1某个时刻再发射的子弹B2
        ///     则 子弹B2通过这个接口可以找到A1
        ///     则 子弹B1通过这个接口可以找到A1
        ///     则 A2通过这个接口可以找到A1
        /// </summary>
        /// <returns></returns>
        public static Unit GetCasterActor(this Unit self, bool isContainSelf = true)
        {
            Unit unit = self;
            while (true)
            {
                Unit casterUnit = unit.GetCaster();
                if (casterUnit == null)
                {
                    break;
                }

                unit = casterUnit;
            }

            if (isContainSelf == false)
            {
                if (unit == self)
                {
                    return null;
                }
            }

            if (UnitHelper.ChkIsPlayer(unit)
                || UnitHelper.ChkIsCameraPlayer(unit)
                || UnitHelper.ChkIsActor(unit))
            {
                return unit;
            }
            return null;
        }

    }
}