﻿using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;

namespace ET.Ability
{
    [FriendOf(typeof (BuffComponent))]
    [FriendOf(typeof (BuffObj))]
    public static class BuffControlStateSystem
    {
        public static void AddBuffPlayAnimatorList(this BuffComponent self, BuffObj buffObj)
        {
            if (string.IsNullOrEmpty(buffObj.model.SelfPlayAnimator))
            {
                return;
            }

            self.buffPlayAnimatorList.Add(buffObj);
        }

        public static void RemoveBuffPlayAnimatorList(this BuffComponent self, BuffObj buffObj)
        {
            self.buffPlayAnimatorList.Remove(buffObj);
        }

        /// <summary>
        /// 获取优先级最高的playAnimator
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static (bool bStopAnimator, AnimatorMotionName animatorMotionName, bool isLoop) GetControlStateAnimatorMotion(this BuffComponent self)
        {
            bool bStopAnimator = self.ChkBuffByTagType(BuffTagType.StopAnimator);
            AnimatorMotionName animatorMotionName = AnimatorMotionName.None;
            bool isLoop = false;

            BuffObj buffObjMaxPriority = null;
            int curPriority = -999;
            foreach (BuffObj buffObj in self.buffPlayAnimatorList)
            {
                if (buffObj.isEnabled && buffObj.model.Priority > curPriority)
                {
                    curPriority = buffObj.model.Priority;
                    buffObjMaxPriority = buffObj;
                }
            }

            if (buffObjMaxPriority != null)
            {
                animatorMotionName = buffObjMaxPriority.model.SelfPlayAnimator_Ref.AnimatorName;
                isLoop = buffObjMaxPriority.model.SelfPlayAnimator_Ref.IsLoop;
            }

            return (bStopAnimator, animatorMotionName, isLoop);
        }

        /// <summary>
        /// 是否可以主动移动
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanMoveInput(this BuffComponent self)
        {
            bool bNoMoveInput = self.ChkBuffByTagType(BuffTagType.NoMoveInput);
            if (bNoMoveInput == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以主动修改面向
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanFaceChgInput(this BuffComponent self)
        {
            bool bNoFaceChgInput = self.ChkBuffByTagType(BuffTagType.NoFaceChgInput);
            if (bNoFaceChgInput == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以释放技能
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanSkillCastInput(this BuffComponent self)
        {
            bool bNoSkillCastInput = self.ChkBuffByTagType(BuffTagType.NoSkillCastInput);
            if (bNoSkillCastInput == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以普通攻击
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanNormalAttack(this BuffComponent self)
        {
            bool bNoNormalAttack = self.ChkBuffByTagType(BuffTagType.NoNormalAttack);
            if (bNoNormalAttack == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以BuffTick
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBuffTick(this BuffComponent self)
        {
            bool bStopBuffTick = self.ChkBuffByTagType(BuffTagType.StopBuffTick);
            if (bStopBuffTick == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以BuffTrig
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBuffTrig(this BuffComponent self)
        {
            bool bStopBuffTrig = self.ChkBuffByTagType(BuffTagType.StopBuffTrig);
            if (bStopBuffTrig == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以播放动画(被冰住则动画停止)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanPlayAnimator(this BuffComponent self)
        {
            bool bStopAnimator = self.ChkBuffByTagType(BuffTagType.StopAnimator);
            if (bStopAnimator == false)
            {
                return true;
            }
            bool bCanBeControl = self.ChkCanBeControl();
            if (bCanBeControl == false)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以被看到(对应隐身单位)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeSee(this BuffComponent self, Unit seeUnit)
        {
            bool bInvisible = self.ChkBuffByTagType(BuffTagType.Invisible);
            if (bInvisible == false)
            {
                return true;
            }
            bool bBeBreakInvisible = self.ChkBuffByTagType(BuffTagType.BeBreakInvisible);
            if (bBeBreakInvisible)
            {
                return true;
            }

            if (BuffHelper.ChkBuffByTagType(seeUnit, BuffTagType.SeeInvisible))
            {
                return true;
            }

            if (ET.GamePlayHelper.ChkIsFriend(self.GetUnit(), seeUnit))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否可以被接触到(对应飞行单位)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeTouchWhenFly(this BuffComponent self, Unit seeUnit)
        {
            bool bFly = self.ChkBuffByTagType(BuffTagType.Fly);
            if (bFly == false)
            {
                return true;
            }
            bool bBeBreakFly = self.ChkBuffByTagType(BuffTagType.BeBreakFly);
            if (bBeBreakFly)
            {
                return true;
            }

            if (BuffHelper.ChkBuffByTagType(seeUnit, BuffTagType.TouchFly))
            {
                return true;
            }

            if (ET.GamePlayHelper.ChkIsFriend(self.GetUnit(), seeUnit))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否飞行单位中
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkIsFly(this BuffComponent self)
        {
            bool bFly = self.ChkBuffByTagType(BuffTagType.Fly);
            if (bFly == false)
            {
                return false;
            }
            bool bBeBreakFly = self.ChkBuffByTagType(BuffTagType.BeBreakFly);
            if (bBeBreakFly)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否不可作为目标
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCannotBeTargeted(this BuffComponent self)
        {
            bool bCannotBeTargeted = self.ChkBuffByTagType(BuffTagType.CannotBeTargeted);
            if (bCannotBeTargeted)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// GameObject的显示状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="playerId"></param>
        /// <returns>0 正常显示; 1 半透明; 2 全透明</returns>
        public static int GetGameObjectShowType(this BuffComponent self, long playerId)
        {
            bool bInvisible = self.ChkBuffByTagType(BuffTagType.Invisible);
            if (bInvisible == false)
            {
                //表示直接看见
                return 0;
            }

            Unit seeUnit = UnitHelper.GetUnit(self.DomainScene(), playerId);
            bool bBeFind = self.ChkCanBeSee(seeUnit);
            if (bBeFind)
            {
                //表示半透明
                return 1;
            }
            else
            {
                //表示全透明
                return 2;
            }
        }

        /// <summary>
        /// 能否被造成伤害
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeDamage(this BuffComponent self)
        {
            return self.ChkBuffByTagType(BuffTagType.Invincible) == false;
        }

        /// <summary>
        /// 能否被控制(破霸体)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeControl(this BuffComponent self, ref ActionContext actionContext)
        {
            //受击者有无敌
            if (self.ChkBuffByTagType(BuffTagType.Invincible))
            {
                return false;
            }
            //受击者有完全霸体
            if (self.ChkBuffByTagType(BuffTagType.FullBati))
            {
                return false;
            }

            Unit attackerUnit = UnitHelper.GetUnit(self.DomainScene(), actionContext.attackerUnitId);
            //受击者有强霸体
            if (self.ChkBuffByTagType(BuffTagType.StrongBati))
            {
                if (self.ChkBuffByTagType(BuffTagType.StrongWeak))
                {
                    return true;
                }

                if (BuffHelper.ChkBuffByTagType(attackerUnit, BuffTagType.BreakStrongBati))
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
            if (self.ChkBuffByTagType(BuffTagType.SoftBati))
            {
                if (self.ChkBuffByTagType(BuffTagType.StrongWeak))
                {
                    return true;
                }
                if (self.ChkBuffByTagType(BuffTagType.Weak))
                {
                    return true;
                }

                if (BuffHelper.ChkBuffByTagType(attackerUnit, BuffTagType.BreakStrongBati) || BuffHelper.ChkBuffByTagType(attackerUnit, BuffTagType.BreakSoftBati))
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

        /// <summary>
        /// 能否被控制(破霸体)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeControl(this BuffComponent self, Unit attackerUnit)
        {
            //受击者有完全霸体
            if (self.ChkBuffByTagType(BuffTagType.FullBati))
            {
                return false;
            }

            //受击者有强霸体
            if (self.ChkBuffByTagType(BuffTagType.StrongBati))
            {
                if (self.ChkBuffByTagType(BuffTagType.StrongWeak))
                {
                    return true;
                }

                if (BuffHelper.ChkBuffByTagType(attackerUnit, BuffTagType.BreakStrongBati))
                {
                    return true;
                }

                return false;
            }
            //受击者有弱霸体
            if (self.ChkBuffByTagType(BuffTagType.SoftBati))
            {
                if (self.ChkBuffByTagType(BuffTagType.StrongWeak))
                {
                    return true;
                }
                if (self.ChkBuffByTagType(BuffTagType.Weak))
                {
                    return true;
                }

                if (BuffHelper.ChkBuffByTagType(attackerUnit, BuffTagType.BreakStrongBati) || BuffHelper.ChkBuffByTagType(attackerUnit, BuffTagType.BreakSoftBati))
                {
                    return true;
                }

                return false;
            }
            //受击者没有霸体
            return true;
        }

        /// <summary>
        /// 能否被控制(破霸体)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkCanBeControl(this BuffComponent self)
        {
            //受击者有完全霸体
            if (self.ChkBuffByTagType(BuffTagType.FullBati))
            {
                return false;
            }

            //受击者有强霸体
            if (self.ChkBuffByTagType(BuffTagType.StrongBati))
            {
                if (self.ChkBuffByTagType(BuffTagType.StrongWeak))
                {
                    return true;
                }

                return false;
            }
            //受击者有弱霸体
            if (self.ChkBuffByTagType(BuffTagType.SoftBati))
            {
                if (self.ChkBuffByTagType(BuffTagType.StrongWeak))
                {
                    return true;
                }
                if (self.ChkBuffByTagType(BuffTagType.Weak))
                {
                    return true;
                }

                return false;
            }
            //受击者没有霸体
            return true;
        }

        /// <summary>
        /// 当前是否被移动中(击退，击飞等)
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool ChkIsMotioning(this BuffComponent self)
        {
            foreach (BuffObj buffObj in self.buffMotionList)
            {
                bool bCanBeMotion = self._ChkCanBeMotion(ref buffObj.actionContext);
                if (bCanBeMotion)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 能否被移动(击退，击飞等)(破霸体)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static bool _ChkCanBeMotion(this BuffComponent self, ref ActionContext actionContext)
        {
            bool bNoMotion = self.ChkBuffByTagType(BuffTagType.NoMotion);
            if (bNoMotion)
            {
                return false;
            }
            return self.ChkCanBeControl(ref actionContext);
        }

    }
}