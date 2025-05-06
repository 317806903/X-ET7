using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class BuffHelper
    {
        public static BuffComponent _GetBuffComponent(Unit unit)
        {
            if (unit == null)
            {
                return null;
            }
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent;
        }

        public static bool ChkIsModifyMotionBuff(ActionCfg_BuffAdd actionCfgAddBuff)
        {
            foreach (var item in actionCfgAddBuff.BuffList)
            {
                foreach (BuffAction buffAction in item.BuffActions)
                {
                    if (buffAction is BuffActionModifyMotion buffActionModifyMotion)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void AddBuff(Unit casterUnit, ActionCfg_BuffAdd actionCfgAddBuff, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                bool isModifyMotionBuff = ET.Ability.BuffHelper.ChkIsModifyMotionBuff(actionCfgAddBuff);

                int count = selectHandle.unitIds.Count;
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitSelect = UnitHelper.GetUnit(casterUnit.DomainScene(), unitId);
                    if (unitSelect == null)
                    {
                        continue;
                    }
                    if (unitSelect.IsDisposed)
                    {
                        continue;
                    }

                    if (isModifyMotionBuff)
                    {
                        if (unitSelect.Id == actionContext.motionUnitId)
                        {
                            actionContext.motionDirection = unitSelect.Forward;
                        }
                        else
                        {
                            actionContext.motionDirection = actionContext.motionPosition - unitSelect.Position;
                        }
                    }

                    foreach (AddBuffInfo addBuffInfo in actionCfgAddBuff.BuffList)
                    {
                        AddBuff(casterUnit, unitSelect, addBuffInfo, ref actionContext);
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static void AddBuff(Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo, ref ActionContext actionContext)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                buffComponent = unit.AddComponent<BuffComponent>();
            }
            buffComponent.AddBuff(casterUnit, unit, addBuffInfo, actionContext).Coroutine();
        }

        public static void DealBuff(Unit casterUnit, ActionCfg_BuffDeal actionCfgBuffDeal, SelectHandle selectHandle, ref ActionContext actionContext)
        {
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                int count = selectHandle.unitIds.Count;
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitSelect = UnitHelper.GetUnit(casterUnit.DomainScene(), unitId);
                    if (unitSelect == null)
                    {
                        continue;
                    }
                    if (unitSelect.IsDisposed)
                    {
                        continue;
                    }
                    BuffComponent buffComponent = _GetBuffComponent(unitSelect);
                    if (buffComponent == null)
                    {
                        continue;
                    }
                    BuffDealSelectCondition buffDealSelectCondition = actionCfgBuffDeal.BuffDealSelectCondition;
                    List<BuffObj> list = GetBuffListByCondition(unitSelect, buffDealSelectCondition, ref actionContext);
                    if (list != null)
                    {
                        foreach (BuffObj buffObj in list)
                        {
                            DealBuffObj(buffObj, actionCfgBuffDeal, ref actionContext);
                        }
                    }
                }
            }
            else
            {
                Log.Error($"ET.Ability.BuffHelper.RemoveBuff selectHandle.selectHandleType != SelectHandleType.SelectUnits");
            }
        }

        public static List<BuffObj> GetBuffListByCondition(Unit unitSelect, BuffDealSelectCondition buffDealSelectCondition, ref ActionContext actionContext)
        {
            if (buffDealSelectCondition is CurBuff curBuff)
            {
                BuffObj buffObj = GetBuffObj(unitSelect, ref actionContext);
                if (buffObj == null)
                {
                    return null;
                }

                List<BuffObj> buffObjs = ListComponent<BuffObj>.Create();
                buffObjs.Add(buffObj);
                return buffObjs;
            }
            else if (buffDealSelectCondition is AllBuff allBuff)
            {
                return GetAllBuffList(unitSelect);
            }
            else if (buffDealSelectCondition is ByBuffCfgId byBuffCfgId)
            {
                return GetBuffListByBuffCfgId(unitSelect, byBuffCfgId.BuffCfgId);
            }
            else if (buffDealSelectCondition is ByBuffType byBuffType)
            {
                return GetBuffListByBuffType(unitSelect, byBuffType.BuffType);
            }
            else if (buffDealSelectCondition is ByBuffTagType byBuffTagType)
            {
                return GetBuffListByTagType(unitSelect, byBuffTagType.BuffTagType);
            }
            else if (buffDealSelectCondition is ByBuffTagGroupType byBuffTagGroupType)
            {
                return GetBuffListByTagGroupType(unitSelect, byBuffTagGroupType.BuffTagGroupType);
            }

            return null;
        }

        public static void DealBuffObj(BuffObj buffObj, ActionCfg_BuffDeal actionCfgBuffDeal, ref ActionContext actionContext)
        {
            if (buffObj == null)
            {
                return;
            }
            BuffDealType buffDealType = actionCfgBuffDeal.BuffDealType;
            if (buffDealType is BuffRemove buffRemove)
            {
                buffObj.ChgDuration(0);
            }
            else if (buffDealType is BuffStackCountChg buffStackCountChg)
            {
                buffObj.AddStackCount(buffStackCountChg.Op, buffStackCountChg.ChgStack);
            }
            else if (buffDealType is BuffLeftTimeChgByLeftTime buffLeftTimeChgByLeftTime)
            {
                if (buffObj.permanent)
                {
                    return;
                }
                if (buffObj.duration <= 0)
                {
                    return;
                }
                float newDuration = buffObj.duration * buffLeftTimeChgByLeftTime.LeftTimeScale + buffLeftTimeChgByLeftTime.StackCountScale * buffObj.orgDuration * (buffObj.stack - 1);
                buffObj.ChgDuration(newDuration);
            }
            else if (buffDealType is BuffLeftTimeChgByOrgTime buffLeftTimeChgByOrgTime)
            {
                if (buffObj.permanent)
                {
                    return;
                }
                if (buffObj.duration <= 0)
                {
                    return;
                }
                float newDuration = buffObj.orgDuration * buffLeftTimeChgByOrgTime.OrgTimeScale + buffLeftTimeChgByOrgTime.StackCountScale * buffObj.orgDuration * (buffObj.stack - 1);
                buffObj.ChgDuration(newDuration);
            }
            else if (buffDealType is BuffDurationChgByOrgTime buffDurationChgByOrgTime)
            {
                if (buffObj.permanent)
                {
                    return;
                }
                if (buffObj.duration <= 0)
                {
                    return;
                }
                float newTotalDuration = buffObj.orgDuration * buffDurationChgByOrgTime.OrgTimeScale + buffDurationChgByOrgTime.StackCountScale * buffObj.orgDuration * (buffObj.stack - 1);
                buffObj.ChgTotalDuration(newTotalDuration);
            }
            else if (buffDealType is BuffDurationChgByValue buffDurationChgByValue)
            {
                if (buffObj.permanent)
                {
                    return;
                }
                if (buffObj.duration <= 0)
                {
                    return;
                }
                float newTotalDuration = buffObj.orgDuration + buffDurationChgByValue.AddTime;
                buffObj.ChgTotalDuration(newTotalDuration);
            }
            else if (buffDealType is BuffDurationChgByPercentValue buffDurationChgByPercentValue)
            {
                if (buffObj.permanent)
                {
                    return;
                }
                if (buffObj.duration <= 0)
                {
                    return;
                }
                float newTotalDuration = buffObj.orgDuration * (100 + buffDurationChgByPercentValue.AddPercent) * 0.01f;
                buffObj.ChgTotalDuration(newTotalDuration);
            }
        }

        public static BuffObj GetBuffObj(Unit unit, ref ActionContext actionContext)
        {
            long buffId = actionContext.buffId;
            if (buffId == 0)
            {
                return null;
            }
            long buffUnitId = actionContext.buffUnitId;
            Unit buffUnit = UnitHelper.GetUnit(unit.DomainScene(), buffUnitId);
            if (UnitHelper.ChkUnitAlive(buffUnit) == false)
            {
                return null;
            }

            BuffObj buffObj = GetBuffObj(buffUnit, buffId);
            return buffObj;
        }

        public static BuffObj GetBuffObj(Unit unit, long buffId)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return null;
            }

            return buffComponent.GetChild<BuffObj>(buffId);
        }

        public static bool ChkBuffByCondition(Unit unitSelect, BuffDealSelectCondition buffDealSelectCondition)
        {
            if (buffDealSelectCondition is AllBuff allBuff)
            {
                List<BuffObj> buffList = GetAllBuffList(unitSelect);
                if (buffList != null)
                {
                    foreach (BuffObj buffObj in buffList)
                    {
                        if (buffObj.isEnabled && buffObj.stack > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else if (buffDealSelectCondition is ByBuffCfgId byBuffCfgId)
            {
                return ChkBuffByBuffCfgId(unitSelect, byBuffCfgId.BuffCfgId);
            }
            else if (buffDealSelectCondition is ByBuffType byBuffType)
            {
                return ChkBuffByBuffType(unitSelect, byBuffType.BuffType);
            }
            else if (buffDealSelectCondition is ByBuffTagType byBuffTagType)
            {
                return ChkBuffByTagType(unitSelect, byBuffTagType.BuffTagType);
            }
            else if (buffDealSelectCondition is ByBuffTagGroupType byBuffTagGroupType)
            {
                return ChkBuffByTagGroupType(unitSelect, byBuffTagGroupType.BuffTagGroupType);
            }

            return false;
        }

        public static bool ChkBuffByBuffCfgId(Unit unit, string buffCfgId)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return false;
            }
            return buffComponent.ChkBuffByBuffCfgId(buffCfgId);
        }

        public static bool ChkBuffByBuffType(Unit unit, BuffType buffType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return false;
            }
            return buffComponent.ChkBuffByBuffType(buffType);
        }

        public static bool ChkBuffByTagType(Unit unit, BuffTagType buffTagType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return false;
            }
            return buffComponent.ChkBuffByTagType(buffTagType);
        }

        public static bool ChkBuffByTagGroupType(Unit unit, BuffTagGroupType buffTagGroupType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return false;
            }
            return buffComponent.ChkBuffByTagGroupType(buffTagGroupType);
        }

        public static List<BuffObj> GetBuffListByBuffCfgId(Unit unit, string buffCfgId)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent.GetBuffListByBuffCfgId(buffCfgId);
        }

        public static List<BuffObj> GetAllBuffList(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent.GetAllBuffList();
        }

        public static List<BuffObj> GetBuffListByBuffType(Unit unit, BuffType buffType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent.GetBuffListByBuffType(buffType);
        }

        public static List<BuffObj> GetBuffListByTagType(Unit unit, BuffTagType buffTagType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent.GetBuffListByTagType(buffTagType);
        }

        public static List<BuffObj> GetBuffListByTagGroupType(Unit unit, BuffTagGroupType buffTagGroupType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent.GetBuffListByTagGroupType(buffTagGroupType);
        }

        public static void EventHandler(Unit unit, AbilityConfig.BuffTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit, ref ActionContext actionContext)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            buffComponent?.EventHandler(abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit, ref actionContext);
        }

        public static bool ChkCanMoveInput(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanMoveInput();
        }

        public static bool ChkCanFaceChgInput(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanFaceChgInput();
        }

        public static bool ChkCanSkillCastInput(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanSkillCastInput();
        }

        public static bool ChkCanNormalAttack(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanNormalAttack();
        }

        public static bool ChkCanBeSee(Unit beFindUnit, Unit seeUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(beFindUnit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeSee(seeUnit);
        }

        public static bool ChkCanBeTouchWhenFly(Unit beFindUnit, Unit seeUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(beFindUnit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeTouchWhenFly(seeUnit);
        }

        public static bool ChkIsFly(Unit beFindUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(beFindUnit);
            if (buffComponent == null)
            {
                return false;
            }
            return buffComponent.ChkIsFly();
        }

        public static bool ChkCannotBeTargeted(Unit beFindUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(beFindUnit);
            if (buffComponent == null)
            {
                return false;
            }
            return buffComponent.ChkCannotBeTargeted();
        }

        public static bool ChkCanBuffTick(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBuffTick();
        }

        public static bool ChkCanBuffTrig(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBuffTrig();
        }

        /// <summary>
        /// GameObject的显示状态
        /// </summary>
        /// <param name="beSeeUnit"></param>
        /// <param name="playerId"></param>
        /// <returns>0 正常显示; 1 半透明; 2 全透明</returns>
        public static int GetGameObjectShowType(Unit beSeeUnit, long playerId)
        {
            BuffComponent buffComponent = _GetBuffComponent(beSeeUnit);
            if (buffComponent == null)
            {
                return 0;
            }
            return buffComponent.GetGameObjectShowType(playerId);
        }

        /// <summary>
        /// 能否被造成伤害
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeDamage(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeDamage();
        }

        public static bool ChkCanBeControl(Scene scene, ref ActionContext actionContext)
        {
            Unit defenderUnit = UnitHelper.GetUnit(scene, actionContext.defenderUnitId);
            BuffComponent buffComponent = _GetBuffComponent(defenderUnit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeControl(ref actionContext);
        }

        public static bool ChkCanBeControl(Unit attackerUnit, Unit defenderUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(defenderUnit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeControl(attackerUnit);
        }

        public static bool ChkIsMotioning(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkIsMotioning();
        }

        public static float3 GetMotionSpeedVector(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return float3.zero;
            }
            return buffComponent.GetMotionSpeedVector();
        }

        public static (bool bStopAnimator, AnimatorMotionName animatorMotionName, bool isLoop) GetControlStateAnimatorMotion(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return (false, AnimatorMotionName.None, false);
            }
            return buffComponent.GetControlStateAnimatorMotion();
        }

    }
}