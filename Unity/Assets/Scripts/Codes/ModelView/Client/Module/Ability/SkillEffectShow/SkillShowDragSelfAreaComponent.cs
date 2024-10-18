using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class SkillShowDragSelfAreaComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform skillShowEffectTrans;
    }
}