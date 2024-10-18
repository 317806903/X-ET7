using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class SkillShowCameraAreaComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform skillShowEffectTrans;
    }
}