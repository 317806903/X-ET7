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

        public static bool ChkMouseInput()
        {
            return Input.GetMouseButton(0) || Input.GetMouseButtonUp(0);
        }

        public static bool ChkMouseClick(Scene scene, float maxDis, int layerMask, out RaycastHit hit)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = ET.Client.CameraHelper.GetMainCamera(scene).ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxDis, layerMask))
                {
                    return true;
                }
            }

            hit = new RaycastHit();
            return false;
        }

        public static bool ChkMouseClick(Scene scene, float maxDis, out RaycastHit hit)
        {
            if (Input.GetMouseButtonUp(0))
            {
                try
                {
                    Ray ray = ET.Client.CameraHelper.GetMainCamera(scene).ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, maxDis))
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            hit = new RaycastHit();
            return false;
        }

        public static bool IsClickUGUI()
        {
            if (UnityEngine.EventSystems.EventSystem.current)
            {
#if UNITY_EDITOR
                return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
#elif UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
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
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
