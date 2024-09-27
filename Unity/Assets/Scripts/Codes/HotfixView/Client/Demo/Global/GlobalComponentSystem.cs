using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (GlobalComponent))]
    public static class GlobalComponentSystem
    {
        [ObjectSystem]
        public class GlobalComponentAwakeSystem: AwakeSystem<GlobalComponent>
        {
            protected override void Awake(GlobalComponent self)
            {
                GlobalComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class GlobalComponentDestroySystem: DestroySystem<GlobalComponent>
        {
            protected override void Destroy(GlobalComponent self)
            {
                GlobalComponent.Instance = null;

                if (self.Global != null)
                {
                    GameObject.DestroyImmediate(self.Global.gameObject);
                    self.Global = null;
                }
            }
        }

        [ObjectSystem]
        public class GlobalComponentUpdateSystem: UpdateSystem<GlobalComponent>
        {
            protected override void Update(GlobalComponent self)
            {
                if (self.Global == null)
                {
                    return;
                }

                self.Update();
            }
        }

        public static async ETTask Init(this GlobalComponent self)
        {
            await self.CreateGlobalRoot();
            self.Unit = self.Global.Find("Unit").transform;
            self.UIRoot = self.Global.Find("UIRoot").transform;
            self.MainCamera = self.Global.Find("MainCamera").GetComponent<Camera>();
            self.UICamera = self.Global.Find("UICamera").GetComponent<Camera>();

            self.ChkApplicationStatus =  self.Global.Find("ChkApplicationStatus").transform;
            self.ClientManagerRoot =  self.Global.Find("ClientManagerRoot").transform;
            self.PoolRoot =  self.Global.Find("PoolRoot").transform;
            self.ErrerLogManagerRoot =  self.Global.Find("ErrerLogManagerRoot").transform;
            self.DebugRoot =  self.Global.Find("DebugRoot").transform;

            self.ShowDebugRoot();

            await self.AddComponents();
        }

        public static async ETTask CreateGlobalRoot(this GlobalComponent self)
        {
            if (self.Global != null)
            {
                return;
            }

            Transform initTrans = GameObject.Find("/Init").transform;
            GameObject go = ResComponent.Instance.LoadAsset<GameObject>("GlobalRoot");
            self.Global = GameObject.Instantiate(go).transform;
            self.Global.name = "GlobalRoot";
            self.Global.SetParent(initTrans);
            self.Global.localPosition = Vector3.zero;
            self.Global.localRotation = Quaternion.identity;
            self.Global.localScale = Vector3.one;
        }

        public static async ETTask AddComponents(this GlobalComponent self)
        {
            ApplicationStatusComponent applicationPauseComponent = self.AddComponent<ApplicationStatusComponent>();
            await applicationPauseComponent.Init(self.ChkApplicationStatus);

            UIRootManagerComponent uiRootManagerComponent = self.AddComponent<UIRootManagerComponent>();
            await uiRootManagerComponent.Init(self.UICamera, self.UIRoot);

            MainQualitySettingComponent mainQualitySettingComponent = self.AddComponent<MainQualitySettingComponent>();

            self.AddComponent<DynamicAtlasManagerComponent>();

            self.AddComponent<DebugConnectComponent>();

            DebugShowComponent debugShowComponent = self.AddComponent<DebugShowComponent>();
            await debugShowComponent.Init(self.DebugRoot);

            DebugWhenEditorComponent debugWhenEditorComponent = self.AddComponent<DebugWhenEditorComponent>();
            await debugWhenEditorComponent.Init(self.DebugRoot);
        }

        public static async ETTask SetUpdateFinished(this GlobalComponent self)
        {
            if (self.isUpdateFinished)
            {
                return;
            }

            self.isUpdateFinished = true;

            await self.AddComponentsAfterUpdate();
        }

        public static async ETTask AddComponentsAfterUpdate(this GlobalComponent self)
        {
            ResDefaultManagerComponent resDefaultManagerComponent = self.AddComponent<ResDefaultManagerComponent>();
            await resDefaultManagerComponent.Init();
        }

        public static void ShowDebugRoot(this GlobalComponent self)
        {
            self.DebugRoot.gameObject.SetActive(false);
            ChkGesture chkGesture = self.Global.gameObject.GetComponent<ChkGesture>();
            chkGesture.doShow = () =>
            {
                self.DebugRoot.gameObject.SetActive(true);
                self.ErrerLogManagerRoot.gameObject.SetActive(false);
            };
            chkGesture.doHide = () =>
            {
                self.DebugRoot.gameObject.SetActive(false);
                self.ErrerLogManagerRoot.gameObject.SetActive(true);
            };
        }

        public static bool ChkIsShowDebugRoot(this GlobalComponent self)
        {
            return self.DebugRoot.gameObject.activeInHierarchy;
        }

        public static void Update(this GlobalComponent self)
        {

        }
    }
}