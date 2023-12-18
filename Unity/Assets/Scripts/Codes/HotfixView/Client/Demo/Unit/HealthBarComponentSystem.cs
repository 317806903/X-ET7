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
                bool isHome = false;
                if (self.GetUnit().GetComponent<HomeComponent>() != null)
                {
                    isHome = true;
                }
                else if (self.GetUnit().GetComponent<TowerComponent>() != null)
                {
                    isHome = true;
                }

                self.isHome = isHome;
                if (self.isHome)
                {
                    //self.AddComponent<HealthBarHomeComponent>();
                    self.AddComponent<HomeHealthBarComponent>();
                }
                else
                {
                    self.AddComponent<HealthBarNormalComponent>();
                }
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

        public static void UpdateHealth(this HealthBarComponent self, bool isInit)
        {
            if (self.isHome)
            {
                //self.GetComponent<HealthBarHomeComponent>()?.UpdateHealth(isInit);
                self.GetComponent<HomeHealthBarComponent>()?.UpdateHealth(isInit);
            }
            else
            {
                self.GetComponent<HealthBarNormalComponent>().UpdateHealth(isInit);
            }
        }
    }
}