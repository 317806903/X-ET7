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

    public class SelectHandle: DisposablClass
    {
        public SelectHandleType selectHandleType;
        public ListComponent<long> unitIds;
        public float3 direction;
        public float3 position;

        private bool _disposed; //表示是否已经被回收
        protected override void Dispose(bool disposing)
        {
            if(_disposed) return; //如果已经被回收，就中断执行
            if(disposing) //如果需要回收一些托管资源
            {
            }
            if (this.unitIds != null)
            {
                this.unitIds.Dispose();
                this.unitIds = null;
            }
            if (ObjectPool.Instance != null)
            {
                ObjectPool.Instance.Recycle(this);
            }

            _disposed = true;

            base.Dispose(disposing);//再调用父类的垃圾回收逻辑
        }

        private int _isHoldingCount;
        public void SetHolding(bool isHolding)
        {
            if (isHolding)
            {
                this._isHoldingCount++;
            }
            else
            {
                this._isHoldingCount--;
            }
        }

        public override void Dispose()
        {
            if (this._isHoldingCount > 0)
            {
                return;
            }
            base.Dispose();
        }

        public static SelectHandle Create()
        {
            try
            {
                SelectHandle selectHandle = ObjectPool.Instance.Fetch(typeof (SelectHandle)) as SelectHandle;
                selectHandle._disposed = false;
                selectHandle._isHoldingCount = 0;
                if (selectHandle.unitIds != null)
                {
                    selectHandle.unitIds.Dispose();
                    selectHandle.unitIds = null;
                }
                return selectHandle;
            }
            catch (Exception e)
            {
                Log.Error($"SelectHandle.Create Error: {e}");
                SelectHandle selectHandle = new ();
                selectHandle._disposed = false;
                selectHandle._isHoldingCount = 0;
                return selectHandle;
            }
        }

        public static SelectHandle Clone(SelectHandle selectHandle)
        {
            SelectHandle selectHandleNew = Create();
            selectHandleNew.selectHandleType = selectHandle.selectHandleType;
            selectHandleNew.direction = selectHandle.direction;
            selectHandleNew.position = selectHandle.position;
            if (selectHandle.unitIds != null)
            {
                selectHandleNew.unitIds = ListComponent<long>.Create();
                for (int i = 0; i < selectHandle.unitIds.Count; i++)
                {
                    selectHandleNew.unitIds.Add(selectHandle.unitIds[i]);
                }
            }
            return selectHandleNew;
        }
    }
}