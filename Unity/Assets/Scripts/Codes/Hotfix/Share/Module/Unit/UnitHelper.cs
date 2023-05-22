using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    [FriendOf(typeof(UnitComponent))]
    [FriendOf(typeof(MoveByPathComponent))]
    [FriendOf(typeof(NumericComponent))]
    public static class UnitHelper
    {
        public static UnitComponent GetUnitComponent(Unit unit)
        {
            return unit.DomainScene().GetComponent<UnitComponent>();
        }
        
        public static UnitComponent GetUnitComponent(Scene scene)
        {
            return scene.GetComponent<UnitComponent>();
        }
        
        public static Unit GetUnit(Scene scene, long unitId)
        {
            Unit unit = GetUnitComponent(scene).Get(unitId);
            return unit;
        }
        
        public static bool ChkUnitAlive(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            
            return ChkUnitAlive(unit);
        }
        
        public static bool ChkUnitAlive(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }
            return true;
        }
        
        public static bool ChkIsPlayer(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsPlayer(unit);
        }
        
        public static bool ChkIsPlayer(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.Player)
            {
                return true;
            }
            return false;
        }
        
        public static bool ChkIsBullet(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsBullet(unit);
        }
        
        public static bool ChkIsBullet(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.Bullet)
            {
                return true;
            }
            return false;
        }
        
        public static bool ChkIsAoe(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsAoe(unit);
        }
        
        public static bool ChkIsAoe(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.Aoe)
            {
                return true;
            }
            return false;
        }
        
        public static ListComponent<Unit> GetFriends(Unit curUnit, bool isOnlyPlayer)
        {
            ListComponent<Unit> friends = ListComponent<Unit>.Create();
            foreach (Unit unit in GetUnitComponent(curUnit).playerList)
            {
                if (TeamFlagHelper.ChkIsFriend(curUnit, unit))
                {
                    friends.Add(unit);
                }
            }

            if (isOnlyPlayer == false)
            {
                foreach (Unit unit in GetUnitComponent(curUnit).monsterList)
                {
                    if (TeamFlagHelper.ChkIsFriend(curUnit, unit))
                    {
                        friends.Add(unit);
                    }
                }
            }
            
            return friends;
        }

        public static ListComponent<Unit> GetHostileForces(Unit curUnit, bool isOnlyPlayer)
        {
            ListComponent<Unit> hostileForces = ListComponent<Unit>.Create();
            foreach (Unit unit in GetUnitComponent(curUnit).playerList)
            {
                if (TeamFlagHelper.ChkIsFriend(curUnit, unit) == false)
                {
                    hostileForces.Add(unit);
                }
            }

            if (isOnlyPlayer == false)
            {
                foreach (Unit unit in GetUnitComponent(curUnit).monsterList)
                {
                    if (TeamFlagHelper.ChkIsFriend(curUnit, unit) == false)
                    {
                        hostileForces.Add(unit);
                    }
                }
            }
            
            return hostileForces;
        }

        public static bool ChkCanAttack(Unit curUnit, Unit targetUnit, float radius)
        {
            float bRadius = 0.1f;
            float cRadius = 0.1f;
            float3 dis = curUnit.Position - targetUnit.Position;
            if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= math.pow(radius + bRadius + cRadius, 2))
            {
                return true;
            }

            return false;
        }
        
        public static void AddWaitRemove(Unit unit)
        {
            UnitComponent unitComponent = GetUnitComponent(unit);
            unitComponent.AddWaitRemove(unit);
        }
        
        public static void ResetNodePosition(Unit unit, Unit targetUnit, string nodeName, Vector3 offSetPosition, Vector3 relateForward)
        {
            targetUnit.Position = unit.Position + new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            
        }
        
        public static void AddSyncUnit(Unit unit)
        {
            GetUnitComponent(unit).AddSyncUnit(unit);
        }
        
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.unitCfgId;
            unitInfo.Type = (int)unit.Type;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            if (moveByPathComponent != null)
            {
                if (!moveByPathComponent.IsArrived())
                {
                    unitInfo.MoveInfo = new MoveInfo() { Points = new List<float3>() };
                    unitInfo.MoveInfo.Points.Add(unit.Position);
                    for (int i = moveByPathComponent.N; i < moveByPathComponent.Targets.Count; ++i)
                    {
                        float3 pos = moveByPathComponent.Targets[i];
                        unitInfo.MoveInfo.Points.Add(pos);
                    }
                }
            }

            unitInfo.KV = new Dictionary<int, long>();

            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.KV.Add(key, value);
            }

            unitInfo.Components = new List<byte[]>();
            foreach (Entity entity in unit.Components.Values)
            {
                if (entity is ITransferClient)
                {
                    unitInfo.Components.Add(entity.ToBson());
                }
            }

            return unitInfo;
        }
        
        public static UnitInfo SyncUnitSimpleInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            unitInfo.UnitId = unit.Id;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            return unitInfo;
        }
    }
}