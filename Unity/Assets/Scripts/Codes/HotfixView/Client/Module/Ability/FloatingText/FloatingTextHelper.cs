using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Ability.Client
{
    [FriendOf(typeof (Unit))]
    public static class FloatingTextHelper
    {
        public static FloatingTextObj CreateFloatingText(Unit unit, string floatingTextActionId, int showNum)
        {
            if (string.IsNullOrEmpty(floatingTextActionId))
            {
                return null;
            }
            ActionCfg_FloatingText _ActionCfg_FloatingText = ActionCfg_FloatingTextCategory.Instance.Get(floatingTextActionId);
            return CreateFloatingText(unit, _ActionCfg_FloatingText, showNum);
        }

        public static FloatingTextObj CreateFloatingText(Unit unit, ActionCfg_FloatingText _ActionCfg_FloatingText, int showNum)
        {
            FloatingTextComponent floatingTextComponent = unit.GetComponent<FloatingTextComponent>();
            return floatingTextComponent.CreateFloatingText(_ActionCfg_FloatingText, showNum);
        }
    }
}