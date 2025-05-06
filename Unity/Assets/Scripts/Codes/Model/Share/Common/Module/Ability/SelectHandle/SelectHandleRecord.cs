using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [ChildOf(typeof(SelectHandleRecordManager))]
    public class SelectHandleRecord: Entity, IAwake, IDestroy
    {
        public long unitId;
        public bool isResetPos;
        public float3 resetPos;
        public bool isResetForward;
        public float3 resetForward;
        public string selectObjectCfgId;
        public ListComponent<long> unitIds;
    }
}