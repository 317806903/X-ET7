using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailRendererScaler: MonoBehaviour
    {
        private TrailRenderer trailRenderer;
        private float initialScaleValueX;
        private float lastScaleValueX;
        public float curWidthMultiplier;
        public float initialWidthMultiplier;
        public float chkIntervalTime = 2f;
        private float lastChkTime;
        private bool isInit;

        void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
            if (this.trailRenderer == null)
            {
                return;
            }
            this.initialScaleValueX = transform.lossyScale.x;
            if (this.initialScaleValueX == 0)
            {
                return;
            }

            this.lastScaleValueX = 0;

            this.initialWidthMultiplier = trailRenderer.widthMultiplier;
            if (this.initialWidthMultiplier == 0)
            {
                return;
            }
            lastChkTime = Time.time;
            isInit = false;
        }

        void Update()
        {
            if (this.trailRenderer == null)
            {
                return;
            }
            if (initialScaleValueX == 0)
            {
                return;
            }

            if (isInit == false)
            {
                isInit = true;
            }
            else
            {
                if (Time.time - this.lastChkTime < this.chkIntervalTime)
                {
                    return;
                }
            }
            this.lastChkTime = Time.time;

            float curScaleValueX = transform.lossyScale.x;
            if (Mathf.Abs(this.lastScaleValueX - curScaleValueX) < 0.01f)
            {
                return;
            }
            this.lastScaleValueX = curScaleValueX;

            // 获取当前对象的绝对缩放
            this.curWidthMultiplier = this.initialWidthMultiplier * curScaleValueX / this.initialScaleValueX;

            // 动态调整轨迹的宽度乘数
            trailRenderer.widthMultiplier = this.curWidthMultiplier;
        }

    }
}