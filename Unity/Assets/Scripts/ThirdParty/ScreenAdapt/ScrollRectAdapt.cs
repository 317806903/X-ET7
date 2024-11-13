using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenAdapt
{
    [Serializable]
    public class ScrollRectConfig
    {
        public bool isSaved;
        public bool vertical;
        public bool horizontal;
    }

    public class ScrollRectAdapt : AdaptBase<ScrollRectConfig, ScrollRect>
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

        protected override void _SaveConfig(ScrollRectConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._SaveConfig(config);
            config.isSaved = true;
            config.vertical = mComponent.vertical;
            config.horizontal = mComponent.horizontal;
        }

        protected override void _LoadConfig(ScrollRectConfig config)
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
            mComponent.vertical = config.vertical;
            mComponent.horizontal = config.horizontal;
        }
    }
}