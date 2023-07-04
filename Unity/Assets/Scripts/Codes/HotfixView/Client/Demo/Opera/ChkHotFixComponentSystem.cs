using System;
using Unity.Mathematics;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(ChkHotFixComponent))]
    public static class ChkHotFixComponentSystem
    {
        [ObjectSystem]
        public class ChkHotFixComponentAwakeSystem : AwakeSystem<ChkHotFixComponent>
        {
            protected override void Awake(ChkHotFixComponent self)
            {
            }
        }

        [ObjectSystem]
        public class ChkHotFixComponentUpdateSystem : UpdateSystem<ChkHotFixComponent>
        {
            protected override void Update(ChkHotFixComponent self)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    CodeLoader.Instance.LoadHotfix();
                    EventSystem.Instance.Load();
                    Log.Debug("hot reload success!");
                }
            }
        }
    }
}