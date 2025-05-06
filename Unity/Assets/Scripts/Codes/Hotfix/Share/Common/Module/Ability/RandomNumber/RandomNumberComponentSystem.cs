using System;
using System.Collections.Generic;
using System.Linq;
using ET.AbilityConfig;
using Unity.Mathematics;

namespace ET.Ability
{
    [FriendOf(typeof (RandomNumberComponent))]
    public static class RandomNumberComponentSystem
    {
        [ObjectSystem]
        public class RandomNumberComponentAwakeSystem: AwakeSystem<RandomNumberComponent>
        {
            protected override void Awake(RandomNumberComponent self)
            {
            }
        }

        [ObjectSystem]
        public class RandomNumberComponentDestroySystem: DestroySystem<RandomNumberComponent>
        {
            protected override void Destroy(RandomNumberComponent self)
            {
            }
        }

        public static int GetRandomNumber(this RandomNumberComponent self)
        {
            long clientFrameTime = TimeHelper.ClientFrameTime();
            if (self.clientFrameTime == clientFrameTime)
            {
                return self.randomNumber;
            }

            self.clientFrameTime = clientFrameTime;
            self.randomNumber = RandomGenerator.RandomNumber(0, 100);
            return self.randomNumber;
        }
    }
}