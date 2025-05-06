using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ET
{
    public class ObjectPool: Singleton<ObjectPool>, ISingletonAwake
    {
        private ConcurrentDictionary<Type, Pool> objPool;

        private readonly Func<Type, Pool> AddPoolFunc = type => new Pool(type);

        private Dictionary<Type, int> maxCount = new();
        private int defaultMaxCount = 4000;
        public void SetDefaultMaxCount(int defaultMaxCount)
        {
            this.defaultMaxCount = defaultMaxCount;
        }

        public int GetDefaultMaxCount()
        {
            return this.defaultMaxCount;
        }

        public void ResetMaxCount(Type type, int maxCount)
        {
            this.maxCount[type] = maxCount;

            Pool pool = GetPool(type);
            pool.ResetMaxCount(maxCount);
        }

        public int GetMaxCount(Type type)
        {
            if (this.maxCount.TryGetValue(type, out int maxCount))
            {
                return maxCount;
            }

            return this.GetDefaultMaxCount();
        }

        public void Awake()
        {
            lock (this)
            {
                objPool = new ConcurrentDictionary<Type, Pool>();
            }
        }

        public T Fetch<T>() where T : class
        {
            return this.Fetch(typeof (T)) as T;
        }

        public object Fetch(Type type, bool isFromPool = true)
        {
            if (!isFromPool)
            {
                return Activator.CreateInstance(type);
            }

            Pool pool = GetPool(type);
            object obj = pool.Get();
            return obj;
        }

        public void Recycle(object obj)
        {
            Type type = obj.GetType();
            Pool pool = GetPool(type);
            pool.Return(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Pool GetPool(Type type)
        {
            return this.objPool.GetOrAdd(type, AddPoolFunc);
        }

        public override void Dispose()
        {
            this.ClearAll();
            base.Dispose();
        }

        public void ClearAll()
        {
            this.objPool.Clear();
        }

        /// <summary>
        /// 线程安全的无锁对象池
        /// </summary>
        private class Pool
        {
            private readonly Type ObjectType;
            private int MaxCapacity;
            private int NumItems;
            private readonly ConcurrentQueue<object> _items = new();
            private object FastItem;

            public Pool(Type objectType)
            {
                int maxCapacity = Instance.GetMaxCount(objectType);
                ObjectType = objectType;
                MaxCapacity = maxCapacity;
            }

            public void ResetMaxCount(int maxCapacity)
            {
                if (MaxCapacity <= maxCapacity)
                {
                    MaxCapacity = maxCapacity;
                    return;
                }

                MaxCapacity = maxCapacity;
                if (_items.Count > maxCapacity)
                {
                    for (int i = _items.Count - maxCapacity; i > 0; i--)
                    {
                        if (_items.TryDequeue(out object item))
                        {
                            Interlocked.Decrement(ref NumItems);
                        }
                    }
                }
            }

            public object Get()
            {
                object item = FastItem;
                if (item == null || Interlocked.CompareExchange(ref FastItem, null, item) != item)
                {
                    if (_items.TryDequeue(out item))
                    {
                        Interlocked.Decrement(ref NumItems);
                        return item;
                    }

                    return Activator.CreateInstance(this.ObjectType);
                }

                return item;
            }

            public void Return(object obj)
            {
                if (FastItem != null || Interlocked.CompareExchange(ref FastItem, obj, null) != null)
                {
                    if (Interlocked.Increment(ref NumItems) <= MaxCapacity)
                    {
                        _items.Enqueue(obj);
                        return;
                    }

                    Interlocked.Decrement(ref NumItems);
                }
            }
        }
    }
}