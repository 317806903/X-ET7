using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET
{
    [RequireComponent(typeof(Light))]
    public class LightScaler: MonoBehaviour
    {
        private Light Light;
        private float initialScaleValueX;
        private float lastScaleValueX;
        public float curIntensity;
        public float initialIntensity;
        public float curRange;
        public float initialRange;
        public float chkIntervalTime = 2f;
        private float lastChkTime;
        private bool isInit;

        void Awake()
        {
            this.Light = GetComponent<Light>();
            if (this.Light == null)
            {
                return;
            }
            this.initialScaleValueX = transform.lossyScale.x;
            if (this.initialScaleValueX == 0)
            {
                return;
            }

            this.lastScaleValueX = 0;

            this.initialIntensity = this.Light.intensity;
            this.initialRange = this.Light.range;
            if (this.initialIntensity == 0)
            {
                return;
            }
            lastChkTime = Time.time;
            isInit = false;
        }

        void Update()
        {
            if (this.Light == null)
            {
                return;
            }
            if (initialScaleValueX == 0)
            {
                return;
            }
            if (this.initialIntensity == 0)
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
            this.curIntensity = this.initialIntensity * curScaleValueX / this.initialScaleValueX;
            this.Light.intensity = this.curIntensity;

            this.curRange = this.initialRange * curScaleValueX / this.initialScaleValueX;
            this.Light.range = this.curRange;
        }

    }
}