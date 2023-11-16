using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenAdapt
{
    [Serializable]
    public class CameraAdaptConfig
    {
        public float orthographicSize;
    }

    public class CameraAdapt : AdaptBase<CameraAdaptConfig, Camera>
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

        protected override void _SaveConfig(CameraAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._SaveConfig(config);
            config.orthographicSize = mComponent.orthographicSize;
        }

        protected override void _LoadConfig(CameraAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._LoadConfig(config);
            mComponent.orthographicSize = config.orthographicSize;
        }
    }
}