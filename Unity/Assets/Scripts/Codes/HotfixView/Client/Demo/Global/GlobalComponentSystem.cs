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

                self.Awake().Coroutine();
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

            }
        }

        public static async ETTask Awake(this GlobalComponent self)
        {
            await self.CreateGlobalRoot();
            self.Unit = self.Global.Find("Unit").transform;
            self.MainCamera = self.Global.Find("MainCamera").GetComponent<Camera>();
            self.UICamera = self.Global.Find("UICamera").GetComponent<Camera>();

            self.ClientManagerRoot =  self.Global.Find("ClientManagerRoot").transform;
            self.PoolRoot =  self.Global.Find("PoolRoot").transform;
            self.ErrerLogManagerRoot =  self.Global.Find("ErrerLogManagerRoot").transform;
            self.DebugRoot =  self.Global.Find("DebugRoot").transform;

            self.ShowDebugRoot();

            await self.AddComponents();

            self.AddIngameDebugConsoleCommand();
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
            UIManagerComponent uiManagerComponent = self.AddComponent<UIManagerComponent>();
            await uiManagerComponent.Init(self.UICamera, self.Global.Find("UIRoot"));

            MainQualitySettingComponent mainQualitySettingComponent = self.AddComponent<MainQualitySettingComponent>();

            self.AddComponent<DebugConnectComponent>();
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

        public static void AddIngameDebugConsoleCommand(this GlobalComponent self)
        {
            IngameDebugConsole.DebugLogConsole.AddCommand("SeeDebugConnectList", "SeeDebugConnectList desc", () => ET.Client.DebugConnectHelper.SeeDebugConnectList());
            IngameDebugConsole.DebugLogConsole.AddCommand("SeeCurDebugConnect", "SeeCurDebugConnect desc", () => ET.Client.DebugConnectHelper.SeeCurDebugConnect());
            IngameDebugConsole.DebugLogConsole.AddCommand("SetDebugConnectNull", "SetDebugConnectNull desc", () => ET.Client.DebugConnectHelper.SetDebugConnectNull());
            IngameDebugConsole.DebugLogConsole.AddCommand<string>( "SetDebugConnect", "SetDebugConnect desc", (str) => ET.Client.DebugConnectHelper.SetDebugConnect(str) );
        }

        public static void Test11(this GlobalComponent self)
        {
            int i = 0;
        }

        public static void Test22(this GlobalComponent self, string str)
        {
            int j = 4;
        }

    }
}