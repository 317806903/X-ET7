﻿using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using UnityEngine;

namespace ET.Ability.Client
{
    [ComponentOf]
	public class SkillControlByControllerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public string skillCfgId;
        public long unitId;
        public bool isCameraPlayer;
        public Camera camera;

        public long targetUnitId;
        public bool isLeft;
    }
}