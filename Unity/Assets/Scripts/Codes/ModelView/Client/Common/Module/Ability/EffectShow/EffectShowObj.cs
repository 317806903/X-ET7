using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Ability.Client
{
    [ChildOf(typeof (EffectShowComponent))]
    [FriendOf(typeof(Unit))]
    public class EffectShowObj: Entity, IAwake, IDestroy
    {
        public GameObject go;

        private EntityRef<EffectObj> effectObj;
        public EffectObj RefEffectObj
        {
            get
            {
                return this.effectObj;
            }
            set
            {
                this.effectObj = value;
            }
        }

        private EntityRef<AudioPlayObj> audioPlayObj;
        public AudioPlayObj RefAudioPlayObj
        {
            get
            {
                return audioPlayObj;
            }
            set
            {
                this.audioPlayObj = value;
            }
        }

        public LineRenderer[] lineRenderers;
        public DigitalRuby.LightningBolt.LightningBoltScript[] lightningBoltScripts;
        public DigitalRuby.LightningBolt.LightningBoltScriptManager[] lightningBoltScriptManagers;
    }
}