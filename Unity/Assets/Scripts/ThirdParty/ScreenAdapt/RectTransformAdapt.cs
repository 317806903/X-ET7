using ScreenAdapt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenAdapt
{
    [Serializable]
    public class RectTransformAdaptConfig
    {
        public bool useIsActive;
        public bool isActive;
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector2 sizeDelta;
        public Vector3 localPosition;
        public Vector3 anchoredPosition;
        public Vector3 localEulerAngles;
        public Vector3 localScale;
        public Vector2 pivot;
    }

    public class RectTransformAdapt : AdaptBase<RectTransformAdaptConfig, RectTransform>
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

        protected override void _LoadConfig(RectTransformAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._LoadConfig(config);
            if (config.useIsActive)
            {
                gameObject.SetActive(config.isActive);
            }

            mComponent.anchorMin = config.anchorMin;
            mComponent.anchorMax = config.anchorMax;
            mComponent.sizeDelta = config.sizeDelta;
            //mComponent.localPosition = config.localPosition;
            mComponent.anchoredPosition = config.anchoredPosition;
            mComponent.localEulerAngles = config.localEulerAngles;
            mComponent.localScale = config.localScale;
            mComponent.pivot = config.pivot;
        }

        protected override void _SaveConfig(RectTransformAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._SaveConfig(config);
            config.isActive = gameObject.activeSelf;
            config.anchorMin = mComponent.anchorMin;
            config.anchorMax = mComponent.anchorMax;
            config.sizeDelta = mComponent.sizeDelta;
            config.localPosition = mComponent.localPosition;
            config.anchoredPosition = mComponent.anchoredPosition;
            config.localEulerAngles = mComponent.localEulerAngles;
            config.localScale = mComponent.localScale;
            config.pivot = mComponent.pivot;
        }
    }
}