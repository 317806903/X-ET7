using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenAdapt
{
    [Serializable]
    public class CanvasAdaptConfig
    {
        public bool isSaved;
        public Vector2 referenceResolution;
        public float matchWidthOrHeight;
    }

    public class CanvasAdapt : AdaptBase<CanvasAdaptConfig, CanvasScaler>
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

        protected override void _SaveConfig(CanvasAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._SaveConfig(config);
            config.isSaved = true;
            config.matchWidthOrHeight = mComponent.matchWidthOrHeight;
            config.referenceResolution = mComponent.referenceResolution;
        }

        protected override void _LoadConfig(CanvasAdaptConfig config)
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
            mComponent.referenceResolution = config.referenceResolution;
            mComponent.matchWidthOrHeight = config.matchWidthOrHeight;
        }
    }
}