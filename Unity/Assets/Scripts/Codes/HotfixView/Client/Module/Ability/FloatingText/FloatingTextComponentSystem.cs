using System;
using System.Collections.Generic;
using System.Numerics;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability.Client
{
    [FriendOf(typeof (FloatingTextComponent))]
    [FriendOf(typeof (FloatingTextObj))]
    public static class FloatingTextComponentSystem
    {
        [ObjectSystem]
        public class FloatingTextComponentAwakeSystem: AwakeSystem<FloatingTextComponent>
        {
            protected override void Awake(FloatingTextComponent self)
            {
            }
        }

        [ObjectSystem]
        public class FloatingTextComponentDestroySystem: DestroySystem<FloatingTextComponent>
        {
            protected override void Destroy(FloatingTextComponent self)
            {
            }
        }

        public static FloatingTextObj CreateFloatingText(this FloatingTextComponent self, ActionCfg_FloatingText _ActionCfg_FloatingText, int showNum)
        {
            FloatingTextObj floatingTextObj = self.AddChild<FloatingTextObj>();
            floatingTextObj.CreateFloatingText(_ActionCfg_FloatingText, showNum).Coroutine();
            return floatingTextObj;
        }

    }
}