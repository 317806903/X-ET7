using System;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(UITextLocalizeComponent))]
    public static class UITextLocalizeComponentSystem
    {
        [ObjectSystem]
        public class UITextLocalizeComponentAwakeSystem : AwakeSystem<UITextLocalizeComponent>
        {
            protected override void Awake(UITextLocalizeComponent self)
            {
                UITextLocalizeComponent.Instance = self;

                self.SetTextLocalizeAction();
            }
        }

        [ObjectSystem]
        public class UITextLocalizeComponentDestroySystem : DestroySystem<UITextLocalizeComponent>
        {
            protected override void Destroy(UITextLocalizeComponent self)
            {
                self._UITextLocalizeMonoViewList.Clear();
                if (UITextLocalizeComponent.Instance == self)
                {
                    UITextLocalizeComponent.Instance = null;
                }
            }
        }

        public static void AddUITextLocalizeView(this UITextLocalizeComponent self, GameObject go)
        {
            UITextLocalizeMonoView[] list = go.GetComponentsInChildren<UITextLocalizeMonoView>(true);
            foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in list)
            {
                if (uiTextLocalizeMonoView == null)
                {
                    continue;
                }
                self._UITextLocalizeMonoViewList.Add(uiTextLocalizeMonoView);
                uiTextLocalizeMonoView.DoRefreshTextValue();
            }
        }

        public static void RemoveUITextLocalizeView(this UITextLocalizeComponent self, GameObject go)
        {
            if (go == null || self == null || self.IsDisposed)
            {
                return;
            }
            UITextLocalizeMonoView[] list = go.GetComponentsInChildren<UITextLocalizeMonoView>(true);
            foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in list)
            {
                if (uiTextLocalizeMonoView == null)
                {
                    continue;
                }
                self._UITextLocalizeMonoViewList.Remove(uiTextLocalizeMonoView);
            }
        }

        public static void SetTextLocalizeAction(this UITextLocalizeComponent self)
        {
            UITextLocalizeMonoView.SetTextLocalizeAction(self.LoadTextLocalizeAction);
        }

        public static string LoadTextLocalizeAction(this UITextLocalizeComponent self, string language, string key, string originText)
        {
            if (language == "None")
            {
                language = ET.LocalizeComponent.Instance.CurrentLanguage.ToString();
            }
            LanguageType languageType = (LanguageType)Enum.Parse(typeof(LanguageType), language);
            var translateUI = LocalizeComponent.Instance.GetCurrentTranslator_UI();

            return translateUI(languageType, key, originText);
        }

        public static void DoRefreshTextValue(this UITextLocalizeComponent self)
        {
            foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in self._UITextLocalizeMonoViewList)
            {
                if (uiTextLocalizeMonoView == null)
                {
                    continue;
                }
                uiTextLocalizeMonoView.DoRefreshTextValue();
            }
        }

    }
}