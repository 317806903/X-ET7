using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    public static class UGUIHelper
    {
        private static List<RaycastResult> results = new ();
        private static PointerEventData eventDataCurrentPosition = new (UnityEngine.EventSystems.EventSystem.current);

        public static int GetTouchCount()
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts != null)
            {
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
            var mouse = UnityEngine.InputSystem.Mouse.current;
            if (mouse != null)
            {
                if (mouse.leftButton.isPressed || mouse.leftButton.wasReleasedThisFrame)
                {
                    return 1;
                }
            }
            return 0;
        }

        public static Vector2 GetTouchPosition(int i)
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts != null)
            {
                return ts.touches[i].position.ReadValue();
            }
            var mouse = UnityEngine.InputSystem.Mouse.current;
            if (mouse != null)
            {
                Vector2 mousePosition = mouse.position.ReadValue();
                return mousePosition;
            }
            return Vector2.zero;
        }

        public static bool CheckUserInput(bool isContainPressUp = true)
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts != null)
            {
                if (GetTouchCount() > 0)
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
                var mouse = UnityEngine.InputSystem.Mouse.current;
                return mouse.leftButton.isPressed;
            }
        }

        public static (bool bRet, Vector3 touchPosition, Vector3 touchForward) GetUserInputDown()
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts != null)
            {
                if (GetTouchCount() == 1)
                {
                    if (ts.touches[0].press.wasPressedThisFrame)
                    {
                        Vector2 screenPos = GetTouchPosition(0);
                        return (true, screenPos, Vector2.zero);
                    }
                }
                return (false, Vector2.zero, Vector2.zero);
            }
            else
            {
                var mouse = UnityEngine.InputSystem.Mouse.current;
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    Vector2 screenPos = mouse.position.ReadValue();
                    return (true, screenPos, Vector2.zero);
                }
                else
                {
                    return (false, Vector2.zero, Vector2.zero);
                }
            }
        }

        public static (bool bRet, Vector3 touchPosition, Vector3 touchForward) GetUserInputDownOrPress()
        {
            bool bRet = false;
            Vector3 touchPosition = Vector3.zero;
            Vector3 touchForward = Vector3.zero;
            (bRet, touchPosition, touchForward) = GetUserInputDown();
            if (bRet)
            {
                return (bRet, touchPosition, touchForward);
            }
            (bRet, touchPosition, touchForward) = GetUserInputPress();
            if (bRet)
            {
                return (bRet, touchPosition, touchForward);
            }
            return (false, Vector2.zero, Vector2.zero);
        }

        public static (bool bRet, Vector3 touchPosition, Vector3 touchForward) GetUserInputPos()
        {
            bool bRet = false;
            Vector3 touchPosition = Vector3.zero;
            Vector3 touchForward = Vector3.zero;
            (bRet, touchPosition, touchForward) = GetUserInputDown();
            if (bRet)
            {
                return (bRet, touchPosition, touchForward);
            }
            (bRet, touchPosition, touchForward) = GetUserInputPress();
            if (bRet)
            {
                return (bRet, touchPosition, touchForward);
            }
            (bRet, touchPosition, touchForward) = GetUserInputUp();
            if (bRet)
            {
                return (bRet, touchPosition, touchForward);
            }
            return (bRet, touchPosition, touchForward);
        }

        public static (bool bRet, Vector3 touchPosition, Vector3 touchForward) GetUserInputUp()
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts != null)
            {
                if (GetTouchCount() == 1)
                {
                    if (ts.touches[0].press.wasReleasedThisFrame)
                    {
                        Vector2 screenPos = GetTouchPosition(0);
                        return (true, screenPos, Vector2.zero);
                    }
                }
                return (false, Vector2.zero, Vector2.zero);
            }
            else
            {
                var mouse = UnityEngine.InputSystem.Mouse.current;
                if (mouse.leftButton.wasReleasedThisFrame)
                {
                    Vector2 screenPos = mouse.position.ReadValue();
                    return (true, screenPos, Vector2.zero);
                }
                else
                {
                    return (false, Vector2.zero, Vector2.zero);
                }
            }
        }

        public static (bool bRet, Vector3 touchPosition, Vector3 touchForward) GetUserInputPress()
        {
            var ts = UnityEngine.InputSystem.Touchscreen.current;
            if (ts != null)
            {
                if (GetTouchCount() == 1)
                {
                    if (ts.touches[0].press.isPressed)
                    {
                        Vector2 screenPos = GetTouchPosition(0);
                        return (true, screenPos, Vector2.zero);
                    }
                }
                return (false, Vector2.zero, Vector2.zero);
            }
            else
            {
                var mouse = UnityEngine.InputSystem.Mouse.current;
                if (mouse.leftButton.isPressed)
                {
                    Vector2 screenPos = mouse.position.ReadValue();
                    return (true, screenPos, Vector2.zero);
                }
                else
                {
                    return (false, Vector2.zero, Vector2.zero);
                }
            }
        }

        public static (bool ret, GameObject go, RaycastHit? raycastHit) ChkClickGameObject(Scene scene, Vector3 posOffSet, float maxDis, int layerMask)
        {
            bool isClickUGUI = ET.UGUIHelper.IsClickUGUI();
            if (isClickUGUI)
            {
                return (false, null, null);
            }

            bool bRet = ET.UGUIHelper.ChkClickRayCollion(scene, posOffSet, maxDis, layerMask, out RaycastHit rayHit);
            if (bRet)
            {
                return (true, rayHit.collider.gameObject, rayHit);
            }
            return (false, null, null);
        }

        public static bool ChkClickRayCollion(Scene scene, Vector3 posOffSet, float maxDis, int layerMask, out RaycastHit hit)
        {
            (bool bRet, Vector3 touchPosition, Vector3 touchForward) = GetUserInputPos();
            if (bRet)
            {
                Ray ray = ET.Client.CameraHelper.GetMainCamera(scene).ScreenPointToRay(touchPosition);
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

        public static bool IsClickUGUI(bool isContainPressUp = false)
        {
            if (UnityEngine.EventSystems.EventSystem.current)
            {
#if false && UNITY_EDITOR
                return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
                if (GetTouchCount() > 0)
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
            eventDataCurrentPosition.position = GetTouchPosition(0);
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
