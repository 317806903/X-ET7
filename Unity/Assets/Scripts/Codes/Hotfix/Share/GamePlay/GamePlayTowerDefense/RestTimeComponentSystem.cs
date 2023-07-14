using ET.Ability;
using System;
using System.Collections.Generic;

namespace ET
{
    [FriendOf(typeof(RestTimeComponent))]
    public static class RestTimeComponentSystem
	{
		[ObjectSystem]
		public class RestTimeComponentAwakeSystem : AwakeSystem<RestTimeComponent>
		{
			protected override void Awake(RestTimeComponent self)
			{
			}
		}
	
		[ObjectSystem]
		public class RestTimeComponentDestroySystem : DestroySystem<RestTimeComponent>
		{
			protected override void Destroy(RestTimeComponent self)
			{
			}
		}

		[ObjectSystem]
		public class RestTimeComponentFixedUpdateSystem: FixedUpdateSystem<RestTimeComponent>
		{
			protected override void FixedUpdate(RestTimeComponent self)
			{
				if (self.DomainScene().SceneType != SceneType.Map)
				{
					return;
				}

				float fixedDeltaTime = TimeHelper.FixedDetalTime;
				self.FixedUpdate(fixedDeltaTime);
			}
		}

		public static void Init(this RestTimeComponent self, float duration)
		{
			self.duration = duration;
			self.timeElapsed = 0;
		}

		public static void FixedUpdate(this RestTimeComponent self, float fixedDeltaTime)
		{
			if (self.duration > 0)
			{
				self.duration -= fixedDeltaTime;
			}
			
			self.timeElapsed += fixedDeltaTime;

			if (self.duration <= 0)
			{
				self.GetParent<GamePlayTowerDefenseComponent>().TransToBattle();
				return;
			}
		}

	}
}