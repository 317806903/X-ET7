using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (ResDefaultManagerComponent))]
    public static class ResDefaultManagerComponentSystem
    {
        [ObjectSystem]
        public class ResDefaultManagerComponentAwakeSystem: AwakeSystem<ResDefaultManagerComponent>
        {
            protected override void Awake(ResDefaultManagerComponent self)
            {
                ResDefaultManagerComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class ResDefaultManagerComponentDestroySystem: DestroySystem<ResDefaultManagerComponent>
        {
            protected override void Destroy(ResDefaultManagerComponent self)
            {
                ResDefaultManagerComponent.Instance = null;
            }
        }

        [ObjectSystem]
        public class ResDefaultManagerComponentUpdateSystem: UpdateSystem<ResDefaultManagerComponent>
        {
            protected override void Update(ResDefaultManagerComponent self)
            {

            }
        }

        public static async ETTask Init(this ResDefaultManagerComponent self)
        {
            await self.ResetTMPDefaultSpriteAsset();
        }

        public static async ETTask ResetTMPDefaultSpriteAsset(this ResDefaultManagerComponent self)
        {
            string resPath = "Assets/ResAB/Font/TMPSprite/CoinSprite.asset";
            TMPro.TMP_SpriteAsset spriteAsset = await ResComponent.Instance.LoadAssetAsync<TMPro.TMP_SpriteAsset>(resPath);
            var tmpSettingsType = typeof(TMPro.TMP_Settings);
            var defaultSpriteAssetField = tmpSettingsType.GetField("m_defaultSpriteAsset", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic| System.Reflection.BindingFlags.Instance);
            var tp1 = defaultSpriteAssetField.GetValue(TMPro.TMP_Settings.GetSettings());
            defaultSpriteAssetField.SetValue(TMPro.TMP_Settings.GetSettings(), spriteAsset);
            var tp2 = defaultSpriteAssetField.GetValue(TMPro.TMP_Settings.GetSettings());
            await ETTask.CompletedTask;
        }

    }
}