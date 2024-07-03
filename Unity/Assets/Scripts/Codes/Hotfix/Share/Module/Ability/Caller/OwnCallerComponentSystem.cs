using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (OwnCallerComponent))]
    public static class OwnCallerComponentSystem
    {
        [ObjectSystem]
        public class OwnCallerComponentAwakeSystem: AwakeSystem<OwnCallerComponent>
        {
            protected override void Awake(OwnCallerComponent self)
            {
                self.ownCallActor = new ();
                self.ownCallBullet = new ();
                self.ownCallAoe = new();
            }
        }

        [ObjectSystem]
        public class OwnCallerComponentDestroySystem: DestroySystem<OwnCallerComponent>
        {
            protected override void Destroy(OwnCallerComponent self)
            {
                self.ownCallActor.Clear();
                self.ownCallBullet.Clear();
                self.ownCallAoe.Clear();
            }
        }

        [ObjectSystem]
        public class OwnCallerComponentFixedUpdateSystem: FixedUpdateSystem<OwnCallerComponent>
        {
            protected override void FixedUpdate(OwnCallerComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

        public static void AddOwnCaller(this OwnCallerComponent self, Unit unit)
        {
            if (UnitHelper.ChkIsActor(unit))
            {
                self.ownCallActor.Add(unit);
            }
            else if (UnitHelper.ChkIsBullet(unit))
            {
                self.ownCallBullet.Add(unit);
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                self.ownCallAoe.Add(unit);
            }
        }

        public static HashSet<long> GetOwnCaller(this OwnCallerComponent self, bool ownCallActor, bool ownBullet, bool ownAoe)
        {
            HashSet<long> unitList = HashSetComponent<long>.Create();
            if (ownCallActor)
            {
                self._GetOwnCaller(self.ownCallActor, ref unitList);
            }
            if (ownBullet)
            {
                self._GetOwnCaller(self.ownCallBullet, ref unitList);
            }
            if (ownAoe)
            {
                self._GetOwnCaller(self.ownCallAoe, ref unitList);
            }
            return unitList;
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetUnit(this OwnCallerComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static void FixedUpdate(this OwnCallerComponent self, float fixedDeltaTime)
        {
            if (++self.curFrameChk >= self.waitFrameChk)
            {
                self.curFrameChk = 0;
                self.ClearNotExist();
            }
        }

        public static void ClearNotExist(this OwnCallerComponent self)
        {
            self._ClearNotExist(self.ownCallActor);
            self._ClearNotExist(self.ownCallBullet);
            self._ClearNotExist(self.ownCallAoe);
        }

        public static void _ClearNotExist(this OwnCallerComponent self, HashSet<EntityRef<Unit>> ownCaller)
        {
            using ListComponent<EntityRef<Unit>> tmp = ListComponent<EntityRef<Unit>>.Create();
            foreach (EntityRef<Unit> entityRef in ownCaller)
            {
                Unit unit = entityRef;
                if (unit == null)
                {
                    tmp.Add(entityRef);
                }
            }

            foreach (EntityRef<Unit> entityRef in tmp)
            {
                ownCaller.Remove(entityRef);
            }
        }

        public static void _GetOwnCaller(this OwnCallerComponent self, HashSet<EntityRef<Unit>> ownCaller, ref HashSet<long> unitList)
        {
            foreach (var entityRef in ownCaller)
            {
                Unit unit = entityRef;
                if (unit != null)
                {
                    unitList.Add(unit.Id);
                }
            }
        }

    }
}