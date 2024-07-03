using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ET.Client
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class PoolObject : MonoBehaviour
    {
        public string poolName;
        //defines whether the object is waiting in pool or is in use
        public bool isPooled;
        public List<ParticleSystem> particleSystemList;
        public List<TrailRenderer> trailRendererList;

        private void Awake()
        {
            ParticleSystem[] particleSystems = this.gameObject.GetComponentsInChildren<ParticleSystem>(true);
            if (particleSystems.Length > 0)
            {
                this.particleSystemList = new(particleSystems);
            }

            TrailRenderer[] trailRenderers = this.gameObject.GetComponentsInChildren<TrailRenderer>(true);
            if (trailRenderers.Length > 0)
            {
                this.trailRendererList = new(trailRenderers);
            }
        }
    }
}