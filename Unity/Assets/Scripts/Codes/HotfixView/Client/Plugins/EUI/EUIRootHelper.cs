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
            if (type == UIWindowType.WorldHub)
            {
                return UIRootManagerComponent.Instance.WorldHubRoot;
            }
            else if (type == UIWindowType.Normal)
            {
                return UIRootManagerComponent.Instance.NormalRoot;
            }
            else if (type == UIWindowType.Fixed)
            {
                return UIRootManagerComponent.Instance.FixedRoot;
            }
            else if (type == UIWindowType.PopUp)
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
    }
}