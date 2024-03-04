using System;
using System.Collections.Generic;

namespace ET
{
    public class DictionaryComponent<T1, T2>: Dictionary<T1, T2>, IDisposable
    {
        public DictionaryComponent()
        {
        }

        public static DictionaryComponent<T1, T2> Create()
        {
            DictionaryComponent<T1, T2> DictionaryComponent = ObjectPool.Instance.Fetch(typeof (DictionaryComponent<T1, T2>)) as DictionaryComponent<T1, T2>;
            DictionaryComponent.Reuse();
            return DictionaryComponent;
        }

        public bool isDisposed; //表示是否已经被回收

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        public void Reuse()
        {
            isDisposed = false;
            //GC.ReRegisterForFinalize(this);
        }

        ~DictionaryComponent()
        {
            Dispose(false);
        }

        protected void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return; //如果已经被回收，就中断执行
            }
            try
            {
                isDisposed = true;
                if(disposing)
                {
                    this.Clear();
                    ObjectPool.Instance?.Recycle(this);
                }
                else
                {
                    this.Clear();
                    if (ObjectPool.Instance != null)
                    {
                        ObjectPool.Instance.Recycle(this);
                        GC.ReRegisterForFinalize(this);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"ET.DictionaryComponent<T>.Dispose {e}");
            }
        }
    }
}