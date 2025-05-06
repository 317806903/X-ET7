using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    [FriendOf(typeof (DynamicAtlasManagerComponent))]
    public static class DynamicAtlasManagerComponentSystem
    {
        [ObjectSystem]
        public class DynamicAtlasManagerComponentAwakeSystem: AwakeSystem<DynamicAtlasManagerComponent>
        {
            protected override void Awake(DynamicAtlasManagerComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class DynamicAtlasManagerComponentDestroySystem: DestroySystem<DynamicAtlasManagerComponent>
        {
            protected override void Destroy(DynamicAtlasManagerComponent self)
            {
                if (self.root != null)
                {
                    GameObject.Destroy(self.root);
                    self.root = null;
                }
            }
        }

        public static void Init(this DynamicAtlasManagerComponent self)
        {
            GameObject go = new GameObject("DynamicAtlasManagerComponent");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;
            go.AddComponent<DynamicAtlasMono>();


            Game.AddSingleton<DaVikingCode.RectanglePacking.RectanglePackerMgr>();
            DynamicAtlas.DynamicAtlasMgr dynamicAtlasMgr = Game.AddSingleton<DynamicAtlas.DynamicAtlasMgr>();
            dynamicAtlasMgr.Status = true;

        }

    }
}