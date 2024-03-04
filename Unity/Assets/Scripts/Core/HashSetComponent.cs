using System;
using System.Collections.Generic;

namespace ET
{
    public class HashSetComponent<T>: HashSet<T>, IDisposable
    {
        public HashSetComponent()
        {
        }

        public static HashSetComponent<T> Create()
        {
            HashSetComponent<T> hashSetComponent = ObjectPool.Instance.Fetch(typeof (HashSetComponent<T>)) as HashSetComponent<T>;
            hashSetComponent.Reuse();
            return hashSetComponent;
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

        ~HashSetComponent()
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
                Log.Error($"ET.HashSetComponent<T>.Dispose {e}");
            }
        }
    }
}