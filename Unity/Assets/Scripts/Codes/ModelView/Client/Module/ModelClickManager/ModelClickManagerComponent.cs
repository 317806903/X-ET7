using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET.Client
{
    public enum ClickDataField
    {
        DataType,
        PlayerId,
        UnitId,
        UnitCfgId,
        TowerCfgId,
    }

    public enum ClickDataType
    {
        HomeWhenPutting,
        MonsterCallWhenPutting,
        Home,
        MonsterCall,
        Tower,
        PlayerUnit,
    }

    public class ModelClickManagerComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public ReferenceSimpleData recordReferenceSimpleData;

        public Action<RaycastHit> ModelClick;
        public Action<RaycastHit> ModelPress;

        public float RayMaxDistance = 3000;
        public GameObject root;

        public long startTime;
        public bool click;
        public bool needChkDownAgain;
        public UnityEngine.Vector3 inputPosition;
        public GameObject downObj;
        public GameObject upObj;

        public long unixBaseMillis = new DateTime(1970, 1, 1, 0, 0, 0).ToFileTimeUtc() / 10000;
        public DateTime lastCacheDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
        public long lastCacheMillis = 0;
    }
}