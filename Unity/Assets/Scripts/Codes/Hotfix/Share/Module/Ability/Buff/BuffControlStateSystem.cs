using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffComponent))]
    [FriendOf(typeof (BuffObj))]
    public static class BuffControlStateSystem
    {
        public static void AddBuffControlStateList(this BuffComponent self, BuffObj buffObj)
        {
            if (buffObj.buffAction is BuffActionModifyControlState buffActionModifyControlState)
            {
                self.buffControlStateList.Add(buffActionModifyControlState.ControlState, buffObj);
            }
        }
        
        public static void RemoveBuffControlStateList(this BuffComponent self, BuffObj buffObj)
        {
            if (buffObj.buffAction is BuffActionModifyControlState buffActionModifyControlState)
            {
                self.buffControlStateList.Remove(buffActionModifyControlState.ControlState, buffObj);
            }
        }

        public static void AddBuffWhenModifyControlState(this BuffObj self)
        {
            if (self.buffAction is BuffActionModifyControlState == false)
            {
                return;
            }
            
        }
        
        public static void UpdateBuffWhenModifyControlState(this BuffObj self)
        {
            if (self.buffAction is BuffActionModifyControlState == false)
            {
                return;
            }
            
        }
        
        public static void RemoveBuffWhenModifyControlState(this BuffObj self)
        {
            if (self.buffAction is BuffActionModifyControlState == false)
            {
                return;
            }

        }

        /// <summary>
        /// 是否可以主动移动
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanMoveInput(this BuffComponent self)
        {
            return self.ChkBuffTagType(BuffTagType.NoMoveInput) == false;
        }

        /// <summary>
        /// 是否可以主动修改面向
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanFaceChgInput(this BuffComponent self)
        {
            return self.ChkBuffTagType(BuffTagType.NoFaceChgInput) == false;
        }

        /// <summary>
        /// 是否可以是否技能
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanSkillCastInput(this BuffComponent self)
        {
            return self.ChkBuffTagType(BuffTagType.NoSkillCastInput) == false;
        }

        /// <summary>
        /// 是否可以播放动画(被冰住则动画停止)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanPlayAnimator(this BuffComponent self)
        {
            return true;
        }

        /// <summary>
        /// 是否可以被搜寻到
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeSee(this BuffComponent self)
        {
            return self.ChkBuffTagType(BuffTagType.Invisible) == false;
        }

        /// <summary>
        /// 能否被造成伤害
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeDamage(this BuffComponent self)
        {
            return self.ChkBuffTagType(BuffTagType.Invincible) == false;
        }

        /// <summary>
        /// 能否被控制(破霸体)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeControl(this BuffComponent self, ActionContext actionContext)
        {
            //受击者有完全霸体
            if (self.ChkBuffTagType(BuffTagType.FullBati))
            {
                return false;
            }

            Unit attackerUnit = UnitHelper.GetUnit(self.DomainScene(), actionContext.attackerUnitId);
            //受击者有强霸体
            if (self.ChkBuffTagType(BuffTagType.StrongBati))
            {
                if (BuffHelper.ChkBuffTagType(attackerUnit, BuffTagType.BreakStrongBati))
                {
                    return true;
                }

                if (actionContext.isBreakStrongBati)
                {
                    return true;
                }
                return false;
            }
            //受击者有弱霸体
            if (self.ChkBuffTagType(BuffTagType.SoftBati))
            {
                if (BuffHelper.ChkBuffTagType(attackerUnit, BuffTagType.BreakStrongBati) || BuffHelper.ChkBuffTagType(attackerUnit, BuffTagType.BreakSoftBati))
                {
                    return true;
                }

                if (actionContext.isBreakStrongBati || actionContext.isBreakSoftBati)
                {
                    return true;
                }
                return false;
            }
            //受击者没有霸体
            return true;
        }

    }
}