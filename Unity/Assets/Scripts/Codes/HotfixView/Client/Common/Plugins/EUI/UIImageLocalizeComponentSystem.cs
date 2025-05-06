using System;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(UIImageLocalizeComponent))]
    public static class UIImageLocalizeComponentSystem
    {
        [ObjectSystem]
        public class UIImageLocalizeComponentAwakeSystem : AwakeSystem<UIImageLocalizeComponent>
        {
            protected override void Awake(UIImageLocalizeComponent self)
            {
                UIImageLocalizeComponent.Instance = self;

                self.SetImageLocalizeAction();
            }
        }

        [ObjectSystem]
        public class UIImageLocalizeComponentDestroySystem : DestroySystem<UIImageLocalizeComponent>
        {
            protected override void Destroy(UIImageLocalizeComponent self)
            {
                self._UIImageLocalizeMonoViewList.Clear();
                if (UIImageLocalizeComponent.Instance == self)
                {
                    UIImageLocalizeComponent.Instance = null;
                }
            }
        }

        public static void AddUIImageLocalizeView(this UIImageLocalizeComponent self, GameObject go)
        {
            UIImageLocalizeMonoView[] list = go.GetComponentsInChildren<UIImageLocalizeMonoView>(true);
            foreach (UIImageLocalizeMonoView uiImageLocalizeMonoView in list)
            {
                if (uiImageLocalizeMonoView == null)
                {
                    continue;
                }
                self._UIImageLocalizeMonoViewList.Add(uiImageLocalizeMonoView);
                uiImageLocalizeMonoView.DoRefreshImageValue();
            }
        }

        public static void RemoveUIImageLocalizeView(this UIImageLocalizeComponent self, GameObject go)
        {
            if (go == null || self == null || self.IsDisposed)
            {
                return;
            }
            UIImageLocalizeMonoView[] list = go.GetComponentsInChildren<UIImageLocalizeMonoView>(true);
            foreach (UIImageLocalizeMonoView uiImageLocalizeMonoView in list)
            {
                if (uiImageLocalizeMonoView == null)
                {
                    continue;
                }
                self._UIImageLocalizeMonoViewList.Remove(uiImageLocalizeMonoView);
            }
        }

        public static void SetImageLocalizeAction(this UIImageLocalizeComponent self)
        {
            UIImageLocalizeMonoView.SetImageLocalizeAction(self.LoadImageLocalizeAction);
        }

        public static Sprite LoadImageLocalizeAction(this UIImageLocalizeComponent self, string language, string path)
        {
            if (language == "None")
            {
                language = ET.LocalizeComponent.Instance.CurrentLanguage.ToString();
            }
            string imgPath = $"{UIImageLocalizeMonoView.UI_MultiLanguagePath}/{language}/{path}";
            Sprite sprite = null;
            bool bRet = ResComponent.Instance.CheckLocationValid(imgPath);
            if (bRet)
            {
                sprite = UIManagerHelper.LoadSprite(imgPath);
            }

            if (sprite == null)
            {
                imgPath = $"{UIImageLocalizeMonoView.UI3D_MultiLanguagePath}/{language}/{path}";
                bRet = ResComponent.Instance.CheckLocationValid(imgPath);
                if (bRet)
                {
                    sprite = UIManagerHelper.LoadSprite(imgPath);
                }
            }
            return sprite;
        }

        public static void DoRefreshImageValue(this UIImageLocalizeComponent self)
        {
            foreach (UIImageLocalizeMonoView uiImageLocalizeMonoView in self._UIImageLocalizeMonoViewList)
            {
                if (uiImageLocalizeMonoView == null)
                {
                    continue;
                }
                uiImageLocalizeMonoView.DoRefreshImageValue();
            }
        }

    }
}