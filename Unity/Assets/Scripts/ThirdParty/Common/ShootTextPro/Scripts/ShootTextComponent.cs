using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShootTextComponent : MonoBehaviour
{
    public CanvasGroup canvasGroup = null;
    public RectTransform rectTransform = null;
    public HorizontalLayoutGroup horizontalLayoutGroup = null;
    public ContentSizeFitter contentSizeFitter = null;

    public List<RectTransform> childTransformGroup = new List<RectTransform>();

    public List<Vector2> sizeDeltaGroup = new List<Vector2>();

    #region From ShootTextInfo
    public TextMoveType moveType;
    public double delayMoveTime;
    public double moveLifeTime;
    public int size;
    public Func<Vector3> getShootTextTopPoint;
    public Func<Vector3> getShootTextButtomPoint;
    public double initializedVerticalPositionOffset;
    public double initializedHorizontalPositionOffset;
    #endregion

    public double fadeCurveTime;
    public double scaleCurveTime;
    public float curScaleCurve;

    public double xMoveOffeset;
    public double yMoveOffeset;

    public bool isMove = false;
    public bool isMoveDone = false;
    public int valueShow;
    public void SetInfo(int valueShow, ShootTextInfo shootTextInfo, bool isFromPool)//执行顺序优于Start
    {
        this.valueShow = valueShow;
        isMove = false;
        isMoveDone = false;
        fadeCurveTime = 0;
        scaleCurveTime = 0;
        xMoveOffeset = 0;
        yMoveOffeset = 0;

        moveType = shootTextInfo.moveType;
        delayMoveTime = Time.time + shootTextInfo.delayMoveTime;
        moveLifeTime = Time.time + shootTextInfo.moveLifeTime;
        getShootTextTopPoint = shootTextInfo.getShootTextTopPoint;
        getShootTextButtomPoint = shootTextInfo.getShootTextButtomPoint;
        initializedHorizontalPositionOffset = shootTextInfo.initializedHorizontalPositionOffset;
        initializedVerticalPositionOffset = shootTextInfo.initializedVerticalPositionOffset;

        if (isFromPool == false)
        {
            childTransformGroup.Clear();
            sizeDeltaGroup.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = this.transform.GetChild(i);
                RectTransform rect = child.gameObject.GetComponent<RectTransform>();
                childTransformGroup.Add(rect);
                sizeDeltaGroup.Add(rect.sizeDelta);
            }
            horizontalLayoutGroup.enabled = true;
            contentSizeFitter.enabled = true;
        }
        else
        {
            for (int i = 0; i < childTransformGroup.Count; i++)
            {
                childTransformGroup[i].sizeDelta = Vector2.zero;
            }
        }
    }

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        contentSizeFitter = GetComponent<ContentSizeFitter>();
        horizontalLayoutGroup.enabled = false;
        contentSizeFitter.enabled = false;
    }

    public void Clear()
    {
        canvasGroup.alpha = 0;
        // childTransformGroup.Clear();
        // sizeDeltaGroup.Clear();
        // horizontalLayoutGroup.enabled = false;
        // contentSizeFitter.enabled = false;
    }

    public void ChangeScale(double scale)
    {
        if (scale == 0)
        {
            scale = 0.01;
        }

        horizontalLayoutGroup.enabled = false;
        contentSizeFitter.enabled = false;
        bool needResetSize = false;
        for (int i = 0; i < childTransformGroup.Count; i++)
        {
            Vector2 sizeDelta = sizeDeltaGroup[i];
            sizeDelta.x = sizeDelta.x * (float)scale * this.curScaleCurve;
            sizeDelta.y = sizeDelta.y * (float)scale * this.curScaleCurve;
            if (childTransformGroup[i].sizeDelta.Equals(sizeDelta) == false)
            {
                needResetSize = true;
                childTransformGroup[i].sizeDelta = sizeDelta;
            }
        }

        if (needResetSize)
        {
            horizontalLayoutGroup.enabled = true;
            contentSizeFitter.enabled = true;
        }
    }

    public void ChangeAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void ChangeScaleCurve(float scaleCurve)
    {
        this.curScaleCurve = scaleCurve;
    }
}

