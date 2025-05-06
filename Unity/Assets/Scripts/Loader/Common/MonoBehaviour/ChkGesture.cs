using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ET
{
    public class ChkGesture: MonoBehaviour
    {
        public int numOfTriangleToShow = 1;
        public int numOfRoundToShow = 2;
        public int numOfRoundToHide = 2;
        private bool isShow = false;
        public Action doShow { get; set; }

        public Action doHide { get; set; }

        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR
            numOfTriangleToShow = 0;
#endif
        }

        void Update()
        {
            recordInput();
            if (isTrigFinished)
            {
                return;
            }
            if (this.isShow)
            {
                if (this.isGestureDoneHide())
                {
                    //Log.Error($"doHide");
                    isTrigFinished = true;
                    this.doHide?.Invoke();
                    this.isShow = !this.isShow;
                }
            }

            if (this.isShow == false)
            {
                if (isGestureDoneShow())
                {
                    //Log.Error($"doShow");
                    isTrigFinished = true;
                    this.doShow?.Invoke();
                    this.isShow = !this.isShow;
                }
            }
        }

        private bool isRoundFinished;
        private bool isTriangleFinished;
        private bool isTrigFinished;

        List<Vector2> gestureDetector = new ();
        List<Vector2> gestureDetectorTriangle = new();
        Vector2 gestureSum = Vector2.zero;
        float gestureLength = 0;
        int gestureCount = 0;

        int GetTouchCount()
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts == null)
            {
                return 0;
            }
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

        void recordInput()
        {
            if (UnityEngine.InputSystem.Touchscreen.current != null)
            {
                var ts = UnityEngine.InputSystem.Touchscreen.current;
                int touchCount = GetTouchCount();
                if (touchCount != 1)
                {
                    gestureDetector.Clear();
                    gestureCount = 0;
                    isRoundFinished = false;
                    isTriangleFinished = false;
                    isTrigFinished = false;
                }
                else
                {
                    var tc = ts.touches[0];
                    if (tc.press.wasReleasedThisFrame)
                    {
                        gestureDetector.Clear();
                    }
                    else if (tc.press.isPressed)
                    {
                        Vector2 p = tc.position.ReadValue();
                        if (gestureDetector.Count == 0 || (p - gestureDetector[gestureDetector.Count - 1]).magnitude > 10)
                            gestureDetector.Add(p);
                    }
                }
            }
            else if (UnityEngine.InputSystem.Mouse.current != null)
            {
                var mouse = UnityEngine.InputSystem.Mouse.current;
                if (mouse.leftButton.wasReleasedThisFrame)
                {
                    gestureDetector.Clear();
                    gestureCount = 0;
                    isRoundFinished = false;
                    isTriangleFinished = false;
                    isTrigFinished = false;
                }
                else
                {
                    if (mouse.leftButton.isPressed)
                    {
                        Vector2 p = mouse.position.ReadValue();
                        if (gestureDetector.Count == 0 || (p - gestureDetector[gestureDetector.Count - 1]).magnitude > 10)
                            gestureDetector.Add(p);
                    }
                }
            }
        }

        bool isGestureDoneHide()
        {
            if (isGestureRound(this.numOfRoundToHide))
            {
                isRoundFinished = false;
                isTriangleFinished = false;
                return true;
            }

            return false;
        }

        bool isGestureDoneShow()
        {
            if (isGestureTriangle(this.numOfTriangleToShow) && this.isGestureRound(this.numOfRoundToShow))
            {
                isRoundFinished = false;
                isTriangleFinished = false;
                return true;
            }

            return false;
        }

        bool isGestureRound(int needCount)
        {
            if (isRoundFinished)
            {
                return true;
            }

            if (needCount == 0)
            {
                return true;
            }

            if (gestureDetector.Count < 10)
                return false;

            gestureSum = Vector2.zero;
            gestureLength = 0;
            Vector2 prevDelta = Vector2.zero;
            for (int i = 0; i < gestureDetector.Count - 2; i++)
            {
                Vector2 delta = gestureDetector[i + 1] - gestureDetector[i];
                float deltaLength = delta.magnitude;
                gestureSum += delta;
                gestureLength += deltaLength;

                float dot = Vector2.Dot(delta, prevDelta);
                if (dot < 0f)
                {
                    gestureDetector.Clear();
                    gestureCount = 0;
                    return false;
                }

                prevDelta = delta;
            }

            int gestureBase = (Screen.width + Screen.height) / 4;

            if (gestureLength > gestureBase && gestureSum.magnitude < gestureBase / 2)
            {
                gestureDetector.Clear();
                gestureCount++;
                if (gestureCount >= needCount)
                {
                    isRoundFinished = true;
                    gestureCount = 0;
                    return true;
                }
            }

            return false;
        }

        bool isGestureTriangle(int needCount)
        {
            if (this.isTriangleFinished)
            {
                return true;
            }

            if (needCount == 0)
            {
                return true;
            }

            if (gestureDetector.Count < 5)
                return false;

            gestureSum = Vector2.zero;
            gestureLength = 0;
            Vector2 prevDelta = Vector2.zero;

            gestureDetectorTriangle.Clear();
            gestureDetectorTriangle.Add(gestureDetector[0]);
            for (int i = 0; i < gestureDetector.Count - 2; i++)
            {
                Vector2 delta = gestureDetector[i + 1] - gestureDetector[i];

                float dot = Vector2.Dot(delta, prevDelta);
                if (dot < 0f)
                {
                    gestureDetectorTriangle.Add(gestureDetector[i]);
                }
                else
                {
                    if (i == gestureDetector.Count - 3)
                    {
                        gestureDetectorTriangle.Add(gestureDetector[i + 1]);
                    }
                }

                prevDelta = delta;
            }

            if (gestureDetectorTriangle.Count < 4)
            {
                return false;
            }

            bool isIntersect = TryGetIntersectPoint(gestureDetectorTriangle[0], gestureDetectorTriangle[1],
                gestureDetectorTriangle[gestureDetectorTriangle.Count - 2], gestureDetectorTriangle[gestureDetectorTriangle.Count - 1],
                out var intersectPos);
            if (isIntersect == false)
            {
                isIntersect = ChkIsNear(gestureDetectorTriangle[0], gestureDetectorTriangle[gestureDetectorTriangle.Count - 1]);
            }
            if (isIntersect)
            {
                //Debug.Log($"--- isGestureTriangle gestureCount[{gestureCount+1}]");
                gestureDetector.Clear();
                //gestureDetector.Add(newList[newList.Count - 1]);
                gestureCount++;
                if (gestureCount >= needCount)
                {
                    isTriangleFinished = true;
                    gestureCount = 0;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 计算AB与CD两条线段的交点.
        /// </summary>
        /// <param name="a">A点</param>
        /// <param name="b">B点</param>
        /// <param name="c">C点</param>
        /// <param name="d">D点</param>
        /// <param name="intersectPos">AB与CD的交点</param>
        /// <returns>是否相交 true:相交 false:未相交</returns>
        private bool TryGetIntersectPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 d, out Vector3 intersectPos)
        {
            intersectPos = Vector3.zero;

            Vector3 ab = b - a;
            Vector3 ca = a - c;
            Vector3 cd = d - c;

            Vector3 v1 = Vector3.Cross(ca, cd);

            if (Mathf.Abs(Vector3.Dot(v1, ab)) > 1e-6)
            {
                // 不共面
                return false;
            }

            if (Vector3.Cross(ab, cd).sqrMagnitude <= 1e-6)
            {
                // 平行
                return false;
            }

            Vector3 ad = d - a;
            Vector3 cb = b - c;
            // 快速排斥
            if (Mathf.Min(a.x, b.x) > Mathf.Max(c.x, d.x) || Mathf.Max(a.x, b.x) < Mathf.Min(c.x, d.x)
                || Mathf.Min(a.y, b.y) > Mathf.Max(c.y, d.y) || Mathf.Max(a.y, b.y) < Mathf.Min(c.y, d.y)
                || Mathf.Min(a.z, b.z) > Mathf.Max(c.z, d.z) || Mathf.Max(a.z, b.z) < Mathf.Min(c.z, d.z)
               )
                return false;

            // 跨立试验
            if (Vector3.Dot(Vector3.Cross(-ca, ab), Vector3.Cross(ab, ad)) > 0
                && Vector3.Dot(Vector3.Cross(ca, cd), Vector3.Cross(cd, cb)) > 0)
            {
                Vector3 v2 = Vector3.Cross(cd, ab);
                float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
                intersectPos = a + ab * ratio;
                return true;
            }

            return false;
        }

        private bool ChkIsNear(Vector3 a, Vector3 b)
        {
            float xxx = (a - b).sqrMagnitude;
            if ((a-b).sqrMagnitude < 10000)
            {
                return true;
            }
            return false;
        }
    }
}