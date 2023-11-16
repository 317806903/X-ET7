using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPannel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform dragTarget;
    [SerializeField] private Canvas canvas;
    Vector3 lastUIPosition;
    private float needHoldTime = 0.1f;
    private float curHoldTime = 0;
    private bool isDraging = false;
    public bool needRaycast = false;

    void Start()
    {
        dragTarget = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this.isDraging == false)
        {
            Raycast(eventData, ExecuteEvents.dragHandler);
            return;
        }

        Vector3 uiPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(dragTarget, eventData.position, eventData.enterEventCamera, out uiPosition);
        dragTarget.position = uiPosition + this.lastUIPosition; //将当前时间摄像机的拖拽事件的位置赋值给当前UI
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.curHoldTime = Time.time + this.needHoldTime;
        this.isDraging = false;

        Raycast(eventData, ExecuteEvents.pointerDownHandler);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.isDraging)
        {
            return;
        }
        Raycast(eventData, ExecuteEvents.pointerUpHandler);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.isDraging)
        {
            return;
        }
        Raycast(eventData, ExecuteEvents.pointerClickHandler);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (this.curHoldTime == 0)
        {
        }
        else if (this.curHoldTime < Time.time)
        {

        }
        else
        {
            this.isDraging = false;
            Raycast(eventData, ExecuteEvents.beginDragHandler);
            return;
        }
        this.isDraging = true;

        Vector3 uiPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(dragTarget, eventData.position, eventData.enterEventCamera, out uiPosition);
        this.lastUIPosition = dragTarget.position - uiPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (this.isDraging)
        {
            this.curHoldTime = 0;
            this.isDraging = false;
        }
        else
        {
            Raycast(eventData, ExecuteEvents.endDragHandler);
        }
    }

    public bool PassEvent<T>(GameObject go, PointerEventData data, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
    {
        return ExecuteEvents.Execute(go, data, function);
    }

    private List<RaycastResult> _rawRaycastResults = new List<RaycastResult>();
    private void Raycast<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
    {
        if (this.needRaycast == false)
        {
            return;
        }
        _rawRaycastResults.Clear();
        EventSystem.current.RaycastAll(eventData, _rawRaycastResults);
        foreach (var rlt in _rawRaycastResults)
        {
            //Debug.Log(rlt.gameObject);
            //遮罩层自身需要添加该脚本，否则会导致ExecuteEvents.Execute再次触发遮罩层自身的IPointerClickHandler导致死循环
            if (rlt.gameObject == this.gameObject)
            {
                continue;
            }
            bool bRet = PassEvent(rlt.gameObject, eventData, function);
            if (bRet)
            {
                break;
            }
        }
    }

}