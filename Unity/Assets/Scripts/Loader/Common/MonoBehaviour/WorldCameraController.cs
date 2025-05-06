using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class WorldCameraController : MonoBehaviour
{
    // Text m_debugTip;
    public bool canRotation_X = true;
    public bool canRotation_Y = true;
    public bool canScale = true;

    #region Field and Property

    /// <summary>
    /// Around center.
    /// </summary>
    public Transform target;
    public float clientScale;
    public float targetHalfHeight = 0;


    /// <summary>
    /// Settings of mouse button, pointer and scrollwheel.
    /// </summary>
    public MouseSettings MouseSettings = new MouseSettings(0, 10, 10);
    // public MouseSettings MouseSettings
    // {
    //     get
    //     {
    //         return new MouseSettings(this.mouseSettings.mouseButtonID, this.mouseSettings.pointerSensitivity * this.clientScale, this.mouseSettings.wheelSensitivity * this.clientScale);
    //     }
    // }

    /// <summary>
    /// Range limit of angle.
    /// </summary>
    public Range angleRange = new Range(0, 90);

    /// <summary>
    /// Range limit of distance.
    /// </summary>
    public Range distanceRange = new Range(2, 10);
    public Range DistanceRange
    {
        get
        {
            return new Range(this.distanceRange.min * this.clientScale, this.distanceRange.max * this.clientScale);
        }
    }

    /// <summary>
    /// Damper for move and rotate.
    /// </summary>
    [Range(0, 10)] public float Damper = 5;

    // public float Damper
    // {
    //     get
    //     {
    //         return this.damper * this.clientScale;
    //     }
    // }

    /// <summary>
    /// Damper for move and rotate.
    /// </summary>
    [Range(0, 10)] public float DamperTargetMove = 5;
    // public float DamperTargetMove
    // {
    //     get
    //     {
    //         return this.damperTargetMove * this.clientScale;
    //     }
    // }

    /// <summary>
    /// Camera current angls.
    /// </summary>
    public Vector2 CurrentAngles { protected set; get; }

    /// <summary>
    /// Current distance from camera to target.
    /// </summary>
    public float CurrentDistance { protected set; get; }

    private Vector3 CurrentTargetPos { set; get; }

    /// <summary>
    /// Camera target angls.
    /// </summary>
    protected Vector2 targetAngles;

    /// <summary>
    /// Target distance from camera to target.
    /// </summary>
    protected float targetDistance;

    private bool bClickUGUI = false;

    #endregion

    #region Protected Method

    protected virtual void Start()
    {
        if (target == null)
        {
            return;
        }

        CurrentAngles = targetAngles = transform.eulerAngles;
        CurrentDistance = targetDistance = Vector3.Distance(transform.position + new Vector3(0, this.targetHalfHeight, 0), target.position);
    }

    public void ForceSetPosition(Transform target, float targetHalfHeight, float dis, Vector3 eulerAngles, float clientScale)
    {
        this.target = target;
        this.clientScale =  clientScale;
        this.targetHalfHeight = targetHalfHeight * this.clientScale;
        CurrentAngles = targetAngles = eulerAngles;
        CurrentDistance = targetDistance = dis * this.clientScale;
    }

    protected virtual void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
#if UNITY_EDITOR
        AroundByMouseInput();
#elif UNITY_ANDROID || UNITY_IPHONE
        AroundByMobileInput();
#endif
    }

    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;

    private bool m_IsSingleFinger;
    /*
    private void ScaleCamera()
    {
        //计算出当前两点触摸点的位置
        var tempPosition1 = Input.GetTouch(0).position;
        var tempPosition2 = Input.GetTouch(1).position;
        float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
        float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);
        //计算上次和这次双指触摸之间的距离差距
        //然后去更改摄像机的距离
        distance -= ( currentTouchDistance - lastTouchDistance ) * scaleFactor * Time.deltaTime;
        //把距离限制住在min和max之间
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        //备份上一次触摸点的位置，用于对比
        oldPosition1 = tempPosition1;
        oldPosition2 = tempPosition2;
    }
    */

    private List<RaycastResult> results = new List<RaycastResult>();
    private PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

    private bool IsPointerOverUIObject()
    {
        eventDataCurrentPosition.position = GetTouchPosition(0);
        results.Clear(); // Just in case
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer.Equals(LayerMask.NameToLayer("UI")))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsClickUGUI()
    {
        if (EventSystem.current)
        {
#if UNITY_EDITOR
            if (this.bClickUGUI)
            {
                var mouse = UnityEngine.InputSystem.Mouse.current;
                if (this.MouseSettings.mouseButtonID == 0)
                {
                    if (mouse.leftButton.isPressed == false)
                    {
                        this.bClickUGUI = false;
                    }
                }
                else
                {
                    if (mouse.rightButton.isPressed == false)
                    {
                        this.bClickUGUI = false;
                    }
                }
            }
            else
            {
                this.bClickUGUI = EventSystem.current.IsPointerOverGameObject();
            }
#elif UNITY_ANDROID || UNITY_IPHONE
            if (this.bClickUGUI)
            {
                if (this.GetTouchCount() == 0)
                {
                    this.bClickUGUI = false;
                }
            }
            else
            {
                if (this.GetTouchCount() > 0)
                {
                    this.bClickUGUI = IsPointerOverUIObject();
                }
            }
#endif
            return this.bClickUGUI;
        }

        return false;
    }

    int GetTouchCount()
    {
        var ts = UnityEngine.InputSystem.Touchscreen.current;
        int touchCount = 0;
        foreach (var touch in ts.touches)
        {
            if (touch.press.isPressed || touch.press.wasReleasedThisFrame)
            {
                touchCount++;
            }
        }
        return touchCount;
    }

    public static Vector2 GetTouchPosition(int i)
    {
        var ts = UnityEngine.InputSystem.Touchscreen.current;
        return ts.touches[i].position.ReadValue();
    }

    private bool isPressLeft = false;//是否刚刚开始旋转
    private Vector3 oldPosition;
    private Vector3 newPosition;
    protected void AroundByMobileInput()
    {
        if (IsClickUGUI())
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            int touchCount = GetTouchCount();
            if (touchCount == 1)
            {
                isPressLeft = false;
            }
            else if (touchCount == 2)
            {
                var tc = ts.touches[1];
                if (isPressLeft == false)
                {
                    oldPosition = tc.position.ReadValue();
                    isPressLeft = true;
                }
                else
                {
                    newPosition = tc.position.ReadValue();
                    if (tc.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
                    {
                        targetAngles.y += (newPosition.x - oldPosition.x) * 0.1f * this.MouseSettings.pointerSensitivity;
                        targetAngles.x -= (newPosition.y - oldPosition.y) * 0.1f * this.MouseSettings.pointerSensitivity;

                        //Range.
                        targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);
                    }
                    oldPosition = newPosition;
                }

            }

        }
        else
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            int touchCount = GetTouchCount();
            UnityEngine.InputSystem.Controls.TouchControl firstTouch;
            UnityEngine.InputSystem.Controls.TouchControl secTouch;
            if (touchCount == 1)
            {
                firstTouch = ts.touches[0];
                if (firstTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    targetAngles.y += firstTouch.delta.x.ReadValue() * this.MouseSettings.pointerSensitivity;
                    targetAngles.x -= firstTouch.delta.y.ReadValue() * this.MouseSettings.pointerSensitivity;

                    //Range.
                    targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);
                }

                //Mouse pointer.
                m_IsSingleFinger = true;
            }

            //Mouse scrollwheel.
            if (canScale)
            {
                if (touchCount > 1)
                {
                    firstTouch = ts.touches[0];
                    secTouch = ts.touches[1];

                    //计算出当前两点触摸点的位置
                    if (m_IsSingleFinger)
                    {
                        oldPosition1 = firstTouch.position.ReadValue();
                        oldPosition2 = secTouch.position.ReadValue();
                    }

                    if (firstTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved && secTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
                    {
                        var tempPosition1 = firstTouch.position.ReadValue();
                        var tempPosition2 = secTouch.position.ReadValue();


                        float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
                        float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

                        //计算上次和这次双指触摸之间的距离差距
                        //然后去更改摄像机的距离
                        targetDistance -= (currentTouchDistance - lastTouchDistance) * Time.deltaTime * this.MouseSettings.wheelSensitivity * this.clientScale;
                        //  m_debugTip.text = ( currentTouchDistance - lastTouchDistance ).ToString() + " + " + targetDistance.ToString();


                        //把距离限制住在min和max之间


                        //备份上一次触摸点的位置，用于对比
                        oldPosition1 = tempPosition1;
                        oldPosition2 = tempPosition2;
                        m_IsSingleFinger = false;
                    }
                }
            }
        }

        targetDistance = Mathf.Clamp(targetDistance, this.DistanceRange.min, this.DistanceRange.max);

        //Lerp.
        bool bLerpPos = true;
        float dis = (CurrentAngles - targetAngles).sqrMagnitude;
        if (dis > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentAngles = Vector2.Lerp(CurrentAngles, targetAngles, this.Damper * Time.deltaTime);

        float dis2 = Mathf.Abs(CurrentDistance - targetDistance);
        if (dis2 > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentDistance = Mathf.Lerp(CurrentDistance, targetDistance, this.Damper * Time.deltaTime);


        if (!canRotation_X) targetAngles.y = 0;
        if (!canRotation_Y) targetAngles.x = 0;


        //Update transform position and rotation.
        transform.rotation = Quaternion.Euler(CurrentAngles);

        Vector3 CurrentTargetPosTmp = target.position + new Vector3(0, this.targetHalfHeight, 0) - transform.forward * CurrentDistance;
        if (bLerpPos)
        {
            CurrentTargetPos = Vector3.Lerp(CurrentTargetPos, CurrentTargetPosTmp, this.DamperTargetMove * Time.deltaTime);
        }
        else
        {
            CurrentTargetPos = Vector3.Lerp(CurrentTargetPos, CurrentTargetPosTmp, 0.5f);
        }


        //transform.position = CurrentTargetPos;
        if (IsEqualVector3(transform.position, CurrentTargetPos, 0.1f))
        {
            transform.position = CurrentTargetPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, CurrentTargetPos, 0.5f);
        }
        //transform.position = target.position + new Vector3(0, 0.5f, 0) - transform.forward * CurrentDistance;
        // transform.position = target.position - Vector3.forward * CurrentDistance;
    }

    private bool isFirstClick = true;
    /// <summary>
    /// Camera around target by mouse input.
    /// </summary>
    protected void AroundByMouseInput()
    {
        if (IsClickUGUI() == false)
        {
            bool isPress = false;
            var mouse = UnityEngine.InputSystem.Mouse.current;
            if (this.MouseSettings.mouseButtonID == 0)
            {
                if (mouse.leftButton.isPressed)
                {
                    isPress = true;
                }
            }
            else
            {
                if (mouse.rightButton.isPressed)
                {
                    isPress = true;
                }
            }
            if (isPress)
            {
                if (isFirstClick)
                {
                    isFirstClick = false;
                    return;
                }

                //Mouse pointer.
                targetAngles.y += mouse.delta.x.ReadValue() * this.MouseSettings.pointerSensitivity;
                targetAngles.x -= mouse.delta.y.ReadValue() * this.MouseSettings.pointerSensitivity;

                //Range.
                targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);
            }
            else
            {
                isFirstClick = true;
            }

            //Mouse scrollwheel.
            if (canScale)
            {
                Vector2 scroll = UnityEngine.InputSystem.Mouse.current.scroll.ReadValue();
                targetDistance -= scroll.y * this.MouseSettings.wheelSensitivity * this.clientScale;
            }
        }

        targetDistance = Mathf.Clamp(targetDistance, this.DistanceRange.min, this.DistanceRange.max);

        //Lerp.
        bool bLerpPos = true;
        float dis = (CurrentAngles - targetAngles).sqrMagnitude;
        if (dis > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentAngles = Vector2.Lerp(CurrentAngles, targetAngles, this.Damper * Time.deltaTime);

        float dis2 = Mathf.Abs(CurrentDistance - targetDistance);
        if (dis2 > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentDistance = Mathf.Lerp(CurrentDistance, targetDistance, this.Damper * Time.deltaTime);

        if (!canRotation_X) targetAngles.y = 0;
        if (!canRotation_Y) targetAngles.x = 0;


        //Update transform position and rotation.
        transform.rotation = Quaternion.Euler(CurrentAngles);

        Vector3 CurrentTargetPosTmp = target.position + new Vector3(0, this.targetHalfHeight, 0) - transform.forward * CurrentDistance;
        if (bLerpPos)
        {
            CurrentTargetPos = Vector3.Lerp(CurrentTargetPos, CurrentTargetPosTmp, this.DamperTargetMove * Time.deltaTime);
        }
        else
        {
            CurrentTargetPos = Vector3.Lerp(CurrentTargetPos, CurrentTargetPosTmp, 0.5f);
        }


        transform.position = CurrentTargetPos;
    }

    public bool IsEqualVector3(Vector3 data1, Vector3 data2, float dis = 0.001f)
    {
        if (Math.Abs(data1.x - data2.x) < dis
            && Math.Abs(data1.y - data2.y) < dis
            && Math.Abs(data1.z - data2.z) < dis
           )
        {
            return true;
        }

        return false;
    }

    #endregion
}

[Serializable]
public struct MouseSettings
{
    /// <summary>
    /// ID of mouse button.
    /// </summary>
    public int mouseButtonID;

    /// <summary>
    /// Sensitivity of mouse pointer.
    /// </summary>
    public float pointerSensitivity;

    /// <summary>
    /// Sensitivity of mouse ScrollWheel.
    /// </summary>
    public float wheelSensitivity;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="mouseButtonID">ID of mouse button.</param>
    /// <param name="pointerSensitivity">Sensitivity of mouse pointer.</param>
    /// <param name="wheelSensitivity">Sensitivity of mouse ScrollWheel.</param>
    public MouseSettings(int mouseButtonID, float pointerSensitivity, float wheelSensitivity)
    {
        this.mouseButtonID = mouseButtonID;
        this.pointerSensitivity = pointerSensitivity;
        this.wheelSensitivity = wheelSensitivity;
    }
}

/// <summary>
/// Range form min to max.
/// </summary>
[Serializable]
public struct Range
{
    /// <summary>
    /// Min value of range.
    /// </summary>
    public float min;

    /// <summary>
    /// Max value of range.
    /// </summary>
    public float max;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="min">Min value of range.</param>
    /// <param name="max">Max value of range.</param>
    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

/// <summary>
/// Rectangle area on plane.
/// </summary>
[Serializable]
public struct PlaneArea
{
    /// <summary>
    /// Center of area.
    /// </summary>
    public Transform center;

    /// <summary>
    /// Width of area.
    /// </summary>
    public float width;

    /// <summary>
    /// Length of area.
    /// </summary>
    public float length;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="center">Center of area.</param>
    /// <param name="width">Width of area.</param>
    /// <param name="length">Length of area.</param>
    public PlaneArea(Transform center, float width, float length)
    {
        this.center = center;
        this.width = width;
        this.length = length;
    }
}

/// <summary>
/// Target of camera align.
/// </summary>
[Serializable]
public struct AlignTarget
{
    /// <summary>
    /// Center of align target.
    /// </summary>
    public Transform center;

    /// <summary>
    /// Angles of align.
    /// </summary>
    public Vector2 angles;

    /// <summary>
    /// Distance from camera to target center.
    /// </summary>
    public float distance;

    /// <summary>
    /// Range limit of angle.
    /// </summary>
    public Range angleRange;

    /// <summary>
    /// Range limit of distance.
    /// </summary>
    public Range distanceRange;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="center">Center of align target.</param>
    /// <param name="angles">Angles of align.</param>
    /// <param name="distance">Distance from camera to target center.</param>
    /// <param name="angleRange">Range limit of angle.</param>
    /// <param name="distanceRange">Range limit of distance.</param>
    public AlignTarget(Transform center, Vector2 angles, float distance, Range angleRange, Range distanceRange)
    {
        this.center = center;
        this.angles = angles;
        this.distance = distance;
        this.angleRange = angleRange;
        this.distanceRange = distanceRange;
    }
}