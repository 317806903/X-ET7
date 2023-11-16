using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (DebugConnectComponent))]
    public static class DebugConnectComponentSystem
    {
        [ObjectSystem]
        public class DebugConnectComponentAwakeSystem: AwakeSystem<DebugConnectComponent>
        {
            protected override void Awake(DebugConnectComponent self)
            {
                DebugConnectComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DebugConnectComponentDestroySystem: DestroySystem<DebugConnectComponent>
        {
            protected override void Destroy(DebugConnectComponent self)
            {
                DebugConnectComponent.Instance = null;

            }
        }

        [ObjectSystem]
        public class DebugConnectComponentUpdateSystem: UpdateSystem<DebugConnectComponent>
        {
            protected override void Update(DebugConnectComponent self)
            {
            }
        }

    }
}