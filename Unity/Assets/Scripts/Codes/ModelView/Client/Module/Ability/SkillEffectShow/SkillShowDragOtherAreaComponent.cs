﻿using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class SkillShowDragOtherAreaComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform skillShowEffectTrans;
        public Transform skillShowEffectPointRangeTrans;
        public Transform skillShowEffectOutRangeTrans;
    }
}