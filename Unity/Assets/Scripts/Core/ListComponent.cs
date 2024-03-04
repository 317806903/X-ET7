using System;
using System.Collections.Generic;

namespace ET
{
    public class ListComponent<T>: List<T>, IDisposable
    {
        public ListComponent()
        {
        }

        public static ListComponent<T> Create()
        {
            if (ObjectPool.Instance == null)
            {
                Log.Error($"ET.ListComponent<T>.Create ObjectPool.Instance == null");
                return new();
            }
            ListComponent<T> list = ObjectPool.Instance.Fetch(typeof (ListComponent<T>)) as ListComponent<T>;
            if (list == null)
            {
                Log.Error($"ET.ListComponent<T>.Create ObjectPool.Instance.Fetch == null");
                return new();
            }

            list.Reuse();
            return list;
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

        ~ListComponent()
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
                    if (this.Capacity <= 64)
                    {
                        ObjectPool.Instance?.Recycle(this);
                    }
                }
                else
                {
                    if (this.Capacity <= 64)
                    {
                        this.Clear();
                        if (ObjectPool.Instance != null)
                        {
                            ObjectPool.Instance.Recycle(this);
                            GC.ReRegisterForFinalize(this);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"ET.ListComponent<T>.Dispose {e}");
            }
        }
    }
}