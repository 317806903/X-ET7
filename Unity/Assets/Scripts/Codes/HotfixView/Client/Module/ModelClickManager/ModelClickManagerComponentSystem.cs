using System;
using System.Collections.Generic;
using ET.AbilityConfig;
using ET.Client;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    [FriendOf(typeof (ModelClickManagerComponent))]
    public static class ModelClickManagerComponentSystem
    {
        [ObjectSystem]
        public class ModelClickManagerComponentAwakeSystem: AwakeSystem<ModelClickManagerComponent>
        {
            protected override void Awake(ModelClickManagerComponent self)
            {
                self.Init();
            }
        }

        [ObjectSystem]
        public class ModelClickManagerComponentDestroySystem: DestroySystem<ModelClickManagerComponent>
        {
            protected override void Destroy(ModelClickManagerComponent self)
            {
                self.downObj = null;
                self.upObj = null;
                if (self.root != null)
                {
                    GameObject.Destroy(self.root);
                    self.root = null;
                }
            }
        }

        [ObjectSystem]
        public class ModelClickManagerComponentFixedUpdateSystem: UpdateSystem<ModelClickManagerComponent>
        {
            protected override void Update(ModelClickManagerComponent self)
            {
                self.Update();
            }
        }

        public static void Init(this ModelClickManagerComponent self)
        {
            GameObject go = new GameObject("ModelClickManagerComponent");
            self.root = go;
            go.transform.SetParent(GlobalComponent.Instance.ClientManagerRoot);
            go.transform.localPosition = UnityEngine.Vector3.zero;
            go.transform.localScale = UnityEngine.Vector3.one;

            self.needChkDownAgain = true;
            self.click = false;
        }

        public static void Update(this ModelClickManagerComponent self)
        {
            if (null != self.ModelClick || null != self.ModelPress)
            {
                if (Application.isMobilePlatform)
                {
                    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        self.PointDown(Input.GetTouch(0).position);
                    }
                    else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        self.PointUp(Input.GetTouch(0).position);
                    }

                    if (null != self.ModelPress)
                    {
                        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Stationary)
                        {
                            self.ChkPressTrig(Input.GetTouch(0).position);
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        self.PointDown(Input.mousePosition);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        self.PointUp(Input.mousePosition);
                    }

                    if (null != self.ModelPress)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            self.ChkPressTrig(Input.mousePosition);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 按下
        /// </summary>
        public static void PointDown(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
        {
            self.inputPosition = position;
            (self.downObj, _) = self.IsClick(self.inputPosition);
            if (null != self.downObj)
            {
                self.click = true;
                self.startTime = self.GetMilliseconds();
            }
            else if(self.needChkDownAgain)
            {
                self.needChkDownAgain = false;
                self.ChkPointDownNextFrame(position).Coroutine();
            }
        }

        public static async ETTask ChkPointDownNextFrame(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
        {
            await TimerComponent.Instance.WaitFrameAsync();
            self.PointDown(position);
        }

        /// <summary>
        /// 抬起
        /// </summary>
        public static void PointUp(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
        {
            if (self.downObj == null)
            {
                return;
            }
            if (self.click == false)
            {
                return;
            }

            RaycastHit? rayHit = null;
            (self.upObj, rayHit) = self.IsClick(position);
            if (null != self.upObj && self.upObj.GetHashCode() == self.downObj.GetHashCode() && (self.GetMilliseconds() - self.startTime <= 220) && UnityEngine.Vector3.Distance(position, self.inputPosition) <= 10)
            {
                if (null != self.ModelClick)
                {
                    self.ModelClick.Invoke(rayHit.Value);
                }
            }
            self.needChkDownAgain = true;
            self.click = false;
        }

        /// <summary>
        /// Press trig
        /// </summary>
        public static void ChkPressTrig(this ModelClickManagerComponent self, UnityEngine.Vector3 position)
        {
            if (self.downObj == null)
            {
                return;
            }
            if (self.click == false)
            {
                return;
            }
            RaycastHit? rayHit = null;
            (self.upObj, rayHit) = self.IsClick(position);
            if (null != self.upObj && self.upObj.GetHashCode() == self.downObj.GetHashCode() && (self.GetMilliseconds() - self.startTime > 220) && UnityEngine.Vector3.Distance(position, self.inputPosition) <= 10)
            {
                self.needChkDownAgain = true;
                self.click = false;
                if (null != self.ModelPress)
                {
                    self.ModelPress.Invoke(rayHit.Value);
                }
            }
        }

        /// <summary>
        /// 判断点击的对象是否为本体
        /// </summary>
        /// <param name="clickPos"></param>
        /// <returns></returns>
        public static (GameObject, RaycastHit?) IsClick(this ModelClickManagerComponent self, UnityEngine.Vector2 clickPos)
        {
            bool isClickUGUI = ET.UGUIHelper.IsClickUGUI();
            if (isClickUGUI)
            {
                return (null, null);
            }
            Ray ray = ET.Client.CameraHelper.GetMainCamera(self.DomainScene()).ScreenPointToRay(clickPos);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, self.RayMaxDistance))
            {
                return (rayHit.collider.gameObject, rayHit);
            }
            return (null, null);
        }

        public static long GetMilliseconds(this ModelClickManagerComponent self)
        {
            DateTime now = DateTime.Now;
            if (self.lastCacheDateTime != now)
            {
                self.lastCacheDateTime = now;
                self.lastCacheMillis = (now.ToFileTime() / 10000) - self.unixBaseMillis;
            }
            return self.lastCacheMillis;
        }

    }
}