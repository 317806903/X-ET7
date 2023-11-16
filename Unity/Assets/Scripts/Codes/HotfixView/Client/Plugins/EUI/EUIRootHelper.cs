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
            if (type == UIWindowType.Normal)
            {
                return UIManagerComponent.Instance.NormalRoot;
            }
            else if (type == UIWindowType.Fixed)
            {
                return UIManagerComponent.Instance.FixedRoot;
            }
            else if (type == UIWindowType.PopUp)
            {
                return UIManagerComponent.Instance.PopUpRoot;
            }
            else if (type == UIWindowType.NoticeRoot)
            {
                return UIManagerComponent.Instance.NoticeRoot;
            }
            else if (type == UIWindowType.LoadingRoot)
            {
                return UIManagerComponent.Instance.LoadingRoot;
            }
            else if (type == UIWindowType.HighestNoticeRoot)
            {
                return UIManagerComponent.Instance.HighestNoticeRoot;
            }

            Log.Error("uiroot type is error: " + type.ToString());
            return null;
        }
    }
}