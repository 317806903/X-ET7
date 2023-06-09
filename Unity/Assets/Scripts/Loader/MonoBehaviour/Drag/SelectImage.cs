using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectImage : MonoBehaviour, IPointerDownHandler
{
    public Action onPointerDown;
    // //需要被实例化的预制
    // public GameObject inistatePrefab;
    // //实例化后的对象
    // private GameObject inistateObj;
 
    // Use this for initialization
    void Start()
    {
        // if (inistatePrefab == null) return;
        
    }
    //实现鼠标按下的接口
    public void OnPointerDown(PointerEventData eventData)
    {
        this.onPointerDown?.Invoke();
        // //实例化预制
        // inistateObj = Instantiate(inistatePrefab) as GameObject;
        // inistateObj.name = inistatePrefab.name;
        //
        // //将当前需要被实例化的对象传递到管理器中
        // SelectObjManager.Instance.AttachNewObject(inistateObj);
    }
    //
    // void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    // {
    //     //实例化预制
    //     inistateObj = Instantiate(inistatePrefab) as GameObject;
    //     inistateObj.name = inistatePrefab.name;
    //
    //     //将当前需要被实例化的对象传递到管理器中
    //     SelectObjManager.Instance.AttachNewObject(inistateObj);
    // }
    //
    // void IDragHandler.OnDrag(PointerEventData eventData)
    // {
    //     //Debug.Log("OnBeginDrag");
    // }
}