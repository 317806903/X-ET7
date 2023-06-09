﻿using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Ability
{
    ///<summary>
    ///游戏中任何一次伤害、治疗等逻辑，都会产生一条damageInfo，由此开始正常的伤害流程，而不是直接改写hp
    ///值得一提的是，在类似“攻击时产生额外一次伤害”这种效果中，额外一次伤害也应该是一个damageInfo。
    ///</summary>
    [ChildOf(typeof (DamageComponent))]
    [FriendOf(typeof (Unit))]
    public class DamageInfo: Entity, IAwake, IDestroy
    {
        ///<summary>
        ///造成伤害的攻击者，当然可以是null的
        ///</summary>
        public long attackerUnitId;

        ///<summary>
        ///造成攻击伤害的受击者，这个必须有
        ///</summary>
        public long defenderUnitId;

        ///<summary>
        ///这次伤害的类型Tag，这个会被用于buff相关的逻辑，是一个极其重要的信息
        ///这里是策划根据游戏设计来定义的，比如游戏中可能存在"frozen" "fire"之类的伤害类型，还会存在"directDamage" "period" "reflect"之类的类型伤害
        ///根据这些伤害类型，逻辑处理可能会有所不同，典型的比如"reflect"，来自反伤的，那本身一个buff的作用就是受到伤害的时候反弹伤害，如果双方都有这个buff
        ///并且这个buff没有判断damageInfo.tags里面有reflect，则可能造成“短路”，最终有一下有一方就秒了。
        ///</summary>
        public DamageSourceTag[] tags;

        ///<summary>
        ///伤害值，其实伤害值是多元的，通常游戏都会有多个属性伤害，所以会用一个struct，否则就会是一个int
        ///尽管起名叫Damage，但实际上治疗也是这个，只是负数叫做治疗量，这个起名看似不严谨，对于游戏（这个特殊的业务）而言却又是严谨的
        ///</summary>
        public Damage damage;

        ///<summary>
        ///是否暴击，这是游戏设计了有暴击的可能性存在。
        ///这里记录一个总暴击率，随着buff的不断改写，最后这个暴击率会得到一个0-1的值，代表0%-100%。
        ///最终处理的时候，会根据这个值来进行抉择，可以理解为，当这个值超过1的时候，buff就可以认为这次攻击暴击了。
        ///</summary>
        public float criticalRate;

        ///<summary>
        ///是否命中，是否命中与是否暴击并不直接相关，都是单独的算法
        ///作为一个射击游戏，子弹命中敌人是一种技巧，所以在这里设计命中了还会miss是愚蠢的
        ///因此这里的hitRate始终是2，就是必定命中的，之所以把这个属性放着，也是为了说明问题，而不是这个属性真的对这个游戏有用。
        ///要不要这个属性还是取决于游戏设计，比如当前游戏，本不该有这个属性。
        ///</summary>
        public float hitRate = 2.00f;

        ///<summary>
        ///伤害的角度，作为伤害打向角色的入射角度，比如子弹，就是它当前的飞行角度
        ///</summary>
        public float degree;
    }
}