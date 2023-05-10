using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (EffectObj))]
    public static class EffectObjSystem
    {
        [ObjectSystem]
        public class EffectObjAwakeSystem: AwakeSystem<EffectObj>
        {
            protected override void Awake(EffectObj self)
            {
            }
        }

        [ObjectSystem]
        public class EffectObjDestroySystem: DestroySystem<EffectObj>
        {
            protected override void Destroy(EffectObj self)
            {
            }
        }

        public static void Init(this EffectObj self, string buffCfgId)
        {
            
        }

        public static void FixedUpdate(this EffectObj self, float fixedDeltaTime)
        {
            float timePassed = fixedDeltaTime;
            if (self.permanent == false) self.duration -= timePassed;
            self.timeElapsed += timePassed;
        }

        public static bool ChkNeedRemove(this EffectObj self)
        {
            //只要duration <= 0，不管是否是permanent都移除掉
            if (self.duration <= 0)
            {
                return true;
            }

            return false;
        }
    }
}