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
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return null;
            }
            return buffComponent;
        }

        public static void AddBuff(Unit casterUnit, ActionCfg_BuffAdd actionCfgAddBuff, SelectHandle selectHandle, ActionContext actionContext)
        {
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                int count = selectHandle.unitIds.Count;
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitSelect = UnitHelper.GetUnit(casterUnit.DomainScene(), unitId);
                    if (unitSelect.Id == actionContext.motionUnitId)
                    {
                        actionContext.motionDirection = unitSelect.Forward;
                    }
                    else
                    {
                        actionContext.motionDirection = actionContext.motionPosition - unitSelect.Position;
                    }
                    foreach (AddBuffInfo addBuffInfo in actionCfgAddBuff.BuffList)
                    {
                        AddBuff(casterUnit, unitSelect, addBuffInfo, actionContext);
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static void AddBuff(Unit casterUnit, Unit unit, AddBuffInfo addBuffInfo, ActionContext actionContext)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                buffComponent = unit.AddComponent<BuffComponent>();
            }
            buffComponent.AddBuff(casterUnit, unit, addBuffInfo, actionContext).Coroutine();
        }

        public static void RemoveBuff(Unit casterUnit, ActionCfg_BuffRemove actionCfgRemoveBuff, SelectHandle selectHandle, ActionContext actionContext)
        {
            BuffRemoveType buffRemoveType = actionCfgRemoveBuff.BuffRemoveType;
            if (buffRemoveType is BuffRemoveCur buffRemoveCur)
            {
                BuffObj buffObj = GetBuffObj(casterUnit, actionContext);
                if (buffObj == null)
                {
                    return;
                }
                buffObj.ChgDuration(0);
                return;
            }

            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                int count = selectHandle.unitIds.Count;
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitSelect = UnitHelper.GetUnit(casterUnit.DomainScene(), unitId);
                    BuffComponent buffComponent = _GetBuffComponent(unitSelect);
                    if (buffComponent == null)
                    {
                        continue;
                    }
                    List<BuffObj> list = null;
                    if (buffRemoveType is BuffRemoveByTag buffRemoveByTag)
                    {
                        list = buffComponent.GetBuffListByTag(buffRemoveByTag.BuffTagType);
                    }
                    else if (buffRemoveType is BuffRemoveByTagGroup buffRemoveByTagGroup)
                    {
                        list = buffComponent.GetBuffListByTagGroup(buffRemoveByTagGroup.BuffTagGroupType);
                    }
                    else if (buffRemoveType is BuffRemoveByType buffRemoveByType)
                    {
                        list = buffComponent.GetBuffListByType(buffRemoveByType.BuffType);
                    }
                    else
                    {
                        Log.Error($"ET.Ability.BuffHelper.RemoveBuff buffRemoveType[{buffRemoveType}]");
                    }

                    if (list != null)
                    {
                        foreach (BuffObj buffObj in list)
                        {
                            buffObj.ChgDuration(0);
                        }
                    }
                }
            }
            else
            {
                Log.Error($"ET.Ability.BuffHelper.RemoveBuff selectHandle.selectHandleType != SelectHandleType.SelectUnits");
            }
        }

        public static void ChgBuffStackCount(Unit unit, ActionCfg_BuffStackCountChg actionCfgBuffStackCountChg, ActionContext actionContext)
        {
            BuffObj buffObj = GetBuffObj(unit, actionContext);
            if (buffObj == null)
            {
                return;
            }
            buffObj.AddStackCount(actionCfgBuffStackCountChg);
        }

        public static void ChgBuffDurationChg(Unit unit, BuffDurationChgType buffDurationChgType, ActionContext actionContext)
        {
            BuffObj buffObj = GetBuffObj(unit, actionContext);
            if (buffObj == null)
            {
                return;
            }
            if (buffObj.permanent)
            {
                return;
            }
            if (buffObj.duration <= 0)
            {
                return;
            }
            float newDuration = buffObj.duration;
            if (buffDurationChgType is BuffDurationChgCur buffDurationChgCur)
            {
                newDuration = buffObj.duration * buffDurationChgCur.LeftTimeScale + buffDurationChgCur.StackCountScale * buffObj.orgDuration * (buffObj.stack - 1);
            }
            else if (buffDurationChgType is BuffDurationChgOrg buffDurationChgorg)
            {
                newDuration = buffObj.orgDuration + buffDurationChgorg.StackCountScale * buffObj.orgDuration * (buffObj.stack - 1);
            }
            buffObj.ChgDuration(newDuration);
        }

        public static BuffObj GetBuffObj(Unit unit, ActionContext actionContext)
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

        public static bool ChkBuffTagType(Unit unit, BuffTagType buffTagType)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkBuffTagType(buffTagType);
        }

        public static void EventHandler(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            buffComponent?.EventHandler(abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit);
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

        public static bool ChkCanBeFind(Unit beFindUnit, Unit seeUnit)
        {
            BuffComponent buffComponent = _GetBuffComponent(beFindUnit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeFind(seeUnit);
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

        public static bool ChkCanBeControl(Scene scene, ActionContext actionContext)
        {
            Unit defenderUnit = UnitHelper.GetUnit(scene, actionContext.defenderUnitId);
            BuffComponent buffComponent = _GetBuffComponent(defenderUnit);
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeControl(actionContext);
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

        public static (bool, AnimatorMotionName) GetControlStateAnimatorMotion(Unit unit)
        {
            BuffComponent buffComponent = _GetBuffComponent(unit);
            if (buffComponent == null)
            {
                return (false, AnimatorMotionName.None);
            }
            return buffComponent.GetControlStateAnimatorMotion();
        }

    }
}