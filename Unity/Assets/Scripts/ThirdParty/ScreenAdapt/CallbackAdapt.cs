using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScreenAdapt
{
    [Serializable]
    public class UnityEventString : UnityEvent<string>
    {
    }

    [Serializable]
    public class CallbackAdaptConfig
    {
        public string param;
        public UnityEventString changeCallback;
    }

    public class CallbackAdapt : AdaptBase<CallbackAdaptConfig, MonoBehaviour>
    {
        [ContextMenu("---LoadConfig")]
        public override void LoadConfig()
        {
            base.LoadConfig();
        }

        [ContextMenu("---SaveConfig")]
        public override void SaveConfig()
        {
            base.SaveConfig();
        }

        protected override void _LoadConfig(CallbackAdaptConfig config)
        {
            base._LoadConfig(config);
            config.changeCallback.Invoke(config.param);
        }
    }
}