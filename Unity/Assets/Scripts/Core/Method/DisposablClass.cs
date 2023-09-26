using System;
using System.Reflection;

namespace ET
{
    public class DisposablClass : IDisposable
    {
        //是否回收完毕
        bool _disposed;
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposablClass()
        {
            Dispose(false);
        }

        //这里的参数表示示是否需要释放那些实现IDisposable接口的托管对象
        protected virtual void Dispose(bool disposing)
        {
            if(_disposed) return; //如果已经被回收，就中断执行
            if(disposing)
            {
                //TODO:释放那些实现IDisposable接口的托管对象
            }
            //TODO：回收非托管资源，把之设置为null，等待CLR调用析构函数的时候回收
            _disposed = true;
        }
    }
}

