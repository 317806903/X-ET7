using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UIRootManagerComponent))]
    public static class UIRootManagerComponentSystem
    {
        [ObjectSystem]
        public class UIRootManagerComponentAwakeSystem: AwakeSystem<UIRootManagerComponent>
        {
            protected override void Awake(UIRootManagerComponent self)
            {
                UIRootManagerComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class UIRootManagerComponentDestroySystem: DestroySystem<UIRootManagerComponent>
        {
            protected override void Destroy(UIRootManagerComponent self)
            {
                UIRootManagerComponent.Instance = null;
                self.UIRootRotationTranList.Clear();
            }
        }

        [ObjectSystem]
        public class UIRootManagerComponentUpdateSystem: UpdateSystem<UIRootManagerComponent>
        {
            protected override void Update(UIRootManagerComponent self)
            {
                if (self.UIRoot == null)
                {
                    return;
                }
                self.ChkUIRootRotation();
            }
        }

        public static async ETTask Init(this UIRootManagerComponent self, Camera UICamera, Transform UIRoot)
        {
            self.UICamera = UICamera;
            self.UIRoot = UIRoot;

            self.World3DRoot = UIRoot.Find("World3DRoot");
            self.WorldHubRoot = UIRoot.Find("WorldHubRoot");
            self.NormalRoot = UIRoot.Find("NormalRoot");
            self.PopUpRoot = UIRoot.Find("PopUpRoot");
            self.FixedRoot = UIRoot.Find("FixedRoot");
            self.NoticeRoot = UIRoot.Find("NoticeRoot");
            self.LoadingRoot = UIRoot.Find("LoadingRoot");
            self.HighestFixedRoot = UIRoot.Find("HighestFixedRoot");
            self.HighestNoticeRoot = UIRoot.Find("HighestNoticeRoot");

            self.AddUIRootRotationAll();

            await ETTask.CompletedTask;
        }


        public static void ChkUIRootRotation(this UIRootManagerComponent self)
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

        public static void AddUIRootRotationAll(this UIRootManagerComponent self)
        {
            return;
            self._AddUIRootRotation(self.WorldHubRoot);
            self._AddUIRootRotation(self.NormalRoot);
            self._AddUIRootRotation(self.PopUpRoot);
            self._AddUIRootRotation(self.FixedRoot);
            self._AddUIRootRotation(self.NoticeRoot);
            self._AddUIRootRotation(self.LoadingRoot);
            self._AddUIRootRotation(self.HighestFixedRoot);
            self._AddUIRootRotation(self.HighestNoticeRoot);
        }

        public static void _AddUIRootRotation(this UIRootManagerComponent self, Transform trans)
        {
            if (self.UIRootRotationTranList.Contains(trans))
            {
                return;
            }

            Canvas canvas = trans.gameObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                return;
            }
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = UIRootManagerComponent.Instance.UICamera;

            CanvasScaler canvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
            if (canvasScaler == null)
            {
                return;
            }

            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            self.UIRootRotationTranList.Add(trans);
            if (self.isInitUI)
            {
                self.SetUIRootRotation(trans);
            }
        }

        public static void AddUIRootRotation(this UIRootManagerComponent self, Transform trans)
        {
            Canvas canvas = trans.gameObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                return;
            }
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = UIRootManagerComponent.Instance.UICamera;
            //canvas.sortingOrder = 0;

            // CanvasScaler canvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
            // if (canvasScaler == null || canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
            // {
            //     canvas.sortingOrder = 0;
            //     return;
            // }

            //self._AddUIRootRotation(trans);
        }

        public static void SetUIRootRotationAll(this UIRootManagerComponent self)
        {
            while (true)
            {
                bool hasNull = false;
                foreach (Transform trans in self.UIRootRotationTranList)
                {
                    if (trans == null)
                    {
                        hasNull = true;
                        self.UIRootRotationTranList.Remove(trans);
                        break;
                    }
                    else
                    {
                        self.SetUIRootRotation(trans);
                    }
                }

                if (hasNull == false)
                {
                    break;
                }
            }
        }

        public static void SetUIRootRotation(this UIRootManagerComponent self, Transform trans)
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

        public static void SetUIRootPortrait(this UIRootManagerComponent self, Transform trans)
        {
            CanvasScaler canvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(self.designHeight, self.designWidth);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            //canvasScaler.matchWidthOrHeight = 0.25f;
            canvasScaler.matchWidthOrHeight = 0.5f;
        }

        public static void SetUIRootLandscape(this UIRootManagerComponent self, Transform trans)
        {
            CanvasScaler canvasScaler = trans.gameObject.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = new Vector2(self.designWidth, self.designHeight);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            //canvasScaler.matchWidthOrHeight = 0.75f;
            canvasScaler.matchWidthOrHeight = 0.5f;
        }

        public static async ETTask SetDefaultRotation(this UIRootManagerComponent self)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            // Screen.autorotateToPortrait = true;
            // Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            await TimerComponent.Instance.WaitFrameAsync();
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        public static async ETTask SetAutoRotation(this UIRootManagerComponent self)
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
        }
    }
}