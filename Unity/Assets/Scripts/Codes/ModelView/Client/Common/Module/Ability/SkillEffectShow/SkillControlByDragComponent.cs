using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class SkillControlByDragComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public string skillCfgId;
        public long unitId;
        public Camera camera;
        public Action<SelectHandle> castSkill;

        public long targetUnitId;
        public bool isClickUGUI;
    }
}