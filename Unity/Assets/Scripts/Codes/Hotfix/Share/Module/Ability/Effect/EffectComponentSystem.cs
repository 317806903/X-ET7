using System;
using System.Collections.Generic;

namespace ET.Ability
{
    [FriendOf(typeof (EffectComponent))]
    [FriendOf(typeof (EffectObj))]
    public static class EffectComponentSystem
    {
        [ObjectSystem]
        public class EffectComponentAwakeSystem: AwakeSystem<EffectComponent>
        {
            protected override void Awake(EffectComponent self)
            {
                self.removeList = new();
                
            }
        }

        [ObjectSystem]
        public class EffectComponentDestroySystem: DestroySystem<EffectComponent>
        {
            protected override void Destroy(EffectComponent self)
            {
                self.removeList.Clear();
            }
        }

        public static EffectObj AddEffect(this EffectComponent self, string effectCfgId)
        {
            EffectObj effectObj = self.AddChild<EffectObj>();
            effectObj.Init(effectCfgId);
            // EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnAwake()
            // {
            //     buff = effectObj,
            // });
            // EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnStart()
            // {
            //     buff = effectObj,
            // });

            return effectObj;
        }

        public static void FixedUpdate(this EffectComponent self, float fixedDeltaTime)
        {
            if (self.Children.Count <= 0)
            {
                return;
            }

            self.removeList.Clear();
            foreach (var effectObjs in self.Children)
            {
                EffectObj effectObj = effectObjs.Value as EffectObj;
                effectObj.FixedUpdate(fixedDeltaTime);

                if (effectObj.ChkNeedRemove())
                {
                    // EventSystem.Instance.Publish(self.DomainScene(), new AbilityTriggerEventType.BuffOnDestroy() { buff = effectObj });
                    self.removeList.Add(effectObj);
                }
            }

            int count = self.removeList.Count;
            for (int i = 0; i < count; i++)
            {
                self.removeList[i].Dispose();
            }

            self.removeList.Clear();
        }
    }
}