using System;
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

        public static SyncDataManager GetSyncDataManagerComponent(Scene scene)
        {
            return scene.GetComponent<SyncDataManager>();
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
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);
                if (curHp > 0.1f)
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

        public static bool ChkIsPlayer(UnitType unitType)
        {
            if (unitType == UnitType.PlayerUnit)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsCameraPlayer(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsCameraPlayer(unit);
        }

        public static bool ChkIsCameraPlayer(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.CameraPlayerUnit)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsCameraPlayer(UnitType unitType)
        {
            if (unitType == UnitType.CameraPlayerUnit)
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

        public static bool ChkIsBullet(string unitCfgId)
        {
            if (BulletCfgCategory.Instance.Contain(unitCfgId))
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

        public static bool ChkIsAoe(string unitCfgId)
        {
            if (AoeCfgCategory.Instance.Contain(unitCfgId))
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsSkillCaster(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (unit.Type == UnitType.SkillCasterUnit)
            {
                return true;
            }

            return false;
        }

        public static bool ChkIsSkillCaster(Scene scene, long unitId)
        {
            Unit unit = GetUnit(scene, unitId);
            return ChkIsSkillCaster(unit);
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

        public static bool ChkIsAttackTower(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (UnitHelper.ChkIsActor(unit) == false)
            {
                return false;
            }

            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent == null)
            {
                return false;
            }
            if (ItemHelper.ChkIsAttackTower(towerComponent.towerCfgId) == false)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsTrapTower(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (UnitHelper.ChkIsActor(unit) == false)
            {
                return false;
            }

            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent == null)
            {
                return false;
            }
            if (ItemHelper.ChkIsTrap(towerComponent.towerCfgId) == false)
            {
                return false;
            }

            return true;
        }

        public static bool ChkIsColliderTower(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }

            if (UnitHelper.ChkIsActor(unit) == false)
            {
                return false;
            }

            TowerComponent towerComponent = unit.GetComponent<TowerComponent>();
            if (towerComponent == null)
            {
                return false;
            }
            if (ItemHelper.ChkIsCollider(towerComponent.towerCfgId) == false)
            {
                return false;
            }

            return true;
        }

        public static List<Unit> GetUnitList(Unit curUnit, SelectObjectTeamFlagType selectObjectTeamFlagType, SelectObjectUnitTypeBase selectObjectUnitTypeBase, bool isNeedChkInvisible, bool isNeedChkFly, bool isNeedIgnoreCannotBeTargeted)
        {
            bool isContainHome = false;
            bool isContainSelf = false;
            bool isContainFriend = false;
            bool isContainHostile = false;
            SelectObjectUnitType selectObjectUnitType;
            switch (selectObjectUnitTypeBase)
            {
                case SelectObjectUnitTypeSelf selectObjectUnitTypeSelf:
                {
                    isContainHome = false;
                    isContainSelf = true;
                    isContainFriend = false;
                    isContainHostile = false;
                    selectObjectUnitType = selectObjectUnitTypeSelf.UnitType;
                    break;
                }
                case SelectObjectUnitTypeSelfAndFriend selectObjectUnitTypeSelfAndFriend:
                {
                    isContainHome = false;
                    isContainSelf = true;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = selectObjectUnitTypeSelfAndFriend.UnitType;
                    break;
                }
                case SelectObjectUnitTypeFriend selectObjectUnitTypeFriend:
                {
                    isContainHome = false;
                    isContainSelf = false;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = selectObjectUnitTypeFriend.UnitType;
                    break;
                }
                case SelectObjectUnitTypeHostile selectObjectUnitTypeHostile:
                {
                    isContainHome = false;
                    isContainSelf = false;
                    isContainFriend = false;
                    isContainHostile = true;
                    selectObjectUnitType = selectObjectUnitTypeHostile.UnitType;
                    break;
                }
                case SelectObjectUnitTypeSelfHome selectObjectUnitTypeSelfHome:
                {
                    isContainHome = true;
                    isContainSelf = true;
                    isContainFriend = false;
                    isContainHostile = false;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                case SelectObjectUnitTypeSelfAndFriendHome selectObjectUnitTypeSelfAndFriendHome:
                {
                    isContainHome = true;
                    isContainSelf = true;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                case SelectObjectUnitTypeFriendHome selectObjectUnitTypeFriendHome:
                {
                    isContainHome = true;
                    isContainSelf = false;
                    isContainFriend = true;
                    isContainHostile = false;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                case SelectObjectUnitTypeHostileHome selectObjectUnitTypeHostileHome:
                {
                    isContainHome = true;
                    isContainSelf = false;
                    isContainFriend = false;
                    isContainHostile = true;
                    selectObjectUnitType = SelectObjectUnitType.All;
                    break;
                }
                default:
                {
                    // isContainHome = false;
                    // isContainSelf = false;
                    // isContainFriend = false;
                    // isContainHostile = false;
                    // selectObjectUnitType = SelectObjectUnitType.All;
                    return null;
                }
            }

            List<Unit> unitList = ListComponent<Unit>.Create();

            if (isContainHome)
            {
                GamePlayComponent gamePlayComponent = GamePlayHelper.GetGamePlay(curUnit.DomainScene());
                if (gamePlayComponent == null)
                {
                    return null;
                }
                GamePlayTowerDefenseComponent gamePlayTowerDefenseComponent = GamePlayHelper.GetGamePlayTowerDefense(curUnit.DomainScene());
                if (gamePlayTowerDefenseComponent == null)
                {
                    return null;
                }
                PutHomeComponent putHomeComponent = gamePlayTowerDefenseComponent.GetComponent<PutHomeComponent>();
                if (putHomeComponent == null)
                {
                    return null;
                }
                Dictionary<TeamFlagType, long> homeUnitList = putHomeComponent.GetHomeUnitList();
                TeamFlagType teamFlagType = ET.Ability.TeamFlagHelper.GetTeamFlag(curUnit);
                foreach (var homeUnits in homeUnitList)
                {
                    TeamFlagType curHomeTeamFlagType = homeUnits.Key;
                    long curHomeUnitId = homeUnits.Value;
                    Unit curHomeUnit = UnitHelper.GetUnit(curUnit.DomainScene(), curHomeUnitId);
                    if (UnitHelper.ChkUnitAlive(curHomeUnit) == false)
                    {
                        continue;
                    }

                    bool bRet = ET.GamePlayHelper.ChkIsSelectObjectTeamFlagType(curUnit.DomainScene(), selectObjectTeamFlagType, curUnit, curHomeUnit);
                    if (bRet == false)
                    {
                        continue;
                    }

                    if (isContainHostile)
                    {
                        bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curHomeTeamFlagType);
                        if (isFriend)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (isContainSelf && isContainFriend)
                        {
                            bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curHomeTeamFlagType);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainSelf)
                        {
                            if (GamePlayHelper.GetHomeTeamFlagType(teamFlagType) != curHomeTeamFlagType)
                            {
                                continue;
                            }
                        }
                        else if (isContainFriend)
                        {
                            if (GamePlayHelper.GetHomeTeamFlagType(teamFlagType) == curHomeTeamFlagType)
                            {
                                continue;
                            }
                            bool isFriend = gamePlayComponent.ChkIsFriend(teamFlagType, curHomeTeamFlagType);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (isNeedIgnoreCannotBeTargeted == false)
                    {
                        bool isCannotBeTargeted = ET.Ability.BuffHelper.ChkCannotBeTargeted(curHomeUnit);
                        if (isCannotBeTargeted)
                        {
                            continue;
                        }
                    }
                    if (isNeedChkInvisible)
                    {
                        bool isBeFind = ET.Ability.BuffHelper.ChkCanBeSee(curHomeUnit, curUnit);
                        if (isBeFind == false)
                        {
                            continue;
                        }
                    }
                    if (isNeedChkFly)
                    {
                        bool isBeFind = ET.Ability.BuffHelper.ChkCanBeTouchWhenFly(curHomeUnit, curUnit);
                        if (isBeFind == false)
                        {
                            continue;
                        }
                    }

                    unitList.Add(curHomeUnit);
                }
            }
            else
            {
                var seeUnits = curUnit.GetComponent<AOIEntity>().GetSeeUnits();
                foreach (var seeUnit in seeUnits)
                {
                    AOIEntity aoiEntityTmp = seeUnit.Value;
                    Unit unit = aoiEntityTmp.Unit;
                    bool isContinue = false;
                    if (UnitHelper.ChkIsPlayer(unit))
                    {
                        if (selectObjectUnitType == SelectObjectUnitType.All || selectObjectUnitType == SelectObjectUnitType.OnlyPlayer)
                        {
                            isContinue = true;
                        }
                        else
                        {
                            isContinue = false;
                        }
                    }
                    else if (UnitHelper.ChkIsActor(unit))
                    {
                        if (selectObjectUnitType == SelectObjectUnitType.All || selectObjectUnitType == SelectObjectUnitType.NotPlayer)
                        {
                            isContinue = true;
                        }
                        else
                        {
                            isContinue = false;
                        }
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

                    bool bRet = ET.GamePlayHelper.ChkIsSelectObjectTeamFlagType(curUnit.DomainScene(), selectObjectTeamFlagType, curUnit, unit);
                    if (bRet == false)
                    {
                        continue;
                    }

                    if (isContainHostile)
                    {
                        bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit);
                        if (isFriend)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (isContainSelf && isContainFriend)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainSelf)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit, true);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else if (isContainFriend)
                        {
                            bool isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit, true);
                            if (isFriend)
                            {
                                continue;
                            }
                            isFriend = ET.GamePlayHelper.ChkIsFriend(curUnit, unit, false);
                            if (isFriend == false)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (isNeedIgnoreCannotBeTargeted == false)
                    {
                        bool isCannotBeTargeted = ET.Ability.BuffHelper.ChkCannotBeTargeted(unit);
                        if (isCannotBeTargeted)
                        {
                            continue;
                        }
                    }
                    if (isNeedChkInvisible)
                    {
                        bool isBeFind = ET.Ability.BuffHelper.ChkCanBeSee(unit, curUnit);
                        if (isBeFind == false)
                        {
                            continue;
                        }
                    }
                    if (isNeedChkFly)
                    {
                        bool isBeFind = ET.Ability.BuffHelper.ChkCanBeTouchWhenFly(unit, curUnit);
                        if (isBeFind == false)
                        {
                            continue;
                        }
                    }

                    unitList.Add(unit);
                }

            }
            return unitList;
        }

        public static (bool, float3) ChkHitMesh(Unit curUnit, float3 curUnitPos, float curUnitAttackPointHeight, Unit targetUnit)
        {
            if (IsNeedChkMesh(curUnit) == false)
            {
                return (false, float3.zero);
            }
            float targetUnitHeight = ET.Ability.UnitHelper.GetBodyHeight(targetUnit);
            float3 startPos = curUnitPos + new float3(0, curUnitAttackPointHeight, 0);
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
            float3 curUnitAttackPoint = ET.Ability.UnitHelper.GetAttackPointPos(curUnit);
            float3 targetUnitBeAttackPoint = ET.Ability.UnitHelper.GetBeAttackPointPos(targetUnit);
            float3 startPos = curUnitAttackPoint;
            float3 endPos = targetUnitBeAttackPoint;
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
                Unit casterPlayerUnit = curUnit.GetCasterNearActor();
                if (casterPlayerUnit != null && casterPlayerUnit.model.IsNeedChkMesh == false)
                {
                    return false;
                }
                return bulletObj.model.IsNeedChkMesh;
            }
            else if (ChkIsAoe(curUnit))
            {
                AoeObj aoeObj = curUnit.GetComponent<AoeObj>();
                Unit casterPlayerUnit = curUnit.GetCasterNearActor();
                if (casterPlayerUnit != null && casterPlayerUnit.model.IsNeedChkMesh == false)
                {
                    return false;
                }
                return aoeObj.model.IsNeedChkMesh;
            }
            return curUnit.model.IsNeedChkMesh;
        }

        public static bool ChkCanMove(Unit unit)
        {
            if (UnitHelper.ChkIsBullet(unit) || ChkIsAoe(unit))
            {
                return true;
            }

            (string moveTimelineId, float moveSpeed) = GetMoveTimeLineId(unit);

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

        public static bool ChkIsFly(Unit unit)
        {
            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return false;
            }
            if (gameObjectComponent.isFly == false)
            {
                return false;
            }
            // if (ET.Ability.BuffHelper.ChkIsFly(unit))
            // {
            // }
            return true;
        }

        public static bool ChkIsNearNoCurUnitRadius(Unit curUnit, Unit targetUnit, float radius, bool ignoreY)
        {
            return ChkIsNearNoCurUnitRadius(curUnit.Position, targetUnit.Position, radius, ignoreY);
        }

        public static bool ChkIsNearNoCurUnitRadius(float3 curUnitPos, float3 targetPos, float radius, bool ignoreY)
        {
            float3 dis = curUnitPos - targetPos;
            float targetDisSq = math.pow(radius, 2);
            if (ignoreY)
            {
                if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= targetDisSq)
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
            return false;
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
                // if (math.pow(dis.y, 2) > math.max(targetDisSq, math.pow(curUnitHeight*0.8f, 2)))
                // {
                //     return false;
                // }
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

        public static bool ChkIsStackedOnTop(float3 curUnitPos, float curUnitHeight, Unit targetUnit, float radius)
        {
            float resScale = ET.Ability.UnitHelper.GetGameResScale(targetUnit);
            float3 targetUnitPos = targetUnit.Position;
            float targetUnitHeight = ET.Ability.UnitHelper.GetBodyHeight(targetUnit);
            float3 dis = curUnitPos - targetUnitPos;
            float targetDisSq = math.pow(radius, 2);

            if (curUnitPos.y >= targetUnitPos.y)
            {
                return false;
            }
            if (curUnitPos.y + curUnitHeight + 0.2f * resScale < targetUnitPos.y)
            {
                return false;
            }

            if (math.pow(dis.x, 2) + math.pow(dis.z, 2) <= targetDisSq)
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
            EffectNodeName nodeName = EffectNodeName.Self;
            Vector3 offSetPosition = Vector3.Zero;
            Vector3 relateRotation = Vector3.Zero;
            if (offSetInfo != null)
            {
                nodeName = offSetInfo.NodeName;
                offSetPosition = offSetInfo.OffSetPosition;
                relateRotation = offSetInfo.RelateRotation;
                if (offSetInfo.KeepHorizontal)
                {
                    relateRotation.X = 0;
                    relateRotation.Z = 0;
                }
            }

            float3 offSetPos = new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            offSetPos *= Ability.UnitHelper.GetGameResScale(unit);

            float3 newPosition;
            float3 newForward;
            bool needDealTilted = false;
            float tiltedAngle = 0;
            if (nodeName == EffectNodeName.SelfTilted)
            {
                AttackTargetComponent attackTargetComponent = unit.GetComponent<AttackTargetComponent>();
                if (attackTargetComponent != null)
                {
                    needDealTilted = true;
                    tiltedAngle = attackTargetComponent.GetAttackTargetAngle();
                }
            }

            if (needDealTilted)
            {
                offSetPos -= new float3(0, UnitHelper.GetAttackPointHeight(unit), 0);

                var localRotation = MathHelper.AngleToQuaternion(tiltedAngle, new float3(1, 0, 0));
                var t = math.mul(unit.Rotation, localRotation);
                var t1 = math.rotate(t, offSetPos);
                newPosition = UnitHelper.GetAttackPointPos(unit) + t1;

                newForward = math.rotate(localRotation, unit.Forward);
            }
            else
            {
                var t1 = math.rotate(unit.Rotation, offSetPos);
                newPosition = unit.Position + t1;

                newForward = unit.Forward;
            }

            float3 relateRotationFloat3 = new float3(relateRotation.X, relateRotation.Y, relateRotation.Z);
            if (relateRotationFloat3.Equals(float3.zero) == false)
            {
                // 创建四元数表示旋转（绕 Y 轴旋转）
                quaternion rotation = quaternion.Euler(math.radians(relateRotationFloat3));

                // 使用四元数旋转 float3 向量
                newForward = math.mul(rotation, newForward);
            }

            return (newPosition, newForward);
        }

        public static (float3, float3) GetNewNodePosition(Unit unit, float3 resetPos, OffSetInfo offSetInfo)
        {
            EffectNodeName nodeName = EffectNodeName.Self;
            Vector3 offSetPosition = Vector3.Zero;
            Vector3 relateRotation = Vector3.Zero;
            if (offSetInfo != null)
            {
                nodeName = offSetInfo.NodeName;
                offSetPosition = offSetInfo.OffSetPosition;
                relateRotation = offSetInfo.RelateRotation;
                if (offSetInfo.KeepHorizontal)
                {
                    relateRotation.X = 0;
                    relateRotation.Z = 0;
                }
            }

            float3 offSetPos = new float3(offSetPosition.X, offSetPosition.Y, offSetPosition.Z);
            offSetPos *= Ability.UnitHelper.GetGameResScale(unit);

            float3 newPosition;
            float3 newForward;
            bool needDealTilted = false;
            float tiltedAngle = 0;
            if (nodeName == EffectNodeName.SelfTilted)
            {
                AttackTargetComponent attackTargetComponent = unit.GetComponent<AttackTargetComponent>();
                if (attackTargetComponent != null)
                {
                    needDealTilted = true;
                    tiltedAngle = attackTargetComponent.GetAttackTargetAngle();
                }
            }

            if (needDealTilted)
            {
                offSetPos -= new float3(0, UnitHelper.GetAttackPointHeight(unit), 0);

                var localRotation = MathHelper.AngleToQuaternion(tiltedAngle, new float3(1, 0, 0));
                var t = math.mul(unit.Rotation, localRotation);
                var t1 = math.rotate(t, offSetPos);
                newPosition = new float3(0, UnitHelper.GetAttackPointHeight(unit), 0) + resetPos + t1;

                newForward = math.rotate(localRotation, unit.Forward);
            }
            else
            {
                var t1 = math.rotate(unit.Rotation, offSetPos);
                newPosition = resetPos + t1;

                newForward = unit.Forward;
            }

            float3 relateRotationFloat3 = new float3(relateRotation.X, relateRotation.Y, relateRotation.Z);
            if (relateRotationFloat3.Equals(float3.zero) == false)
            {
                // 创建四元数表示旋转（绕 Y 轴旋转）
                quaternion rotation = quaternion.Euler(math.radians(relateRotationFloat3));

                // 使用四元数旋转 float3 向量
                newForward = math.mul(rotation, newForward);
            }

            return (newPosition, newForward);
        }

        public static void AddSyncNoticeUnitAdd(Unit beNoticeUnit, Unit unit)
        {
            GetUnitComponent(beNoticeUnit).AddSyncNoticeUnitAdd(beNoticeUnit, unit);
        }

        public static void AddSyncNoticeUnitRemove(Unit beNoticeUnit, long unitId)
        {
            GetUnitComponent(beNoticeUnit).AddSyncNoticeUnitRemove(beNoticeUnit, unitId);
        }

        public static void AddSyncData_UnitBaseInfo(Unit unit)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitBaseInfo(unit);
        }

        public static void AddSyncData_UnitPosInfo(Unit unit)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitPosInfo(unit);
        }

        public static void AddSyncData_UnitNumericInfo(Unit unit)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitNumericInfo(unit);
        }

        public static void AddSyncData_UnitNumericInfoByKey(Unit unit, int numericKey)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitNumericInfo(unit, numericKey);
        }

        public static void AddSyncData_UnitPlayAudio(Unit unit, string playAudioActionId, bool isOnlySelfShow)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitPlayAudio(unit, playAudioActionId, isOnlySelfShow);
        }

        public static void AddSyncData_UnitFloatingText(Unit unit, string floatingTextId, int showNum, bool isOnlySelfShow)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitFloatingText(unit, floatingTextId, showNum, isOnlySelfShow);
        }

        public static void AddSyncData_UnitGetCoinShow(long playerId, Unit unit, CoinTypeInGame coinType, int chgValue)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitGetCoinShow(playerId, unit, coinType, chgValue);
        }

        public static void AddSyncData_DamageShow(Unit unit, int damageValue, bool isCrt)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_DamageShow(unit, damageValue, isCrt);
        }

        public static void AddSyncData_UnitComponent(Unit unit, System.Type type)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitComponent(unit, type);
        }

        public static void AddSyncData_UnitEffects(Unit unit, long effectObjId, bool isOnlySelfShow = false)
        {
            GetSyncDataManagerComponent(unit.DomainScene()).AddSyncData_UnitEffects(unit, effectObjId, isOnlySelfShow);
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
            unitInfo.Rotation = unit.Rotation;

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
                foreach (Entity entity in effectComponent.Children.Values)
                {
                    unitInfo.EffectComponents.Add(entity.ToBson());
                }
            }

            return unitInfo;
        }

        public static UnitBaseInfo CreateUnitBaseInfo(Unit unit)
        {
            UnitBaseInfo unitBaseInfo = new ();
            unitBaseInfo.UnitId = unit.Id;
            unitBaseInfo.ConfigId = unit.CfgId;
            unitBaseInfo.Level = unit.level;
            unitBaseInfo.Type = (int)unit.Type;
            unitBaseInfo.Position = unit.Position;
            unitBaseInfo.Rotation = unit.Rotation;

            return unitBaseInfo;
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

        public static float GetTargetUnitDisSqr(Unit curUnit, Unit targetUnit)
        {
            float3 curPos = curUnit.Position;
            float3 targetPos = targetUnit.Position;
            return math.lengthsq(targetPos - curPos);
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
            if (ET.Ability.SelectHandleHelper.ChkIsNullSelectHandle(selectHandle))
            {
                return;
            }
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
            if (selectHandleObj == null)
            {
                return null;
            }
            return selectHandleObj.GetSaveSelectHandle();
        }

        public static void SaveExcludeSelectHandle(Unit curUnit, SelectHandle selectHandle)
        {
            ExcludeSelectHandleObj excludeSelectHandleObj = curUnit.GetComponent<ExcludeSelectHandleObj>();
            if (excludeSelectHandleObj == null)
            {
                excludeSelectHandleObj = curUnit.AddComponent<ExcludeSelectHandleObj>();
            }

            excludeSelectHandleObj.SaveExcludeSelectHandle(selectHandle);
        }

        public static void ClearExcludeSelectHandle(Unit curUnit)
        {
            ExcludeSelectHandleObj excludeSelectHandleObj = curUnit.GetComponent<ExcludeSelectHandleObj>();
            if (excludeSelectHandleObj == null)
            {
                return;
            }
            excludeSelectHandleObj.ClearExcludeSelectHandle();
        }

        public static HashSet<long> GetSaveExcludeSelectHandle(Unit curUnit)
        {
            ExcludeSelectHandleObj excludeSelectHandleObj = curUnit.GetComponent<ExcludeSelectHandleObj>();
            if (excludeSelectHandleObj == null)
            {
                return null;
            }
            return excludeSelectHandleObj.GetSaveExcludeSelectHandle();
        }

        public static void ResetPos(Unit unit, float3 resetPos, float3 resetForward)
        {
            //Log.Error($"--zpb reset --{resetPos}");
            unit.Position = resetPos;
            if (resetForward.Equals(float3.zero) == false)
            {
                unit.Forward = math.normalize(resetForward);
            }

            ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();
            PathfindingComponent pathfindingComponent = unit.GetComponent<PathfindingComponent>();
            pathfindingComponent?.ResetPos(resetPos);
        }

        public static float GetUnitResScale(Unit unit)
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

        public static float GetUnitResScale(string unitCfgId)
        {
            float resScale = 1;
            if (ET.Ability.UnitHelper.ChkIsBullet(unitCfgId))
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                resScale = bulletCfg.ResScale;
            }
            else if (ET.Ability.UnitHelper.ChkIsAoe(unitCfgId))
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                resScale = aoeCfg.ResScale;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                resScale = unitCfg.ResScale;
            }

            return resScale;
        }

        public static float GetGameResScale(Unit unit)
        {
            float resScale = GetUnitResScale(unit);

            Scene scene = unit.DomainScene();
            if (scene == null && unit.Parent != null)
            {
                scene = unit.Parent.DomainScene();
            }
            if (scene != null)
            {
                float gameResScale = GetGameResScale(scene);
                resScale *= gameResScale;
            }

            return resScale;
        }

        public static float GetGameResScale(Scene scene, string unitCfgId)
        {
            float resScale = GetUnitResScale(unitCfgId);

            float gameResScale = GetGameResScale(scene);
            resScale *= gameResScale;

            return resScale;
        }

        public static float GetGameResScale(Scene scene)
        {
            float gameResScale = ET.GamePlayHelper.GetGameResScale(scene);
            return gameResScale;
        }

        public static float GetClientGameResScale(Scene scene)
        {
            float clientGameResScale = ET.GamePlayHelper.GetGameResScale(scene, true);
            return clientGameResScale;
        }

        public static float GetClientGameMapScale(Scene scene)
        {
            float clientGameMapScale = ET.GamePlayHelper.GetGameMapScale(scene, true);
            return clientGameMapScale;
        }

        public static float GetServerGameMapScale(Scene scene)
        {
            float serverGameMapScale = ET.GamePlayHelper.GetGameMapScale(scene, false);
            return serverGameMapScale;
        }

        public static float3 TranClientPos2ServerPos(Scene scene, float3 clientPos)
        {
            float3 serverPos = ET.GamePlayCoordinateConversionHelper.TranClientPos2ServerPos(scene, clientPos);
            return serverPos;
        }

        public static float3 TranClientForward2ServerForward(float3 clientForward)
        {
            float3 serverForward = ET.GamePlayCoordinateConversionHelper.TranClientForward2ServerForward(clientForward);
            return serverForward;
        }

        public static quaternion TranClientQuaternion2ServerQuaternion(quaternion clientQuaternion)
        {
            quaternion serverQuaternion = ET.GamePlayCoordinateConversionHelper.TranClientQuaternion2ServerQuaternion(clientQuaternion);
            return serverQuaternion;
        }

        public static float3 TranServerPos2ClientPos(Scene scene, float3 serverPos)
        {
            float3 clientPos = ET.GamePlayCoordinateConversionHelper.TranServerPos2ClientPos(scene, serverPos);
            return clientPos;
        }

        public static float3 TranServerForward2ClientForward(float3 serverForward)
        {
            float3 clientForward = ET.GamePlayCoordinateConversionHelper.TranServerForward2ClientForward(serverForward);
            return clientForward;
        }

        public static quaternion TranServerQuaternion2ClientQuaternion(quaternion serverQuaternion)
        {
            quaternion clientQuaternion = ET.GamePlayCoordinateConversionHelper.TranServerQuaternion2ClientQuaternion(serverQuaternion);
            return clientQuaternion;
        }

        public static string GetResName(Unit unit)
        {
            string resName;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                resName = bulletCfg.ResId_Ref.ResName;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                resName = aoeCfg.ResId_Ref.ResName;
            }
            else
            {
                resName = unit.model.ResId_Ref.ResName;
            }

            return resName;
        }

        public static string GetResName(string unitCfgId)
        {
            string resName;
            if (ET.Ability.UnitHelper.ChkIsBullet(unitCfgId))
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                resName = bulletCfg.ResId_Ref.ResName;
            }
            else if (ET.Ability.UnitHelper.ChkIsAoe(unitCfgId))
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                resName = aoeCfg.ResId_Ref.ResName;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                resName = unitCfg.ResId_Ref.ResName;
            }

            return resName;
        }

        public static float GetBodyRadius(Unit unit)
        {
            float bodyRadius = 0.1f;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                bodyRadius = bulletCfg.BodyRadius;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                bodyRadius = aoeCfg.BodyRadius;
            }
            else
            {
                bodyRadius = unit.model.BodyRadius;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(unit);

            return bodyRadius * resScale;
        }

        public static float GetBodyRadius(Scene scene, string unitCfgId)
        {
            float bodyRadius = 0.1f;
            if (ET.Ability.UnitHelper.ChkIsBullet(unitCfgId))
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                bodyRadius = bulletCfg.BodyRadius;
            }
            else if (ET.Ability.UnitHelper.ChkIsAoe(unitCfgId))
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                bodyRadius = aoeCfg.BodyRadius;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                bodyRadius = unitCfg.BodyRadius;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(scene, unitCfgId);

            return bodyRadius * resScale;
        }

        public static float GetBodyHeight(Unit unit)
        {
            float bodyHeight = 0.1f;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                bodyHeight = bulletCfg.BodyHeight;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                bodyHeight = aoeCfg.BodyHeight;
            }
            else
            {
                bodyHeight = unit.model.BodyHeight;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(unit);
            bodyHeight *= resScale;

            if (ET.Ability.UnitHelper.ChkIsFly(unit))
            {
                float gameResScale = GetGameResScale(unit.DomainScene());
                bodyHeight += GlobalSettingCfgCategory.Instance.FlyHeight * gameResScale;
            }

            return bodyHeight;
        }

        public static float GetBodyHeight(Scene scene, string unitCfgId)
        {
            float bodyHeight = 0.1f;
            if (ET.Ability.UnitHelper.ChkIsBullet(unitCfgId))
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                bodyHeight = bulletCfg.BodyHeight;
            }
            else if (ET.Ability.UnitHelper.ChkIsAoe(unitCfgId))
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                bodyHeight = aoeCfg.BodyHeight;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                bodyHeight = unitCfg.BodyHeight;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(scene, unitCfgId);

            return bodyHeight * resScale;
        }

        public static float GetAttackPointHeight(Unit unit)
        {
            float attackPointHeight = 0.1f;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                attackPointHeight = bulletCfg.BodyHeight;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                attackPointHeight = aoeCfg.BodyHeight;
            }
            else
            {
                attackPointHeight = unit.model.AttackPointHeight;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(unit);
            attackPointHeight *= resScale;

            if (ET.Ability.UnitHelper.ChkIsFly(unit))
            {
                float gameResScale = GetGameResScale(unit.DomainScene());
                attackPointHeight += GlobalSettingCfgCategory.Instance.FlyHeight * gameResScale;
            }

            return attackPointHeight;
        }

        public static float GetAttackPointHeight(Scene scene, string unitCfgId)
        {
            float attackPointHeight = 0.1f;
            if (ET.Ability.UnitHelper.ChkIsBullet(unitCfgId))
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                attackPointHeight = bulletCfg.BodyHeight;
            }
            else if (ET.Ability.UnitHelper.ChkIsAoe(unitCfgId))
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                attackPointHeight = aoeCfg.BodyHeight;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                attackPointHeight = unitCfg.AttackPointHeight;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(scene, unitCfgId);

            return attackPointHeight * resScale;
        }

        public static float3 GetAttackPointPos(Unit unit)
        {
            return unit.GetUnitClientPos() + new float3(0, UnitHelper.GetAttackPointHeight(unit), 0);
        }

        public static float3 GetBeAttackPointPos(Unit unit)
        {
            return unit.GetUnitClientPos() + new float3(0, UnitHelper.GetBeAttackPointHeight(unit), 0);
        }

        public static float GetBeAttackPointHeight(Unit unit)
        {
            float beAttackPointHeight = 0.1f;
            if (Ability.UnitHelper.ChkIsBullet(unit))
            {
                BulletCfg bulletCfg = unit.GetComponent<BulletObj>().model;
                beAttackPointHeight = bulletCfg.BodyHeight * 0.5f;
            }
            else if (Ability.UnitHelper.ChkIsAoe(unit))
            {
                AoeCfg aoeCfg = unit.GetComponent<AoeObj>().model;
                beAttackPointHeight = aoeCfg.BodyHeight * 0.5f;
            }
            else
            {
                beAttackPointHeight = unit.model.BeAttackPointHeight;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(unit);
            beAttackPointHeight *= resScale;

            if (ET.Ability.UnitHelper.ChkIsFly(unit))
            {
                float gameResScale = GetGameResScale(unit.DomainScene());
                beAttackPointHeight += GlobalSettingCfgCategory.Instance.FlyHeight * gameResScale;
            }

            return beAttackPointHeight;
        }

        public static float GetBeAttackPointHeight(Scene scene, string unitCfgId)
        {
            float beAttackPointHeight = 0.1f;
            if (ET.Ability.UnitHelper.ChkIsBullet(unitCfgId))
            {
                BulletCfg bulletCfg = BulletCfgCategory.Instance.Get(unitCfgId);
                beAttackPointHeight = bulletCfg.BodyHeight * 0.5f;
            }
            else if (ET.Ability.UnitHelper.ChkIsAoe(unitCfgId))
            {
                AoeCfg aoeCfg = AoeCfgCategory.Instance.Get(unitCfgId);
                beAttackPointHeight = aoeCfg.BodyHeight * 0.5f;
            }
            else
            {
                UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
                beAttackPointHeight = unitCfg.BeAttackPointHeight;
            }

            float resScale = ET.Ability.UnitHelper.GetGameResScale(scene, unitCfgId);

            return beAttackPointHeight * resScale;
        }

        public static bool ChkIsNavObstacle(string unitCfgId)
        {
            UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
            if (unitCfg != null && unitCfg.IsNavObstacle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static float GetNavObstacleRadius(Scene scene, string unitCfgId)
        {
            if (ChkIsNavObstacle(unitCfgId) == false)
            {
                return 0;
            }

            return GetBodyRadius(scene, unitCfgId);
        }

        public static float GetNavObstacleHeight(Scene scene, string unitCfgId)
        {
            if (ChkIsNavObstacle(unitCfgId) == false)
            {
                return 0;
            }

            return GetBodyHeight(scene, unitCfgId);
        }

        public static float GetNavObstacleRadius(Unit unit)
        {
            string unitCfgId = unit.CfgId;
            if (ChkIsNavObstacle(unitCfgId) == false)
            {
                return 0;
            }

            UnitResetNavObstacleComponent unitResetNavObstacleComponent = unit.GetComponent<UnitResetNavObstacleComponent>();
            if (unitResetNavObstacleComponent != null)
            {
                return unitResetNavObstacleComponent.resetNavObstacleRadius;
            }
            return GetNavObstacleRadius(unit.DomainScene(), unitCfgId);
        }

        public static float GetNavObstacleHeight(Unit unit)
        {
            string unitCfgId = unit.CfgId;
            if (ChkIsNavObstacle(unitCfgId) == false)
            {
                return 0;
            }

            UnitResetNavObstacleComponent unitResetNavObstacleComponent = unit.GetComponent<UnitResetNavObstacleComponent>();
            if (unitResetNavObstacleComponent != null)
            {
                return unitResetNavObstacleComponent.resetNavObstacleHeight;
            }
            float navObstacleHeight = GetNavObstacleHeight(unit.DomainScene(), unitCfgId);
            if (ET.Ability.UnitHelper.ChkIsFly(unit))
            {
                float gameResScale = GetGameResScale(unit.DomainScene());
                navObstacleHeight += GlobalSettingCfgCategory.Instance.FlyHeight * gameResScale;
            }
            return navObstacleHeight;
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

            float speed = 0;
            if (ChkIsBullet(unit) || ChkIsAoe(unit))
            {
                if (unit.GetComponent<MoveTweenObj>() != null)
                {
                    speed = unit.GetComponent<MoveTweenObj>().speed;
                    return speed;
                }
                else
                {
                    NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                    speed = numericComponent.GetAsFloat(NumericType.Speed);
                }
            }
            else if (ChkIsSceneEffect(unit))
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                speed = numericComponent.GetAsFloat(NumericType.Speed);
            }
            else
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);
                if (curHp <= 0)
                {
                    return 0;
                }

                speed = numericComponent.GetAsFloat(NumericType.Speed);
            }

            float gameResScale = ET.GamePlayHelper.GetGameResScale(unit.DomainScene());
            speed *= gameResScale;

            return speed;
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

            float rotationSpeed = 0;
            if (ChkIsBullet(unit))
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                rotationSpeed = numericComponent.GetAsFloat(NumericType.RotationSpeed);
            }
            else if (ChkIsSceneEffect(unit))
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                rotationSpeed = numericComponent.GetAsFloat(NumericType.RotationSpeed);
            }
            else
            {
                NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                float curHp = numericComponent.GetAsFloat(NumericType.Hp);
                if (curHp <= 0)
                {
                    return 0;
                }

                rotationSpeed = numericComponent.GetAsFloat(NumericType.RotationSpeed);
            }

            return rotationSpeed;
        }

        public static string GetIdleTimeLineId(Unit unit)
        {
            string idleTimelineId = "";
            if (UnitHelper.ChkIsBullet(unit))
            {
                BulletObj bulletObj = unit.GetComponent<BulletObj>();
                if (bulletObj != null)
                {
                    idleTimelineId = bulletObj.model.IdleTimelineId;
                }
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                AoeObj aoeObj = unit.GetComponent<AoeObj>();
                if (aoeObj != null)
                {
                    idleTimelineId = aoeObj.model.IdleTimelineId;
                }
            }
            else
            {
                idleTimelineId = unit.model.IdleTimelineId;
            }

            return idleTimelineId;
        }

        public static (string moveTimelineId, float moveSpeed) GetMoveTimeLineId(Unit unit)
        {
            string moveTimelineId = "";
            float moveSpeed = 0;
            if (UnitHelper.ChkIsBullet(unit))
            {
                moveTimelineId = "";
                moveSpeed = 0;
            }
            else if (UnitHelper.ChkIsAoe(unit))
            {
                moveTimelineId = unit.GetComponent<AoeObj>().model.MoveTimelineId;
                moveSpeed = unit.GetComponent<AoeObj>().model.MoveSpeed;
            }
            else
            {
                if (ChkIsFly(unit) && string.IsNullOrEmpty(unit.model.FlyTimelineId) == false)
                {
                    moveTimelineId = unit.model.FlyTimelineId;
                }
                else
                {
                    moveTimelineId = unit.model.MoveTimelineId;
                }
                moveSpeed = unit.model.MoveSpeed;
            }

            return (moveTimelineId, moveSpeed);
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

        public static float GetMaxSkillDis(Scene scene, UnitCfg unitCfg, ET.AbilityConfig.SkillSlotType skillSlotType)
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

            float resScale = ET.Ability.UnitHelper.GetGameResScale(scene);
            dis *= resScale;

            return dis;
        }

        public static int GetSkillCommonEnergyFullNum(Unit unit)
        {
            UnitCfg unitCfg = unit.model;
            if (unitCfg == null)
            {
                return 0;
            }

            var totalCommonEnergy = unitCfg.TotalCommonEnergy;

            NumericComponent numeric = unit.GetComponent<NumericComponent>();
            if (numeric == null)
            {
                return totalCommonEnergy;
            }

            long newTotalEnergyModifyBase = (long)(totalCommonEnergy * 10000);
            numeric.SetNoEvent(NumericType.TotalEnergyModifyBase, newTotalEnergyModifyBase);

            totalCommonEnergy = (int)numeric.GetAsFloat(NumericType.TotalEnergyModify);
            return totalCommonEnergy;
        }

        public static Unit GetOneObserverUnit(Scene scene)
        {
            Unit observerUnit = null;
            UnitComponent unitComponent = UnitHelper.GetUnitComponent(scene);
            var observerUnits = unitComponent?.GetRecordList(UnitType.ObserverUnit);
            if (observerUnits == null)
            {
                return null;
            }
            foreach (var _observerUnit in unitComponent.GetRecordList(UnitType.ObserverUnit))
            {
                observerUnit = _observerUnit;
                break;
            }

            return observerUnit;
        }

    }
}