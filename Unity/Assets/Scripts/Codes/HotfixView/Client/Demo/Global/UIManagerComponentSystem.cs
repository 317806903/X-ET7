using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UIManagerComponent))]
    public static class UIManagerComponentSystem
    {
        [ObjectSystem]
        public class UIManagerComponentAwakeSystem: AwakeSystem<UIManagerComponent>
        {
            protected override void Awake(UIManagerComponent self)
            {
                UIManagerComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class UIManagerComponentDestroySystem: DestroySystem<UIManagerComponent>
        {
            protected override void Destroy(UIManagerComponent self)
            {
                UIManagerComponent.Instance = null;

            }
        }

        [ObjectSystem]
        public class UIManagerComponentUpdateSystem: UpdateSystem<UIManagerComponent>
        {
            protected override void Update(UIManagerComponent self)
            {
                if (self.UIRoot == null)
                {
                    return;
                }
                self.ChkUIRootRotation();
            }
        }

        public static async ETTask Init(this UIManagerComponent self, Camera UICamera, Transform UIRoot)
        {
            self.UICamera = UICamera;
            self.UIRoot = UIRoot;

            self.WorldHubRoot = UIRoot.Find("WorldHubRoot").transform;
            self.NormalRoot = UIRoot.Find("NormalRoot").transform;
            self.PopUpRoot = UIRoot.Find("PopUpRoot").transform;
            self.FixedRoot = UIRoot.Find("FixedRoot").transform;
            self.NoticeRoot = UIRoot.Find("NoticeRoot").transform;
            self.LoadingRoot = UIRoot.Find("LoadingRoot").transform;
            self.HighestNoticeRoot = UIRoot.Find("HighestNoticeRoot").transform;

            await ETTask.CompletedTask;
        }


        public static void ChkUIRootRotation(this UIManagerComponent self)
        {
            if (Screen.width < Screen.height)
            {
                if (self.isInitUI == false)
                {
                    self.isInitUI = true;
                    self.isLandscape = false;
                    self.SetUIRootRotationAll();
                }
                else if (self.isLandscape)
                {
                    self.isLandscape = false;
                    self.SetUIRootRotationAll();
                }
            }
            else
            {
                if (self.isInitUI == false)
                {
                    self.isInitUI = true;
                    self.isLandscape = true;
                    self.SetUIRootRotationAll();
                }
                if (self.isLandscape == false)
                {
                    self.isLandscape = true;
                    self.SetUIRootRotationAll();
                }
            }
        }

        public static void SetUIRootRotationAll(this UIManagerComponent self)
        {
            self.SetUIRootRotation(self.WorldHubRoot);
            self.SetUIRootRotation(self.NormalRoot);
            self.SetUIRootRotation(self.PopUpRoot);
            self.SetUIRootRotation(self.FixedRoot);
            self.SetUIRootRotation(self.NoticeRoot);
            self.SetUIRootRotation(self.LoadingRoot);
            self.SetUIRootRotation(self.HighestNoticeRoot);
        }

        public static void SetUIRootRotation(this UIManagerComponent self, Transform trans)
        {
            if (self.isLandscape == false)
            {
                self.SetUIRootPortrait(trans);
            }
            else
            {
                self.SetUIRootLandscape(trans);
            }
        }

        public static void SetUIRootPortrait(this UIManagerComponent self, Transform trans)
        {
            CanvasScaler canvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(self.designHeight, self.designWidth);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
        }

        public static void SetUIRootLandscape(this UIManagerComponent self, Transform trans)
        {
            CanvasScaler canvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(self.designWidth, self.designHeight);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;
        }
    }
}