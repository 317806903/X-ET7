using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (Unit))]
    [FriendOf(typeof (UnitComponent))]
    public static class UnitHelper_Create
    {
        public static UnitComponent GetUnitComponent(Unit unit)
        {
            return unit.DomainScene().GetComponent<UnitComponent>();
        }

        public static UnitComponent GetUnitComponent(Scene scene)
        {
            return scene.GetComponent<UnitComponent>();
        }

        public static Unit CreateWhenClient(UnitComponent unitComponent, UnitInfo unitInfo)
        {
            long id = unitInfo.UnitId;
            string unitCfgId = unitInfo.ConfigId;
            UnitType unitType = (UnitType)unitInfo.Type;
            Unit unit = unitComponent.AddChildWithId<Unit, string>(id, unitCfgId);
            unit.level = unitInfo.Level;
            unit.Type = unitType;
            unitComponent.Add(unit);
            return unit;
        }

        public static Unit CreateWhenServer_Common_Before(UnitComponent unitComponent, string unitCfgId, UnitType unitType, float3 position, float3 forward)
        {
            Unit unit = unitComponent.AddChild<Unit, string>(unitCfgId);
            //unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;

            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            if (unit.model != null)
            {
                numericComponent.SetAsFloat(NumericType.SpeedBase, unit.model.MoveSpeed);
                numericComponent.SetAsFloat(NumericType.RotationSpeedBase, unit.model.RotationSpeed);
            }

            unit.AddComponent<ET.Ability.TimelineComponent>();
            unit.AddComponent<ET.Ability.BuffComponent>();
            unit.AddComponent<ET.Ability.SkillComponent>();

            unit.AddComponent<ET.Ability.EffectComponent>();
            unit.AddComponent<ET.Ability.MoveComponent>();
            unit.AddComponent<ET.Ability.RotateComponent>();

            return unit;
        }

        public static void CreateWhenServer_Common_After(UnitComponent unitComponent, Unit unit)
        {
            unit.AddComponent<GameObjectComponent>();
            unitComponent.Add(unit);
            ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();
        }

        public static Unit CreateWhenServer_PlayerUnit(Scene scene, long playerId, int level, float3 position, float3 forward)
        {
            string unitCfgId = "Unit_PlayerPK";
            UnitType unitType = UnitType.PlayerUnit;

            UnitComponent unitComponent = GetUnitComponent(scene);

            Unit unit = unitComponent.AddChild<Unit, string>(unitCfgId);

            //unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;
            unit.level = level;

            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.SetAsFloat(NumericType.SpeedBase, unit.model.MoveSpeed);
            numericComponent.SetAsFloat(NumericType.RotationSpeedBase, unit.model.RotationSpeed);

            unit.AddComponent<ET.Ability.TimelineComponent>();
            unit.AddComponent<ET.Ability.BuffComponent>();
            unit.AddComponent<ET.Ability.SkillComponent>();

            unit.AddComponent<ET.Ability.EffectComponent>();
            unit.AddComponent<ET.Ability.MoveComponent>();
            unit.AddComponent<ET.Ability.RotateComponent>();

            PlayerUnitComponent playerUnitComponent = unit.AddComponent<ET.PlayerUnitComponent>();
            playerUnitComponent.playerId = playerId;

            SetUnitNumeric(numericComponent, unit.model.PropertyType, level);

            unit.AddComponent<GameObjectComponent>();
            unitComponent.Add(unit);
            ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();

            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);
            return unit;
        }

        public static Unit CreateWhenServer_CameraPlayerUnit(Scene scene, long playerId, int level, float3 position, float3 forward)
        {
            string unitCfgId = "Unit_CameraPlayer";
            UnitType unitType = UnitType.CameraPlayerUnit;

            UnitComponent unitComponent = GetUnitComponent(scene);

            Unit unit = unitComponent.AddChild<Unit, string>(unitCfgId);

            //unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;
            unit.level = level;

            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.SetAsFloat(NumericType.SpeedBase, unit.model.MoveSpeed);
            numericComponent.SetAsFloat(NumericType.RotationSpeedBase, unit.model.RotationSpeed);

            unit.AddComponent<ET.Ability.TimelineComponent>();
            unit.AddComponent<ET.Ability.BuffComponent>();
            unit.AddComponent<ET.Ability.SkillComponent>();

            unit.AddComponent<ET.Ability.EffectComponent>();
            unit.AddComponent<ET.Ability.MoveComponent>();
            unit.AddComponent<ET.Ability.RotateComponent>();

            CameraPlayerUnitComponent cameraPlayerUnitComponent = unit.AddComponent<ET.CameraPlayerUnitComponent>();
            cameraPlayerUnitComponent.playerId = playerId;

            SetUnitNumeric(numericComponent, unit.model.PropertyType, level);

            unitComponent.Add(unit);
            ET.Ability.MoveOrIdleHelper.DoIdle(unit).Coroutine();

            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);
            return unit;
        }

        public static Unit CreateWhenServer_ObserverUnit(Scene scene, long playerId, float3 position, float3 forward)
        {
            int level = 1;
            string unitCfgId = "Unit_Observer";
            UnitType unitType = UnitType.ObserverUnit;

            UnitComponent unitComponent = GetUnitComponent(scene);

            Unit unit = unitComponent.AddChildWithId<Unit, string>(playerId, unitCfgId);

            //unit.AddComponent<MoveByPathComponent>();
            unit.Position = position;
            unit.Forward = forward;
            unit.Type = unitType;
            unit.level = level;

            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.SetAsFloat(NumericType.SpeedBase, unit.model.MoveSpeed);
            numericComponent.SetAsFloat(NumericType.RotationSpeedBase, unit.model.RotationSpeed);

            SetUnitNumeric(numericComponent, unit.model.PropertyType, level);

            unit.AddComponent<GameObjectComponent>();
            unitComponent.Add(unit);

            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);
            return unit;
        }

        public static Unit CreateWhenServer_HomeUnit(Scene scene, string unitCfgId, float3 position, float3 forward, string aiCfg)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, unitCfgId, UnitType.PlayerUnit, position, forward);
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            SetUnitNumeric(numericComponent, unit.model.PropertyType, 1);

            if (string.IsNullOrEmpty(aiCfg) == false)
            {
                unit.AddComponent<AIComponent, string>(aiCfg);
            }

            CreateWhenServer_Common_After(unitComponent, unit);
            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);

            return unit;
        }

        /// <summary>
        /// 创建Actor
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unitCfgId"></param>
        /// <param name="level"></param>
        /// <param name="position"></param>
        /// <param name="forward"></param>
        /// <param name="aiCfg"></param>
        /// <param name="pathfindingMapName"></param>
        /// <returns></returns>
        public static Unit CreateWhenServer_ActorUnit(Scene scene, string unitCfgId, int level, float3 position, float3 forward, string aiCfg)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, unitCfgId, UnitType.ActorUnit, position, forward);
            unit.level = level;

            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            SetUnitNumeric(numericComponent, unit.model.PropertyType, level);

            if (string.IsNullOrEmpty(aiCfg) == false)
            {
                unit.AddComponent<AIComponent, string>(aiCfg);
            }
            CreateWhenServer_Common_After(unitComponent, unit);

            // UnitCfg unitCfg = UnitCfgCategory.Instance.Get(unitCfgId);
            // int count = unitCfg.SkillList.Count;
            // for (int i = 0; i < count; i++)
            // {
            //     SkillHelper.LearnSkill(unit, unitCfg.SkillList[i], 1, SkillSlotType.NormalAttack);
            // }
            //
            // float maxSkillDis = ET.Ability.SkillHelper.GetMaxSkillDis(unit, SkillSlotType.NormalAttack);
            // numericComponent.SetAsFloat(NumericType.SkillDisBase, maxSkillDis);

            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);

            return unit;
        }

        public static void ActorUnitLearnSkillWhenCreate(Unit unit)
        {
            UnitCfg unitCfg = unit.model;
            foreach (var item in unitCfg.SkillList)
            {
                string skillCfgId = item.Key;
                ET.AbilityConfig.SkillSlotType skillSlotType = item.Value;
                SkillHelper.LearnSkill(unit, skillCfgId, 1, skillSlotType);
            }

            float maxSkillDis = ET.Ability.SkillHelper.GetMaxSkillDis(unit, SkillSlotType.NormalAttack);
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            numericComponent.SetAsFloat(NumericType.SkillDisBase, maxSkillDis);
        }

        /// <summary>
        /// 召唤Actor
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unitCfgId"></param>
        /// <param name="level"></param>
        /// <param name="position"></param>
        /// <param name="forward"></param>
        /// <param name="aiCfg"></param>
        /// <param name="pathfindingMapName"></param>
        /// <returns></returns>
        public static Unit CreateWhenServer_CallActorUnit(Scene scene, Unit unitCaster, ActionCfg_CallActor actionCfgCallActor, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            (float3 newPosition, float3 newForward) = UnitHelper.GetNewNodePosition(unitCaster, actionCfgCallActor.OffSetInfo);

            int unitLevel = 0;
            string propertyType = "";
            BeCallActorAttrType beCallActorAttrType = actionCfgCallActor.BeCallActorAttrType;
            if (beCallActorAttrType is UseSelfAttrTypeAndCallerLevel useSelfAttrTypeAndCallerLevel)
            {
                unitLevel = unitCaster.level + useSelfAttrTypeAndCallerLevel.OffsetLevel;
                propertyType = UnitCfgCategory.Instance.Get(actionCfgCallActor.UnitId).PropertyType;
            }
            else if (beCallActorAttrType is UseSelfAttrTypeAndFixedLevel useSelfAttrTypeAndFixedLevel)
            {
                unitLevel = useSelfAttrTypeAndFixedLevel.FixedLevel;
                propertyType = UnitCfgCategory.Instance.Get(actionCfgCallActor.UnitId).PropertyType;
            }
            else if (beCallActorAttrType is UseCallerAttrTypeAndCallerLevel useCallerAttrTypeAndCallerLevel)
            {
                unitLevel = unitCaster.level + useCallerAttrTypeAndCallerLevel.OffsetLevel;
                propertyType = unitCaster.model.PropertyType;
            }
            else if (beCallActorAttrType is UseCallerAttrTypeAndFixedLevel useCallerAttrTypeAndFixedLevel)
            {
                unitLevel = useCallerAttrTypeAndFixedLevel.FixedLevel;
                propertyType = unitCaster.model.PropertyType;
            }
            else if (beCallActorAttrType is UseCallerCurAttr useCallerCurAttr)
            {
                unitLevel = unitCaster.level;
            }

            string aiCfgId = actionCfgCallActor.UnitAiCfg;
            if (string.IsNullOrEmpty(aiCfgId))
            {
                AIComponent aiComponent = unitCaster.GetComponent<AIComponent>();
                if (aiComponent != null)
                {
                    aiCfgId = aiComponent.GetAICfgId();
                }
            }

            Unit actorUnit = CreateWhenServer_ActorUnit(scene, actionCfgCallActor.UnitId, unitLevel, newPosition, newForward, aiCfgId);
            GamePlayHelper.AddUnitPathfinding(actorUnit);

            float3 hitPosition = ET.RecastHelper.GetNearNavmeshPos(actorUnit, actorUnit.Position);
            ET.Ability.UnitHelper.ResetPos(actorUnit, hitPosition, float3.zero);

            actorUnit.AddComponent<UnitWaitResetPosComponent, float3>(unitCaster.Position);

            if (beCallActorAttrType is UseCallerCurAttr)
            {
                NumericComponent numericComponentFrom = unitCaster.GetComponent<NumericComponent>();
                NumericComponent numericComponentTo = actorUnit.GetComponent<NumericComponent>();
                CopyUnitNumericWhenCallActor(numericComponentFrom, numericComponentTo);
            }
            else
            {
                NumericComponent numericComponent = actorUnit.GetComponent<NumericComponent>();
                SetUnitNumeric(numericComponent, propertyType, unitLevel);
            }

            ActorUnitLearnSkillWhenCreate(actorUnit);

            if (actionCfgCallActor.BeCallActorActionId.Count > 0)
            {
                SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(actorUnit);
                foreach (var actionId in actionCfgCallActor.BeCallActorActionId)
                {
                    ActionHandlerHelper.CreateAction(unitCaster, null, actionId, 0, selectHandleSelf, ref actionContext);
                }
            }
            return actorUnit;
        }

        public static Unit CreateWhenServer_Bullet(Scene scene, Unit unitCaster, ActionCfg_FireBullet actionCfgFireBullet, SelectHandle selectHandle,  ActionContext actionContext)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);

            float3 position = unitCaster.Position;
            float3 forward = unitCaster.Forward;
            //Unit bulletUnit = CreateWhenServer_Common_Before(unitComponent, "Unit_Bullet1", UnitType.Bullet, position, forward);
            Unit bulletUnit = CreateWhenServer_Common_Before(unitComponent, "", UnitType.Bullet, position, forward);

            BulletObj bulletObj = bulletUnit.AddComponent<BulletObj>();
            bulletObj.Init(unitCaster.Id, actionCfgFireBullet.BulletId, actionCfgFireBullet.Duration);
            bulletObj.InitActionContext(ref actionContext);

            NumericComponent numericComponentCaster = unitCaster.GetComponent<NumericComponent>();
            NumericComponent numericComponentBullet = bulletUnit.GetComponent<NumericComponent>();
            CopyUnitNumeric(numericComponentCaster, numericComponentBullet);
            numericComponentBullet.SetAsFloat(NumericType.RotationSpeedBase, bulletObj.model.RotationSpeed);

            float3 newPosition;
            float3 newForward;
            (newPosition, newForward) = UnitHelper.GetNewNodePosition(unitCaster, actionCfgFireBullet.OffSetInfo);

            bulletUnit.Position = newPosition;
            bulletUnit.Forward = newForward;

            MoveTweenHelper.CreateMoveTween(bulletUnit, actionCfgFireBullet.MoveTweenId, selectHandle);
            if (actionCfgFireBullet.MoveTweenId_Ref.MoveType is SpeedMoveTweenType speedMoveTweenType)
            {
                numericComponentBullet.SetAsFloat(NumericType.SpeedBase, speedMoveTweenType.Speed);
            }

            CreateWhenServer_Common_After(unitComponent, bulletUnit);
            // 加入aoi
            bulletUnit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, bulletUnit.Position);

            return bulletUnit;
        }

        public static Unit CreateWhenServer_Aoe(Scene scene, Unit unitCaster, ActionCfg_CallAoe actionCfgCallAoe, SelectHandle selectHandle,  ActionContext actionContext)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);

            float3 position = unitCaster.Position;
            float3 forward = unitCaster.Forward;
            //Unit bulletUnit = CreateWhenServer_Common_Before(unitComponent, "Unit_Bullet1", UnitType.Bullet, position, forward);
            Unit aoeUnit = CreateWhenServer_Common_Before(unitComponent, "", UnitType.Aoe, position, forward);

            AoeObj aoeObj = aoeUnit.AddComponent<AoeObj>();
            aoeObj.Init(unitCaster.Id, actionCfgCallAoe.AoeId, actionCfgCallAoe.Duration, actionCfgCallAoe.AoeTargetCondition);
            aoeObj.InitActionContext(ref actionContext);

            NumericComponent numericComponentCaster = unitCaster.GetComponent<NumericComponent>();
            NumericComponent numericComponentAoe = aoeUnit.GetComponent<NumericComponent>();
            CopyUnitNumeric(numericComponentCaster, numericComponentAoe);
            numericComponentAoe.SetAsFloat(NumericType.RotationSpeedBase, aoeObj.model.RotationSpeed);

            (float3 newPosition, float3 newForward) = UnitHelper.GetNewNodePosition(unitCaster, actionCfgCallAoe.OffSetInfo);
            aoeUnit.Position = newPosition;
            aoeUnit.Forward = newForward;

            MoveTweenHelper.CreateMoveTween(aoeUnit, actionCfgCallAoe.MoveTweenId, selectHandle);
            if (actionCfgCallAoe.MoveTweenId_Ref.MoveType is SpeedMoveTweenType speedMoveTweenType)
            {
                numericComponentAoe.SetAsFloat(NumericType.SpeedBase, speedMoveTweenType.Speed);
            }

            CreateWhenServer_Common_After(unitComponent, aoeUnit);
            // 加入aoi
            float radius = actionCfgCallAoe.AoeTargetCondition.Radius;
            if (radius == -1f)
            {
                radius = 200;
            }
            aoeUnit.AddComponent<AOIEntity, int, float3>((int)(radius * 1000), aoeUnit.Position);

            if (actionCfgCallAoe.BeCallActorActionId.Count > 0)
            {
                SelectHandle selectHandleSelf = SelectHandleHelper.CreateUnitSelfSelectHandle(aoeUnit);
                foreach (var actionId in actionCfgCallAoe.BeCallActorActionId)
                {
                    ActionHandlerHelper.CreateAction(unitCaster, null, actionId, 0, selectHandleSelf, ref actionContext);
                }
            }
            return aoeUnit;
        }

        public static Unit CreateWhenServer_SceneEffect(Scene scene, float3 position, float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, "Unit_SceneEffectNone", UnitType.SceneEffect, position, forward);
            CreateWhenServer_Common_After(unitComponent, unit);
            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);

            return unit;
        }

        public static Unit CreateWhenServer_NPC(Scene scene, string unitCfgId, float3 position, float3 forward)
        {
            UnitComponent unitComponent = GetUnitComponent(scene);
            Unit unit = CreateWhenServer_Common_Before(unitComponent, unitCfgId, UnitType.NPC, position, forward);
            CreateWhenServer_Common_After(unitComponent, unit);
            // 加入aoi
            unit.AddComponent<AOIEntity, int, float3>(ET.GamePlayHelper.GetAOIDis(scene) * 1000, unit.Position);

            return unit;
        }

        public static void SetUnitNumeric(NumericComponent numericComponent, string propertyType, int unitLevel)
        {
            UnitPropertyCfg unitPropertyCfg = UnitPropertyCfgCategory.Instance.Get(propertyType, unitLevel);
            if (unitPropertyCfg == null)
            {
                if (UnitPropertyCfgCategory.Instance.Get(propertyType, 1) != null)
                {
                    Log.Error($"UnitHelper_Create.SetUnitNumeric unitPropertyCfg == null propertyType=[{propertyType}] unitLevel=[{unitLevel}] ForceSet unitLevel=1");
                    SetUnitNumeric(numericComponent, propertyType, 1);
                }
                else
                {
                    Log.Error($"UnitHelper_Create.SetUnitNumeric unitPropertyCfg == null propertyType=[{propertyType}] unitLevel=[{unitLevel}]");
                }
                return;
            }
            numericComponent.SetAsInt(NumericType.MaxHpBase, unitPropertyCfg.HpBase);
            numericComponent.SetAsInt(NumericType.HpBase, unitPropertyCfg.HpBase);
            numericComponent.SetAsInt(NumericType.PhysicalAttackBase, unitPropertyCfg.PhysicalAttackBase);
            numericComponent.SetAsInt(NumericType.CriticalHitDamageBase, unitPropertyCfg.CriticalHitDamageBase);
            numericComponent.SetAsInt(NumericType.CriticalStrikeRateBase, unitPropertyCfg.CriticalStrikeRateBase);
            numericComponent.SetAsInt(NumericType.DamageDeepeningBase, unitPropertyCfg.DamageDeepeningBase);
            numericComponent.SetAsInt(NumericType.DamageReliefBase, unitPropertyCfg.DamageReliefBase);
        }

        public static void CopyUnitNumericWhenCallActor(NumericComponent numericComponentFrom, NumericComponent numericComponentTo)
        {
            foreach (var numeric in numericComponentFrom.NumericDic)
            {
                numericComponentTo.SetAsLong(numeric.Key, numeric.Value);
            }
            numericComponentTo.SetHpFull();
        }

        public static void CopyUnitNumeric(NumericComponent numericComponentFrom, NumericComponent numericComponentTo)
        {
            foreach (var numeric in numericComponentFrom.NumericDic)
            {
                numericComponentTo.SetAsLong(numeric.Key, numeric.Value);
            }
        }
    }
}