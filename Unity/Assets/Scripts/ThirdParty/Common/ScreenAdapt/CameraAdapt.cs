using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenAdapt
{
    [Serializable]
    public class CameraAdaptConfig
    {
        public bool isSaved;
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
            config.isSaved = true;
            config.orthographicSize = mComponent.orthographicSize;
        }

        protected override void _LoadConfig(CameraAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._LoadConfig(config);
            if (config.isSaved == false)
            {
                return;
            }
            mComponent.orthographicSize = config.orthographicSize;
        }
    }
}