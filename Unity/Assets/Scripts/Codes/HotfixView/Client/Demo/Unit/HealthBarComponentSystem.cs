using System;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class HealthBarComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<HealthBarComponent>
        {
            protected override void Awake(HealthBarComponent self)
            {
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<HealthBarComponent>
        {
            protected override void Destroy(HealthBarComponent self)
            {
            }
        }

        public static Unit GetUnit(this HealthBarComponent self)
        {
            return self.GetParent<Unit>();
        }

        public static async ETTask Init(this HealthBarComponent self)
        {
            while (TimeHelper.ClientNow() > TimeHelper.ClientFrameTime() + 200)
            {
                //await TimerComponent.Instance.WaitFrameAsync();
                await TimerComponent.Instance.WaitAsync(200);
                if (self.IsDisposed)
                {
                    return;
                }
            }

            bool isHome = false;
            if (self.GetUnit().GetComponent<HomeComponent>() != null)
            {
                isHome = true;
            }
            else if (self.GetUnit().GetComponent<TowerComponent>() != null)
            {
                //isHome = true;
                isHome = false;
            }

            self.isHome = isHome;
            if (self.isHome)
            {
                //self.AddComponent<HealthBarHomeComponent>();
                HomeHealthBarComponent homeHealthBarComponent = self.AddComponent<HomeHealthBarComponent>();
                await homeHealthBarComponent.Init();
            }
            else
            {
                HealthBarNormalComponent healthBarNormalComponent = self.AddComponent<HealthBarNormalComponent>();
                await healthBarNormalComponent.Init();
            }
        }

        public static void UpdateHealth(this HealthBarComponent self, bool isInit)
        {
            if (self.isHome)
            {
                //self.GetComponent<HealthBarHomeComponent>()?.UpdateHealth(isInit);
                UIAudioManagerHelper.PlayUIAudio(self.DomainScene(), SoundEffectType.Attacked);
                self.GetComponent<HomeHealthBarComponent>()?.UpdateHealth(isInit);
            }
            else
            {
                self.GetComponent<HealthBarNormalComponent>().UpdateHealth(isInit);
            }
        }
    }
}