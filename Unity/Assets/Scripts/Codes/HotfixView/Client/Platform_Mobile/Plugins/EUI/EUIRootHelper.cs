using System;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(GlobalComponent))]
    public static class EUIRootHelper
    {
        public static void Init()
        {

        }

        public static Transform GetTargetRoot(UIWindowType type)
        {
            if (type == UIWindowType.World3DRoot)
            {
                return UIRootManagerComponent.Instance.World3DRoot;
            }
            else if (type == UIWindowType.WorldHubRoot)
            {
                return UIRootManagerComponent.Instance.WorldHubRoot;
            }
            else if (type == UIWindowType.NormalRoot)
            {
                return UIRootManagerComponent.Instance.NormalRoot;
            }
            else if (type == UIWindowType.FixedRoot)
            {
                return UIRootManagerComponent.Instance.FixedRoot;
            }
            else if (type == UIWindowType.PopUpRoot)
            {
                return UIRootManagerComponent.Instance.PopUpRoot;
            }
            else if (type == UIWindowType.NoticeRoot)
            {
                return UIRootManagerComponent.Instance.NoticeRoot;
            }
            else if (type == UIWindowType.LoadingRoot)
            {
                return UIRootManagerComponent.Instance.LoadingRoot;
            }
            else if (type == UIWindowType.HighestFixedRoot)
            {
                return UIRootManagerComponent.Instance.HighestFixedRoot;
            }
            else if (type == UIWindowType.HighestNoticeRoot)
            {
                return UIRootManagerComponent.Instance.HighestNoticeRoot;
            }

            Log.Error("uiroot type is error: " + type.ToString());
            return null;
        }

        public static int GetCanvasSortingOrderByWindowType(UIWindowType uiWindowType)
        {
            switch (uiWindowType)
            {
                case UIWindowType.World3DRoot:
                    return -1;
                case UIWindowType.WorldCameraRoot:
                    return -1;
                case UIWindowType.WorldHubRoot:
                    return -1;
                case UIWindowType.NormalRoot:
                    return 0;
                case UIWindowType.FixedRoot:
                    return 10;
                case UIWindowType.PopUpRoot:
                    return 20;
                case UIWindowType.NoticeRoot:
                    return 30;
                case UIWindowType.LoadingRoot:
                    return 40;
                case UIWindowType.HighestFixedRoot:
                    return 50;
                case UIWindowType.HighestNoticeRoot:
                    return 60;
            }

            return 0;
        }

        public static float GetCanvasSortingOrder(Transform transUIRoot)
        {
            UIPannelSortingOrder uiPannelSortingOrder = transUIRoot.gameObject.GetComponent<UIPannelSortingOrder>();
            if (uiPannelSortingOrder == null)
            {
                return 0;
            }

            int sortingOrder = uiPannelSortingOrder.sortingOrder;
            if (sortingOrder <= 10)
            {
                return sortingOrder * 0.5f;
            }

            int moduloResult = sortingOrder % 10;
            int moduloResult2 = sortingOrder / 10;
            return moduloResult * 0.5f + moduloResult2 * 2f;
        }

        public static void DealCanvas(GameObject uiRootGo)
        {
        }

        public static void DealCanvasAfter(UIBaseWindow baseWindow)
        {
        }

        public static (bool, bool, string) ChkIsResetSortingLayer(UIWindowType type)
        {
            return (false, false, type.ToString());
        }

        public static (bool, float) ChkIsResetScale(UIWindowType type)
        {
            return (false, 0.001f);
        }

        public static void ResetRendererSortingOrder(GameObject gameObject)
        {
            Canvas parentCanvas = gameObject.transform.parent.gameObject.GetComponentInParent<Canvas>();
            if (parentCanvas == null)
            {
                return;
            }

            int sortingOrder = parentCanvas.sortingOrder;
            var list = gameObject.GetComponentsInChildren<ResetUIRendererSortingOrder>();
            foreach (ResetUIRendererSortingOrder resetUIRendererSortingOrder in list)
            {
                resetUIRendererSortingOrder.sortingOrder = sortingOrder;
                Renderer[] renderers = resetUIRendererSortingOrder.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.sortingOrder = sortingOrder + 1;
                }
            }
        }

    }
}