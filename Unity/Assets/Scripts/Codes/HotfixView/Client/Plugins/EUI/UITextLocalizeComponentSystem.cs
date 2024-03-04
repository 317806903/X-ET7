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
                uiTextLocalizeMonoView.SetGetTextKeyValueActionBack(self.getTextKeyValueActionBack);
                uiTextLocalizeMonoView.SetTextLocalizeAction(self.getTextKeyValue);
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

        public static void SetGetTextKeyValueActionBack(this UITextLocalizeComponent self, Func<string, Func<string, string, string>> getTextKeyValueActionBack)
        {
            self.getTextKeyValueActionBack = getTextKeyValueActionBack;
            foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in self._UITextLocalizeMonoViewList)
            {
                uiTextLocalizeMonoView.SetGetTextKeyValueActionBack(getTextKeyValueActionBack);
            }
        }

        public static void SetTextLocalizeAction(this UITextLocalizeComponent self, Func<string, string, string> getTextKeyValue)
        {
            self.getTextKeyValue = getTextKeyValue;
            foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in self._UITextLocalizeMonoViewList)
            {
                uiTextLocalizeMonoView.SetTextLocalizeAction(getTextKeyValue);
            }
        }

        public static void DoRefreshTextValue(this UITextLocalizeComponent self)
        {
            foreach (UITextLocalizeMonoView uiTextLocalizeMonoView in self._UITextLocalizeMonoViewList)
            {
                uiTextLocalizeMonoView.DoRefreshTextValue();
            }
        }

    }
}