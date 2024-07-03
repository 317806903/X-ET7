using System.Collections.Generic;
using ET.AbilityConfig;

namespace ET.Ability
{
    // public enum GlobalBuffType
    // {
    //     Unit,
    //     Player,
    //     Game,
    // }

    [ComponentOf(typeof(Scene))]
	public class GlobalBuffManagerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
    }
}