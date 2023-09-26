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
                return GlobalComponent.Instance.NormalRoot;
            }
            else if (type == UIWindowType.Fixed)
            {
                return GlobalComponent.Instance.FixedRoot;
            }
            else if (type == UIWindowType.PopUp)
            {
                return GlobalComponent.Instance.PopUpRoot;
            }
            else if (type == UIWindowType.NoticeRoot)
            {
                return GlobalComponent.Instance.NoticeRoot;
            }
            else if (type == UIWindowType.LoadingRoot)
            {
                return GlobalComponent.Instance.LoadingRoot;
            }
            else if (type == UIWindowType.HighestNoticeRoot)
            {
                return GlobalComponent.Instance.HighestNoticeRoot;
            }

            Log.Error("uiroot type is error: " + type.ToString());
            return null;
        }
    }
}