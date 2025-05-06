using System;
using System.Threading;

namespace ET
{

    public class MainThreadSynchronizationContext: Singleton<MainThreadSynchronizationContext>, ISingletonUpdate
    {
        private readonly ThreadSynchronizationContext threadSynchronizationContext = new ThreadSynchronizationContext();

#if UNITY_EDITOR 
        [StaticField] private static SynchronizationContext USC;
#endif
        
        public MainThreadSynchronizationContext()
        {
#if UNITY_EDITOR 
            USC = SynchronizationContext.Current;
#endif
            SynchronizationContext.SetSynchronizationContext(this.threadSynchronizationContext);
        }
        
        public void Update()
        {
            this.threadSynchronizationContext.Update();
        }
        
        public void Post(SendOrPostCallback callback, object state)
        {
            this.Post(() => callback(state));
        }
		
        public void Post(Action action)
        {
            this.threadSynchronizationContext.Post(action);
        }

        public override void Dispose()
        {
            base.Dispose();
#if UNITY_EDITOR 
            SynchronizationContext.SetSynchronizationContext(USC); 
#endif
        }
    }
}