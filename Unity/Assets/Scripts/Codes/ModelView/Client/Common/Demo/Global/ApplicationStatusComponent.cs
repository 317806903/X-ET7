using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(GlobalComponent))]
    public class ApplicationStatusComponent: Entity, IAwake, IDestroy, IUpdate
    {
        [StaticField]
        public static ApplicationStatusComponent Instance;

        public SimulateSwitchToBackground simulateSwitchToBackground;

        public HashSet<Action<bool>> OnApplicationPauseListern = new();
        public HashSet<Action> OnApplicationEscapeListern = new();
        public HashSet<Action<bool>> OnApplicationPauseListernWaitAdd = new();
        public HashSet<Action> OnApplicationEscapeListernWaitAdd = new();
        public HashSet<Action<bool>> OnApplicationPauseListernWaitRemove = new();
        public HashSet<Action> OnApplicationEscapeListernWaitRemove = new();
    }
}