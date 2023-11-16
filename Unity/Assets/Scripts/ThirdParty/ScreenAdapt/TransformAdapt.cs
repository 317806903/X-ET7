using ScreenAdapt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenAdapt
{
    [Serializable]
    public class TransformAdaptConfig
    {
        public bool updateApply;
        public bool isActive;
        public Vector3 localPosition;
        public Vector3 localEulerAngles;
        public Vector3 localScale;
    }

    public class TransformAdapt : AdaptBase<TransformAdaptConfig, Transform>
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

        protected override void _LoadConfig(TransformAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._LoadConfig(config);
            gameObject.SetActive(config.isActive);
            mComponent.localPosition = config.localPosition;
            mComponent.localEulerAngles = config.localEulerAngles;
            mComponent.localScale = config.localScale;
        }

        protected override void _SaveConfig(TransformAdaptConfig config)
        {
            if (mComponent == null || config == null)
            {
                return;
            }

            base._SaveConfig(config);
            config.isActive = gameObject.activeSelf;
            config.localPosition = mComponent.localPosition;
            config.localEulerAngles = mComponent.localEulerAngles;
            config.localScale = mComponent.localScale;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (CurrentConfig.updateApply)
            {
                this._LoadConfig(CurrentConfig);
            }
        }
    }
}