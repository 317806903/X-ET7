using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ET.AbilityConfig;
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
            if (scene == null)
            {
                return null;
            }
            UnitComponent unitComponent = GetUnitComponent(scene);
            if (unitComponent == null)
            {
                return null;
            }
            Unit unit = unitComponent.Get(unitId);
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
            if (unit.IsDisposed)
            {
                return false;
            }

            if (ChkIsBullet(unit))
            {
                return true;
            }
            else
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                int curHp = numericComponent.GetAsInt(NumericType.Hp);
                if (curHp > 0)
                {
                    return true;
                }
                return false;
            }
        }
        
        public static bool ChkIsObserver(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsObserver(unit);
        }
        
        public static bool ChkIsObserver(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.ObserverUnit)
            {
                return true;
            }
            return false;
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

            if (unit.Type == UnitType.PlayerUnit)
            {
                return true;
            }
            return false;
        }
        
        public static bool ChkIsActor(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsBullet(unit);
        }
        
        public static bool ChkIsActor(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.ActorUnit)
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
        
        public static bool ChkIsSceneEffect(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsBullet(unit);
        }
        
        public static bool ChkIsSceneEffect(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.SceneEffect)
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
                if (ET.GamePlayHelper.ChkIsFriend(curUnit, unit))
                {
                    if (UnitHelper.ChkUnitAlive(unit))
                    {
                        friends.Add(unit);
                    }
                }
            }

            if (isOnlyPlayer == false)
            {
                foreach (Unit unit in GetUnitComponent(curUnit).actorList)
                {
                    if (ET.GamePlayHelper.ChkIsFriend(curUnit, unit))
                    {
                        if (UnitHelper.ChkUnitAlive(unit))
                        {
                            friends.Add(unit);
                        }
                    }
                }
            }
            
            return friends;
        }

        /// <summary>
        /// 获取敌对势力的对象列表
        /// </summary>
        /// <param name="curUnit"></param>
        /// <param name="isOnlyPlayer"></param>
        /// <returns></returns>
        public static ListComponent<Unit> GetHostileForces(Unit curUnit, bool isOnlyPlayer)
        {
            ListComponent<Unit> hostileForces = ListComponent<Unit>.Create();
            
            Dictionary<long, AOIEntity> seeUnits = curUnit.GetComponent<AOIEntity>().GetSeeUnits();
            foreach (var seeUnit in seeUnits)
            {
                Unit unit = seeUnit.Value.Unit;
                bool isContinue = false;
                if (UnitHelper.ChkIsPlayer(unit) || UnitHelper.ChkIsActor(unit))
                {
                    isContinue = true;
                }
                else
                {
                    isContinue = false;
                }
                if (isContinue == false)
                {
                    continue;
                }
                ProfilerSample.BeginSample("seeUnits ET.GamePlayHelper.ChkIsFriend");
                bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit);
                ProfilerSample.EndSample();
                if (isFriend == false)
                {
                    if (UnitHelper.ChkUnitAlive(unit))
                    {
                        if (UnitHelper.ChkIsPlayer(unit))
                        {
                            hostileForces.Add(unit);
                        }
                        else if (UnitHelper.ChkIsActor(unit) && isOnlyPlayer == false)
                        {
                            hostileForces.Add(unit);
                        }
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
        
        public static bool ChkIsNear(Unit curUnit, Unit targetUnit)
        {
            float radius = 0.3f;
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
        
        public static (float3, float3) GetNewNodePosition(Unit unit, OffSetInfo offSetInfo)
        {
            string nodeName = offSetInfo.NodeName;
            Vector3 offSetPosition = offSetInfo.OffSetPosition;
            Vector3 relateForward = offSetInfo.RelateForward;
            
            float3 newPosition = unit.Position + new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            float3 newForward = unit.Forward + new float3(relateForward.X, relateForward.Y, relateForward.Z);
            return (newPosition, newForward);
        }
        
        public static float3 GetNewNodePosition(float3 resetPos, OffSetInfo offSetInfo)
        {
            string nodeName = offSetInfo.NodeName;
            Vector3 offSetPosition = offSetInfo.OffSetPosition;
            Vector3 relateForward = offSetInfo.RelateForward;
            
            float3 newPosition = resetPos + new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            return newPosition;
        }
        
        public static void AddSyncPosUnit(Unit unit)
        {
            GetUnitComponent(unit).AddSyncPosUnit(unit);
        }
        
        public static void AddSyncNumericUnit(Unit unit)
        {
            GetUnitComponent(unit).AddSyncNumericUnit(unit);
        }
        
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.CfgId;
            unitInfo.Type = (int)unit.Type;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            if (moveByPathComponent != null)
            {
                if (!moveByPathComponent.IsArrived())
                {
                    unitInfo.MoveInfo = new MoveInfo() { Points = ListComponent<float3>.Create() };
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

            unitInfo.Components = ListComponent<byte[]>.Create();
            foreach (Entity entity in unit.Components.Values)
            {
                if (entity is ITransferClient)
                {
                    unitInfo.Components.Add(entity.ToBson());
                }
            }
            
            EffectComponent effectComponent = unit.GetComponent<EffectComponent>();
            if (effectComponent != null)
            {
                unitInfo.EffectComponents = ListComponent<byte[]>.Create();
                foreach (Entity entity in effectComponent.Components.Values)
                {
                    unitInfo.EffectComponents.Add(entity.ToBson());
                }
            }

            return unitInfo;
        }
        
        public static UnitPosInfo SyncPosUnitInfo(Unit unit)
        {
            UnitPosInfo unitInfo = new UnitPosInfo();
            unitInfo.UnitId = unit.Id;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            return unitInfo;
        }
        
        public static UnitNumericInfo SyncNumericUnitInfo(Unit unit)
        {
            UnitNumericInfo unitInfo = new UnitNumericInfo();
            unitInfo.UnitId = unit.Id;
            unitInfo.KV = new Dictionary<int, long>();
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            foreach ((int key, long value) in numericComponent.NumericDic)
            {
                unitInfo.KV.Add(key, value);
            }

            return unitInfo;
        }
        
        public static float GetTargetUnitAngle(Unit curUnit, Unit targetUnit)
        {
            float3 targetPos = targetUnit.Position;
            return GetTargetPosAngle(curUnit, targetPos);
        }
        
        public static float GetTargetPosAngle(Unit curUnit, float3 targetPos)
        {
            float3 targetDir = math.normalize(targetPos - curUnit.Position);
            return GetTargetDirAngle(curUnit, targetDir);
        }
        
        /// <summary>
        /// 返回 夹角(弧度角)
        /// </summary>
        /// <param name="curUnit"></param>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        public static float GetTargetDirAngle(Unit curUnit, float3 targetDir)
        {
            float3 forward = math.normalize(curUnit.Forward);
            //float angleTmp = math.degrees(math.acos(math.clamp(math.dot(forward, targetDir), -1, 1)));
            float angleTmp = math.acos(math.clamp(math.dot(forward, targetDir), -1, 1));
            float y = math.cross(forward, targetDir).y;
            if (y > 0)
            {
                return angleTmp;
            }
            else
            {
                return -angleTmp;
            }
        }
        
        public static void SaveSelectHandle(Unit curUnit, SelectHandle selectHandle)
        {
            SelectHandleObj selectHandleObj = curUnit.GetComponent<SelectHandleObj>();
            if (selectHandleObj == null)
            {
                selectHandleObj = curUnit.AddComponent<SelectHandleObj>();
            }
            selectHandleObj.SaveSelectHandle(selectHandle);
        }
        
        public static SelectHandle GetSaveSelectHandle(Unit curUnit)
        {
            SelectHandleObj selectHandleObj = curUnit.GetComponent<SelectHandleObj>();
            return selectHandleObj?.GetSaveSelectHandle();
        }
        
    }
}