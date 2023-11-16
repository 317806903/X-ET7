using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace UIGuide
{
    public class EventTriggerListener: EventTrigger
    {

        #region 变量

        //带参数是为了方便取得绑定了UI事件的对象
        public delegate void UIDelegate(GameObject go);

        public UIDelegate onEnter;
        public UIDelegate onExit;
        public UIDelegate onDown;
        public UIDelegate onUp;
        public UIDelegate onClick;
        public UIDelegate onInitializePotentialDrag;
        public UIDelegate onBeginDrag;
        public UIDelegate onDrag;
        public UIDelegate onEndDrag;
        public UIDelegate onDrop;
        public UIDelegate onScroll;
        public UIDelegate onUpdateSelected;
        public UIDelegate onSelect;
        public UIDelegate onDeselect;
        public UIDelegate onMove;
        public UIDelegate onSubmit;
        public UIDelegate onCancel;

        #endregion

        public static EventTriggerListener GetListener(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null) listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        #region 方法

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null) onEnter(gameObject);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null) onExit(gameObject);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null) onDown(gameObject);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null) onUp(gameObject);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject);
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (onInitializePotentialDrag != null) onInitializePotentialDrag(gameObject);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (onBeginDrag != null) onBeginDrag(gameObject);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null) onDrag(gameObject);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null) onEndDrag(gameObject);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (onDrop != null) onDrop(gameObject);
        }

        public override void OnScroll(PointerEventData eventData)
        {
            if (onScroll != null) onScroll(gameObject);
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (onUpdateSelected != null) onUpdateSelected(gameObject);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            if (onSelect != null) onSelect(gameObject);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            if (onDeselect != null) onDeselect(gameObject);
        }

        public override void OnMove(AxisEventData eventData)
        {
            if (onMove != null) onMove(gameObject);
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            if (onSubmit != null) onSubmit(gameObject);
        }

        public override void OnCancel(BaseEventData eventData)
        {
            if (onCancel != null) onCancel(gameObject);
        }

        #endregion
    }
}