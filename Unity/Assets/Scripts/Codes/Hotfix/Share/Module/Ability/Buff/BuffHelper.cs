using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof(Unit))]
    public static class BuffHelper
    {
        public static BuffComponent GetBuffComponent(Unit unit)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                buffComponent = unit.AddComponent<BuffComponent>();
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
            GetBuffComponent(unit).AddBuff(casterUnit, unit, addBuffInfo, actionContext);
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
            }
            
            if (selectHandle.selectHandleType == SelectHandleType.SelectUnits)
            {
                int count = selectHandle.unitIds.Count;
                foreach (var unitId in selectHandle.unitIds)
                {
                    Unit unitSelect = UnitHelper.GetUnit(casterUnit.DomainScene(), unitId);
                    BuffComponent buffComponent = GetBuffComponent(unitSelect);
                    List<BuffObj> list;
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
                        return;
                    }

                    foreach (BuffObj buffObj in list)
                    {
                        buffObj.ChgDuration(0);
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static void ChgBuffStackCount(Unit unit, int addStackCount, ActionContext actionContext)
        {
            BuffObj buffObj = GetBuffObj(unit, actionContext);
            if (buffObj == null)
            {
                return;
            }
            buffObj.AddStackCount(addStackCount);
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
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return null;
            }

            return buffComponent.GetChild<BuffObj>(buffId);
        }
        
        public static void EventHandler(Unit unit, AbilityBuffMonitorTriggerEvent abilityBuffMonitorTriggerEvent, Unit onAttackUnit, Unit beHurtUnit)
        {
            unit.GetComponent<BuffComponent>()?.EventHandler(abilityBuffMonitorTriggerEvent, onAttackUnit, beHurtUnit);
        }
        
        public static bool ChkCanMoveInput(Unit unit)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanMoveInput();
        }

        public static bool ChkCanFaceChgInput(Unit unit)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanFaceChgInput();
        }

        public static bool ChkCanSkillCastInput(Unit unit)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanSkillCastInput();
        }

        public static bool ChkCanBeSee(Unit unit)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeSee();
        }

        /// <summary>
        /// 能否被造成伤害
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeDamage(Unit unit)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeDamage();
        }

        public static bool ChkBuffTagType(Unit unit, BuffTagType buffTagType)
        {
            BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkBuffTagType(buffTagType);
        }

        public static bool ChkCanBeControl(Unit unit, ActionContext actionContext)
        {
            Unit defenderUnit = UnitHelper.GetUnit(unit.DomainScene(), actionContext.defenderUnitId);
            BuffComponent buffComponent = defenderUnit.GetComponent<BuffComponent>();
            if (buffComponent == null)
            {
                return true;
            }
            return buffComponent.ChkCanBeControl(actionContext);
        }

    }
}