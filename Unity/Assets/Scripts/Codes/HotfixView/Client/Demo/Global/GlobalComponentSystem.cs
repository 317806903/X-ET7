using UnityEngine;

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

                self.Global = GameObject.Find("/Global").transform;
                self.Unit = self.Global.Find("Unit").transform;
                self.MainCamera = self.Global.Find("MainCamera").GetComponent<Camera>();
                self.UICamera = self.Global.Find("UICamera").GetComponent<Camera>();

                self.NormalRoot = self.Global.Find("UIRoot/NormalRoot").transform;
                self.PopUpRoot = self.Global.Find("UIRoot/PopUpRoot").transform;
                self.FixedRoot = self.Global.Find("UIRoot/FixedRoot").transform;
                self.NoticeRoot = self.Global.Find("UIRoot/NoticeRoot").transform;
                self.LoadingRoot = self.Global.Find("UIRoot/LoadingRoot").transform;
                self.HighestNoticeRoot = self.Global.Find("UIRoot/HighestNoticeRoot").transform;

                self.ClientManagerRoot =  self.Global.Find("ClientManagerRoot").transform;
                self.PoolRoot =  self.Global.Find("PoolRoot").transform;
                self.DebugRoot =  self.Global.Find("DebugRoot").transform;

                self.DebugRoot.gameObject.SetActive(false);

                self.ShowDebugRoot();
            }
        }

        public static void ShowDebugRoot(this GlobalComponent self)
        {
            self.DebugRoot.gameObject.SetActive(false);
            ChkGesture chkGesture = self.Global.gameObject.GetComponent<ChkGesture>();
            chkGesture.doShow = () =>
            {
                self.DebugRoot.gameObject.SetActive(true);
            };
            chkGesture.doHide = () =>
            {
                self.DebugRoot.gameObject.SetActive(false);
            };
        }
    }
}