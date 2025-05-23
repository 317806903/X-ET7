﻿using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    ///<summary>
    ///游戏中伤害值的struct，这游戏的伤害类型包括子弹伤害（治疗）、爆破伤害（治疗）、精神伤害（治疗）3种，这两种的概念更像是类似物理伤害、金木水火土属性伤害等等这种元素伤害的概念
    ///但是游戏的逻辑可能会依赖于这个伤害做一些文章，比如“受到子弹伤害减少90%”之类的
    ///</summary>
    public struct Damage
    {
        public Dictionary<int, float> list;

        public Damage(Dictionary<int, float> damageList)
        {
            list = DictionaryComponent<int, float>.Create();
            if (damageList != null)
            {
                foreach (var damageInfo in damageList)
                {
                    list[damageInfo.Key] = damageInfo.Value;
                }
            }
        }

        public Damage(int damageType, float damageValue)
        {
            list = new();
            list[damageType] = damageValue;
        }
        //
        // public void SetScale(float scale)
        // {
        //     list = new();
        //     foreach (var damageInfo in list)
        //     {
        //         list[damageInfo.Key] = damageInfo.Value * scale;
        //     }
        //     return new Damage(list);
        // }

        ///<summary>
        ///统计规则，在这个游戏里伤害和治疗不能共存在一个结果里，作为抵消用
        ///<param name="asHeal">是否当做治疗来统计</name>
        ///</summary>
        public int Overall()
        {
            float damageValue = 0;
            foreach (var damageInfo in list)
            {
                damageValue += damageInfo.Value;
            }

            if (damageValue >= -0.01f && damageValue <= 0.01f)
            {
                return 0;
            }
            else if (damageValue > 0)
            {
                damageValue += 0.01f;
                return math.max(1, (int)damageValue);
            }
            else
            {
                damageValue -= 0.01f;
                return math.min(-1, (int)damageValue);
            }
        }

        public static Damage operator +(Damage a, Damage b)
        {
            Dictionary<int, float> list = DictionaryComponent<int, float>.Create();
            foreach (var damageInfo in a.list)
            {
                if (list.ContainsKey(damageInfo.Key))
                {
                    list[damageInfo.Key] += damageInfo.Value;
                }
                else
                {
                    list[damageInfo.Key] = damageInfo.Value;
                }
            }
            foreach (var damageInfo in b.list)
            {
                if (list.ContainsKey(damageInfo.Key))
                {
                    list[damageInfo.Key] += damageInfo.Value;
                }
                else
                {
                    list[damageInfo.Key] = damageInfo.Value;
                }
            }
            return new Damage(list);
        }

        public static Damage operator +(Damage a, float chgValue)
        {
            if (chgValue == 0)
            {
                return a;
            }
            Dictionary<int, float> list = DictionaryComponent<int, float>.Create();
            foreach (var damageInfo in a.list)
            {
                list[damageInfo.Key] = damageInfo.Value + chgValue;
            }
            return new Damage(list);
        }

        public static Damage Set(Damage a, float setValue)
        {
            Dictionary<int, float> list = DictionaryComponent<int, float>.Create();
            foreach (var damageInfo in a.list)
            {
                list[damageInfo.Key] = setValue;
            }
            return new Damage(list);
        }

        public static Damage operator *(Damage a, float scale)
        {
            if (scale == 1)
            {
                return a;
            }
            Dictionary<int, float> list = DictionaryComponent<int, float>.Create();
            foreach (var damageInfo in a.list)
            {
                list[damageInfo.Key] = damageInfo.Value * scale;
            }
            return new Damage(list);
        }

    }
}