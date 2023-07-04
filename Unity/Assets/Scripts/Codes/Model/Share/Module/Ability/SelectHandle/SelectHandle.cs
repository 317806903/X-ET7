using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    public enum SelectHandleType
    {
        SelectUnits,
        SelectDirection,
        SelectPosition,
    }

    public class SelectHandle: IDisposable
    {
        public SelectHandleType selectHandleType;
        public List<long> unitIds;
        public float3 direction;
        public float3 position;

        public static SelectHandle Create()
        {
            return ObjectPool.Instance.Fetch(typeof (SelectHandle)) as SelectHandle;
        }

        public void Dispose()
        {
            ObjectPool.Instance.Recycle(this);
        }
    }
}