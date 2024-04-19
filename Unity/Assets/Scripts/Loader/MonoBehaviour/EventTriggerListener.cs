using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    public class EventTriggerListener:
        MonoBehaviour,
        IPointerClickHandler,
        IPointerDownHandler,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerUpHandler,
        ISelectHandler,
        IUpdateSelectedHandler,
        IDeselectHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IScrollHandler,
        IMoveHandler
    {
        public delegate void UIEventHandle<T>(GameObject go, T eventData) where T : BaseEventData;

        public class UIEvent<T> where T : BaseEventData
        {
            public UIEvent()
            {
            }

            public void AddListener(UIEventHandle<T> handle, bool clear = true)
            {
                if (clear)
                {
                    RemoveAllListeners();
                }
                m_UIEventHandle += handle;
            }

            public void RemoveListener(UIEventHandle<T> handle)
            {
                m_UIEventHandle -= handle;
            }

            public void RemoveAllListeners()
            {
                m_UIEventHandle -= m_UIEventHandle;
                m_UIEventHandle = null;
            }

            public void Invoke(GameObject go, T eventData)
            {
                m_UIEventHandle?.Invoke(go, eventData);
            }

            public bool IsNull()
            {
                return m_UIEventHandle == null;
            }

            private event UIEventHandle<T> m_UIEventHandle = null;
        }

        public UIEvent<PointerEventData> onClick = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onDoubleClick = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onPress = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onUp = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onDown = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onEnter = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onExit = new UIEvent<PointerEventData>();
        public UIEvent<BaseEventData> onSelect = new UIEvent<BaseEventData>();
        public UIEvent<BaseEventData> onUpdateSelect = new UIEvent<BaseEventData>();
        public UIEvent<BaseEventData> onDeselect = new UIEvent<BaseEventData>();
        public UIEvent<PointerEventData> onBeginDrag = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onDrag = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onEndDrag = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onDrop = new UIEvent<PointerEventData>();
        public UIEvent<PointerEventData> onScroll = new UIEvent<PointerEventData>();
        public UIEvent<AxisEventData> onMove = new UIEvent<AxisEventData>();

        public static EventTriggerListener Get(GameObject go)
        {
            if (go == null)
            {
                return null;
            }

            EventTriggerListener eventTrigger = go.GetComponent<EventTriggerListener>();
            if (eventTrigger == null) eventTrigger = go.AddComponent<EventTriggerListener>();
            return eventTrigger;
        }

        private void Update()
        {
            if (m_IsPointDown)
            {
                if (onPress.IsNull() == false)
                {
                    if (Time.unscaledTime - m_CurrDonwTime >= PRESS_TIME)
                    {
                        m_IsPress = true;
                        m_IsPointDown = false;
                        m_CurrDonwTime = 0f;
                        onPress.Invoke(gameObject, null);
                    }
                }
            }

            if (m_ClickCount > 0)
            {
                if (Time.unscaledTime - m_CurrDonwTime >= DOUBLE_CLICK_TIME)
                {
                    if (m_ClickCount < 2)
                    {
                        onUp.Invoke(gameObject, m_OnUpEventData);
                        onClick.Invoke(gameObject, m_OnUpEventData);
                        m_OnUpEventData = null;
                    }

                    m_ClickCount = 0;
                }

                if (m_ClickCount >= 2)
                {
                    onDoubleClick.Invoke(gameObject, m_OnUpEventData);
                    if (onDoubleClick.IsNull() && this.onClick.IsNull() == false)
                    {
                        onClick.Invoke(gameObject, m_OnUpEventData);
                    }
                    m_OnUpEventData = null;
                    m_ClickCount = 0;
                }
            }
        }

        private void OnDestroy()
        {
            RemoveAllListeners();
        }

        public void RemoveAllListeners()
        {
            m_OnUpEventData = null;
            m_ClickCount = 0;
            m_ClickCountDone = false;

            onClick.RemoveAllListeners();
            onDoubleClick.RemoveAllListeners();
            onPress.RemoveAllListeners();
            onUp.RemoveAllListeners();
            onDown.RemoveAllListeners();
            onEnter.RemoveAllListeners();
            onExit.RemoveAllListeners();
            onSelect.RemoveAllListeners();
            onUpdateSelect.RemoveAllListeners();
            onDeselect.RemoveAllListeners();
            onBeginDrag.RemoveAllListeners();
            onDrag.RemoveAllListeners();
            onEndDrag.RemoveAllListeners();
            onDrop.RemoveAllListeners();
            onScroll.RemoveAllListeners();
            onMove.RemoveAllListeners();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_OnUpEventData = null;
            m_ClickCountDone = false;

            m_IsPointDown = true;
            m_IsPointExit = false;
            m_IsPress = false;
            m_CurrDonwTime = Time.unscaledTime;
            onDown?.Invoke(gameObject, eventData);
            if (this.onDown.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.pointerDownHandler);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_IsPointDown = false;
            m_OnUpEventData = eventData;

            if (!m_IsPress)
            {
                if (this.m_IsPointExit == false)
                {
                    if (m_ClickCountDone == false)
                    {
                        m_ClickCount++;
                        m_ClickCountDone = true;
                    }
                }
            }
            if (this.onUp.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.pointerUpHandler);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.onClick.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.pointerClickHandler);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter.Invoke(gameObject, eventData);
            if (this.onEnter.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.pointerEnterHandler);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //m_IsPointDown = false;
            m_IsPointExit = true;
            onExit.Invoke(gameObject, eventData);
            if (this.onExit.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.pointerExitHandler);
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelect.Invoke(gameObject, eventData);
            if (this.onSelect.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.selectHandler);
            }
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            onUpdateSelect.Invoke(gameObject, eventData);
            if (this.onUpdateSelect.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.updateSelectedHandler);
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            onDeselect.Invoke(gameObject, eventData);
            if (this.onDeselect.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.deselectHandler);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.beginDragPos = eventData.position;
            onBeginDrag.Invoke(gameObject, eventData);
            if (this.onBeginDrag.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.beginDragHandler);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            float disX = Math.Abs(this.beginDragPos.x - eventData.position.x);
            float disY = Math.Abs(this.beginDragPos.y - eventData.position.y);
            //Log.Debug($"---OnDrag {disX} {disY}");
            if (this.m_IsHorizontalDrag)
            {
                if (disX > 30)
                {
                    if (disX > disY)
                    {
                        this.m_IsPointDown = false;
                    }
                }
            }
            else
            {
                if (disY > 30)
                {
                    if (disY > disX)
                    {
                        this.m_IsPointDown = false;
                    }
                }
            }

            onDrag.Invoke(gameObject, eventData);
            if (this.onDrag.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.dragHandler);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke(gameObject, eventData);
            if (this.onEndDrag.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.endDragHandler);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            onDrop.Invoke(gameObject, eventData);
            if (this.onDrop.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.dropHandler);
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            onScroll.Invoke(gameObject, eventData);
            if (this.onScroll.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.scrollHandler);
            }
        }

        public void OnMove(AxisEventData eventData)
        {
            onMove.Invoke(gameObject, eventData);
            if (this.onMove.IsNull())
            {
                PassEvent(eventData, ExecuteEvents.moveHandler);
            }
        }

        private const float DOUBLE_CLICK_TIME = 0.2f;
        public float PRESS_TIME = 0.1f;

        private float m_CurrDonwTime = 0f;
        public bool m_IsHorizontalDrag = true;
        private Vector2 beginDragPos;
        private bool m_IsPointDown = false;
        private bool m_IsPointExit = false;
        private bool m_IsPress = false;
        private int m_ClickCount = 0;
        private bool m_ClickCountDone = false;
        private PointerEventData m_OnUpEventData = null;

        List<RaycastResult> result = new ();
        List<GameObject> resultGo = new ();
        HashSet<GameObject> record = new ();

        GameObject PassEvent<T>(BaseEventData data, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
        {
            PointerEventData eventData = data as PointerEventData;
            if (eventData == null)
            {
                GameObject nextGo = ExecuteEvents.GetEventHandler<T>(this.gameObject.transform.parent.gameObject);
                ExecuteEvents.Execute(nextGo, data, function);
                return null;
            }
            var pointerGo = eventData.pointerCurrentRaycast.gameObject ?? eventData.pointerDrag;
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, result);
            try
            {
                resultGo.Clear();
                foreach (RaycastResult raycastResult in this.result)
                {
                    resultGo.Add(raycastResult.gameObject);
                }
                if (typeof(T) == typeof(UnityEngine.EventSystems.IEndDragHandler))
                {
                    Transform parentTrans = this.gameObject.transform.parent;
                    while (parentTrans != null)
                    {
                        if (ExecuteEvents.GetEventHandler<IDragHandler>(parentTrans.gameObject))
                        {
                            resultGo.Insert(0, parentTrans.gameObject);
                            break;
                        }
                        else
                        {
                            parentTrans = parentTrans.parent;
                        }
                    }
                }
                foreach (var go in resultGo)
                {
                    if (go != null && go != pointerGo && record.Contains(go) == false)
                    {
                        record.Add(go);
                        var excuteGo = ExecuteEvents.GetEventHandler<T>(go);
                        if (excuteGo)
                        {
                            if (excuteGo.TryGetComponent<UITouchPass>(out var __))
                                return null;
                            ExecuteEvents.Execute(excuteGo, data, function);
                            return excuteGo;
                        }
                        else
                        {
                            if (go.TryGetComponent<UnityEngine.UI.Graphic>(out var com))
                            {
                                if (com.raycastTarget)
                                    return null;
                            }
                        }
                    }
                }
            }
            finally
            {
                result.Clear();
                resultGo.Clear();
                record.Clear();
            }

            return null;
        }
    }
}