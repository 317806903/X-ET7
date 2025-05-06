using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIGuide
{
    /// <summary>
    /// 事件渗透
    /// </summary>
    public class GuidanceEventPenetrate: MonoBehaviour, ICanvasRaycastFilter
    {
        private RectTransform rectTransform;

        public void SetTargetRectTransform(RectTransform targetRectTransform)
        {
            rectTransform = targetRectTransform;
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (rectTransform == null)
            {
                return true;
            }

            return !RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sp, eventCamera);
        }
    }
}