using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    public static class UGUIHelper
    {
        private static List<RaycastResult> results = new ();
        private static  PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);

        public static bool CheckUserInput()
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Input.GetMouseButton(0);
            }
        }

        public static (bool bRet, Vector2 screenPos) GetUserInputDown()
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector2 screenPos = Input.GetTouch(0).position;
                    return (true, screenPos);
                }
                else
                {
                    return (false, Vector2.zero);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 screenPos = Input.mousePosition;
                    return (true, screenPos);
                }
                else
                {
                    return (false, Vector2.zero);
                }
            }
        }

        public static (bool bRet, Vector2 screenPos) GetUserInputDownOrPress()
        {
            bool bRet = false;
            Vector2 screenPos = Vector2.zero;
            (bRet, screenPos) = GetUserInputDown();
            if (bRet)
            {
                return (bRet, screenPos);
            }
            (bRet, screenPos) = GetUserInputPress();
            if (bRet)
            {
                return (bRet, screenPos);
            }
            return (false, Vector2.zero);
        }

        public static (bool bRet, Vector2 screenPos) GetUserInputUp()
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Vector2 screenPos = Input.GetTouch(0).position;
                    return (true, screenPos);
                }
                else
                {
                    return (false, Vector2.zero);
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Vector2 screenPos = Input.mousePosition;
                    return (true, screenPos);
                }
                else
                {
                    return (false, Vector2.zero);
                }
            }
        }

        public static (bool bRet, Vector2 screenPos) GetUserInputPress()
        {
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
                {
                    Vector2 screenPos = Input.GetTouch(0).position;
                    return (true, screenPos);
                }
                else
                {
                    return (false, Vector2.zero);
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 screenPos = Input.mousePosition;
                    return (true, screenPos);
                }
                else
                {
                    return (false, Vector2.zero);
                }
            }
        }

        public static bool ChkClickRayCollion(Scene scene, float maxDis, int layerMask, out RaycastHit hit)
        {
            (bool bRet, Vector3 pos) = GetUserInputUp();
            if (bRet)
            {
                Ray ray = ET.Client.CameraHelper.GetMainCamera(scene).ScreenPointToRay(pos);
                if (layerMask == -1)
                {
                    if (Physics.Raycast(ray, out hit, maxDis))
                    {
                        return true;
                    }
                }
                else
                {
                    if (Physics.Raycast(ray, out hit, maxDis, layerMask))
                    {
                        return true;
                    }
                }
            }

            hit = new RaycastHit();
            return false;
        }

        public static bool IsClickUGUI()
        {
            if (UnityEngine.EventSystems.EventSystem.current)
            {
#if false && UNITY_EDITOR
                return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
#elif UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
                if (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                {
                    return IsPointerOverUIObject();
                }

                return false;
#else
                return false;
#endif
            }

            return false;
        }

        public static bool IsPointerOverUIObject()
        {
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            results.Clear(); // Just in case
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            if (results.Count > 0)
            {
                // for (int i = 0; i < self.results.Count; i++)
                // {
                //     Log.Debug($"---IsPointerOverUIObject {i}:{results[i].gameObject}");
                // }
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].gameObject.layer.Equals(LayerMask.NameToLayer("UI")))
                    {
                        if (results[i].gameObject.GetComponent<UITouchPass>() == null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
