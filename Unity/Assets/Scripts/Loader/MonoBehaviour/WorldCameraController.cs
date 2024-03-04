using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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
    public float targetHalfHeight = 0;


    /// <summary>
    /// Settings of mouse button, pointer and scrollwheel.
    /// </summary>
    public MouseSettings mouseSettings = new MouseSettings(0, 10, 10);

    /// <summary>
    /// Range limit of angle.
    /// </summary>
    public Range angleRange = new Range(0, 90);

    /// <summary>
    /// Range limit of distance.
    /// </summary>
    public Range distanceRange = new Range(2, 10);

    /// <summary>
    /// Damper for move and rotate.
    /// </summary>
    [Range(0, 10)] public float damper = 5;

    /// <summary>
    /// Damper for move and rotate.
    /// </summary>
    [Range(0, 10)] public float damperTargetMove = 5;

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

    public void ForceSetPosition(Transform target, float targetHalfHeight, float dis, Vector3 eulerAngles)
    {
        this.target = target;
        this.targetHalfHeight = targetHalfHeight;
        CurrentAngles = targetAngles = eulerAngles;
        CurrentDistance = targetDistance = dis;
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
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        results.Clear(); // Just in case
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public bool IsClickUGUI()
    {
        if (EventSystem.current)
        {
#if UNITY_EDITOR
            if (this.bClickUGUI)
            {
                if (Input.GetMouseButton(mouseSettings.mouseButtonID) == false)
                {
                    this.bClickUGUI = false;
                }
            }
            else
            {
                this.bClickUGUI = EventSystem.current.IsPointerOverGameObject();
            }
#elif UNITY_ANDROID || UNITY_IPHONE
            if (this.bClickUGUI)
            {
                if (Input.touchCount == 0)
                {
                    this.bClickUGUI = false;
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    bool t1 = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
                    if (t1)
                    {
                        //Debug.LogError("zpb ==========true=============== ");
                    }
                    //this.bClickUGUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
                    this.bClickUGUI = IsPointerOverUIObject();
                }
            }
#endif
            return this.bClickUGUI;
        }

        return false;
    }

    private bool isPressLeft = false;//是否刚刚开始旋转
    private Vector3 oldPosition;
    private Vector3 newPosition;
    protected void AroundByMobileInput()
    {
        if (IsClickUGUI())
        {
            Touch firstTouch;
            if (Input.touchCount == 1)
            {
                isPressLeft = false;
            }
            else if (Input.touchCount == 2)
            {
                firstTouch = Input.touches[1];
                if (isPressLeft == false)
                {
                    oldPosition = firstTouch.position;
                    isPressLeft = true;
                }
                else
                {
                    newPosition = firstTouch.position;
                    if (firstTouch.phase == TouchPhase.Moved)
                    {
                        targetAngles.y += (newPosition.x - oldPosition.x) * 0.1f * mouseSettings.pointerSensitivity;
                        targetAngles.x -= (newPosition.y - oldPosition.y) * 0.1f * mouseSettings.pointerSensitivity;

                        //Range.
                        targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);
                    }
                    oldPosition = newPosition;
                }

            }

        }
        else
        {
            Touch firstTouch;
            Touch secTouch;
            if (Input.touchCount == 1)
            {
                firstTouch = Input.touches[0];
                if (firstTouch.phase == TouchPhase.Moved)
                {
                    targetAngles.y += Input.GetAxis("Mouse X") * mouseSettings.pointerSensitivity;
                    targetAngles.x -= Input.GetAxis("Mouse Y") * mouseSettings.pointerSensitivity;

                    //Range.
                    targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);
                }

                //Mouse pointer.
                m_IsSingleFinger = true;
            }

            //Mouse scrollwheel.
            if (canScale)
            {
                if (Input.touchCount > 1)
                {
                    firstTouch = Input.touches[0];
                    secTouch = Input.touches[1];
                    //计算出当前两点触摸点的位置
                    if (m_IsSingleFinger)
                    {
                        oldPosition1 = firstTouch.position;
                        oldPosition2 = secTouch.position;
                    }


                    if (firstTouch.phase == TouchPhase.Moved && secTouch.phase == TouchPhase.Moved)
                    {
                        var tempPosition1 = firstTouch.position;
                        var tempPosition2 = secTouch.position;


                        float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
                        float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

                        //计算上次和这次双指触摸之间的距离差距
                        //然后去更改摄像机的距离
                        targetDistance -= (currentTouchDistance - lastTouchDistance) * Time.deltaTime * mouseSettings.wheelSensitivity;
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

        targetDistance = Mathf.Clamp(targetDistance, distanceRange.min, distanceRange.max);

        //Lerp.
        bool bLerpPos = true;
        float dis = (CurrentAngles - targetAngles).sqrMagnitude;
        if (dis > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentAngles = Vector2.Lerp(CurrentAngles, targetAngles, damper * Time.deltaTime);

        float dis2 = Mathf.Abs(CurrentDistance - targetDistance);
        if (dis2 > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentDistance = Mathf.Lerp(CurrentDistance, targetDistance, damper * Time.deltaTime);


        if (!canRotation_X) targetAngles.y = 0;
        if (!canRotation_Y) targetAngles.x = 0;


        //Update transform position and rotation.
        transform.rotation = Quaternion.Euler(CurrentAngles);

        Vector3 CurrentTargetPosTmp = target.position + new Vector3(0, this.targetHalfHeight, 0) - transform.forward * CurrentDistance;
        if (bLerpPos)
        {
            CurrentTargetPos = Vector3.Lerp(CurrentTargetPos, CurrentTargetPosTmp, damperTargetMove * Time.deltaTime);
        }
        else
        {
            CurrentTargetPos = CurrentTargetPosTmp;
        }


        transform.position = CurrentTargetPos;
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
            if (Input.GetMouseButton(mouseSettings.mouseButtonID))
            {
                if (isFirstClick)
                {
                    isFirstClick = false;
                    return;
                }
                //Mouse pointer.
                targetAngles.y += Input.GetAxis("Mouse X") * mouseSettings.pointerSensitivity;
                targetAngles.x -= Input.GetAxis("Mouse Y") * mouseSettings.pointerSensitivity;

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
                targetDistance -= Input.GetAxis("Mouse ScrollWheel") * mouseSettings.wheelSensitivity;
            }
        }

        targetDistance = Mathf.Clamp(targetDistance, distanceRange.min, distanceRange.max);

        //Lerp.
        bool bLerpPos = true;
        float dis = (CurrentAngles - targetAngles).sqrMagnitude;
        if (dis > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentAngles = Vector2.Lerp(CurrentAngles, targetAngles, damper * Time.deltaTime);

        float dis2 = Mathf.Abs(CurrentDistance - targetDistance);
        if (dis2 > 0.1f)
        {
            bLerpPos = false;
        }
        CurrentDistance = Mathf.Lerp(CurrentDistance, targetDistance, damper * Time.deltaTime);

        if (!canRotation_X) targetAngles.y = 0;
        if (!canRotation_Y) targetAngles.x = 0;


        //Update transform position and rotation.
        transform.rotation = Quaternion.Euler(CurrentAngles);

        Vector3 CurrentTargetPosTmp = target.position + new Vector3(0, this.targetHalfHeight, 0) - transform.forward * CurrentDistance;
        if (bLerpPos)
        {
            CurrentTargetPos = Vector3.Lerp(CurrentTargetPos, CurrentTargetPosTmp, damperTargetMove * Time.deltaTime);
        }
        else
        {
            CurrentTargetPos = CurrentTargetPosTmp;
        }


        transform.position = CurrentTargetPos;
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