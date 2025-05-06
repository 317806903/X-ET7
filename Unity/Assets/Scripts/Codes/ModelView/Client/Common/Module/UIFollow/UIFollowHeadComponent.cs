using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class UIFollowHeadComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Transform transUIRoot;
        public Camera MainCamera { get; set; }
        public UIFollowHeadCfg uiFollowHeadCfg;
        public Vector3 offset;

        public bool isNeedMove;
        public Vector3 lastRecordPosition;
        public Quaternion lastRecordRotation;

        public bool isStaying;
        public int chkStayCount = 0;
        public int chkStayIndex;
        public Vector3 lastPosition;
        public Quaternion lastRotation;

        public float startTime;
        public Vector3 targetPosition;

        public bool isForceReset;
    }
}