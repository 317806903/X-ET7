using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ET.AbilityConfig;
using MongoDB.Bson;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    [FriendOf(typeof (UnitComponent))]
    //[FriendOf(typeof(MoveByPathComponent))]
    [FriendOf(typeof (NumericComponent))]
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

        public static RecycleSelectHandleComponent GetRecycleSelectHandleComponent(Scene scene)
        {
            return scene.GetComponent<RecycleSelectHandleComponent>();
        }

        public static UnitDelayRemoveComponent GetUnitDelayRemoveComponent(Scene scene)
        {
            return scene.GetComponent<UnitDelayRemoveComponent>();
        }

        public static void AddUnitDelayRemove(Scene scene, Unit unit)
        {
            GetUnitDelayRemoveComponent(scene).AddRemoveUnit(unit);
        }

        public static Unit GetUnit(Scene scene, long unitId)
        {
            if (scene == null)
            {
                return null;
            }

            UnitComponent unitComponent = GetUnitComponent(scene);
            if (unitComponent != null)
            {
                Unit unit = unitComponent.Get(unitId);
                if (unit != null)
                {
                    return unit;
                }
            }

            UnitDelayRemoveComponent unitDelayRemoveComponent = GetUnitDelayRemoveComponent(scene);
            if (unitDelayRemoveComponent != null)
            {
                Unit unit = unitDelayRemoveComponent.Get(unitId);
                return unit;
            }

            return null;
        }

        public static bool ChkUnitAlive(Scene scene, long unitId, bool isContainDeathShow = false)
        {
            Unit unit = GetUnit(scene, unitId);

            return ChkUnitAlive(unit, isContainDeathShow);
        }

        public static bool ChkUnitAlive(Unit unit, bool isContainDeathShow = false)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.IsDisposed)
            {
                return false;
            }

            if (isContainDeathShow)
            {
                return true;
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

        public static List<Unit> GetUnitListBySelectObjectType(Unit curUnit, SelectObjectType selectObjectType, bool isNeedChkCanBeFind)
        {
            List<Unit> list = null;
            if (selectObjectType == SelectObjectType.FriendPlayers
                || selectObjectType == SelectObjectType.FriendButNotPlayers
                || selectObjectType == SelectObjectType.Friends)
            {
                list = UnitHelper.GetFriends(curUnit, selectObjectType);
            }
            else if (selectObjectType == SelectObjectType.HostilePlayers
                     || selectObjectType == SelectObjectType.HostileButNotPlayers
                     || selectObjectType == SelectObjectType.Hostiles)
            {
                list = UnitHelper.GetHostileForces(curUnit, selectObjectType, isNeedChkCanBeFind);
            }
            else if (selectObjectType == SelectObjectType.AllPlayers)
            {
                list = UnitHelper.GetFriends(curUnit, SelectObjectType.FriendPlayers);
                List<Unit> listHostileForces = UnitHelper.GetHostileForces(curUnit, SelectObjectType.HostilePlayers, isNeedChkCanBeFind);
                list.AddRange(listHostileForces);
            }
            else if (selectObjectType == SelectObjectType.AllButNotPlayers)
            {
                list = UnitHelper.GetFriends(curUnit, SelectObjectType.FriendButNotPlayers);
                List<Unit> listHostileForces = UnitHelper.GetHostileForces(curUnit, SelectObjectType.HostileButNotPlayers, isNeedChkCanBeFind);
                list.AddRange(listHostileForces);
            }
            else if (selectObjectType == SelectObjectType.All)
            {
                list = UnitHelper.GetFriends(curUnit, SelectObjectType.Friends);
                List<Unit> listHostileForces = UnitHelper.GetHostileForces(curUnit, SelectObjectType.Hostiles, isNeedChkCanBeFind);
                list.AddRange(listHostileForces);
            }
            else
            {
            }
            return list;
        }

        public static List<Unit> GetFriends(Unit curUnit, SelectObjectType selectObjectType)
        {
            List<Unit> friends = ListComponent<Unit>.Create();
            bool isContainPlayer = false;
            bool isContainActor = false;
            if (selectObjectType == SelectObjectType.Friends)
            {
                isContainPlayer = true;
                isContainActor = true;
            }
            else if (selectObjectType == SelectObjectType.FriendPlayers)
            {
                isContainPlayer = true;
                isContainActor = false;
            }
            else if (selectObjectType == SelectObjectType.FriendButNotPlayers)
            {
                isContainPlayer = false;
                isContainActor = true;
            }

            if (isContainPlayer)
            {
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
            }

            if (isContainActor)
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
        public static List<Unit> GetHostileForces(Unit curUnit, SelectObjectType selectObjectType, bool isNeedChkCanBeFind)
        {
            List<Unit> hostileForces = ListComponent<Unit>.Create();

            bool isContainPlayer = false;
            bool isContainActor = false;
            if (selectObjectType == SelectObjectType.Hostiles)
            {
                isContainPlayer = true;
                isContainActor = true;
            }
            else if (selectObjectType == SelectObjectType.HostilePlayers)
            {
                isContainPlayer = true;
                isContainActor = false;
            }
            else if (selectObjectType == SelectObjectType.HostileButNotPlayers)
            {
                isContainPlayer = false;
                isContainActor = true;
            }

            var seeUnits = curUnit.GetComponent<AOIEntity>().GetSeeUnits();
            foreach (var seeUnit in seeUnits)
            {
                AOIEntity aoiEntityTmp = seeUnit.Value;
                Unit unit = aoiEntityTmp.Unit;
                bool isContinue = false;
                if (UnitHelper.ChkIsPlayer(unit) && isContainPlayer)
                {
                    isContinue = true;
                }
                else if (UnitHelper.ChkIsActor(unit) && isContainActor)
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

                if (UnitHelper.ChkUnitAlive(unit) == false)
                {
                    continue;
                }
                bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit);
                if (isFriend)
                {
                    continue;
                }

                if (isNeedChkCanBeFind)
                {
                    bool isBeFind = ET.Ability.BuffHelper.ChkCanBeFind(unit, curUnit);
                    if (isBeFind == false)
                    {
                        continue;
                    }
                }

                hostileForces.Add(unit);
            }

            return hostileForces;
        }

        public static bool ChkCanAttack(Unit curUnit, Unit targetUnit, float radius, bool ignoreY = true)
        {
            if (ChkIsNear(curUnit, targetUnit, radius, ignoreY))
            {
                return true;
            }

            return false;
        }

        public static (bool, float3) ChkHitMesh(Unit curUnit, float3 curUnitPos, float curUnitAttackPoint, Unit targetUnit)
        {
            if (IsNeedChkMesh(curUnit) == false)
            {
                return (false, float3.zero);
            }
            float targetUnitHeight = ET.Ability.UnitHelper.GetBodyHeight(targetUnit);
            float3 startPos = curUnitPos + new float3(0, curUnitAttackPoint, 0);
            float3 endPos = targetUnit.Position + new float3(0, targetUnitHeight * 0.5f, 0);
            (bool bHitMesh, float3 hitPos) = RecastHelper.ChkHitMesh(targetUnit.DomainScene(), startPos, endPos);
            if (bHitMesh)
            {
                return (true, hitPos);
            }

            return (false, float3.zero);
        }

        public static (bool, float3) ChkHitMesh(Unit curUnit, Unit targetUnit)
        {
            if (IsNeedChkMesh(curUnit) == false)
            {
                return (false, float3.zero);
            }
            float curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointHeight(curUnit);
            float targetUnitHeight = ET.Ability.UnitHelper.GetBodyHeight(targetUnit);
            float3 startPos = curUnit.Position + new float3(0, curUnitAttackPoint, 0);
            float3 endPos = targetUnit.Position + new float3(0, targetUnitHeight * 0.5f, 0);
            (bool bHitMesh, float3 hitPos) = RecastHelper.ChkHitMesh(curUnit.DomainScene(), startPos, endPos);
            if (bHitMesh)
            {
                return (true, hitPos);
            }

            return (false, float3.zero);
        }

        public static bool IsNeedChkMesh(Unit curUnit)
        {
            if (UnitHelper.ChkIsBullet(curUnit))
            {
                BulletObj bulletObj = curUnit.GetComponent<BulletObj>();
                Unit casterPlayerUnit = bulletObj?.GetCasterActorUnit();
                if (casterPlayerUnit != null && casterPlayerUnit.model.IsNeedChkMesh == false)
                {
                    return false;
                }
                return bulletObj.model.IsNeedChkMesh;
            }
            return curUnit.model.IsNeedChkMesh;
        }

        public static bool ChkCanMove(Unit unit)
        {
            if (UnitHelper.ChkIsBullet(unit))
            {
                return true;
            }

            string moveTimelineId = "";
            float moveSpeed = 0;
            if (UnitHelper.ChkIsAoe(unit))
            {
                moveTimelineId = unit.GetComponent<AoeObj>().model.MoveTimelineId;
                moveSpeed = unit.GetComponent<AoeObj>().model.MoveSpeed;
            }
            else
            {
                moveTimelineId = unit.model.MoveTimelineId;
                moveSpeed = unit.model.MoveSpeed;
            }

            if (string.IsNullOrEmpty(moveTimelineId))
            {
                return false;
            }

            if (moveSpeed <= 0)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsNear(Unit curUnit, Unit targetUnit, float radius, bool ignoreY)
        {
            float targetUnitRadius = ET.Ability.UnitHelper.GetBodyRadius(targetUnit);
            return ChkIsNear(curUnit, targetUnit.Position, targetUnitRadius, radius, ignoreY);
        }

        public static bool ChkIsNear(Unit curUnit, float3 targetPos, float targetUnitRadius, float radius, bool ignoreY)
        {
            float curUnitRadius = ET.Ability.UnitHelper.GetBodyRadius(curUnit);
            float curUnitHeight = ET.Ability.UnitHelper.GetBodyHeight(curUnit);
            float3 dis = curUnit.Position - targetPos;
            float targetDisSq = math.pow(radius + curUnitRadius + targetUnitRadius, 2);
            if (ignoreY)
            {
                if (math.pow(dis.y, 2) > math.max(targetDisSq, math.pow(curUnitHeight*0.8f, 2)))
                {
                    return false;
                }
                if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= targetDisSq)
                {
                    return true;
                }
            }
            else
            {
                if (dis.y + math.max(curUnitHeight, targetUnitRadius*0.5f) < 0)
                {
                    targetDisSq = math.pow(radius + curUnitRadius, 2);
                    if (math.lengthsq(dis) <= targetDisSq)
                    {
                        return true;
                    }
                }
                else
                {
                    if (math.lengthsq(dis) <= targetDisSq)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void AddWaitRemove(Unit unit)
        {
            UnitComponent unitComponent = GetUnitComponent(unit);
            unitComponent.AddWaitRemove(unit);
        }

        private static float2 PointRotate(float2 center, float2 p1, float angle)
        {
            float angleHude = angle * math.PI / 180;
            /*角度变成弧度*/
            float x1 = (p1.x - center.x) * math.cos(angleHude) + (p1.y - center.y) * math.sin(angleHude) + center.x;
            float y1 = -(p1.x - center.x) * math.sin(angleHude) + (p1.y - center.y) * math.cos(angleHude) + center.y;
            return new float2(x1, y1);
        }

        public static (float3, float3) GetNewNodePosition(Unit unit, OffSetInfo offSetInfo)
        {
            string nodeName = "";
            Vector3 offSetPosition = Vector3.Zero;
            Vector3 relateForward = Vector3.Zero;
            if (offSetInfo != null)
            {
                nodeName = offSetInfo.NodeName;
                offSetPosition = offSetInfo.OffSetPosition;
                relateForward = offSetInfo.RelateForward;
            }

            float3 offSetPos = new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            offSetPos *= Ability.UnitHelper.GetResScale(unit);
            var t1 = math.rotate(unit.Rotation, offSetPos);
            float3 newPosition = unit.Position + t1;
            float3 newForward = unit.Forward + new float3(relateForward.X, relateForward.Y, relateForward.Z);
            return (newPosition, newForward);
        }

        public static float3 GetNewNodePosition(Unit unit, float3 resetPos, OffSetInfo offSetInfo)
        {
            string nodeName = "";
            Vector3 offSetPosition = Vector3.Zero;
            Vector3 relateForward = Vector3.Zero;
            if (offSetInfo != null)
            {
                nodeName = offSetInfo.NodeName;
                offSetPosition = offSetInfo.OffSetPosition;
                relateForward = offSetInfo.RelateForward;
            }

            float3 offSetPos = new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            offSetPos *= Ability.UnitHelper.GetResScale(unit);
            var t1 = math.rotate(unit.Rotation, offSetPos);
            float3 newPosition = resetPos + t1;
            return newPosition;
        }

        public static void AddSyncNoticeUnitAdd(Unit beNoticeUnit, Unit unit)
        {
            GetUnitComponent(beNoticeUnit).AddSyncNoticeUnitAdd(beNoticeUnit, unit);
        }

        public static void AddSyncNoticeUnitRemove(Unit beNoticeUnit, long unitId)
        {
            GetUnitComponent(beNoticeUnit).AddSyncNoticeUnitRemove(beNoticeUnit, unitId);
        }

        public static void AddSyncPosUnit(Unit unit)
        {
            GetUnitComponent(unit).AddSyncPosUnit(unit);
        }

        public static void AddSyncNumericUnit(Unit unit)
        {
            GetUnitComponent(unit).AddSyncNumericUnit(unit);
        }

        public static void AddSyncNumericUnitByKey(Unit unit, int numericKey)
        {
            GetUnitComponent(unit).AddSyncNumericUnitByKey(unit, numericKey);
        }

        public static void AddRecycleSelectHandles(Scene scene, SelectHandle selectHandle)
        {
            {
                return;
            }
            //GetRecycleSelectHandleComponent(scene).AddRecycleSelectHandles(selectHandle);
        }

        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new ();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.CfgId;
            unitInfo.Level = unit.level;
            unitInfo.Type = (int)unit.Type;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            // MoveByPathComponent moveByPathComponent = unit.GetComponent<MoveByPathComponent>();
            // if (moveByPathComponent != null)
            // {
            //     if (!moveByPathComponent.IsArrived())
            //     {
            //         unitInfo.MoveInfo = new MoveInfo() { Points = ListComponent<float3>.Create() };
            //         unitInfo.MoveInfo.Points.Add(unit.Position);
            //         for (int i = moveByPathComponent.N; i < moveByPathComponent.Targets.Count; ++i)
            //         {
            //             float3 pos = moveByPathComponent.Targets[i];
            //             unitInfo.MoveInfo.Points.Add(pos);
            //         }
            //     }
            // }

            unitInfo.KV = new Dictionary<int, long>();

            NumericComponent nc = unit.GetComponent<NumericComponent>();
            if (nc != null && nc.NumericDic != null)
            {
                foreach ((int key, long value) in nc.NumericDic)
                {
                    unitInfo.KV.Add(key, value);
                }
            }

            unitInfo.Components = new();
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
                foreach (Entity entity in effectComponent.Children.Values)
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
            unitInfo.PositionX = (int)(unit.Position.x * 100);
            unitInfo.PositionY = (int)(unit.Position.y * 100);
            unitInfo.PositionZ = (int)(unit.Position.z * 100);
            unitInfo.ForwardX = (int)(unit.Forward.x * 100);
            unitInfo.ForwardY = (int)(unit.Forward.y * 100);
            unitInfo.ForwardZ = (int)(unit.Forward.z * 100);

            return unitInfo;
        }

        public static UnitNumericInfo SyncNumericUnitInfo(Unit unit)
        {
            UnitNumericInfo unitInfo = new ();
            unitInfo.UnitId = unit.Id;
            unitInfo.KV = new Dictionary<int, long>();
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            foreach ((int key, long value) in numericComponent.NumericDic)
            {
                unitInfo.KV.Add(key, value);
            }

            return unitInfo;
        }

        public static UnitNumericInfo SyncNumericUnitInfoKey(Unit unit, List<int> keys)
        {
            UnitNumericInfo unitInfo = new ();
            unitInfo.UnitId = unit.Id;
            unitInfo.KV = new Dictionary<int, long>();
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            for (int i = 0; i < keys.Count; i++)
            {
                int numericKey = keys[i];
                if (numericComponent.NumericDic.TryGetValue(numericKey, out long numericValue))
                {
                    unitInfo.KV.Add(numericKey, numericValue);
                }
            }

            return unitInfo;
        }

        public static float GetTargetUnitRadian(Unit curUnit, Unit targetUnit)
        {
            float3 targetPos = targetUnit.Position;
            return GetTargetPosRadian(curUnit, targetPos);
        }

        public static float GetTargetPosRadian(Unit curUnit, float3 targetPos)
        {
            float3 targetDir = math.normalize(targetPos - curUnit.Position);
            return GetTargetDirRadian(curUnit, targetDir);
        }

        /// <summary>
        /// 返回 夹角(弧度角)
        /// </summary>
        /// <param name="curUnit"></param>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        public static float GetTargetDirRadian(Unit curUnit, float3 targetDir)
        {
            float3 forward = math.normalize(curUnit.Forward);
            targetDir.y = 0;
            targetDir = math.normalize(targetDir);
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

        public static void SaveSelectHandle(Unit curUnit, SelectHandle selectHandle, bool isOnce)
        {
            SelectHandleObj selectHandleObj = curUnit.GetComponent<SelectHandleObj>();
            if (selectHandleObj == null)
            {
                selectHandleObj = curUnit.AddComponent<SelectHandleObj>();
            }

            selectHandleObj.SaveSelectHandle(selectHandle, isOnce);
        }

        public static void ClearOnceSelectHandle(Unit curUnit)
        {
            SelectHandleObj selectHandleObj = curUnit.GetComponent<SelectHandleObj>();
            if (selectHandleObj == null)
            {
                return;
            }
            selectHandleObj.ClearOnceSelectHandle();
        }

        public static SelectHandle GetSaveSelectHandle(Unit curUnit)
        {
            SelectHandleObj selectHandleObj = curUnit.GetComponent<SelectHandleObj>();
            return selectHandleObj?.GetSaveSelectHandle();
        }

        public static void ResetPos(Unit unit, float3 resetPos)
        {
            unit.Position = resetPos;

            ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            pathfindingComponent?.ResetPos(resetPos);
        }

        public static float GetResScale(Unit unit)
        {
            float resScale = 1;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                resScale = bulletCfg.ResScale;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                resScale = unit.model.ResScale;
            }

            return resScale;
        }

        public static float GetBodyRadius(Unit unit)
        {
            float bodyRadius = 0.1f;
            float resScale = 1;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                bodyRadius = bulletCfg.BodyRadius;
                resScale = bulletCfg.ResScale;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                bodyRadius = aoeCfg.BodyRadius;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                bodyRadius = unit.model.BodyRadius;
                resScale = unit.model.ResScale;
            }

            return bodyRadius * resScale;
        }

        public static float GetBodyRadius(string unitCfgId, bool isBullet, bool isAoe)
        {
            float bodyRadius = 0.1f;
            float resScale = 1;
            if (isBullet)
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                bodyRadius = bulletCfg.BodyRadius;
                resScale = bulletCfg.ResScale;
            }
            else if (isAoe)
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                bodyRadius = aoeCfg.BodyRadius;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                bodyRadius = unitCfg.BodyRadius;
                resScale = unitCfg.ResScale;
            }

            return bodyRadius * resScale;
        }

        public static float GetBodyHeight(Unit unit)
        {
            float bodyHeight = 0.1f;
            float resScale = 1;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                bodyHeight = bulletCfg.BodyHeight;
                resScale = bulletCfg.ResScale;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                bodyHeight = aoeCfg.BodyHeight;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                bodyHeight = unit.model.BodyHeight;
                resScale = unit.model.ResScale;
            }

            return bodyHeight * resScale;
        }

        public static float GetBodyHeight(string unitCfgId, bool isBullet, bool isAoe)
        {
            float bodyHeight = 0.1f;
            float resScale = 1;
            if (isBullet)
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                bodyHeight = bulletCfg.BodyHeight;
                resScale = bulletCfg.ResScale;
            }
            else if (isAoe)
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                bodyHeight = aoeCfg.BodyHeight;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                bodyHeight = unitCfg.BodyHeight;
                resScale = unitCfg.ResScale;
            }

            return bodyHeight * resScale;
        }

        public static float GetAttackPointHeight(Unit unit)
        {
            float attackPointHeight = 0.1f;
            float resScale = 1;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                attackPointHeight = bulletCfg.BodyHeight;
                resScale = bulletCfg.ResScale;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                attackPointHeight = aoeCfg.BodyHeight;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                attackPointHeight = unit.model.AttackPointHeight;
                resScale = unit.model.ResScale;
            }

            return attackPointHeight * resScale;
        }

        public static float GetAttackPointHeight(string unitCfgId, bool isBullet, bool isAoe)
        {
            float attackPointHeight = 0.1f;
            float resScale = 1;
            if (isBullet)
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                attackPointHeight = bulletCfg.BodyHeight;
                resScale = bulletCfg.ResScale;
            }
            else if (isAoe)
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                attackPointHeight = aoeCfg.BodyHeight;
                resScale = aoeCfg.ResScale;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                attackPointHeight = unitCfg.AttackPointHeight;
                resScale = unitCfg.ResScale;
            }

            return attackPointHeight * resScale;
        }

        public static float GetMoveSpeed(Unit unit)
        {
            if (unit == null)
            {
                return 0;
            }

            if (unit.IsDisposed)
            {
                return 0;
            }

            if (ChkIsBullet(unit) || ChkIsAoe(unit))
            {
                if (unit.GetComponent<MoveTweenObj>() != null)
                {
                    return unit.GetComponent<MoveTweenObj>().speed;
                }

                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                float speed = numericComponent.GetAsFloat(NumericType.Speed);
                return speed;
            }
            else if (ChkIsSceneEffect(unit))
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                float speed = numericComponent.GetAsFloat(NumericType.Speed);
                return speed;
            }
            else
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                int curHp = numericComponent.GetAsInt(NumericType.Hp);
                if (curHp <= 0)
                {
                    return 0;
                }

                float speed = numericComponent.GetAsFloat(NumericType.Speed);
                return speed;
            }
        }

        public static float GetRotationSpeed(Unit unit)
        {
            if (unit == null)
            {
                return 0;
            }

            if (unit.IsDisposed)
            {
                return 0;
            }

            if (ChkIsBullet(unit))
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                float rotationSpeed = numericComponent.GetAsFloat(NumericType.RotationSpeed);
                return rotationSpeed;
            }
            else if (ChkIsSceneEffect(unit))
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                float speed = numericComponent.GetAsFloat(NumericType.RotationSpeed);
                return speed;
            }
            else
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                int curHp = numericComponent.GetAsInt(NumericType.Hp);
                if (curHp <= 0)
                {
                    return 0;
                }

                float rotationSpeed = numericComponent.GetAsFloat(NumericType.RotationSpeed);
                return rotationSpeed;
            }
        }

        public static string GetIdleTimeLineId(Unit unit)
        {
            string idleTimelineId = "";
            if (UnitHelper.ChkIsBullet(unit))
            {
                idleTimelineId = unit.GetComponent<BulletObj>().model.IdleTimelineId;
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                idleTimelineId = unit.GetComponent<AoeObj>().model.IdleTimelineId;
            }
            else
            {
                idleTimelineId = unit.model.IdleTimelineId;
            }

            return idleTimelineId;
        }

        public static string GetMoveTimeLineId(Unit unit)
        {
            string moveTimelineId = "";
            if (UnitHelper.ChkIsBullet(unit))
            {
                moveTimelineId = "";
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                moveTimelineId = unit.GetComponent<AoeObj>().model.MoveTimelineId;
            }
            else
            {
                moveTimelineId = unit.model.MoveTimelineId;
            }

            return moveTimelineId;
        }

        /// <summary>
        /// 发送的Actor，例如 B发射的子弹，则 子弹通过这个接口可以找到B (A 召唤了 B， B发射的子弹，则 子弹通过这个接口可以找到B)
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Unit GetCasterUnit(Unit unit)
        {
            Unit casterPlayerUnit = null;
            if (UnitHelper.ChkIsBullet(unit))
            {
                BulletObj bulletObj = unit.GetComponent<BulletObj>();
                if (bulletObj == null)
                {
#if UNITY_EDITOR
                    Log.Error($"bulletObj == null");
#endif
                }
                else
                {
                    casterPlayerUnit = bulletObj.GetCasterActorUnit();
                }
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                AoeObj aoeObj = unit.GetComponent<AoeObj>();
                if (aoeObj == null)
                {
#if UNITY_EDITOR
                    Log.Error($"aoeObj == null");
#endif
                }
                else
                {
                    casterPlayerUnit = aoeObj.GetCasterActorUnit();
                }
            }
            else
            {
                casterPlayerUnit = unit;
            }

            return casterPlayerUnit;
        }

        /// <summary>
        /// 最开始的Actor，例如 A 召唤了 B， B发射的子弹，则 子弹通过这个接口可以找到A
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="casterUnitId"></param>
        /// <returns></returns>
        public static Unit GetCasterActorUnit(Scene scene, long casterUnitId)
        {
            Unit unit = UnitHelper.GetUnit(scene, casterUnitId);
            while (true)
            {
                Unit casterUnit = UnitHelper.GetCasterUnit(unit);
                if (casterUnit == null)
                {
                    break;
                }

                if (casterUnit == unit)
                {
                    break;
                }

                unit = casterUnit;
            }

            return unit;
        }

        public static Unit GetCasterActorUnit(Unit unit)
        {
            while (true)
            {
                Unit casterUnit = UnitHelper.GetCasterUnit(unit);
                if (casterUnit == null)
                {
                    break;
                }

                if (casterUnit == unit)
                {
                    break;
                }

                unit = casterUnit;
            }

            return unit;
        }

        public static ActionCfg_DeathShow GetDeathShow(Unit unit)
        {
            ActionCfg_DeathShow actionCfg_DeathShow;
            if (UnitHelper.ChkIsBullet(unit))
            {
                actionCfg_DeathShow = unit.GetComponent<BulletObj>().model.DeathShow_Ref;
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                actionCfg_DeathShow = unit.GetComponent<AoeObj>().model.DeathShow_Ref;
            }
            else
            {
                actionCfg_DeathShow = unit.model.DeathShow_Ref;
            }

            return actionCfg_DeathShow;
        }

        public static float GetMaxSkillDis(Unit unit, ET.AbilityConfig.SkillSlotType skillSlotType)
        {
            return SkillHelper.GetMaxSkillDis(unit, skillSlotType);
        }

        public static float GetMaxSkillDis(UnitCfg unitCfg, ET.AbilityConfig.SkillSlotType skillSlotType)
        {
            float dis = 0;
            foreach (var item in unitCfg.SkillList)
            {
                string skillCfgId = item.Key;
                SkillSlotType skillSlotTypeTmp = item.Value;
                if (skillSlotType != skillSlotTypeTmp)
                {
                    continue;
                }
                SkillCfg skillCfg = SkillCfgCategory.Instance.Get(skillCfgId);
                if (dis < skillCfg.Dis)
                {
                    dis = skillCfg.Dis;
                }
            }
            return dis;
        }
    }
}