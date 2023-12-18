using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (RecycleSelectHandleComponent))]
    public static class RecycleSelectHandleComponentSystem
    {
        [ObjectSystem]
        public class RecycleSelectHandleComponentAwakeSystem: AwakeSystem<RecycleSelectHandleComponent>
        {
            protected override void Awake(RecycleSelectHandleComponent self)
            {
                self.time2SelectHandle = new();
                self.timeList = new();
            }
        }

        [ObjectSystem]
        public class RecycleSelectHandleComponentDestroySystem: DestroySystem<RecycleSelectHandleComponent>
        {
            protected override void Destroy(RecycleSelectHandleComponent self)
            {
                self.time2SelectHandle.Clear();
                self.timeList.Clear();
            }
        }

        [ObjectSystem]
        public class RecycleSelectHandleComponentFixedUpdateSystem: FixedUpdateSystem<RecycleSelectHandleComponent>
        {
            protected override void FixedUpdate(RecycleSelectHandleComponent self)
            {
                if (self.IsDisposed || self.DomainScene().SceneType != SceneType.Map)
                {
                    return;
                }

                float fixedDeltaTime = TimeHelper.FixedDetalTime;
                self.FixedUpdate(fixedDeltaTime);
            }
        }

		public static void FixedUpdate(this RecycleSelectHandleComponent self, float fixedDeltaTime)
		{
			if (++self.curFrameRecycleSelectHandle >= self.waitFrameRecycleSelectHandle)
			{
				self.curFrameRecycleSelectHandle = 0;

				bool bContinue = false;
				do
				{
					bContinue = self.DoRecycleSelectHandle();
				}
				while (bContinue);
			}
		}

		public static bool DoRecycleSelectHandle(this RecycleSelectHandleComponent self)
		{
			if (self.timeList.Count == 0)
			{
				return false;
			}

			long recycleTime = self.timeList.Peek();
			if (recycleTime > TimeHelper.ServerFrameTime())
			{
				return false;
			}
			self.timeList.Dequeue();
			foreach (var selectHandle in self.time2SelectHandle[recycleTime])
			{
				selectHandle.Dispose();
			}

			self.time2SelectHandle.Remove(recycleTime);

			return true;
		}

		public static void AddRecycleSelectHandles(this RecycleSelectHandleComponent self, SelectHandle selectHandle)
		{
			long serverFrameTime = TimeHelper.ServerFrameTime() + self.nextRecycleTime * 1000;
			self.time2SelectHandle.Add(serverFrameTime, selectHandle);
			self.timeList.Enqueue(serverFrameTime);
		}
    }
}