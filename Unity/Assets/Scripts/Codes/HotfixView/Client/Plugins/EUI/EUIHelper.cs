using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET.Client
{
    public static class EUIHelper
    {
        #region UI辅助方法

        public static void SetText(this Text Label, string content)
        {
            if (null == Label)
            {
                Log.Error("label is null");
                return;
            }

            Label.text = content;
        }

        public static void SetVisibleWithScale(this UIBehaviour uiBehaviour, bool isVisible)
        {
            if (null == uiBehaviour)
            {
                Log.Error("uibehaviour is null!");
                return;
            }

            if (null == uiBehaviour.gameObject)
            {
                Log.Error("uiBehaviour gameObject is null!");
                return;
            }

            if (uiBehaviour.gameObject.activeSelf == isVisible)
            {
                return;
            }

            uiBehaviour.transform.localScale = isVisible? Vector3.one : Vector3.zero;
        }

        public static void SetVisible(this UIBehaviour uiBehaviour, bool isVisible)
        {
            if (null == uiBehaviour)
            {
                Log.Error("uibehaviour is null!");
                return;
            }

            if (null == uiBehaviour.gameObject)
            {
                Log.Error("uiBehaviour gameObject is null!");
                return;
            }

            if (uiBehaviour.gameObject.activeSelf == isVisible)
            {
                return;
            }

            uiBehaviour.gameObject.SetActive(isVisible);
        }

        public static void SetVisible(this LoopScrollRect loopScrollRect, bool isVisible, int count = 0)
        {
            loopScrollRect.gameObject.SetActive(isVisible);
            loopScrollRect.totalCount = count;
            loopScrollRect.RefillCells();
        }

        public static void SetSrcollMiddle(this LoopHorizontalScrollRect loopHorizontalScrollRect)
        {
            RectTransform contentRectTrans = loopHorizontalScrollRect.content;
            if (contentRectTrans.childCount == 0)
            {
                return;
            }
            int count = loopHorizontalScrollRect.totalCount;
            if (count > loopHorizontalScrollRect.prefabSource.poolSize || count > contentRectTrans.childCount)
            {
                loopHorizontalScrollRect.horizontal = true;
                contentRectTrans.GetComponent<HorizontalLayoutGroup>().padding.left = 0;
                return;
            }

            // HorizontalLayoutGroup左边距 = 循环列表宽度的一半 - （每个Item宽度的一半*总共ITem数量+（总共ITem数量-1)*间隔距离一半）
            float scrollRectWidth = loopHorizontalScrollRect.transform.GetComponent<RectTransform>().rect.width;

            Transform itemTransform = contentRectTrans.GetChild(0).GetComponent<Transform>();
            float spacing = contentRectTrans.GetComponent<HorizontalLayoutGroup>().spacing;
            float cellWidth = itemTransform.GetComponent<RectTransform>().rect.width * itemTransform.localScale.x;
            float leftoffset = (scrollRectWidth - (cellWidth * count + (count - 1) * spacing) * contentRectTrans.localScale.x)*0.5f / contentRectTrans.localScale.x;
            if (leftoffset < 0)
            {
                loopHorizontalScrollRect.horizontal = true;
                leftoffset = 0;
            }
            else
            {
                loopHorizontalScrollRect.horizontal = false;
            }
            contentRectTrans.GetComponent<HorizontalLayoutGroup>().padding.left = (int)(leftoffset);
        }

        public static void SetSrcollRight(this LoopHorizontalScrollRect loopHorizontalScrollRect)
        {
            RectTransform contentRectTrans = loopHorizontalScrollRect.content;
            if (contentRectTrans.childCount == 0)
            {
                return;
            }
            int count = loopHorizontalScrollRect.totalCount;
            if (count > loopHorizontalScrollRect.prefabSource.poolSize || count > contentRectTrans.childCount)
            {
                loopHorizontalScrollRect.horizontal = true;
                contentRectTrans.GetComponent<HorizontalLayoutGroup>().padding.left = 0;
                return;
            }

            // HorizontalLayoutGroup左边距 = 循环列表宽度的一半 - （每个Item宽度的一半*总共ITem数量+（总共ITem数量-1)*间隔距离一半）
            float scrollRectWidth = loopHorizontalScrollRect.transform.GetComponent<RectTransform>().rect.width;

            Transform itemTransform = contentRectTrans.GetChild(0).GetComponent<Transform>();
            float spacing = contentRectTrans.GetComponent<HorizontalLayoutGroup>().spacing;
            float cellWidth = itemTransform.GetComponent<RectTransform>().rect.width * itemTransform.localScale.x;
            float leftoffset = (scrollRectWidth - (cellWidth * count + (count - 1) * spacing) * contentRectTrans.localScale.x - 10) / contentRectTrans.localScale.x;
            if (leftoffset < 0)
            {
                loopHorizontalScrollRect.horizontal = true;
                leftoffset = 0;
            }
            else
            {
                loopHorizontalScrollRect.horizontal = false;
            }
            contentRectTrans.GetComponent<HorizontalLayoutGroup>().padding.left = (int)(leftoffset);
        }

        public static void SetVisibleWithScale(this Transform transform, bool isVisible)
        {
            if (null == transform)
            {
                Log.Error("uibehaviour is null!");
                return;
            }

            if (null == transform.gameObject)
            {
                Log.Error("uiBehaviour gameObject is null!");
                return;
            }

            transform.localScale = isVisible? Vector3.one : Vector3.zero;
        }

        public static void SetVisible(this Transform transform, bool isVisible)
        {
            if (null == transform)
            {
                Log.Error("uibehaviour is null!");
                return;
            }

            if (null == transform.gameObject)
            {
                Log.Error("uiBehaviour gameObject is null!");
                return;
            }

            if (transform.gameObject.activeSelf == isVisible)
            {
                return;
            }

            transform.gameObject.SetActive(isVisible);
        }

        public static void SetTogglesInteractable(this ToggleGroup toggleGroup, bool isEnable)
        {
            var toggles = toggleGroup.transform.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].interactable = isEnable;
            }
        }

        public static (int, Toggle) GetSelectedToggle(this ToggleGroup toggleGroup)
        {
            var togglesList = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < togglesList.Length; i++)
            {
                if (togglesList[i].isOn)
                {
                    return (i, togglesList[i]);
                }
            }

            Log.Error("none Toggle is Selected");
            return (-1, null);
        }

        public static void SetToggleSelected(this ToggleGroup toggleGroup, int index)
        {
            var togglesList = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < togglesList.Length; i++)
            {
                if (i != index)
                {
                    continue;
                }

                togglesList[i].IsSelected(true);
            }
        }

        public static void IsSelected(this Toggle toggle, bool isSelected)
        {
            toggle.isOn = isSelected;
            toggle.onValueChanged?.Invoke(isSelected);
        }

        public static void RemoveUIScrollItems<K, T>(this K self, ref Dictionary<int, T> dictionary) where K : Entity, IUILogic, IUIDlg
                where T : Entity, IUIScrollItem
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }

            dictionary.Clear();
            dictionary = null;
        }

        public static void GetUIComponent<T>(this ReferenceCollector rf, string key, ref T t) where T : class
        {
            GameObject obj = rf.Get<GameObject>(key);

            if (obj == null)
            {
                t = null;
                return;
            }

            t = obj.GetComponent<T>();
        }

        #endregion

        #region UI按钮事件

        public static void AddListenerAsyncWithId(this Button button, Func<int, ETTask> action, int id)
        {
            button.onClick.RemoveAllListeners();

            async ETTask clickActionAsync()
            {
                UIEventComponent.Instance?.SetUIClicked(true);
                await action(id);
                UIEventComponent.Instance?.SetUIClicked(false);
            }

            button.onClick.AddListener(() =>
            {
                if (UIEventComponent.Instance == null)
                {
                    return;
                }

                if (UIEventComponent.Instance.IsClicked)
                {
                    return;
                }

                clickActionAsync().Coroutine();
            });
        }

        public static void AddListenerAsync(this Button button, Func<ETTask> action)
        {
            button.onClick.RemoveAllListeners();

            async ETTask clickActionAsync()
            {
                UIEventComponent.Instance?.SetUIClicked(true);
                try
                {
                    await action();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }

                UIEventComponent.Instance?.SetUIClicked(false);
            }

            button.onClick.AddListener(() =>
            {
                if (UIEventComponent.Instance == null)
                {
                    return;
                }

                if (UIEventComponent.Instance.IsClicked)
                {
                    return;
                }

                clickActionAsync().Coroutine();
            });
        }

        public static void AddListener(this Toggle toggle, UnityAction<bool> selectEventHandler)
        {
            toggle.onValueChanged.RemoveAllListeners();
            toggle.onValueChanged.AddListener(selectEventHandler);
        }

        public static void AddListener(this Slider slider, UnityAction<float> valueChgEventHandler)
        {
            slider.onValueChanged.RemoveAllListeners();
            if (valueChgEventHandler != null)
            {
                slider.onValueChanged.AddListener(valueChgEventHandler);
            }
        }

        public static void AddListener(this TMPro.TMP_Dropdown dropdown, UnityAction<int> valueChgEventHandler)
        {
            dropdown.onValueChanged.RemoveAllListeners();
            if (valueChgEventHandler != null)
            {
                dropdown.onValueChanged.AddListener(valueChgEventHandler);
            }
        }

        public static void AddListener(this Button button, UnityAction clickEventHandler)
        {
            button.onClick.RemoveAllListeners();
            if (clickEventHandler != null)
            {
                button.onClick.AddListener(clickEventHandler);
            }
        }

        public static void AddListenerWithId(this Button button, Action<int> clickEventHandler, int id)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(id); });
        }

        public static void AddListenerWithId(this Button button, Action<long> clickEventHandler, long id)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(id); });
        }

        public static void AddListenerWithParam<T>(this Button button, Action<T> clickEventHandler, T param)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(param); });
        }

        public static void AddListenerWithParam<T, A>(this Button button, Action<T, A> clickEventHandler, T param1, A param2)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { clickEventHandler(param1, param2); });
        }

        public static void AddListener(this ToggleGroup toggleGroup, UnityAction<int> selectEventHandler)
        {
            var togglesList = toggleGroup.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < togglesList.Length; i++)
            {
                int index = i;
                togglesList[i].AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        selectEventHandler(index);
                    }
                });
            }
        }

        /// <summary>
        /// 注册窗口关闭事件
        /// </summary>
        /// <OtherParam name="self"></OtherParam>
        /// <OtherParam name="closeButton"></OtherParam>
        public static void RegisterCloseEvent<T>(this Entity self, Button closeButton, bool isClose = false) where T : Entity, IAwake, IUILogic, IUIDlg
        {
            closeButton.onClick.RemoveAllListeners();
            if (isClose)
            {
                closeButton.onClick.AddListener(() =>
                {
                    UIManagerHelper.GetUIComponent(self.DomainScene()).CloseWindow(self.GetParent<UIBaseWindow>().WindowID);
                });
            }
            else
            {
                closeButton.onClick.AddListener(() =>
                {
                    UIManagerHelper.GetUIComponent(self.DomainScene()).HideWindow(self.GetParent<UIBaseWindow>().WindowID);
                });
            }
        }

        public static void RegisterEvent(this EventTrigger trigger, EventTriggerType eventType, UnityAction<BaseEventData> callback)
        {
            EventTrigger.Entry entry = null;

            // 查找是否已经存在要注册的事件
            foreach (EventTrigger.Entry existingEntry in trigger.triggers)
            {
                if (existingEntry.eventID == eventType)
                {
                    entry = existingEntry;
                    break;
                }
            }

            // 如果这个事件不存在，就创建新的实例
            if (entry == null)
            {
                entry = new EventTrigger.Entry();
                entry.eventID = eventType;
            }

            // 添加触发回调并注册事件
            entry.callback.AddListener(callback);
            trigger.triggers.Add(entry);
        }

        #endregion


        public static void ChgTMPColor(this Transform trans, bool isEnough)
        {
            if (trans == null)
            {
                return;
            }
            TextMeshProUGUI textMeshProUGUI = trans.gameObject.GetComponent<TextMeshProUGUI>();
            if (textMeshProUGUI == null)
            {
                return;
            }

            textMeshProUGUI.ChgTMPColor(isEnough);
        }

        public static void ChgTMPColor(this TextMeshProUGUI textMeshProUGUI, bool isEnough)
        {
            if (isEnough)
            {
                textMeshProUGUI.color = Color.white;
            }
            else
            {
                textMeshProUGUI.color = Color.red;
            }
        }

        public static void ChgTMPText(this Transform trans, string txt)
        {
            if (trans == null)
            {
                return;
            }
            TextMeshProUGUI textMeshProUGUI = trans.gameObject.GetComponent<TextMeshProUGUI>();
            if (textMeshProUGUI == null)
            {
                return;
            }

            textMeshProUGUI.ChgTMPText(txt);
        }

        public static void ChgTMPText(this TextMeshProUGUI textMeshProUGUI, string txt)
        {
            textMeshProUGUI.text = txt;
        }

        public static void SetRtAnchorSafe(this RectTransform rt, Vector2 anchorMin, Vector2 anchorMax)
        {
            if (anchorMin.x < 0 || anchorMin.x > 1 || anchorMin.y < 0 || anchorMin.y > 1 || anchorMax.x < 0 || anchorMax.x > 1 || anchorMax.y < 0 || anchorMax.y > 1)
                return;

            var lp = rt.localPosition;
            //注意不要直接用sizeDelta因为该值会随着anchor改变而改变
            var ls = new Vector2(rt.rect.width, rt.rect.height);

            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;

            //动态改变anchor后size和localPostion可能会发生变化需要重新设置
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ls.x);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ls.y);
            rt.localPosition = lp;
        }

        public static Vector3[] corners = new Vector3[4];
        public static Vector3 GetRectTransformMidTop(this RectTransform rt)
        {
            rt.GetWorldCorners(corners);
            return (corners[1] + corners[2]) * 0.5f;
        }
    }
}