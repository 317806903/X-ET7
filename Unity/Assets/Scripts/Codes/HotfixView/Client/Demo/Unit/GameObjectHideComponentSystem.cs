using System;
using System.Collections.Generic;
using ET.Ability;
using ET.AbilityConfig;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    public static class GameObjectHideComponentSystem
    {
        [ObjectSystem]
        public class AwakeSystem: AwakeSystem<GameObjectHideComponent>
        {
            protected override void Awake(GameObjectHideComponent self)
            {
                self.GetGo()?.SetActive(false);
            }
        }

        [ObjectSystem]
        public class DestroySystem: DestroySystem<GameObjectHideComponent>
        {
            protected override void Destroy(GameObjectHideComponent self)
            {
                self.GetGo()?.SetActive(true);
            }
        }

        public static GameObject GetGo(this GameObjectHideComponent self)
        {
            GameObjectShowComponent gameObjectShowComponent = self.GetParent<GameObjectShowComponent>();
            return gameObjectShowComponent?.GetGo();
        }

    }
}