using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemScaler: MonoBehaviour
    {
        private ParticleSystem particleSystem;
        private float initialScaleValueX;
        private float lastScaleValueX;
        public float curGravityModifierMultiplier;
        public float initialGravityModifierMultiplier;
        public float chkIntervalTime = 2f;
        private float lastChkTime;
        private bool isInit;

        void Awake()
        {
            this.particleSystem = GetComponent<ParticleSystem>();
            if (this.particleSystem == null)
            {
                return;
            }
            this.initialScaleValueX = transform.lossyScale.x;
            if (this.initialScaleValueX == 0)
            {
                return;
            }

            this.lastScaleValueX = 0;

            this.initialGravityModifierMultiplier = this.particleSystem.main.gravityModifierMultiplier;
            if (this.initialGravityModifierMultiplier == 0)
            {
                return;
            }
            lastChkTime = Time.time;
            isInit = false;
        }

        void Update()
        {
            if (this.particleSystem == null)
            {
                return;
            }
            if (initialScaleValueX == 0)
            {
                return;
            }
            if (this.initialGravityModifierMultiplier == 0)
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
            this.curGravityModifierMultiplier = this.initialGravityModifierMultiplier * curScaleValueX / this.initialScaleValueX;

            this.initialGravityModifierMultiplier = this.particleSystem.main.gravityModifierMultiplier;
            // 动态调整轨迹的宽度乘数
            var mainModule = this.particleSystem.main;
            mainModule.gravityModifierMultiplier = curGravityModifierMultiplier;
        }

    }
}