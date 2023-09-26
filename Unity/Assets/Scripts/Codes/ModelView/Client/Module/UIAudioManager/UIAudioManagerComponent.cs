using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Ability.Client
{
    public class UIAudioManagerComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public GameObject root;
        public AudioSource audioSource;
        public AudioSource musicSource;
        public List<string> resAudioCfgIds;
        public int index;
    }
}