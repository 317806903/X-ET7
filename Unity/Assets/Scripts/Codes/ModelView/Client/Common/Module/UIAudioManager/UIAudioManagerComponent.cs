using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public class UIAudioManagerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public GameObject root;
        public AudioSource audioSource;
        public AudioSource musicSource;
        public List<string> resAudioCfgIds;
        public List<float> resAudioPitchs;
        public int index;
        public List<string> resAudioCfgIds_heighest;
        public List<float> resAudioPitchs_heighest;
        public int index_heighest;

        public float audioPitch;

        public bool isMute;

        public bool isLoop;
    }
}